using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an taskmaster corpse" )]
	public class Taskmaster: BaseCreature
	{
		[Constructable]
		public Taskmaster() : base( AIType.AI_NecromageHive, FightMode.Closest, 10, 0, 0.1, 0.2 )
		{
			Name = "A Taskmaster";
			Body = 1402;
			BaseSoundID = 412;//TODO: change sound ID

			SetStr( 116, 205 );
			SetDex( 96, 220 );
			SetInt( 966, 1045 );

			SetHits( 750, 1400 );

			SetDamage( 16, 28 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Cold, 40 );
			SetDamageType( ResistanceType.Energy, 40 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 35, 45 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 70, 80 );
			SetResistance( ResistanceType.Energy, 45, 50 );

			SetSkill( SkillName.EvalInt, 130.1, 160.0 );
			SetSkill( SkillName.Magery, 140.1, 160.0 );
			SetSkill( SkillName.Meditation, 100.1, 120.0 );
			SetSkill( SkillName.Poisoning, 120.1, 141.0 );
			SetSkill( SkillName.MagicResist, 80.1, 100.0 );

            SetSkill( SkillName.Anatomy, 100.1, 120.0 );
			SetSkill( SkillName.Tactics, 120.1, 140.0 );
			SetSkill( SkillName.Wrestling, 110.1, 130.0 );
			SetSkill( SkillName.Necromancy, 110.1, 130.0 );
			SetSkill( SkillName.SpiritSpeak, 100.1, 120.0 );
            

			Fame = 23000;
			Karma = -23000;

			VirtualArmor = 60;
			PackNecroReg( 30, 275 );
            AddNewbied(new StaffRing());

        }

        public void AddNewbied(Item item)
        {
            item.LootType = LootType.Newbied;

            AddItem(item);
        }

        public override TimeSpan ReacquireDelay { get { return TimeSpan.FromSeconds(3.0); } }
        public virtual bool ReacquireOnMovement { get { return true; } }
        public override Poison HitPoison { get { return (Poison.Lethal); } }

        public override bool OnBeforeDeath()
        {
            SpillAcid(6);

            return base.OnBeforeDeath();
        }

        public override int GetIdleSound()
		{
			return 0x19D;
		}

		public override int GetAngerSound()
		{
			return 0x175;
		}

		public override int GetDeathSound()
		{
			return 0x108;
		}

		public override int GetAttackSound()
		{
			return 0xE2;
		}

		public override int GetHurtSound()
		{
			return 0x28B;
		}

		public override void GenerateLoot()
		{
            AddLoot(LootPack.UltraRich, 2);
            AddLoot(LootPack.LowEpic1, 1);
            AddLoot(LootPack.LowScrolls, 2);
            AddLoot(LootPack.MedScrolls, 2);
            AddLoot(LootPack.HighScrolls, 3);
        }

		
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		public Taskmaster( Serial serial ) : base( serial )
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