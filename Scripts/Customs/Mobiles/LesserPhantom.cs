namespace Server.Mobiles
{
	[CorpseName( "a phantom corpse" )]
	public class LesserPhantom : Phantom
	{
        new protected double ColorChangeChance = .20;

        [Constructable]
		public LesserPhantom()
		{
			Name = "a lesser phantom";
			Body = 606;
			BaseSoundID = 0x47E;
            Hue = 1;

			SetStr( 500 );
			SetDex( 151, 175 );
			SetInt( 171, 220 );

			SetHits( 1000 );

			SetDamage( 24, 26 );

			SetDamageType( ResistanceType.Physical, 20 );
            SetDamageType( ResistanceType.Fire, 20 );
            SetDamageType(ResistanceType.Cold, 20);
            SetDamageType( ResistanceType.Poison, 20 );
            SetDamageType(ResistanceType.Energy, 20);

            SetResistance( ResistanceType.Physical, 200 );
			SetResistance( ResistanceType.Fire, 200 );
			SetResistance( ResistanceType.Cold, 200 );
			SetResistance( ResistanceType.Poison, 200 );
			SetResistance( ResistanceType.Energy, 200 );

            SetSkill(SkillName.Necromancy, 120, 130.0);
            SetSkill(SkillName.SpiritSpeak, 120.0, 140.0);

            SetSkill( SkillName.DetectHidden, 80.0 );
            SetSkill( SkillName.EvalInt, 120.0, 130.0 );
            SetSkill( SkillName.Magery, 112.6, 117.5 );
            SetSkill( SkillName.Meditation, 200.0 );
            SetSkill( SkillName.MagicResist, 150.1, 200.0 );
            SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 100.0, 110.0 );

			Fame = 10000;
			Karma = -10000;

			VirtualArmor = 44;

            AddMonkRobe();
		}

		public override void GenerateLoot()
		{
            AddLoot(LootPack.LowEpic1, 1);
        }
        
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 4; } }


        public LesserPhantom( Serial serial ) : base( serial )
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