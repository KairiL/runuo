using System;
using Server;
using Server.Items;
using Server.Factions;

namespace Server.Mobiles
{
	[CorpseName( "a phoenix corpse" )]
	public class GreaterPhoenix : BaseCreature
	{
        public override Faction FactionAllegiance { get { return CouncilOfMages.Instance; } }
        public override Ethics.Ethic EthicAllegiance { get { return Ethics.Ethic.Hero; } }

        [Constructable]
		public GreaterPhoenix() : base( AIType.AI_Mage, FightMode.Aggressor, 10, 1, 0.1, 0.2 )
		{
			Name = "a greater phoenix";
			Body = 0x340;
			BaseSoundID = 0x8F;

            SetStr(798, 930);
            SetDex(268, 400);
            SetInt(488, 620);

            SetHits(558, 599);

            SetDamage( 25, 35 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 50 );

			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 100 );
			SetResistance( ResistanceType.Poison, 45, 55 );
			SetResistance( ResistanceType.Energy, 60, 70 );

			SetSkill( SkillName.EvalInt, 130.2, 140.0 );
			SetSkill( SkillName.Magery, 120.2, 140.0 );
			SetSkill( SkillName.Meditation, 120.1, 140.0 );
			SetSkill( SkillName.MagicResist, 136.0, 165.0 );
			SetSkill( SkillName.Tactics, 80.1, 90.0 );
			SetSkill( SkillName.Wrestling, 120.1, 140.0 );

			Fame = 20000;
			Karma = 0;

			VirtualArmor = 60;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 3 );
			AddLoot( LootPack.Rich );
		}

		public override int Meat{ get{ return 1; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override int Feathers{ get{ return 36; } }
		public override bool CanFly { get { return true; } }

		public GreaterPhoenix( Serial serial ) : base( serial )
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