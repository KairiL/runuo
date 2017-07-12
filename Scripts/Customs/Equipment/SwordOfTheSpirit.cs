using System;
using System.Collections;
using Server.Items;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
	[FlipableAttribute( 0x906, 0x906 )]
	public class SwordOfTheSpirit : BaseAxe
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.CrushingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Dismount; } }

		public override int AosStrengthReq{ get{ return 35; } }
		public override int AosMinDamage{ get{ return 20; } }
		public override int AosMaxDamage{ get{ return 24; } }
		public override int AosSpeed{ get{ return 56; } }
		public override float MlSpeed{ get{ return 3.00f; } }

		public override int OldStrengthReq{ get{ return 35; } }
		public override int OldMinDamage{ get{ return 40; } }
		public override int OldMaxDamage{ get{ return 48; } }
		public override int OldSpeed{ get{ return 56; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

        public override int ArtifactRarity { get { return 777; } }

        [Constructable]
		public SwordOfTheSpirit() : base( 0x906 )
		{
            Name = "The Sword of The Spirit";
            Layer = Layer.FirstValid;
			Weight = 0.0;
            LootType = LootType.Blessed;
            FollowersBonus = 2;
            Attributes.SpellChanneling = 1;
            Attributes.CastSpeed = 1;
            Attributes.CastRecovery = 3;
            Attributes.SpellDamage = 125;
            Attributes.LowerRegCost = 20;
            Attributes.AttackChance = 45;
            Attributes.BonusDex = 65;
            Attributes.Luck = 400;
            SkillBonuses.SetValues(0, SkillName.Healing, 100);
            SkillBonuses.SetValues(1, SkillName.Wrestling, 100);
            SkillBonuses.SetValues(2, SkillName.Parry, 100);
            MaxRange = 10;
            WeaponAttributes.MageWeapon = 30;
            WeaponAttributes.HitLowerAttack = 100;
            WeaponAttributes.HitLowerDefend = 100;

        }

		public SwordOfTheSpirit( Serial serial ) : base( serial )
		{
		}

        public override void OnDoubleClick(Mobile from)
        {
            if (Deleted)
                return;
            Point3D loc = GetWorldLocation();

            if (!from.InLOS(loc) || !from.InRange(loc, 2))
            {
                from.LocalOverheadMessage(Server.Network.MessageType.Regular, 0x3E9, 1019045); // I can't reach that
                return;
            }
            else if (!this.IsAccessibleTo(from))
            {
                from.PublicOverheadMessage(MessageType.Regular, 0x3E9, 1061637); // You are not allowed to access this.
                return;
            }
            from.Target = new SpiritTarget(from, this);
        }

        private class SpiritInfo
        {
            public Mobile m_From;
            public Mobile m_Creature;
            public DateTime m_EndTime;
            public bool m_Ending;
            public Timer m_Timer;
            public int m_Effect;
            public ArrayList m_Mods;

            public SpiritInfo(Mobile from, Mobile creature, int effect, ArrayList mods)
            {
                m_From = from;
                m_Creature = creature;
                m_EndTime = DateTime.UtcNow;
                m_Ending = false;
                m_Effect = effect;
                m_Mods = mods;

                Apply();
            }

            public void Apply()
            {
                for (int i = 0; i < m_Mods.Count; ++i)
                {
                    object mod = m_Mods[i];

                    if (mod is ResistanceMod)
                        m_Creature.AddResistanceMod((ResistanceMod)mod);
                    else if (mod is StatMod)
                        m_Creature.AddStatMod((StatMod)mod);
                    else if (mod is SkillMod)
                        m_Creature.AddSkillMod((SkillMod)mod);
                }
            }

            public void Clear()
            {
                for (int i = 0; i < m_Mods.Count; ++i)
                {
                    object mod = m_Mods[i];

                    if (mod is ResistanceMod)
                        m_Creature.RemoveResistanceMod((ResistanceMod)mod);
                    else if (mod is StatMod)
                        m_Creature.RemoveStatMod(((StatMod)mod).Name);
                    else if (mod is SkillMod)
                        m_Creature.RemoveSkillMod((SkillMod)mod);
                }
            }
        }

        private static Hashtable m_Table = new Hashtable();

        public static bool GetEffect(Mobile targ, ref int effect)
        {
            SpiritInfo info = m_Table[targ] as SpiritInfo;

            if (info == null)
                return false;

            effect = info.m_Effect;
            return true;
        }

        private static void ProcessSpirit(SpiritInfo info)
        {
            Mobile from = info.m_From;
            Mobile targ = info.m_Creature;
            bool ends = false;

            // According to uoherald bard must remain alive, visible, and 
            // within range of the target or the effect ends in 15 seconds.
            if (!targ.Alive || targ.Deleted || !from.Alive )
                ends = true;
            else
            {
                int range = (int)targ.GetDistanceToSqrt(from);

                if (from.Map != targ.Map || range > info.m_Effect)
                    ends = true;
            }

            if (ends && info.m_Ending && info.m_EndTime < DateTime.UtcNow)
            {
                if (info.m_Timer != null)
                    info.m_Timer.Stop();

                info.Clear();
                m_Table.Remove(targ);
            }
            else
            {
                if (ends && !info.m_Ending)
                {
                    info.m_Ending = true;
                    info.m_EndTime = DateTime.UtcNow + TimeSpan.FromSeconds(15);
                }
                else if (!ends)
                {
                    info.m_Ending = false;
                    info.m_EndTime = DateTime.UtcNow;
                }

                targ.FixedEffect(0x376A, 1, 32);
            }
        }

        public class SpiritTarget : Target
        {
            private BaseWeapon m_wep;

            public SpiritTarget(Mobile from, BaseWeapon wep) : base(wep.MaxRange, false, TargetFlags.None)
            {
                m_wep=wep;
            }
            protected override void OnTarget(Mobile from, object target)
            {
                ArrayList mods = new ArrayList();
                double scalar = m_wep.MaxRange / 50.0;

                if (target is Mobile)
                {
                    Mobile targ = (Mobile)target;
                    if (m_Table.Contains(targ)) //Already buffed
                    {
                        from.SendMessage("Your target is already under the influence of The Spirit");
                    }
                    else
                    {
                        from.SendMessage("You imbue them with the power of spirit.");
                    
                        mods.Add(new ResistanceMod(ResistanceType.Physical, m_wep.MaxRange /2));
                        mods.Add(new ResistanceMod(ResistanceType.Fire, m_wep.MaxRange /2));
                        mods.Add(new ResistanceMod(ResistanceType.Cold, m_wep.MaxRange /2));
                        mods.Add(new ResistanceMod(ResistanceType.Poison, m_wep.MaxRange /2));
                        mods.Add(new ResistanceMod(ResistanceType.Energy, m_wep.MaxRange /2));

                        for (int i = 0; i < targ.Skills.Length; ++i)
                        {
                            if (targ.Skills[i].Value > 0)
                                mods.Add(new DefaultSkillMod((SkillName)i, true, targ.Skills[i].Value * scalar));
                        }
                        mods.Add(new StatMod(StatType.Str, "SpiritStr", (int)(targ.RawStr * scalar), TimeSpan.Zero));
                        mods.Add(new StatMod(StatType.Int, "SpiritInt", (int)(targ.RawInt * scalar), TimeSpan.Zero));
                        mods.Add(new StatMod(StatType.Dex, "SpiritDex", (int)(targ.RawDex * scalar), TimeSpan.Zero));
                        SpiritInfo info = new SpiritInfo(from, targ, m_wep.MaxRange / 2, mods);
                        info.m_Timer = Timer.DelayCall<SpiritInfo>(TimeSpan.Zero, TimeSpan.FromSeconds(1.25), new TimerStateCallback<SpiritInfo>(ProcessSpirit), info);

                        m_Table[target] = info;
                    }
                }
                else
                {
                    from.SendMessage("You point your sword, but nothing significant appears to happen.");
                }
            }
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