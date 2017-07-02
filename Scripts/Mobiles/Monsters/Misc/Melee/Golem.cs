using System;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a golem corpse" )]
	public class Golem : BaseCreature
	{
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

		public override bool IsBondable{ get{ return true; } }

		[Constructable]
		public Golem() : this( 0, 1.0, 0 )
		{
		}

		[Constructable]
		public Golem( int summoned, double scalar, double met ) : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.4, 0.8 )
		{
			if ( summoned == 0 )
				{
					Name = "a golem";
					Body = 752;			
					SetStr( (int)(251*scalar), (int)(350*scalar) );
					SetDex( (int)(76*scalar), (int)(100*scalar) );
					SetInt( (int)(101*scalar), (int)(150*scalar) );

					SetHits( (int)(151*scalar), (int)(210*scalar) );

					SetDamage( (int)(13*scalar), (int)(24*scalar) );

					SetDamageType( ResistanceType.Physical, 100 );

					SetResistance( ResistanceType.Physical, (int)(35*scalar), (int)(55*scalar) );
					SetResistance( ResistanceType.Fire, (int)(100*scalar) );
					SetResistance( ResistanceType.Cold, (int)(10*scalar), (int)(30*scalar) );
					SetResistance( ResistanceType.Poison, (int)(10*scalar), (int)(25*scalar) );
					SetResistance( ResistanceType.Energy, (int)(30*scalar), (int)(40*scalar) );

					SetSkill( SkillName.MagicResist, (150.1*scalar), (190.0*scalar) );
					SetSkill( SkillName.Tactics, (60.1*scalar), (100.0*scalar) );
					SetSkill( SkillName.Wrestling, (60.1*scalar), (100.0*scalar) );
					Fame = 3500;
					Karma = -3500;

					PackItem( new IronIngot( Utility.RandomMinMax( 13, 21 ) ) );

					if ( 0.1 > Utility.RandomDouble() )
						PackItem( new PowerCrystal() );

					if ( 0.15 > Utility.RandomDouble() )
						PackItem( new ClockworkAssembly() );

					if ( 0.2 > Utility.RandomDouble() )
						PackItem( new ArcaneGem() );

					if ( 0.25 > Utility.RandomDouble() )
						PackItem( new Gears() );
					return;
				}
			
			Body = 752;
			
			SetStr( (int)(151*scalar), (int)(250*scalar) );
			SetDex( (int)(76*scalar), (int)(100*scalar) );
			SetInt( (int)(51*scalar), (int)(100*scalar) );

			if (met > 0.19 && met < 0.21)
				SetHits( (int)(151*(scalar+0.9)), (int)(210*(scalar+0.9)) );
			else if (met > 0.29 && met < 0.31)
				SetHits( (int)(151*(scalar+0.8)), (int)(210*(scalar+0.8)) );
			else
				SetHits( (int)(151*scalar), (int)(210*scalar) );

			if ( met < 0.2 )
				{
				Name = "an iron golem";
				SetDamageType( ResistanceType.Physical, 100 );
				SetResistance( ResistanceType.Physical, (int)(20*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Fire, (int)(20*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Cold, (int)(10*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Poison, (int)(10*scalar), (int)(25*scalar) );
				SetResistance( ResistanceType.Energy, (int)(20*scalar), (int)(30*scalar) );
				ControlSlots = 4;
				Hue = 0;
				}
			else if ( met < 0.3 )
				{
				Name = "a dull copper golem";
				SetDamageType( ResistanceType.Physical, 100 );
				SetResistance( ResistanceType.Physical, (int)(26*scalar), (int)(36*scalar) );
				SetResistance( ResistanceType.Fire, (int)(20*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Cold, (int)(10*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Poison, (int)(10*scalar), (int)(25*scalar) );
				SetResistance( ResistanceType.Energy, (int)(20*scalar), (int)(30*scalar) );
				ControlSlots = 4;
				Hue = 2419;
				}
			else if ( met < 0.4 )
				{
				Name = "a shadowiron golem";
				SetDamageType( ResistanceType.Physical, 80 );
				SetDamageType( ResistanceType.Cold, 20 );
				SetResistance( ResistanceType.Physical, (int)(22*scalar), (int)(32*scalar) );
				SetResistance( ResistanceType.Fire, (int)(21*scalar), (int)(31*scalar) );
				SetResistance( ResistanceType.Cold, (int)(10*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Poison, (int)(10*scalar), (int)(25*scalar) );
				SetResistance( ResistanceType.Energy, (int)(25*scalar), (int)(35*scalar) );
				ControlSlots = 4;
				Hue = 2406;
				}
			else if ( met < 0.5 )
				{
				Name = "a copper golem";
				SetDamageType( ResistanceType.Physical, 70 );
				SetDamageType( ResistanceType.Poison, 10 );
				SetDamageType( ResistanceType.Energy, 20 );
				SetResistance( ResistanceType.Physical, (int)(21*scalar), (int)(31*scalar) );
				SetResistance( ResistanceType.Fire, (int)(21*scalar), (int)(31*scalar) );
				SetResistance( ResistanceType.Cold, (int)(10*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Poison, (int)(15*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Energy, (int)(22*scalar), (int)(32*scalar) );
				ControlSlots = 3;
				Hue = 2413;
				}
			else if ( met < 0.6 )
				{
				Name = "a bronze golem";
				SetDamageType( ResistanceType.Physical, 60 );
				SetDamageType( ResistanceType.Fire, 40 );
				SetResistance( ResistanceType.Physical, (int)(23*scalar), (int)(33*scalar) );
				SetResistance( ResistanceType.Fire, (int)(20*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Cold, (int)(15*scalar), (int)(35*scalar) );
				SetResistance( ResistanceType.Poison, (int)(11*scalar), (int)(26*scalar) );
				SetResistance( ResistanceType.Energy, (int)(21*scalar), (int)(31*scalar) );
				ControlSlots = 4;
				Hue = 2418;
				}
			else if ( met < 0.7 )
				{
				Name = "a gold golem";
				SetDamageType( ResistanceType.Physical, 100 );
				SetResistance( ResistanceType.Physical, (int)(21*scalar), (int)(31*scalar) );
				SetResistance( ResistanceType.Fire, (int)(21*scalar), (int)(31*scalar) );
				SetResistance( ResistanceType.Cold, (int)(12*scalar), (int)(32*scalar) );
				SetResistance( ResistanceType.Poison, (int)(10*scalar), (int)(25*scalar) );
				SetResistance( ResistanceType.Energy, (int)(22*scalar), (int)(32*scalar) );
				ControlSlots = 4;
				Hue = 2213;
				}
			else if ( met < 0.8 )
				{
				Name = "an agapite golem";
				SetDamageType( ResistanceType.Physical, 50 );
				SetDamageType( ResistanceType.Cold, 30 );
				SetDamageType( ResistanceType.Energy, 20 );
				SetResistance( ResistanceType.Physical, (int)(22*scalar), (int)(32*scalar) );
				SetResistance( ResistanceType.Fire, (int)(23*scalar), (int)(33*scalar) );
				SetResistance( ResistanceType.Cold, (int)(12*scalar), (int)(32*scalar) );
				SetResistance( ResistanceType.Poison, (int)(12*scalar), (int)(27*scalar) );
				SetResistance( ResistanceType.Energy, (int)(22*scalar), (int)(32*scalar) );
				ControlSlots = 5;
				Hue = 2425;
				}
			else if ( met < 0.9 )
				{
				Name = "a verite golem";
				SetDamageType( ResistanceType.Physical, 40 );
				SetDamageType( ResistanceType.Poison, 40 );
				SetDamageType( ResistanceType.Energy, 20 );
				SetResistance( ResistanceType.Physical, (int)(23*scalar), (int)(33*scalar) );
				SetResistance( ResistanceType.Fire, (int)(23*scalar), (int)(34*scalar) );
				SetResistance( ResistanceType.Cold, (int)(12*scalar), (int)(32*scalar) );
				SetResistance( ResistanceType.Poison, (int)(13*scalar), (int)(28*scalar) );
				SetResistance( ResistanceType.Energy, (int)(21*scalar), (int)(31*scalar) );
				ControlSlots = 5;
				Hue = 2207;
				}
			else
				{
				Name = "a valorite golem";
				SetDamageType( ResistanceType.Physical, 40 );
				SetDamageType( ResistanceType.Fire, 10 );
				SetDamageType( ResistanceType.Cold, 20 );
				SetDamageType( ResistanceType.Poison, 10 );
				SetDamageType( ResistanceType.Energy, 20 );
				SetResistance( ResistanceType.Physical, (int)(24*scalar), (int)(34*scalar) );//45-64 at gm
				SetResistance( ResistanceType.Fire, (int)(20*scalar), (int)(30*scalar) );//28-57 at gm
				SetResistance( ResistanceType.Cold, (int)(13*scalar), (int)(34*scalar) );//24-62 at gm
				SetResistance( ResistanceType.Poison, (int)(13*scalar), (int)(23*scalar) );//24-43 at gm
				SetResistance( ResistanceType.Energy, (int)(23*scalar), (int)(33*scalar) );//43-62 at gm
				ControlSlots = 5;
				Hue = 2219;
				}
			
			
			SetSkill( SkillName.MagicResist, (100.1*(scalar-met/2)), (120.0*(scalar-met/2)) );
			SetSkill( SkillName.Tactics, (50.1*(scalar-met/2)), (80.0*(scalar-met/2)) );
			SetSkill( SkillName.Wrestling, (50.1*scalar), (60.0*(scalar-met/2)) );
			SetSkill( SkillName.Anatomy, (40.1*scalar), (50.1*scalar) );
			
			SetDamage( (int)(13*(scalar-met*0.8)), (int)(24*(scalar-met*0.8)) );

			Fame = 10;
			Karma = 10;
						
			
		}

		public override bool DeleteOnRelease{ get{ return true; } }

		public override int GetAngerSound()
		{
			return 541;
		}

		public override int GetIdleSound()
		{
			if ( !Controlled )
				return 542;

			return base.GetIdleSound();
		}

		public override int GetDeathSound()
		{
			if ( !Controlled )
				return 545;

			return base.GetDeathSound();
		}

		public override int GetAttackSound()
		{
			return 562;
		}

		public override int GetHurtSound()
		{
			if ( Controlled )
				return 320;

			return base.GetHurtSound();
		}

		public override bool AutoDispel{ get{ return !Controlled; } }

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.2 > Utility.RandomDouble() )
				defender.Combatant = null;
		}

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
            double pskill = 0;
            int level = 0;
            int lmc = AosAttributes.GetValue(from, AosAttribute.LowerManaCost);
            int cost = (amount * (100 - lmc)) / 300;
            if (Controlled || Summoned)
            {
                Mobile master = (this.ControlMaster);

                if (master == null)
                    master = this.SummonMaster;

                if (master != null && master.Player)
                {

                    if (master.Mana >= cost)
                    {
                        master.Mana -= cost;
                    }
                    else
                    {
                        master.Damage(cost - master.Mana);
                        master.Mana = 0;
                        
                    }
                    if (from is BaseCreature || from is PlayerMobile)
                    {
                        pskill = master.Skills[SkillName.Poisoning].Value;
                        if (pskill >= 100.0)
                            level = 4;
                        else if (pskill >= 85.0)
                            level = 3;
                        else if (pskill > 65.0)
                            level = 2;
                        else if (pskill > 50.0)
                            level = 1;
                        else
                            level = 0;

                        if (Utility.RandomDouble() < master.Skills[SkillName.Poisoning].Value / 140.0)
                        {
                            if (Utility.RandomDouble() < master.Skills[SkillName.Poisoning].Value / 140.0)
                                ++level;
                            from.ApplyPoison(master, Poison.GetPoison(level));
                        }
                    }

                }
            }

            base.OnDamage( cost, from, willKill );
		}

		public override bool BardImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public Golem( Serial serial ) : base( serial )
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