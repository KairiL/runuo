//TODO: OnHit/OnDamage increase respective experience points.  If one or the other hits cap, evolve.  If both hit cap, evolve in the other direction.
//      At each evo ncrease skill caps, stat caps, resists, base damage
//      Aggro, Loot
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a dragon corpse" )]
	public class BabyDragon : BaseCreature
	{
        private int m_OffensiveXP;//TODO: serialize
        private int m_DefensiveXP;//TODO: serialize

        public int OffensiveXP
        { 
            set
            {
                m_OffensiveXP = value;
            }
            get
            {
                return m_OffensiveXP;
            }
        }

        public int DefensiveXP
        {
            set
            {
                m_DefensiveXP = value;
            }
            get
            {
                return m_DefensiveXP;
            }
        }


        public override bool IgnoreYoungProtection { get { return Core.ML; } }
        
		public override WeaponAbility GetWeaponAbility()
		{
			switch ( Utility.Random( 3 ) )
			{
				default:
				case 0: return WeaponAbility.WhirlwindAttack;
				case 1: return WeaponAbility.ArmorIgnore;
                case 2: return WeaponAbility.ParalyzingBlow;
			}
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
		}

        [Constructable]
		public BabyDragon () : base( AIType.AI_NecromageEpic, FightMode.Closest, 14, 0, 0.1, 0.2 )
		{
            Name = "A Baby Dragon";
            BaseSoundID = 0x165;
            BodyValue = 733;

            SetStr( 300 );
			SetDex( 50 );
			SetInt( 120 );

			SetDamage( 17, 21 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Cold, 20 );
			SetDamageType( ResistanceType.Poison, 20 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 30 );
			SetResistance( ResistanceType.Fire, 30 );
			SetResistance( ResistanceType.Cold, 30 );
			SetResistance( ResistanceType.Poison, 30 );
			SetResistance( ResistanceType.Energy, 30 );

			SetSkill( SkillName.Necromancy, 100.1, 120.0 );
			SetSkill( SkillName.SpiritSpeak, 10.0, 30.0 );
            
			SetSkill( SkillName.EvalInt, 10.0, 30.0 );
			SetSkill( SkillName.Magery, 101.0 );
			SetSkill( SkillName.Meditation, 10.0 );
			SetSkill( SkillName.MagicResist, 101.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 120.0 );

			Fame = 28000;
			Karma = -28000;

			VirtualArmor = 64;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.SuperBoss, 2 );
			AddLoot( LootPack.HighScrolls, Utility.RandomMinMax( 6, 60 ) );
		}

		public override bool BardImmune{ get{ return !Core.SE; } }
		public override bool Unprovokable{ get{ return Core.SE; } }
		public override bool AreaPeaceImmune { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override int TreasureMapLevel{ get{ return 1; } }

		private static bool m_InHere;

		public BabyDragon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );//version
            writer.Write(m_OffensiveXP);
            writer.Write(m_DefensiveXP);

        }

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            m_OffensiveXP = reader.ReadInt();
            m_DefensiveXP = reader.ReadInt();
		}
	}
}