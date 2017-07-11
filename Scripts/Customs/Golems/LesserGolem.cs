using System;
using System.Collections.Generic;
using Server.Items;
using Server.Spells;
using System.Collections;
using Server.Gumps;

namespace Server.Mobiles
{
	public class LesserGolem : Golem
	{
        public override bool DeleteCorpseOnDeath { get { return true; } }
        public override bool IsHouseSummonable { get { return true; } }

        public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

        public override double DispelDifficulty { get { return 125.0; } }
        public override double DispelFocus { get { return 90.0; } }

        public override bool IsBondable{ get{ return false; } }

		[Constructable]
		public LesserGolem() : this( 0, 1.8, 0 )
		{
		}

		[Constructable]
		public LesserGolem( int summoned, double scalar, double met )
		{
			if ( summoned == 0 )
				{
                    Name = "a lesser golem";
					Body = 334;			
					SetStr( (int)(10*scalar), (int)(20*scalar) );
					SetDex( (int)(20*scalar), (int)(30*scalar) );
					SetInt( (int)(1*scalar), (int)(3*scalar) );

					SetHits( (int)(10*scalar), (int)(20*scalar) );

					SetDamage( (int)(3*scalar), (int)(4*scalar) );

					SetDamageType( ResistanceType.Physical, 100 );

					SetResistance( ResistanceType.Physical, (int)(35*scalar), (int)(55*scalar) );
					SetResistance( ResistanceType.Fire, (int)(100*scalar) );
					SetResistance( ResistanceType.Cold, (int)(10*scalar), (int)(30*scalar) );
					SetResistance( ResistanceType.Poison, (int)(10*scalar), (int)(25*scalar) );
					SetResistance( ResistanceType.Energy, (int)(30*scalar), (int)(40*scalar) );

					SetSkill( SkillName.MagicResist, (130.1*scalar), (150.1*scalar) );
					SetSkill( SkillName.Tactics, (40.1*scalar), (60.0*scalar) );
					SetSkill( SkillName.Wrestling, (40.1*scalar), (60.0*scalar) );
					Fame = 3500;
					Karma = -3500;

					PackItem( new IronIngot( Utility.RandomMinMax( 13, 21 ) ) );

					if ( 0.1 > Utility.RandomDouble() )
						PackItem( new PowerCrystal() );

					//if ( 0.15 > Utility.RandomDouble() )
					//	PackItem( new OverseerAssembly() );

					if ( 0.2 > Utility.RandomDouble() )
						PackItem( new ArcaneGem() );

					if ( 0.25 > Utility.RandomDouble() )
						PackItem( new Gears() );
					return;
				}
            ControlSlots = 1;
            Body = 334;
		    
			SetStr( (int)(30*scalar), (int)(40*scalar) );
			SetDex( (int)(30*scalar), (int)(40*scalar) );
			SetInt( (int)(1*scalar), (int)(3*scalar) );

			SetHits( (int)(5*(scalar)), (int)(10*(scalar)) );

			if ( met < 0.2 )
				{
				Name = "an iron lesser golem";
				SetDamageType( ResistanceType.Physical, 100 );
				SetResistance( ResistanceType.Physical, (int)(20*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Fire, (int)(20*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Cold, (int)(10*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Poison, (int)(10*scalar), (int)(25*scalar) );
				SetResistance( ResistanceType.Energy, (int)(20*scalar), (int)(30*scalar) );
				ControlSlots = 1;
				Hue = 0;
				}
			else if ( met < 0.3 )
				{
				Name = "a dull copper lesser golem";
				SetDamageType( ResistanceType.Physical, 100 );
				SetResistance( ResistanceType.Physical, (int)(26*scalar), (int)(36*scalar) );
				SetResistance( ResistanceType.Fire, (int)(20*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Cold, (int)(10*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Poison, (int)(10*scalar), (int)(25*scalar) );
				SetResistance( ResistanceType.Energy, (int)(20*scalar), (int)(30*scalar) );
				ControlSlots = 1;
				Hue = 2419;
				}
			else if ( met < 0.4 )
				{
				Name = "a shadow iron lesser golem";
				SetDamageType( ResistanceType.Physical, 80 );
				SetDamageType( ResistanceType.Cold, 20 );
				SetResistance( ResistanceType.Physical, (int)(22*scalar), (int)(32*scalar) );
				SetResistance( ResistanceType.Fire, (int)(21*scalar), (int)(31*scalar) );
				SetResistance( ResistanceType.Cold, (int)(10*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Poison, (int)(10*scalar), (int)(25*scalar) );
				SetResistance( ResistanceType.Energy, (int)(25*scalar), (int)(35*scalar) );
				ControlSlots = 1;
				Hue = 2406;
				}
			else if ( met < 0.5 )
				{
				Name = "a copper lesser golem";
				SetDamageType( ResistanceType.Physical, 70 );
				SetDamageType( ResistanceType.Poison, 10 );
				SetDamageType( ResistanceType.Energy, 20 );
				SetResistance( ResistanceType.Physical, (int)(21*scalar), (int)(31*scalar) );
				SetResistance( ResistanceType.Fire, (int)(21*scalar), (int)(31*scalar) );
				SetResistance( ResistanceType.Cold, (int)(10*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Poison, (int)(15*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Energy, (int)(22*scalar), (int)(32*scalar) );
				ControlSlots = 1;
				Hue = 2413;
				}
			else if ( met < 0.6 )
				{
				Name = "a bronze lesser golem";
				SetDamageType( ResistanceType.Physical, 60 );
				SetDamageType( ResistanceType.Fire, 40 );
				SetResistance( ResistanceType.Physical, (int)(23*scalar), (int)(33*scalar) );
				SetResistance( ResistanceType.Fire, (int)(20*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Cold, (int)(15*scalar), (int)(35*scalar) );
				SetResistance( ResistanceType.Poison, (int)(11*scalar), (int)(26*scalar) );
				SetResistance( ResistanceType.Energy, (int)(21*scalar), (int)(31*scalar) );
				ControlSlots = 1;
				Hue = 2418;
				}
			else if ( met < 0.7 )
				{
				Name = "a gold lesser golem";
				SetDamageType( ResistanceType.Physical, 100 );
				SetResistance( ResistanceType.Physical, (int)(21*scalar), (int)(31*scalar) );
				SetResistance( ResistanceType.Fire, (int)(21*scalar), (int)(31*scalar) );
				SetResistance( ResistanceType.Cold, (int)(12*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Poison, (int)(10*scalar), (int)(25*scalar) );
				SetResistance( ResistanceType.Energy, (int)(22*scalar), (int)(32*scalar) );
				ControlSlots = 1;
				Hue = 2213;
				}
			else if ( met < 0.8 )
				{
				Name = "an agapite lesser golem";
				SetDamageType( ResistanceType.Physical, 50 );
				SetDamageType( ResistanceType.Cold, 30 );
				SetDamageType( ResistanceType.Energy, 20 );
				SetResistance( ResistanceType.Physical, (int)(27*scalar), (int)(32*scalar) );
				SetResistance( ResistanceType.Fire, (int)(29*scalar), (int)(34*scalar) );
				SetResistance( ResistanceType.Cold, (int)(27*scalar), (int)(32*scalar) );
				SetResistance( ResistanceType.Poison, (int)(22*scalar), (int)(27*scalar) );
				SetResistance( ResistanceType.Energy, (int)(27*scalar), (int)(32*scalar) );
				ControlSlots = 1;
				Hue = 2425;
				}
			else if ( met < 0.9 )
				{
				Name = "a verite lesser golem";
				SetDamageType( ResistanceType.Physical, 40 );
				SetDamageType( ResistanceType.Poison, 40 );
				SetDamageType( ResistanceType.Energy, 20 );
				SetResistance( ResistanceType.Physical, (int)(28*scalar), (int)(33*scalar) );
				SetResistance( ResistanceType.Fire, (int)(29*scalar), (int)(34*scalar) );
				SetResistance( ResistanceType.Cold, (int)(27*scalar), (int)(32*scalar) );
				SetResistance( ResistanceType.Poison, (int)(23*scalar), (int)(28*scalar) );
				SetResistance( ResistanceType.Energy, (int)(26*scalar), (int)(31*scalar) );
				ControlSlots = 1;
				Hue = 2207;
				}
			else
				{
				Name = "a valorite lesser golem";
				SetDamageType( ResistanceType.Physical, 40 );
				SetDamageType( ResistanceType.Fire, 10 );
				SetDamageType( ResistanceType.Cold, 20 );
				SetDamageType( ResistanceType.Poison, 10 );
				SetDamageType( ResistanceType.Energy, 20 );
				SetResistance( ResistanceType.Physical, (int)(29*scalar), (int)(34*scalar) );//45-64 at gm
				SetResistance( ResistanceType.Fire, (int)(25*scalar), (int)(30*scalar) );//28-57 at gm
				SetResistance( ResistanceType.Cold, (int)(29*scalar), (int)(34*scalar) );//24-62 at gm
				SetResistance( ResistanceType.Poison, (int)(18*scalar), (int)(23*scalar) );//24-43 at gm
				SetResistance( ResistanceType.Energy, (int)(28*scalar), (int)(33*scalar) );//43-62 at gm
				ControlSlots = 1;
				Hue = 2219;
				}
			
			
			SetSkill( SkillName.MagicResist, (80.1*(scalar-met/2)), (100.0*(scalar-met/2)) );
			SetSkill( SkillName.Tactics, (30.1*(scalar-met/2)), (40.0*(scalar-met/2)) );
			SetSkill( SkillName.Wrestling, (30.1* (scalar - met / 2)), (40.0*(scalar-met/2)) );
			SetSkill( SkillName.Anatomy, (40.1* (scalar - met / 2)), (50.1* (scalar - met / 2)) );
			
			SetDamage( (int)(4*(scalar-met*0.8)), (int)(6*(scalar-met*0.8)) );

			Fame = 10;
			Karma = 10;
						
			
		}

		public override bool DeleteOnRelease{ get{ return true; } }

		public override int GetAngerSound()
		{
			return 541;
		}

        public override void CheckReflect(Mobile caster, ref bool reflect)
        {
            if (Hue == 2413 || Hue == 2219) // Copper || Valorite
                reflect = true; // Every spell is reflected back to the caster
            else
                reflect = false;
        }
        public override void AlterMeleeDamageFrom(Mobile from, ref int damage)
        {
            if (Hue == 2413) //Copper
                from.Damage(damage / 2, this);
            base.AlterMeleeDamageFrom(from, ref damage);
        }
        /*
        public override void AlterDamageScalarFrom(Mobile caster, ref double scalar)
        {
            if (Hue == 2219 || Hue == 2406)
                scalar = 0.0; // Immune to all the things?
        }
        */
        public override void AlterSpellDamageFrom(Mobile from, ref int damage)
        {
            if (Hue == 2406 || Hue == 2219) //Shadow || Valorite
                damage = 0;// Immune to magic
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
            Mobile master = (this.ControlMaster);
            if (master == null)
                master = this;
            int total = (int)master.Skills[SkillName.Poisoning].Value;
            int level;

            if (total >= 100)
                level = 4;
            else if (total >= 85)
                level = 3;
            else if (total > 65)
                level = 2;
            else if (total > 50)
                level = 1;
            else
                level = 0;

            if (Utility.RandomDouble() < master.Skills[SkillName.Poisoning].Value / 140.0)
            {
                if (Utility.RandomDouble() < master.Skills[SkillName.Poisoning].Value / 140.0)
                    ++level;
                defender.ApplyPoison(this, Poison.GetPoison(level));
            }

        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            base.AggressiveAction(aggressor, criminal);
        }

        private DateTime m_NextBomb;
        private int m_Thrown;
        private double m_Speed;
        public override void OnActionCombat()
        {
            base.OnActionCombat();
        }

        public override void OnThink()
        {
            base.OnThink();
            if (ControlMaster == null)
                return;
        }

        public override void OnDeath(Container c)
        {
            int alchemyBonus = 0;
            int exploDamage;
            int minDamage;
            int range = 4;
            Mobile m = ControlMaster;
            if (m == null)
                m = SummonMaster;
            if (Hue == 2419)//Dull Copper
            {
                if ((Controlled || Summoned) && (m != null) && (!(m is BaseCreature) || ((BaseCreature)m).ControlMaster != null))
                {
                    if (m is BaseCreature && ((BaseCreature)m).ControlMaster != null)
                        m = ((BaseCreature)m).ControlMaster;
                    range = (int)((m.Skills[SkillName.Tinkering].Value + m.Skills[SkillName.Alchemy].Value) / 20);
                    alchemyBonus = AosAttributes.GetValue(m, AosAttribute.EnhancePotions);
                    if (alchemyBonus > 50)
                        alchemyBonus = 50;
                    alchemyBonus += m.Skills.Alchemy.Fixed / 330 * 10;

                    exploDamage = (int)((1 + (m.Skills[SkillName.Alchemy].Value / 5.0)) * (1 + alchemyBonus));
                    minDamage = (int)m.Skills[SkillName.Carpentry].Value;
                    minDamage += (int)m.Skills[SkillName.ArmsLore].Value;
                    minDamage /= 12;
                }
                else
                {
                    minDamage = 20;
                    exploDamage = 20;
                }
            
                if (m != null)
                {
                    m.SendMessage(String.Format("dying with alive = {0}", Alive));
                    if (ControlMaster == null)
                        if (SummonMaster == null)
                            return;
                        else if (SummonMaster == m)
                        {
                                foreach (Mobile o in this.GetMobilesInRange(range))
                                        if ((o != m) && o != this && (SpellHelper.ValidIndirectTarget(this, (Mobile)o) && CanBeHarmful((Mobile)o, false)) && !(o is Golem) && o.Alive)
                                            AOS.Damage(o, this, (int)(Utility.RandomDouble() * (minDamage + exploDamage)), 80, 20, 0, 0, 0);
                        }
                        else
                        {
                            foreach (Mobile o in this.GetMobilesInRange(range))
                                if ((o != SummonMaster) && o != m && o != this && (SpellHelper.ValidIndirectTarget(this, (Mobile)o) && CanBeHarmful((Mobile)o, false)) && !(o is Golem) && o.Alive)
                                    AOS.Damage(o, this, (int)(Utility.RandomDouble() * (minDamage + exploDamage)), 80, 20, 0, 0, 0);
                        }
                    else
                    {
                        foreach (Mobile o in this.GetMobilesInRange(range))
                            if ((o != ControlMaster) && o != m && o != this && (SpellHelper.ValidIndirectTarget(this, (Mobile)o) && CanBeHarmful((Mobile)o, false)) && !(o is Golem) && o.Alive)
                                AOS.Damage(o, this, (int)(Utility.RandomDouble() * (minDamage + exploDamage)), 80, 20, 0, 0, 0);
                    }
                }
            }
            base.OnDeath(c);
        }

        public override bool BardImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public LesserGolem( Serial serial ) : base( serial )
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