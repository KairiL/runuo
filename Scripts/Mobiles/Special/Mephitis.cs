using System;
using Server;
using Server.Items;
using Server.Engines.CannedEvil;
using System.Collections;
using Server.Misc;
using Server.Spells;

namespace Server.Mobiles
{
	public class Mephitis : BaseChampion
	{
		public override ChampionSkullType SkullType{ get{ return ChampionSkullType.Venom; } }

		public override Type[] UniqueList{ get{ return new Type[] { typeof( Calm ) }; } }
		public override Type[] SharedList { get { return new Type[] { typeof(OblivionsNeedle), typeof(ANecromancerShroud), typeof(EmbroideredOakLeafCloak), typeof(TheMostKnowledgePerson) }; } }
		public override Type[] DecorativeList{ get{ return new Type[] { typeof( Web ), typeof( MonsterStatuette ) }; } }

		public override MonsterStatuetteType[] StatueTypes{ get{ return new MonsterStatuetteType[] { MonsterStatuetteType.Spider }; } }

		[Constructable]
		public Mephitis() : base( AIType.AI_Melee )
		{
			Body = 173;
			Name = "Mephitis";

			BaseSoundID = 0x183;

			SetStr( 505, 1000 );
			SetDex( 102, 300 );
			SetInt( 402, 600 );

			SetHits( 3000 );
			SetStam( 105, 600 );

			SetDamage( 21, 33 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Poison, 50 );

			SetResistance( ResistanceType.Physical, 75, 80 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 60, 70 );

			SetSkill( SkillName.MagicResist, 70.7, 140.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 97.6, 100.0 );

			Fame = 22500;
			Karma = -22500;

			VirtualArmor = 80;
		}

		public override void GenerateLoot()
		{
			//AddLoot( LootPack.UltraRich, 4 );
            AddLoot(LootPack.LowEpic1, 2);
            AddLoot(LootPack.LowEpic2, 1);
        }

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            if ( CanSee(from) )
                if (Utility.RandomDouble() > .5)
                    PullIn(from);
            if (from is BaseCreature && ((BaseCreature)from).ControlMaster != null)
            {
                if (Utility.RandomDouble() > .75 && CanSee(((BaseCreature)from).ControlMaster))
                    PullIn(((BaseCreature)from).ControlMaster);
            }
            else if (from is BaseCreature && ((BaseCreature)from).SummonMaster != null)
                if (Utility.RandomDouble() > .75 && CanSee(((BaseCreature)from).SummonMaster))
                    PullIn(((BaseCreature)from).SummonMaster);
            base.OnDamage( amount, from, willKill );
        }

        public void PullIn( Mobile from )
        {
            from.Paralyze(TimeSpan.FromSeconds(5));
            new WebItem(0x10D4, (IPoint3D)from, from, from.Map, TimeSpan.FromSeconds(15), 1, 0);
            from.Location = Location;

        }
        #region Web stuff
        [DispellableField]
        public class WebItem : Item
        {
            private Timer m_Timer;
            private DateTime m_End;
            private Mobile m_Caster;
            private int m_Damage;
            private double m_StickChance = .75;
            private double m_TeleChance = .75;

            public override bool BlocksFit { get { return true; } }

            public WebItem(int itemID, IPoint3D loc, Mobile caster, Map map, TimeSpan duration, int val)
                : this(itemID, loc, caster, map, duration, val, 0)
            {
            }

            public WebItem(int itemID, IPoint3D loc, Mobile caster, Map map, TimeSpan duration, int val, int damage) : base(itemID)
            {
                Point3D p = new Point3D(loc.X, loc.Y, loc.Z);

                bool canFit = SpellHelper.AdjustField(ref p, map, 12, false);

                Visible = false;
                Movable = false;

                MoveToWorld(p, map);

                m_Caster = caster;

                m_Damage = damage;

                m_End = DateTime.UtcNow + duration;

                m_Timer = new InternalTimer(this, TimeSpan.FromSeconds(Math.Abs(val) * 0.2), caster.InLOS(this), canFit);
                m_Timer.Start();
            }

            public override bool OnMoveOff(Mobile m)
            {
                return ( Utility.RandomDouble() > m_StickChance || m == m_Caster);
            }

            public override bool OnMoveOver(Mobile m)
            {
                if ( Utility.RandomDouble() < m_TeleChance && m != m_Caster)
                    m.Location = m_Caster.Location;
                return (Utility.RandomDouble() > m_StickChance || m == m_Caster);
            }
            public override void OnAfterDelete()
            {
                base.OnAfterDelete();

                if (m_Timer != null)
                    m_Timer.Stop();
            }
            
            public WebItem(Serial serial) : base(serial)
            {
            }
            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);

                writer.Write((int)0); // version

                writer.Write(m_Damage);
                writer.Write(m_Caster);
                writer.WriteDeltaTime(m_End);
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);

                int version = reader.ReadInt();

                m_Damage = reader.ReadInt();
                m_Caster = reader.ReadMobile();
                m_End = reader.ReadDeltaTime();
                m_Timer = new InternalTimer(this, TimeSpan.Zero, true, true);
                m_Timer.Start();
            }

            private class InternalTimer : Timer
            {
                private WebItem m_Item;
                private bool m_InLOS, m_CanFit;

                private static Queue m_Queue = new Queue();

                public InternalTimer(WebItem item, TimeSpan delay, bool inLOS, bool canFit) : base(delay, TimeSpan.FromSeconds(1.0))
                {
                    m_Item = item;
                    m_InLOS = inLOS;
                    m_CanFit = canFit;

                    Priority = TimerPriority.FiftyMS;
                }
                protected override void OnTick()
                {
                    if (m_Item.Deleted)
                        return;

                    if (!m_Item.Visible)
                    {
                        if (m_InLOS && m_CanFit)
                            m_Item.Visible = true;
                        else
                            m_Item.Delete();

                        if (!m_Item.Deleted)
                        {
                            m_Item.ProcessDelta();
                            Effects.SendLocationParticles(EffectItem.Create(m_Item.Location, m_Item.Map, EffectItem.DefaultDuration), 0x376A, 9, 10, 5029);
                        }
                    }
                    else if (DateTime.UtcNow > m_Item.m_End)
                    {
                        m_Item.Delete();
                        Stop();
                    }
                    else
                    {
                        Map map = m_Item.Map;
                        Mobile caster = m_Item.m_Caster;

                        if (map != null && caster != null)
                        {
                            foreach (Mobile m in m_Item.GetMobilesInRange(0))
                            {
                                if ((m.Z + 16) > m_Item.Z && (m_Item.Z + 12) > m.Z && (!Core.AOS || m != caster) && SpellHelper.ValidIndirectTarget(caster, m) && caster.CanBeHarmful(m, false))
                                    m_Queue.Enqueue(m);
                            }

                            while (m_Queue.Count > 0)
                            {
                                int damage = m_Item.m_Damage;
                                
                                Mobile m = (Mobile)m_Queue.Dequeue();

                                if (damage > 0)
                                {
                                    if (SpellHelper.CanRevealCaster(m))
                                        caster.RevealingAction();

                                    caster.DoHarmful(m);


                                    if (!Core.AOS && m.CheckSkill(SkillName.MagicResist, 0.0, 30.0))
                                    {
                                        damage = 1;

                                        m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                                    }

                                    AOS.Damage(m, caster, damage, 0, 100, 0, 0, 0);
                                    m.PlaySound(0x208);

                                    if (m is BaseCreature)
                                        ((BaseCreature)m).OnHarmfulSpell(caster);
                                }
                            }
                        }
                    }
                }
            }

        }
        #endregion

        public Mephitis( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}