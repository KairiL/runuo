using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a Gnaw corpse" )]
	public class Gnaw : DireWolf
	{
		[Constructable]
		public Gnaw()
        {
			Name = "Gnaw";
			Hue = 0x130;

			SetStr( 142, 172 );
			SetDex( 120, 145 );
			SetInt( 44, 60 );

			SetHits( 793, 857 );
			SetStam( 120, 145 );
			SetMana( 52, 86 );

			SetDamage( 16, 22 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 60, 70 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 25, 30 );
			SetResistance( ResistanceType.Energy, 20, 35 );

			SetSkill( SkillName.Wrestling, 96.3, 119.9 );
			SetSkill( SkillName.Tactics, 84.1, 109.9 );
			SetSkill( SkillName.MagicResist, 90.1, 114.9 );

			Fame = 17500;
			Karma = -17500;
		}

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 4);
            AddLoot(LootPack.FilthyRich);
        }
        public override int Meat { get { return 1; } }
        public override int Hides { get { return 7; } }
        public override HideType HideType { get { return HideType.Spined; } }
        public override FoodType FavoriteFood { get { return FoodType.Meat; } }
        public override PackInstinct PackInstinct { get { return PackInstinct.Canine; } }

        public override bool GivesMLMinorArtifact{ get{ return true; } }

        public override void OnDeath(Container c)
        {
            if (Utility.Random(4) == 0)
            {
                Item item;

                switch (Utility.Random(1))
                {
                    default:
                    case 1: item = new GnawFang(); break;
                }

                c.DropItem(item);
            }

            base.OnDeath(c);
        }

        public Gnaw( Serial serial )
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
