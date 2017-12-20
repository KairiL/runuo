using System;
using Server;
using Server.Items;
using Server.Factions;

namespace Server.Mobiles
{
	[CorpseName( "a magic elemental corpse" )]
	public class MagicElemental : BaseCreature
	{
        public override Faction FactionAllegiance { get { return CouncilOfMages.Instance; } }
        public override Ethics.Ethic EthicAllegiance { get { return Ethics.Ethic.Hero; } }

        [Constructable]
		public MagicElemental () : base( AIType.AI_Mage, FightMode.Closest, 12, 1, 0.2, 0.4 )
		{
			Name = "a magic elemental";
			Body = 16;
            Hue = 2067;
			BaseSoundID = 278;
            CanSwim = true;

			SetStr( 526, 615 );
			SetDex( 66, 85 );
			SetInt( 426, 550 );

            SetHits(592, 711);

            SetDamage( 17, 27 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Poison, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 125.1, 160.0 );
			SetSkill( SkillName.Magery, 105.1, 160.0 );
			SetSkill( SkillName.Meditation, 300.4, 400.0 );
			SetSkill( SkillName.MagicResist, 1000.0, 1200.0 );
			SetSkill( SkillName.Necromancy, 100.1, 120.0 );
            SetSkill( SkillName.SpiritSpeak, 125.1, 160.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 12500;
			Karma = 0;

			VirtualArmor = 60;
            AddNewbied(new StaffRing());

        }

        public void AddNewbied(Item item)
        {
            item.LootType = LootType.Newbied;

            AddItem(item);
        }

        public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 3 );
            AddLoot(LootPack.LowScrolls, 2);
            AddLoot(LootPack.MedScrolls, 3);
            AddLoot(LootPack.HighScrolls, 4);
        }

        public override bool ReacquireOnMovement { get { return true; } }
        public override bool HasBreath { get { return true; } } // fire breath enabled

        public override int BreathFireDamage { get { return 20; } }
        public override int BreathColdDamage { get { return 20; } }
        public override int BreathPhysicalDamage { get { return 20; } }
        public override int BreathEnergyDamage { get { return 20; } }
        public override int BreathPoisonDamage { get { return 20; } }

        public override int BreathEffectHue { get { return 2067; } }

        public virtual double BreathMinDelay { get { return 2.0; } }
        public virtual double BreathMaxDelay { get { return 15.0; } }
        
        public virtual double BreathStallTime { get { return 0.0; } }

        public override void CheckReflect(Mobile caster, ref bool reflect)
        {
            reflect = true; // Always reflect if caster isn't female
        }

        public override int TreasureMapLevel{ get{ return 5; } }

		public MagicElemental( Serial serial ) : base( serial )
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