using System;
using Server;
using Server.Items;
using Server.Factions;

namespace Server.Mobiles
{
	[CorpseName( "a daemon corpse" )]
	public class Archdemon : BaseCreature
	{
		[Constructable]
		public Archdemon() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "An archdemon";
			Body = 9;
			BaseSoundID = 0x2B9;
            Hue = 0x455;

            SetStr( 476, 505 );
			SetDex( 76, 95 );
			SetInt( 301, 325 );

			SetHits( 286, 303 );

			SetDamage( 7, 14 ); // probably wrong

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 55, 70 );// Resists are almost certainly wrong
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.EvalInt, 95.1, 120.0 );
			SetSkill( SkillName.Magery, 105.1, 125.0 );
			SetSkill( SkillName.MagicResist, 100.1, 115.0 );
            SetSkill( SkillName.Meditation, 90.1, 105.0 );
			SetSkill( SkillName.Tactics, 120.1, 140.0 );
			SetSkill( SkillName.Wrestling, 70.1, 90.0 );

			Fame = 30000;
			Karma = -15000;

			VirtualArmor = 58;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average, 2 );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Meat{ get{ return 1; } }
		public override bool CanFly { get { return true; } }

		public Archdemon( Serial serial ) : base( serial )
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
