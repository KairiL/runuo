using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a Lady Sabrix corpse" )]
	public class LadySabrix : GiantBlackWidow
	{
		[Constructable]
		public LadySabrix()
        {
			Name = "Lady Sabrix";
			Hue = 0x497;

            BaseSoundID = 1170;

            SetStr( 82, 130 );
			SetDex( 117, 160 );
			SetInt( 45, 100 );

			SetHits( 233, 369 );
			SetStam( 117, 160 );
			SetMana( 45, 100 );

			SetDamage( 15, 22 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 39 );
			SetResistance( ResistanceType.Poison, 70, 80 );
			SetResistance( ResistanceType.Energy, 30, 45 );

			SetSkill( SkillName.Wrestling, 109.8, 129.9 );
			SetSkill( SkillName.Tactics, 102.8, 120.0 );
			SetSkill( SkillName.MagicResist, 75.1, 96.1 );
			SetSkill( SkillName.Anatomy, 68.8, 105.1 );
			SetSkill( SkillName.Poisoning, 97.8, 119.9 );

			Fame = 18900;
			Karma = -18900;

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
            AddLoot(LootPack.UltraRich, 4);
            AddLoot(LootPack.FilthyRich);
        }

        public override Poison PoisonImmune { get { return Poison.Lethal; } }
        public override Poison HitPoison { get { return Poison.Lethal; } }
        public override int TreasureMapLevel { get { return 3; } }

        public override void OnDeath(Container c)
        {
            if (Utility.Random(4) == 0)
            {
                Item item;

                switch (Utility.Random(1))
                {
                    default:
                    case 1: item = new SabrixEye(); break;
                }

                c.DropItem(item);
            }
            /*
            // TODO: uncomment once added
            if ( Utility.RandomDouble() < 0.25 )
            {
                switch ( Utility.Random( 2 ) )
                {
                    case 0: AddToBackpack( new PaladinArms() ); break;
                    case 1: AddToBackpack( new HunterLegs() ); break;
                }
            }

            if ( Utility.RandomDouble() < 0.1 )
                c.DropItem( new ParrotItem() );
            */

            base.OnDeath(c);
        }

        public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.ArmorIgnore;
		}



		public override bool GivesMLMinorArtifact{ get{ return true; } }

		public LadySabrix( Serial serial )
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
