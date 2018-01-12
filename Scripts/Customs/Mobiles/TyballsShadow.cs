using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "Tyball's Shadow's corpse" )]
	public class TyballsShadow : BaseCreature
	{
		[Constructable]
		public TyballsShadow() : base( AIType.AI_NecromageEpic, FightMode.Closest, 10, 1, 0.1, 0.2 )
		{
			Name = NameList.RandomName( "Tyball's Shadow" );
			Body = 400;
            Hue = 1909;

			SetStr( 216, 305 );
			SetDex( 96, 115 );
			SetInt( 966, 1045 );

			SetHits( 7500, 12000 );

			SetDamage( 20, 30 );

			SetDamageType( ResistanceType.Poison, 20 );
            SetDamageType( ResistanceType.Fire, 20);
            SetDamageType( ResistanceType.Cold, 40);
            SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 75, 85 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.EvalInt, 130.1, 160.0 );
			SetSkill( SkillName.Magery, 140.1, 160.0 );
			SetSkill( SkillName.Meditation, 300.1, 401.0 );
			SetSkill( SkillName.Poisoning, 100.1, 141.0 );
			SetSkill( SkillName.MagicResist, 100.2, 120.0 );

			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 95.1, 120.0 );
			SetSkill( SkillName.Necromancy, 120.1, 140.0 );
			SetSkill( SkillName.SpiritSpeak, 110.1, 130.0 );

            
            Fame = 25000;
            Karma = -25000;

            VirtualArmor = 60;
			PackNecroReg( 30, 275 );
            Item ring = new StaffRing();
            ring.Visible = false;
            AddNewbied(ring);
            Item shroud = new ShroudOfTheCondemned();
            shroud.Movable = false;
            AddNewbied(shroud);

        }

        public void AddNewbied(Item item)
        {
            item.LootType = LootType.Newbied;
            AddItem(item);
        }
        public override TimeSpan ReacquireDelay { get { return TimeSpan.FromSeconds(1.0); } }
        public virtual bool ReacquireOnMovement { get { return true; } }

		public override void GenerateLoot()
		{
            AddLoot(LootPack.UltraRich, 2);
            AddLoot(LootPack.Epic, 2);
            AddLoot(LootPack.LowEpic2, 1);
            AddLoot(LootPack.LowScrolls, 2);
            AddLoot(LootPack.MedScrolls, 2);
            AddLoot(LootPack.HighScrolls, 3);
        }

		public override bool Unprovokable{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		public TyballsShadow( Serial serial ) : base( serial )
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