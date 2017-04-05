using System;
using Server.Items;
using Server.Spells;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a golem corpse" )]
	public class GolemScorpion : Golem
	{
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

		public override bool IsBondable{ get{ return true; } }

		[Constructable]
		public GolemScorpion() : this( 0, 1.8, 0 )
		{
		}

		[Constructable]
		public GolemScorpion( int summoned, double scalar, double met )
		{
			if ( summoned == 0 )
				{
					Name = "a golem scorpion";
					Body = 717;			
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

					SetSkill( SkillName.MagicResist, (130.1*scalar), (150.1*scalar) );
					SetSkill( SkillName.Tactics, (40.1*scalar), (60.0*scalar) );
					SetSkill( SkillName.Wrestling, (40.1*scalar), (60.0*scalar) );
					Fame = 3500;
					Karma = -3500;

					PackItem( new IronIngot( Utility.RandomMinMax( 13, 21 ) ) );

					if ( 0.1 > Utility.RandomDouble() )
						PackItem( new PowerCrystal() );

					if ( 0.15 > Utility.RandomDouble() )
						PackItem( new ScorpionAssembly() );

					if ( 0.2 > Utility.RandomDouble() )
						PackItem( new ArcaneGem() );

					if ( 0.25 > Utility.RandomDouble() )
						PackItem( new Gears() );
					return;
				}
			
			Body = 717;
		    
			SetStr( (int)(100*scalar), (int)(125*scalar) );
			SetDex( (int)(76*scalar), (int)(125*scalar) );
			SetInt( (int)(51*scalar), (int)(100*scalar) );

			if (met < 0.3)
				SetHits( (int)(100*(scalar+0.9)), (int)(140*(scalar+0.9)) );
			else if (met < 0.4)
				SetHits( (int)(100*(scalar+0.8)), (int)(140*(scalar+0.8)) );
			else
				SetHits( (int)(100*scalar), (int)(140*scalar) );

			if ( met < 0.2 )
				{
				Name = "an iron golem scorpion";
				SetDamageType( ResistanceType.Physical, 100 );
				SetResistance( ResistanceType.Physical, (int)(20*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Fire, (int)(20*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Cold, (int)(10*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Poison, (int)(10*scalar), (int)(25*scalar) );
				SetResistance( ResistanceType.Energy, (int)(20*scalar), (int)(30*scalar) );
				ControlSlots = 2;
				Hue = 0;
				}
			else if ( met < 0.3 )
				{
				Name = "a dull copper golem scorpion";
				SetDamageType( ResistanceType.Physical, 100 );
				SetResistance( ResistanceType.Physical, (int)(26*scalar), (int)(36*scalar) );
				SetResistance( ResistanceType.Fire, (int)(20*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Cold, (int)(10*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Poison, (int)(10*scalar), (int)(25*scalar) );
				SetResistance( ResistanceType.Energy, (int)(20*scalar), (int)(30*scalar) );
				ControlSlots = 2;
				Hue = 2419;
				}
			else if ( met < 0.4 )
				{
				Name = "a shadowiron golem scorpion";
				SetDamageType( ResistanceType.Physical, 80 );
				SetDamageType( ResistanceType.Cold, 20 );
				SetResistance( ResistanceType.Physical, (int)(22*scalar), (int)(32*scalar) );
				SetResistance( ResistanceType.Fire, (int)(21*scalar), (int)(31*scalar) );
				SetResistance( ResistanceType.Cold, (int)(10*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Poison, (int)(10*scalar), (int)(25*scalar) );
				SetResistance( ResistanceType.Energy, (int)(25*scalar), (int)(35*scalar) );
				ControlSlots = 2;
				Hue = 2406;
				}
			else if ( met < 0.5 )
				{
				Name = "a copper golem scorpion";
				SetDamageType( ResistanceType.Physical, 70 );
				SetDamageType( ResistanceType.Poison, 10 );
				SetDamageType( ResistanceType.Energy, 20 );
				SetResistance( ResistanceType.Physical, (int)(21*scalar), (int)(31*scalar) );
				SetResistance( ResistanceType.Fire, (int)(21*scalar), (int)(31*scalar) );
				SetResistance( ResistanceType.Cold, (int)(10*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Poison, (int)(15*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Energy, (int)(22*scalar), (int)(32*scalar) );
				ControlSlots = 2;
				Hue = 2413;
				}
			else if ( met < 0.6 )
				{
				Name = "a bronze golem scorpion";
				SetDamageType( ResistanceType.Physical, 60 );
				SetDamageType( ResistanceType.Fire, 40 );
				SetResistance( ResistanceType.Physical, (int)(23*scalar), (int)(33*scalar) );
				SetResistance( ResistanceType.Fire, (int)(20*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Cold, (int)(15*scalar), (int)(35*scalar) );
				SetResistance( ResistanceType.Poison, (int)(11*scalar), (int)(26*scalar) );
				SetResistance( ResistanceType.Energy, (int)(21*scalar), (int)(31*scalar) );
				ControlSlots = 2;
				Hue = 2418;
				}
			else if ( met < 0.7 )
				{
				Name = "a gold golem scorpion";
				SetDamageType( ResistanceType.Physical, 100 );
				SetResistance( ResistanceType.Physical, (int)(21*scalar), (int)(31*scalar) );
				SetResistance( ResistanceType.Fire, (int)(21*scalar), (int)(31*scalar) );
				SetResistance( ResistanceType.Cold, (int)(12*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Poison, (int)(10*scalar), (int)(25*scalar) );
				SetResistance( ResistanceType.Energy, (int)(22*scalar), (int)(32*scalar) );
				ControlSlots = 2;
				Hue = 2213;
				}
			else if ( met < 0.8 )
				{
				Name = "an agapite golem scorpion";
				SetDamageType( ResistanceType.Physical, 50 );
				SetDamageType( ResistanceType.Cold, 30 );
				SetDamageType( ResistanceType.Energy, 20 );
				SetResistance( ResistanceType.Physical, (int)(22*scalar), (int)(32*scalar) );
				SetResistance( ResistanceType.Fire, (int)(23*scalar), (int)(33*scalar) );
				SetResistance( ResistanceType.Cold, (int)(12*scalar), (int)(32*scalar) );
				SetResistance( ResistanceType.Poison, (int)(12*scalar), (int)(27*scalar) );
				SetResistance( ResistanceType.Energy, (int)(22*scalar), (int)(32*scalar) );
				ControlSlots = 3;
				Hue = 2425;
				}
			else if ( met < 0.9 )
				{
				Name = "a verite golem scorpion";
				SetDamageType( ResistanceType.Physical, 40 );
				SetDamageType( ResistanceType.Poison, 40 );
				SetDamageType( ResistanceType.Energy, 20 );
				SetResistance( ResistanceType.Physical, (int)(23*scalar), (int)(33*scalar) );
				SetResistance( ResistanceType.Fire, (int)(23*scalar), (int)(34*scalar) );
				SetResistance( ResistanceType.Cold, (int)(12*scalar), (int)(32*scalar) );
				SetResistance( ResistanceType.Poison, (int)(13*scalar), (int)(28*scalar) );
				SetResistance( ResistanceType.Energy, (int)(21*scalar), (int)(31*scalar) );
				ControlSlots = 3;
				Hue = 2207;
				}
			else
				{
				Name = "a valorite golem scorpion";
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
				ControlSlots = 3;
				Hue = 2219;
				}
			
			
			SetSkill( SkillName.MagicResist, (80.1*(scalar-met/2)), (100.0*(scalar-met/2)) );
			SetSkill( SkillName.Tactics, (50.1*(scalar-met/2)), (80.0*(scalar-met/2)) );
			SetSkill( SkillName.Wrestling, (30.1*scalar), (50.0*(scalar-met/2)) );
			SetSkill( SkillName.Anatomy, (40.1*scalar), (50.1*scalar) );
			
			SetDamage( (int)(4*(scalar-met*0.8)), (int)(10*(scalar-met*0.8)) );

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
            Mobile master = (this.ControlMaster);
            if (master == null)
                master = this;
            int total = (int)(master.Skills[SkillName.Poisoning].Value + master.Skills[SkillName.Tinkering].Value) / 2;
            int level;
            if (total >= 100.0)
                level = 3;
            else if (total > 85.0)
                level = 2;
            else if (total > 65.0)
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

            Item item = aggressor.FindItemOnLayer(Layer.Helm);

            if (item is OrcishKinMask)
            {
                AOS.Damage(aggressor, 50, 0, 100, 0, 0, 0);
                item.Delete();
                aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                aggressor.PlaySound(0x307);
            }
        }

        private DateTime m_NextBomb;
        private int m_Thrown;
        private double m_Speed;
        public override void OnActionCombat()
        {
            Mobile combatant = Combatant;

            if (combatant == null || combatant.Deleted || combatant.Map != Map || !InRange(combatant, 14) || !CanBeHarmful(combatant) || !InLOS(combatant))
                return;

                if (DateTime.UtcNow >= m_NextBomb)
                {
                    ThrowBomb(combatant);
                    m_Thrown++;
                    m_Speed = 6;
                if ((((BaseCreature)this).Controlled || ((BaseCreature)this).Summoned) && ((BaseCreature)this).ControlMaster != null)
                {
                    Mobile master = (((BaseCreature)this).ControlMaster);
                    m_Speed = master.Skills[SkillName.Fletching].Value / 10.0;
                }

                    if (0.75 >= Utility.RandomDouble() && (m_Thrown % 2) == 1) // 75% chance to quickly throw another bomb
                        m_NextBomb = DateTime.UtcNow + TimeSpan.FromSeconds(2.1 - m_Speed/5.0);
                    else
                        m_NextBomb = DateTime.UtcNow + TimeSpan.FromSeconds(3.0 + (3.0 * Utility.RandomDouble()) - m_Speed/2); // 3-6 seconds (0-1)
                }
        }

        public void ThrowBomb(Mobile m)
        {
            DoHarmful(m);

            this.MovingParticles(m, 0x1C19, 1, 0, false, true, 0, 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0);

            new InternalTimer(m, this).Start();
        }

        private class InternalTimer : Timer
        {
            int minDamage;
            int exploDamage;
            private int ExplosionRange = 5; // How long is the blast radius?
            private Mobile m_Mobile, m_From;
            private Point3D m_loc;
            private Map map;

            public InternalTimer(Mobile m, Mobile from) : base(TimeSpan.FromSeconds(0.0))
            {
                m_Mobile = m;
                m_From = from;
                m_loc = m_Mobile.Location;
                map = m_Mobile.Map;

                if ((((BaseCreature)from).Controlled || ((BaseCreature)from).Summoned) && ((BaseCreature)from).ControlMaster != null)
                {
                    Mobile master = (((BaseCreature)from).ControlMaster);
                    exploDamage = (int)(master.Skills[SkillName.Alchemy].Value/3.0);
                    minDamage = (int)master.Skills[SkillName.Carpentry].Value;
                    minDamage += (int)master.Skills[SkillName.ArmsLore].Value;
                    minDamage /= 12;
                }
                else
                {
                    minDamage = 20;
                    exploDamage = 20;
                }
                Priority = TimerPriority.TwoFiftyMS;
            }

            protected override void OnTick()
            {
                m_Mobile.PlaySound(0x11D);
                if ((Map)map != null)
                {
                    IPooledEnumerable eable = (IPooledEnumerable)map.GetMobilesInRange(m_loc, 5);

                    foreach (object o in eable)
                    {
                        if ( (o is Mobile) && (o != m_From) && (m_Mobile == null || (SpellHelper.ValidIndirectTarget(m_From, (Mobile)o) && m_From.CanBeHarmful((Mobile)o, false))))
                        {
                            if (o is PlayerMobile)
                                AOS.Damage((Mobile)o, m_From, Utility.RandomMinMax(0, exploDamage/4), 0, 100, 0, 0, 0);
                            else if (o is BaseCreature && ((BaseCreature)m_From).Controlled)
                                if (((BaseCreature)m_From).ControlMaster != ((BaseCreature)o).ControlMaster)
                                    AOS.Damage((Mobile)o, m_From, Utility.RandomMinMax(0, exploDamage), 0, 100, 0, 0, 0);
                            else if (o is BaseCreature)
                                AOS.Damage((Mobile)o, m_From, Utility.RandomMinMax(0, exploDamage), 0, 100, 0, 0, 0);
                        }
                    }
                }
                AOS.Damage(m_Mobile, m_From, Utility.RandomMinMax(minDamage, minDamage*3), 100, 0, 0, 0, 0);
            }
        }

        public override void OnDamage( int amount, Mobile from, bool willKill )
		{
            double total = 0;
            int level = 0;
			if ( Controlled || Summoned )
			{
				Mobile master = ( this.ControlMaster );

				if ( master == null )
					master = this.SummonMaster;

				if ( master != null && master.Player && master.Map == this.Map && master.InRange( Location, 20 ) )
				{
					if ( master.Mana >= (int)(amount/3) )
					{
						master.Mana -= (int)(amount/3);
					}
					else
					{
						amount -= master.Mana;
						master.Mana = 0;
						master.Damage( (int)(amount/3) );
					}
                    if (from is BaseCreature)
                    {
                        total = (master.Skills[SkillName.Poisoning].Value + master.Skills[SkillName.Tinkering].Value) / 2;
                        if (total >= 100.0)
                            level = 3;
                        else if (total > 85.0)
                            level = 2;
                        else if (total > 65.0)
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
            
            base.OnDamage( (int)(amount/3), from, willKill );
		}

		public override bool BardImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public GolemScorpion( Serial serial ) : base( serial )
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