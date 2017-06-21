using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an Irk corpse" )]
	public class Irk : Changeling
	{
		public override string DefaultName{ get{ return "Irk"; } }
		public override int DefaultHue{ get{ return 0x489; } }

		[Constructable]
		public Irk()
		{
			IsParagon = true;

			Hue = DefaultHue;

			SetStr( 57, 168 );
			SetDex( 259, 385 );
			SetInt( 430, 555 );

			SetHits( 1006, 1064 );
			SetStam( 259, 385 );
			SetMana( 430, 555 );

            SetDamage( 14, 20 );

			SetResistance( ResistanceType.Physical, 80, 90 );
			SetResistance( ResistanceType.Fire, 30, 50 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Wrestling, 120.3, 123.0 );
			SetSkill( SkillName.Tactics, 120.1, 131.8 );
			SetSkill( SkillName.MagicResist, 132.3, 165.8 );
			SetSkill( SkillName.Magery, 108.1, 119.7 );
			SetSkill( SkillName.EvalInt, 108.4, 120.0 );
			SetSkill( SkillName.Meditation, 108.9, 119.1 );

			Fame = 21000;
			Karma = -21000;

            VirtualArmor = 75;
            AddItem(new DarkSource());

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

        // TODO: Angry fire
        public override void OnDamagedBySpell(Mobile from)
        {
            if (from.Combatant == null)
                return;

            Mobile m = from.Combatant;

            if (m.Body == 58)
                m.Say("I now own your soul!!!");

            if (m.Body != from.Body)
            {
                m.BoltEffect(0);

                m.Body = from.Body;
                m.Hue = from.Hue;
                m.Name = from.Name;

                m.Fame = from.Fame;
                m.Karma = (0 - from.Karma);
                m.Title = from.Title;

                m.Str = from.Str;
                m.Int = from.Int;
                m.Dex = from.Dex;

                m.Hits = from.Hits + 2000;

                m.Dex = from.Dex;
                m.Mana = from.Mana;
                m.Stam = from.Stam;

                m.Female = from.Female;

                m.VirtualArmor = (from.VirtualArmor + 95);

                Item hair = new Item(Utility.RandomList(8265));
                hair.Hue = 1167;
                hair.Layer = Layer.Hair;
                hair.Movable = false;
                m.AddItem(hair);

                Kasa hat = new Kasa();
                hat.Hue = 1167;
                hat.Movable = false;
                m.AddItem(hat);

                DeathRobe robe = new DeathRobe();
                robe.Name = "Death Robe";
                robe.Hue = 1167;
                robe.Movable = false;
                m.AddItem(robe);

                Sandals sandals = new Sandals();
                sandals.Hue = 1167;
                sandals.Movable = false;
                m.AddItem(sandals);

                BagOfAllReagents bag = new BagOfAllReagents();
                m.AddToBackpack(bag);

                m.BoltEffect(0);
            }
            switch (Utility.Random(5))
            {

                case 0: m.Say("We are now one with each other!!"); break;
                case 1: m.Say("Your weak spells have no effect on me, muahahaha!!"); break;
                case 2: m.Say("Your end is near young adventurer!!"); break;
                case 3: m.Say("Thou shalt not pass my post!!"); break;
                case 4: m.Say("I now own your soul!!!"); break;
            }
            from.BoltEffect(0);
            from.Damage(Utility.Random(1, 50));
            m.Hits += (Utility.Random(1, 50));
        }

        public override bool AutoDispel { get { return true; } }
        public override bool AlwaysMurderer { get { return true; } }
        public override bool BardImmune { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }

        public override void OnDeath(Container c)
        {
            if (Utility.Random(2) == 0)
            {
                Item item;

                switch (Utility.Random(1))
                {
                    default:
                    case 1: item = new IrkBrain(); break;
                }

                c.DropItem(item);
            }
            /*
            // TODO: uncomment once added
            if ( Utility.RandomDouble() < 0.025 )
                c.DropItem( new PaladinGloves() );
            */
            base.OnDeath(c);
        }

        public override bool GivesMLMinorArtifact{ get{ return true; } }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 4);
            AddLoot(LootPack.FilthyRich);
        }



        public Irk( Serial serial )
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
