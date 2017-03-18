using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a kitty corpse" )]
	[TypeAlias( "Server.Mobiles.Kitty" )]
	public class Kitty : BaseCreature
	{
		[Constructable]
		public HellCat() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a kitty";
			Body = 0xC9;
			Hue = Utility.RandomList( 0x647, 0x650, 0x659, 0x662, 0x66B, 0x674);
			BaseSoundID = 0x69;

			SetStr( 5, 15 );
			SetDex( 5, 40 );
			SetInt( 5, 15 );
			SetHits( 48, 67 );

			SetDamage( 6, 12 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Fire, 60 );

			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Fire, 80, 90 );
			SetResistance( ResistanceType.Energy, 15, 20 );

			SetSkill( SkillName.MagicResist, 0.1, 0.1 );
			SetSkill( SkillName.Tactics, 0.1, 0.1 );
			SetSkill( SkillName.Wrestling, 0.1, 0.1 );
			SetSkill( SkillName.Swordsmanship, 0.1, 0.1);
			SetSkill( SkillName.Anatomy, 0.1, 0.1);
			SetSkill( SkillName.Poisoning, 0.1, 0.1);
			SetSkill( SkillName.Parrying, 0.1, 0.1);
      
      Skills[SkillName.Poisoning].Cap = 120;
      Skills[SkillName.Poisoning].Cap = 120;
      
			Fame = 1000;
			Karma = -1000;

			VirtualArmor = 30;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 100.1;
			StatCap = 60000;
			Skills.Cap = 7000;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override int Hides{ get{ return 10; } }
		public override HideType HideType{ get{ return HideType.Spined; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Feline; } }

		public HellCat(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}
