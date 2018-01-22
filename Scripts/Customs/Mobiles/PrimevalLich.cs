using System;
using System.Collections.Generic;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a primeval liche's corpse" )]
	public class PrimevalLich : BaseCreature
	{
		[Constructable]
		public PrimevalLich() : base( AIType.AI_NecromageEpic, FightMode.Closest, 12, 1, 0.1, 0.2 )
		{
			Name = "Primeval Lich";
            Body = 830;
            BaseSoundID = 0x3E9;


            SetStr( 216, 305 );
			SetDex( 96, 115 );
			SetInt( 466, 545 );

			SetHits( 10000, 15000 );

			SetDamage( 20, 30 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Cold, 40 );
			SetDamageType( ResistanceType.Energy, 40 );

			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 35, 40 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 90, 100 );
			SetResistance( ResistanceType.Energy, 55, 60 );

			SetSkill( SkillName.EvalInt, 140.1, 160.0 );
			SetSkill( SkillName.Magery, 140.1, 160.0 );
			SetSkill( SkillName.Meditation, 100.1, 121.0 );
            SetSkill( SkillName.Poisoning, 120.1, 141.0 );
			SetSkill( SkillName.MagicResist, 0, 75.0 );

			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 120.1, 140.0 );
			SetSkill( SkillName.Necromancy, 130.1, 160.0 );
			SetSkill( SkillName.SpiritSpeak, 130.1, 160.0 );
            

			Fame = 30000;
			Karma = -30000;

			VirtualArmor = 60;
			PackNecroReg( 30, 275 );
            AddNewbied(new PLichRing());


        }

        public void AddNewbied(Item item)
        {
            item.LootType = LootType.Newbied;

            AddItem(item);
        }
        public override TimeSpan ReacquireDelay { get { return TimeSpan.FromSeconds(1.0); } }
        public override bool ReacquireOnMovement { get { return true; } }
        public override bool CanFlee { get { return false; } }

        public override void OnThink()
        {
            base.OnThink();
            Suppress(Combatant);
        }

        public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public override void GenerateLoot()
		{
            AddLoot(LootPack.Epic, 2);
            AddLoot(LootPack.HighEpic, 1);
            AddLoot(LootPack.LowScrolls, 2);
            AddLoot(LootPack.MedScrolls, 2);
            AddLoot(LootPack.HighScrolls, 3);
        }

		public override bool Unprovokable{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 6; } }

        #region Suppress
        private static Dictionary<Mobile, Timer> m_Suppressed = new Dictionary<Mobile, Timer>();
        private DateTime m_NextSuppress;

        public void Suppress(Mobile target)
        {
            if (target == null || m_Suppressed.ContainsKey(target) || Deleted || !Alive || m_NextSuppress > DateTime.UtcNow || 0.1 < Utility.RandomDouble())
                return;

            TimeSpan delay = TimeSpan.FromSeconds(Utility.RandomMinMax(20, 80));

            if (!target.Hidden && CanBeHarmful(target))
            {
                for (int i = 0; i < target.Skills.Length; i++)
                {
                    Skill s = target.Skills[i];

                    target.AddSkillMod(new TimedSkillMod(s.SkillName, true, s.Base * -0.28, delay));
                }

                int count = (int)Math.Round(delay.TotalSeconds / 1.25);
                Timer timer = new AnimateTimer(target, count);
                m_Suppressed.Add(target, timer);
                timer.Start();

                PlaySound(0x58C);
            }

            m_NextSuppress = DateTime.UtcNow + TimeSpan.FromSeconds(10);
        }

        public static void SuppressRemove(Mobile target)
        {
            if (target != null && m_Suppressed.ContainsKey(target))
            {
                Timer timer = m_Suppressed[target];

                if (timer != null || timer.Running)
                    timer.Stop();

                m_Suppressed.Remove(target);
            }
        }

        private class AnimateTimer : Timer
        {
            private Mobile m_Owner;
            private int m_Count;

            public AnimateTimer(Mobile owner, int count) : base(TimeSpan.Zero, TimeSpan.FromSeconds(1.25))
            {
                m_Owner = owner;
                m_Count = count;
            }

            protected override void OnTick()
            {
                if (m_Owner.Deleted || !m_Owner.Alive || m_Count-- < 0)
                {
                    SuppressRemove(m_Owner);
                }
                else
                    m_Owner.FixedParticles(0x376A, 1, 32, 0x15BD, EffectLayer.Waist);
            }
        }
        #endregion


        public PrimevalLich( Serial serial ) : base( serial )
		{
		}
    
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}