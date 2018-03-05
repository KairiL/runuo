using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Mobiles
{
	[CorpseName( "a Shimmering Effusion corpse" )]
	public class ShimmeringEffusion : BaseCreature
	{
		[Constructable]
		public ShimmeringEffusion() : base( AIType.AI_MageEpic, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a shimmering effusion";
			Body = 0x105;			

			SetStr( 509, 538 );
			SetDex( 300, 400 );
			SetInt( 1513, 1578 );

			SetHits( 20000 );

			SetDamage( 27, 31 );
			
			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Cold, 20 );
			SetDamageType( ResistanceType.Poison, 20 );
			SetDamageType( ResistanceType.Energy, 20 );
			
			SetResistance( ResistanceType.Physical, 60, 80 );
			SetResistance( ResistanceType.Fire, 60, 80 );
			SetResistance( ResistanceType.Cold, 60, 80 );
			SetResistance( ResistanceType.Poison, 60, 80 );
			SetResistance( ResistanceType.Energy, 60, 80 );

			SetSkill( SkillName.Wrestling, 100.2, 101.4 );
			SetSkill( SkillName.Tactics, 105.5, 102.1 );
			SetSkill( SkillName.MagicResist, 150 );
			SetSkill( SkillName.Magery, 150.0 );
			SetSkill( SkillName.EvalInt, 150.0 );
			SetSkill( SkillName.Meditation, 120.0 );

			Fame = 8000;
			Karma = -8000;

			VirtualArmor = 70;
			
			PackItem( new GnarledStaff() );
			PackNecroReg( 15, 25 );
            Timer.DelayCall(TimeSpan.FromMinutes(20.0), new TimerStateCallback(DeletePeerless), this);
        }

        public void DeletePeerless(object state)
        {
            Mobile from = (Mobile)state;
            from.Delete();
        }

        public override void GenerateLoot()
		{
			AddLoot( LootPack.AosSuperBoss, 8 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override bool AutoDispel{ get{ return false; } }
		public override int TreasureMapLevel{ get{ return 5; } }

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
        }

        public void SpawnAllies(Mobile target)
        {
            Map map = this.Map;

            if (map == null)
                return;

            int allies = 0;

            foreach (Mobile m in this.GetMobilesInRange(10))
            {
                if (m is CorporealBrume || m is FetidEssence || m is Effervescence)
                    ++allies;
            }

            if (allies < 8)
            {
                PlaySound(0x3D);

                int newAllies = Utility.RandomMinMax(3, 4);

                for (int i = 0; i < newAllies; ++i)
                {
                    BaseCreature ally;

                    switch (Utility.Random(3))
                    {
                        default:
                        case 0: ally = new CorporealBrume(); break;
                        case 1: ally = new FetidEssence(); break;
                        case 2: ally = new Effervescence(); break;
                    }

                    ally.Team = this.Team;

                    bool validLocation = false;
                    Point3D loc = this.Location;

                    for (int j = 0; !validLocation && j < 10; ++j)
                    {
                        int x = X + Utility.Random(3) - 1;
                        int y = Y + Utility.Random(3) - 1;
                        int z = map.GetAverageZ(x, y);

                        if (validLocation = map.CanFit(x, y, this.Z, 16, false, false))
                            loc = new Point3D(x, y, Z);
                        else if (validLocation = map.CanFit(x, y, z, 16, false, false))
                            loc = new Point3D(x, y, z);
                    }

                    ally.MoveToWorld(loc, map);
                    ally.Combatant = target;
                }
            }
        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            base.OnDamage(amount, from, willKill);
            if (RandomDouble() > .05)
                SpawnAllies(Combatant);
        }

        public ShimmeringEffusion( Serial serial ) : base( serial )
		{
		}

		public override int GetIdleSound()
		{
			return 0x1BF;
		}

		public override int GetAttackSound()
		{
			return 0x1C0;
		}

		public override int GetHurtSound()
		{
			return 0x1C1;
		}

		public override int GetDeathSound()
		{
			return 0x1C2;
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