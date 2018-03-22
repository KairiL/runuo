using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Mobiles
{
	[CorpseName( "an abyssal infernal's corpse" )]
	public class AbyssalInfernal : BaseCreature
	{
		[Constructable]
		public AbyssalInfernal() : base( AIType.AI_NecromageEpic, FightMode.Closest, 12, 1, 0.1, 0.2 )
		{
			Name = "Abyssal Infernal";
            Body = 0x2C9;
            BaseSoundID = 0x451; ;


            SetStr( 316, 405 );
			SetDex( 196, 215 );
			SetInt( 150, 250 );

			SetHits( 15000, 20000 );

			SetDamage( 20, 30 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Fire, 40 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 75, 85 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 55, 60 );

			SetSkill( SkillName.EvalInt, 140.1, 160.0 );
			SetSkill( SkillName.Magery, 140.1, 160.0 );
			SetSkill( SkillName.Meditation, 100.1, 121.0 );
            SetSkill( SkillName.Poisoning, 120.1, 141.0 );
			SetSkill( SkillName.MagicResist, 275.2, 300.0 );

			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 120.1, 140.0 );
			SetSkill( SkillName.Necromancy, 110.1, 130.0 );
			SetSkill( SkillName.SpiritSpeak, 130.1, 160.0 );
            

			Fame = 30000;
			Karma = -30000;

			VirtualArmor = 60;
			PackNecroReg( 30, 275 );
            AddNewbied(new PLichRing());


        }

        public void AddNewbied(BaseRing item)
        {
            item.LootType = LootType.Newbied;
            item.Attributes.CastSpeed = 6;
            AddItem(item);
        }
        public override TimeSpan ReacquireDelay { get { return TimeSpan.FromSeconds(1.0); } }
        public override bool ReacquireOnMovement { get { return true; } }
        public override bool CanFlee { get { return false; } }

        public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public override void GenerateLoot()
		{
            AddLoot(LootPack.Epic, 2);
            AddLoot(LootPack.HighEpic, 1);
            AddLoot(LootPack.LowScrolls, 2);
            AddLoot(LootPack.MedScrolls, 2);
            AddLoot(LootPack.HighScrolls, 3);
        }

		public override bool Unprovokable{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 6; } }


        private void RandoTarget(Mobile from)
        {
            double SwitchRate = .01;
            int PullRange = 10;
            foreach (Mobile m_target in GetMobilesInRange(PullRange))
                if ((m_target != from) && (SpellHelper.ValidIndirectTarget(from, (Mobile)m_target) && from.CanBeHarmful((Mobile)m_target, false)))
                {
                    if (Utility.RandomDouble() < SwitchRate)
                        from.Combatant = m_target;
                }
        }

        public override void OnThink()
        {
            base.OnThink();
            RandoTarget(this);
            double SummonRate = .01;
            if (Utility.RandomDouble() < SummonRate)
                SummonCritters();
            //Meteor Strike dismount poof
            //Condemnation (send to fire pits)
            //Power of one or w/e does a bunch of damage to one target
        }

        public void SummonCritters()
        {
            int SummonRange;
            Mobile SummonTarget;
            Map map = this.Map;

            if (map == null)
                return;

            int CritterNum = Utility.Random(8);
            int newCritters = Utility.RandomMinMax(4, 8);

            for (int i = 0; i < newCritters; ++i)
            {
                BaseCreature critter;

                switch (CritterNum)
                {
                    default:
                    case 0: critter = new PredatorHellCat(); break;
                    case 1: critter = new FireSteed(); break;
                    case 2: critter = new Nightmare(); break;
                    case 3: critter = new FireGargoyle(); break;
                    case 4: critter = new HellHound(); break;
                    case 5: critter = new Gargoyle(); break;
                    case 6: critter = new LavaElemental(); break;
                    case 7: critter = new Efreet(); break;
                }

                critter.Team = this.Team;

                bool validLocation = false;
                Point3D loc = this.Location;

                for (int j = 0; !validLocation && j < 10; ++j)
                {
                    int x = X + Utility.Random(8) - 1;
                    int y = Y + Utility.Random(8) - 1;
                    int z = map.GetAverageZ(x, y);

                    if (validLocation = map.CanFit(x, y, Z, 16, false, false))
                        loc = new Point3D(x, y, this.Z);
                    else if (validLocation = map.CanFit(x, y, z, 16, false, false))
                        loc = new Point3D(x, y, z);
                }

                critter.MoveToWorld(loc, map);
            }
                
        }

        public void MeteorStrike()
        {
            int MeteorRadius = 10;
            for (int i = -1; i<2; i++)
                for (int j = -1; j<2; j++)
                    Effects.SendLocationEffect(new Point3D(X+i, Y+j, Z), Map, 0x3709, 13);
            //smoke and icy poofs here
            Say("You will burn to a pile of ash!");
            //foreach( Mobile m_target in inRangeGetMobilesInRange(MeteorRadius)
        }

        public AbyssalInfernal( Serial serial ) : base( serial )
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