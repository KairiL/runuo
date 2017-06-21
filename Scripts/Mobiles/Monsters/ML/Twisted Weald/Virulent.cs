using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a Virulent corpse" )]
	public class Virulent : DreadSpider
	{
		[Constructable]
		public Virulent()
        {
			Name = "Virulent";
			Hue = 0x8FD;

			SetStr( 207, 252 );
			SetDex( 156, 194 );
			SetInt( 340, 398 );

			SetHits( 616, 750 );
			SetStam( 156, 200 );
			SetMana( 346, 398 );

			SetDamage( 15, 22 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Poison, 80 );

			SetResistance( ResistanceType.Physical, 60, 70 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Wrestling, 92.8, 115.0 );
			SetSkill( SkillName.Tactics, 91.6, 109.9 );
			SetSkill( SkillName.MagicResist, 78.1, 99.9 );
			SetSkill( SkillName.Poisoning, 120.0 );
			SetSkill( SkillName.Magery, 103.2, 119.8 );
			SetSkill( SkillName.EvalInt, 102.8, 117.8 );

			Fame = 21000;
			Karma = -21000;

            VirtualArmor = 36;

            switch (Utility.Random(64))
            {
                case 0: AddItem(new PlateOfHonorArms()); break;
                case 1: AddItem(new PlateOfHonorChest()); break;
                case 2: AddItem(new PlateOfHonorGloves()); break;
                case 3: AddItem(new PlateOfHonorGorget()); break;
                case 4: AddItem(new PlateOfHonorHelm()); break;
                case 5: AddItem(new PlateOfHonorLegs()); break;
                case 6: AddItem(new AcolyteArms()); break;
                case 7: AddItem(new AcolyteChest()); break;
                case 8: AddItem(new AcolyteGloves()); break;
                case 9: AddItem(new AcolyteLegs()); break;
                case 10: AddItem(new EvocaricusSword()); break;
                case 11: AddItem(new MalekisHonor()); break;
                case 12: AddItem(new GrizzleArms()); break;
                case 13: AddItem(new GrizzleChest()); break;
                case 14: AddItem(new GrizzleGloves()); break;
                case 15: AddItem(new GrizzleHelm()); break;
                case 16: AddItem(new PlateOfHonorLegs()); break;
                case 17: AddItem(new MageArmorArms()); break;
                case 18: AddItem(new MageArmorChest()); break;
                case 19: AddItem(new MageArmorGloves()); break;
                case 20: AddItem(new MageArmorLegs()); break;
                case 21: AddItem(new MyrmidonArms()); break;
                case 22: AddItem(new MyrmidonChest()); break;
                case 23: AddItem(new MyrmidonGloves()); break;
                case 24: AddItem(new MyrmidonGorget()); break;
                case 25: AddItem(new MyrmidonHelm()); break;
                case 26: AddItem(new MyrmidonLegs()); break;
                case 27: AddItem(new DeathEssenceArms()); break;
                case 28: AddItem(new DeathEssenceChest()); break;
                case 29: AddItem(new DeathEssenceGloves()); break;
                case 30: AddItem(new DeathEssenceHelm()); break;
                case 31: AddItem(new DeathEssenceLegs()); break;
            }
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
            AddLoot(LootPack.FilthyRich);
        }

        public override Poison PoisonImmune { get { return Poison.Lethal; } }
        public override Poison HitPoison { get { return Poison.Lethal; } }
        public override int TreasureMapLevel { get { return 3; } }

        public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.MortalStrike;
		}

		/*
		// TODO: uncomment once added
		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if ( Utility.RandomDouble() < 0.025 )
			{
				switch ( Utility.Random( 2 ) )
				{
					case 0: c.DropItem( new HunterLegs() ); break;
					case 1: c.DropItem( new MalekisHonor() ); break;
				}
			}

			if ( Utility.RandomDouble() < 0.1 )
				c.DropItem( new ParrotItem() );
		}
		*/

		public override bool GivesMLMinorArtifact{ get{ return true; } }

		public Virulent( Serial serial )
			: base( serial )
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
