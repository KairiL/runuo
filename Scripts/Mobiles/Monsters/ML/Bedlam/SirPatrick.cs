using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a Sir Patrick corpse" )]
	public class SirPatrick : SkeletalKnight
	{
		[Constructable]
		public SirPatrick()
        {
			IsParagon = true;

			Name = "Sir Patrick";
			Hue = 0x47E;

			SetStr( 208, 319 );
			SetDex( 98, 132 );
			SetInt( 55, 100 );

			SetHits( 616, 794 );

			SetDamage( 15, 25 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Cold, 60 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 50, 60 );

			SetSkill( SkillName.Wrestling, 126.3, 136.5 );
			SetSkill( SkillName.Tactics, 128.5, 143.8 );
			SetSkill( SkillName.MagicResist, 102.8, 117.9 );
			SetSkill( SkillName.Anatomy, 126.1, 137.2 );

			Fame = 18000;
			Karma = -18000;

            VirtualArmor = 40;

            switch (Utility.Random(6))
            {
                case 0: PackItem(new PlateArms()); break;
                case 1: PackItem(new PlateChest()); break;
                case 2: PackItem(new PlateGloves()); break;
                case 3: PackItem(new PlateGorget()); break;
                case 4: PackItem(new PlateLegs()); break;
                case 5: PackItem(new PlateHelm()); break;
            }

            PackSlayer();
            PackItem(new Scimitar());
            PackItem(new WoodenShield());

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
            AddLoot(LootPack.AosUltraRich);
            AddLoot(LootPack.AosSuperBoss);
        }

        public override bool BleedImmune { get { return true; } }
        public override bool AutoDispel { get { return true; } }

        public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( Utility.RandomDouble() < 0.1 )
				DrainLife();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( Utility.RandomDouble() < 0.1 )
				DrainLife();
		}

		public virtual void DrainLife()
		{
			List<Mobile> list = new List<Mobile>();

			foreach ( Mobile m in GetMobilesInRange( 2 ) )
			{
				if ( m == this || !CanBeHarmful( m, false ) || ( Core.AOS && !InLOS( m ) ) )
					continue;

				if ( m is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)m;

					if ( bc.Controlled || bc.Summoned || bc.Team != Team )
						list.Add( m );
				}
				else if ( m.Player )
				{
					list.Add( m );
				}
			}

			foreach ( Mobile m in list )
			{
				DoHarmful( m );

				m.FixedParticles( 0x374A, 10, 15, 5013, 0x455, 0, EffectLayer.Waist );
				m.PlaySound( 0x1EA );

				int drain = Utility.RandomMinMax( 14, 30 );

				Hits += drain;
				m.Damage( drain, this );
			}
		}

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (Utility.Random(10) == 0)
            {
                Item item;

                switch (Utility.Random(3))
                {
                    default:
                    case 1: item = new JonahNote1(); break;
                    case 2: item = new JonahNote2(); break;
                    case 3: item = new JonahNote3(); break;
                }

                c.DropItem(item);
                // TODO: uncomment once added
                /*
                if (Utility.RandomDouble() < 0.15)
                    c.DropItem(new DisintegratingThesisNotes());

                if (Utility.RandomDouble() < 0.05)
                    c.DropItem(new AssassinChest());
                */
            }

            base.OnDeath(c);
        }

        public override OppositionGroup OppositionGroup
        {
            get { return OppositionGroup.FeyAndUndead; }
        }

        public override bool GivesMLMinorArtifact{ get{ return true; } }

		public SirPatrick( Serial serial )
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

