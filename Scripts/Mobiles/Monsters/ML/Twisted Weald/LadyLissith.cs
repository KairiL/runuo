using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a Lady Lissith corpse" )]
	public class LadyLissith : GiantBlackWidow
	{
		[Constructable]
		public LadyLissith()
		{
			IsParagon = true;

			Name = "Lady Lissith";
			Hue = 0x452;

			SetStr( 81, 130 );
			SetDex( 116, 152 );
			SetInt( 44, 100 );

			SetHits( 245, 375 );
			SetStam( 116, 152 );
			SetMana( 44, 100 );

			SetDamage( 15, 22 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 70, 80 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.Wrestling, 108.6, 124.0 );
			SetSkill( SkillName.Tactics, 102.7, 119.0 );
			SetSkill( SkillName.MagicResist, 78.8, 95.6 );
			SetSkill( SkillName.Anatomy, 68.6, 106.8 );
			SetSkill( SkillName.Poisoning, 96.6, 119.9 );

			Fame = 18900;
			Karma = -18900;
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
            if (Utility.Random(2) == 0)
            {
                Item item;

                switch (Utility.Random(1))
                {
                    default:
                    case 1: item = new LissithSilk(); break;
                }

                c.DropItem(item);
            }
            /*
            // TODO: uncomment once added
            if ( Utility.RandomDouble() < 0.025 )
                c.DropItem( new GreymistChest() );

            if ( Utility.RandomDouble() < 0.1 )
                c.DropItem( new ParrotItem() );
            */

            base.OnDeath(c);
        }

        public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}



		public override bool GivesMLMinorArtifact{ get{ return true; } }

		public LadyLissith( Serial serial )
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
            if (BaseSoundID == 263)
                BaseSoundID = 1170;
        }
	}
}
