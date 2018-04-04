using System; 
using Server;
using Server.Items;

namespace Server.Mobiles 
{ 
	[CorpseName( "a golem controller corpse" )] 
	public class GolemController : BaseCreature 
	{ 
		[Constructable] 
		public GolemController() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 
			Name = NameList.RandomName( "golem controller" );
			Title = "the controller";

			Body = 400;
			Hue = 0x455;

			AddArcane( new Robe() );
			AddArcane( new ThighBoots() );
			AddArcane( new LeatherGloves() );
			AddArcane( new Cloak() );
            
            SetStr( 126, 150 );
			SetDex( 96, 120 );
			SetInt( 211, 275 );

			SetHits( 100, 140 );

			SetDamage( 6, 12 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 35, 45 );
			SetResistance( ResistanceType.Cold, 45, 55 );
			SetResistance( ResistanceType.Poison, 15, 25 );
			SetResistance( ResistanceType.Energy, 25, 35 );

			SetSkill( SkillName.EvalInt, 105.1, 120.0 );
			SetSkill( SkillName.Magery, 105.1, 120.0 );
			SetSkill( SkillName.Meditation, 95.1, 120.0 );
			SetSkill( SkillName.MagicResist, 102.5, 125.0 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 95.0, 107.5 );

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 16;

			if ( 0.7 > Utility.RandomDouble() )
				PackItem( new ArcaneGem() );
            if (0.8 < Utility.RandomDouble() )
                PackItem(new TrapRepairKit());
        }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich );
		}

		public void AddArcane( Item item )
		{
			if ( item is IArcaneEquip )
			{
				IArcaneEquip eq = (IArcaneEquip)item;
				eq.CurArcaneCharges = eq.MaxArcaneCharges = 20;
			}

			item.Hue = ArcaneGem.DefaultArcaneHue;
			item.LootType = LootType.Newbied;

			AddItem( item );
		}

		public override bool ClickTitle{ get{ return false; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysMurderer{ get{ return true; } }

		public GolemController( Serial serial ) : base( serial ) 
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