using System;
using System.Collections.Generic;
using Server.Items;
using Server.Spells;
using System.Collections;
using Server.Gumps;
using Server.ContextMenus;
using Server.Spells.Seventh;

namespace Server.Mobiles
{
	[CorpseName( "a golem corpse" )]
	public class GolemOverseer : Golem
	{
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }

		public override bool IsBondable{ get{ return true; } }

		[Constructable]
		public GolemOverseer() : this( 0, 1.8, 0 )
		{
		}

		[Constructable]
		public GolemOverseer( int summoned, double scalar, double met )
		{
			if ( summoned == 0 )
				{
					Name = "a golem overseer";
					Body = 756;			
					SetStr( (int)(51*scalar), (int)(110*scalar) );
					SetDex( (int)(76*scalar), (int)(100*scalar) );
					SetInt( (int)(101*scalar), (int)(150*scalar) );

					SetHits( (int)(71*scalar), (int)(110*scalar) );

					SetDamage( (int)(6*scalar), (int)(12*scalar) );

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
						PackItem( new OverseerAssembly() );

					if ( 0.2 > Utility.RandomDouble() )
						PackItem( new ArcaneGem() );

					if ( 0.25 > Utility.RandomDouble() )
						PackItem( new Gears() );
					return;
				}
			
			Body = 756;
		    
			SetStr( (int)(50*scalar), (int)(100*scalar) );
			SetDex( (int)(76*scalar), (int)(125*scalar) );
			SetInt( (int)(100*scalar), (int)(150*scalar) );

			if (met < 0.3)
				SetHits( (int)(50*(scalar+0.9)), (int)(70*(scalar+0.9)) );
			else if (met < 0.4)
				SetHits( (int)(50*(scalar+0.8)), (int)(70*(scalar+0.8)) );
			else
				SetHits( (int)(50*(scalar+0.7)), (int)(70*(scalar+0.7)) );

            Container pack = Backpack;

            if (pack != null)
                pack.Delete();

            if ( met < 0.2 )
			{
				Name = "an iron golem overseer";
				SetDamageType( ResistanceType.Physical, 100 );
				SetResistance( ResistanceType.Physical, (int)(20*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Fire, (int)(20*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Cold, (int)(10*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Poison, (int)(10*scalar), (int)(25*scalar) );
				SetResistance( ResistanceType.Energy, (int)(20*scalar), (int)(30*scalar) );
				ControlSlots = 2;
				Hue = 0;
                

                pack = new StrongBackpack();
                pack.Movable = false;

                AddItem(pack);
            }
			else if ( met < 0.3 )
			{
				Name = "a dull copper golem overseer";
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
				Name = "a shadow iron golem overseer";
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
				Name = "a copper golem overseer";
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
				Name = "a bronze golem overseer";
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
				Name = "a gold golem overseer";
				SetDamageType( ResistanceType.Physical, 100 );
				SetResistance( ResistanceType.Physical, (int)(21*scalar), (int)(31*scalar) );
				SetResistance( ResistanceType.Fire, (int)(21*scalar), (int)(31*scalar) );
				SetResistance( ResistanceType.Cold, (int)(12*scalar), (int)(30*scalar) );
				SetResistance( ResistanceType.Poison, (int)(10*scalar), (int)(25*scalar) );
				SetResistance( ResistanceType.Energy, (int)(22*scalar), (int)(32*scalar) );
				ControlSlots = 2;
				Hue = 2213;

                if (pack != null)
                    pack.Delete();

                pack = new StrongBackpack();
                pack.Movable = false;

                AddItem(pack);
            }
			else if ( met < 0.8 )
			{
				Name = "an agapite golem overseer";
				SetDamageType( ResistanceType.Physical, 50 );
				SetDamageType( ResistanceType.Cold, 30 );
				SetDamageType( ResistanceType.Energy, 20 );
				SetResistance( ResistanceType.Physical, (int)(27*scalar), (int)(32*scalar) );
				SetResistance( ResistanceType.Fire, (int)(29*scalar), (int)(34*scalar) );
				SetResistance( ResistanceType.Cold, (int)(27*scalar), (int)(32*scalar) );
				SetResistance( ResistanceType.Poison, (int)(22*scalar), (int)(27*scalar) );
				SetResistance( ResistanceType.Energy, (int)(27*scalar), (int)(32*scalar) );
				ControlSlots = 3;
				Hue = 2425;

                if (pack != null)
                    pack.Delete();

                pack = new StrongBackpack();
                pack.Movable = false;

                AddItem(pack);
            }
			else if ( met < 0.9 )
			{
				Name = "a verite golem overseer";
				SetDamageType( ResistanceType.Physical, 40 );
				SetDamageType( ResistanceType.Poison, 40 );
				SetDamageType( ResistanceType.Energy, 20 );
				SetResistance( ResistanceType.Physical, (int)(28*scalar), (int)(33*scalar) );
				SetResistance( ResistanceType.Fire, (int)(29*scalar), (int)(34*scalar) );
				SetResistance( ResistanceType.Cold, (int)(27*scalar), (int)(32*scalar) );
				SetResistance( ResistanceType.Poison, (int)(23*scalar), (int)(28*scalar) );
				SetResistance( ResistanceType.Energy, (int)(26*scalar), (int)(31*scalar) );
				ControlSlots = 3;
				Hue = 2207;
			}
			else
			{
				Name = "a valorite golem overseer";
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
				ControlSlots = 3;
				Hue = 2219;
			}

            SetSkill(SkillName.Magery, (40.0 * (scalar - met / 2)), (50.0 * (scalar - met / 2)));
            SetSkill(SkillName.Stealth, (40.0 * (scalar - met / 2)), (50.0 * (scalar - met / 2)));
            SetSkill( SkillName.MagicResist, (80.1*(scalar-met/2)), (100.0*(scalar-met/2)) );
			SetSkill( SkillName.Tactics, (10.1*(scalar-met/2)), (20.0*(scalar-met/2)) );
			SetSkill( SkillName.Wrestling, (30.1* (scalar - met / 2)), (40.0*(scalar-met/2)) );
			SetSkill( SkillName.Anatomy, (40.1* (scalar - met / 2)), (50.1* (scalar - met / 2)) );
            SetSkill( SkillName.Meditation, (80.1 * (scalar - met / 2)), (100.0 * (scalar - met / 2)));


            SetDamage( (int)(2*(scalar-met*0.8)), (int)(5*(scalar-met*0.8)) );

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
            int m_Range;
            double delay;
            if ( Hue == 2418 || Hue == 2219 ) // Bronze, Valorite
            {
                Mobile combatant = Combatant;
                if ((Controlled || Summoned) && ControlMaster != null)
                    m_Range = 4 + (int)(ControlMaster.Skills[SkillName.Fletching].Value / 10.0);
                else
                    m_Range = 10;

                if (combatant == null || combatant.Deleted || combatant.Map != Map || !InRange(combatant, m_Range) || !CanBeHarmful(combatant) || !InLOS(combatant))
                    return;

                if (DateTime.UtcNow >= m_NextBomb)
                {
                    ThrowGas(combatant);
                    m_Thrown++;
                    m_Range = 10;
                    m_Speed = 6;
                    if ((((BaseCreature)this).Controlled || ((BaseCreature)this).Summoned) && ((BaseCreature)this).ControlMaster != null)
                    {
                        Mobile master = (((BaseCreature)this).ControlMaster);
                        m_Speed = (int)((master.Skills[SkillName.Fletching].Value * 3 + master.Skills[SkillName.Carpentry].Value + master.Skills[SkillName.Blacksmith].Value) / 58.0);
                    }
                
                    if (0.75 >= Utility.RandomDouble() && (m_Thrown % 2) == 1) // 75% chance to quickly throw another bomb
                        m_NextBomb = DateTime.UtcNow + TimeSpan.FromSeconds(2.1 - m_Speed / 5.0);
                    else
                    {
                        delay = 3.0 + 3.0 * Utility.RandomDouble() - m_Speed / 2;
                        if (delay < 0.25)
                            delay = 0.25;
                        m_NextBomb = DateTime.UtcNow + TimeSpan.FromSeconds(delay);
                    }
                }
            }
        }

        public void ThrowGas(Mobile m)
        {
            DoHarmful(m);

            this.MovingParticles(m, 0x1C19, 1, 0, false, true, 0, 0, 9502, 6014, 0x11D, EffectLayer.Waist, 0);
            
            new InternalTimer(m, this).Start();
        }

        private DateTime m_NextFlare;
        private DateTime m_NextRes;
        private int m_SpeedBonus;

        public override void OnThink()
        {
            Item rune;
            
            if (Target is GateTravelSpell.InternalTarget)
            {
                rune = Backpack.FindItemByType(typeof( RecallRune ), false);
                if (rune == null)
                    rune = Backpack.FindItemByType(typeof( Runebook ), false);
                if ( rune != null )
                    Target.Invoke( this, rune);
            }
            
            base.OnThink();
            
            if (ControlMaster != null)
            {
                m_SpeedBonus = (int)((ControlMaster.Skills.Carpentry.Value + ControlMaster.Skills.Blacksmith.Value)*4/280);

                if (DateTime.UtcNow < m_NextFlare)
                    return;

                if (ControlMaster == null)
                    m_NextFlare = DateTime.UtcNow + TimeSpan.FromSeconds(0.5 + 100.0*Utility.RandomDouble());
                else
                {
                    m_SpeedBonus = (int)((ControlMaster.Skills.Carpentry.Value + ControlMaster.Skills.Blacksmith.Value) * 4 / 280);
                    m_NextFlare = DateTime.UtcNow + TimeSpan.FromSeconds(1 + (4.0-m_SpeedBonus)*Utility.RandomDouble());//1-4.5 (1-1.85) (1 with hammer)
                }

                this.FixedEffect(0x37C4, 1, 12, 1109, 6);
                this.PlaySound(0x1D3);

                Timer.DelayCall(TimeSpan.FromSeconds(0.25), new TimerCallback(Flare));
            }
        }

        private void Flare()
        {
            Mobile caster = this.ControlMaster;
            if (caster == null)
                caster = this.SummonMaster;

            if (caster == null)
                return;
            int range = (int)((caster.Skills.Fletching.Value + caster.Skills.Inscribe.Value + caster.Skills.ArmsLore.Value + caster.Skills.Tinkering.Value) / 100.0);
            int overseerCount = 0;
            int amount = 0;
            if (!InRange(ControlMaster, 4))
                return;
            ArrayList deadList = new ArrayList();
            ArrayList list = new ArrayList();
            Spell spell;

            if (( Hue == 0 || Hue == 2425 || Hue == 2213 ) && Mana > 40 && Backpack.GetAmount(typeof(RecallRune), false) + Backpack.GetAmount(typeof(Runebook), false) > 0)
            {
                spell = new GateTravelSpell( this, null);
                if ( spell != null )
                    spell.Cast();
                return;
            }
            if ( Hue == 2419 )
            {
                if ( ControlOrder == OrderType.Guard )
                    BaseCreature.Summon(new LesserGolem(1, 1, .2), false, this, Location, 0x212, TimeSpan.FromSeconds(120));
                return;
            }

            foreach (Mobile m_patient in this.GetMobilesInRange(range))
            {
                if (m_patient.Alive || ((m_patient is BaseCreature) && ((BaseCreature)m_patient).IsDeadPet))
                    if (m_patient is GolemOverseer)
                        overseerCount += 1;
                    else
                        { }
                else
                    deadList.Add(m_patient);
            }
            if (overseerCount > 8)
                overseerCount = 8;
            range += overseerCount;
            
            foreach (Mobile m_patient in this.GetMobilesInRange(range))
            {
                if (m_patient.Alive && (!(m_patient is BaseCreature) || !((BaseCreature)m_patient).IsDeadPet))
                    list.Add(m_patient);
            }

            amount = (int)((caster.Skills.Inscribe.Value*caster.Skills.Tinkering.Value / 1000.0)*Utility.RandomDouble());
            
            for (int i = 0; i < list.Count; ++i)
            {
                Mobile m = (Mobile)list[i];
                bool friendly = true;

                for (int j = 0; friendly && j < caster.Aggressors.Count; ++j)
                    friendly = (caster.Aggressors[j].Attacker != m);

                for (int j = 0; friendly && j < caster.Aggressed.Count; ++j)
                    friendly = (caster.Aggressed[j].Defender != m);
                
                if (friendly)
                {
                    if ( Hue == 2413 || Hue == 2425) //Copper || Agapite
                    {
                        if (m is ControlMaster)
                            m.Mana += (amount * 3);
                        else
                            m.Mana += (amount);
                        m.Hits += (amount/4);
                        m.Stam += (amount/2);
                        if ( Hue == 2425 )
                            if (m.MeleeDamageAbsorb <= amount * 2)
                                m.MeleeDamageAbsorb = amount * 2;
                        
                    }
                    else if ( Hue == 2406 || Hue == 2219 ) //Shadow || Valorite
                    {
                        if (m.MagicDamageAbsorb <= amount*2)
                            m.MagicDamageAbsorb = amount*2;
                        if (Hue == 2406)
                        {
                            ShadowBonus(m, amount);
                            if ( ControlMaster.Hidden )
                            {
                                Hidden = true;
                                AllowedStealthSteps = ControlMaster.AllowedStealthSteps;
                            }
                        }
                    }
                }
                if ((m != caster) && m != this && (SpellHelper.ValidIndirectTarget(this, (Mobile)m) && CanBeHarmful((Mobile)m, false)))
                {
                    if (ControlOrder == OrderType.Guard)
                    {
                        
                        if ( Hue == 2413 || Hue == 2425 )//Copper or Agapite
                        {
                            DoHarmful(m);
                            m.Mana -= (amount / 2);
                            m.Hits -= (amount / 4);
                            m.Stam -= (amount / 2);
                            Mana += (amount / 2);
                            if (InRange(caster, range))
                                caster.Mana += (amount / 2);
                        }
                        else if ( Hue == 2207 ) //Verite
                        {
                            DoHarmful(m);
                            EatArmor(m);
                        }
                        else if ( Hue == 2418 || Hue == 2219) //Bronze || Valorite
                        {
                            DoHarmful(m);
                            ThrowGas(m);
                        }
                    }
                }

            }
            if (Mana >= 50 && caster.Skills.Inscribe.Value >= 80 && overseerCount >= 3 && (Hue == 2425 || Hue == 0 || Hue == 2213))//Agapite or Iron or Gold
            {
                for (int i = 0; i < deadList.Count; ++i)
                {
                    if (DateTime.UtcNow < m_NextRes)
                        return;

                    if (ControlMaster == null)
                        m_NextRes = DateTime.UtcNow + TimeSpan.FromSeconds(0.5 + 100.0 * Utility.RandomDouble());
                    else
                    {
                        m_SpeedBonus = (int)((ControlMaster.Skills.Carpentry.Value + ControlMaster.Skills.Blacksmith.Value) * 4 / 280);
                        m_NextRes = DateTime.UtcNow + TimeSpan.FromSeconds(0.5 + (8.0 - m_SpeedBonus) * Utility.RandomDouble());//0.5-8.0 (0.5-5.35) (0.5-4.5 with hammer)
                    }
                    
                    int patientNumber = -1;
                    if (Mana >= 50)
                    {
                        Mobile m_Patient = (Mobile)deadList[i];
                        BaseCreature petPatient = m_Patient as BaseCreature;
                        if ( this == null || !this.Alive || ((BaseCreature)this).IsDeadPet )
			            {
				            patientNumber = -1;
			            }
			            else if ( !m_Patient.Alive || (petPatient != null && petPatient.IsDeadPet) )
			            {
					        if ( m_Patient.Map == null || !m_Patient.Map.CanFit( m_Patient.Location, 16, false, false ) )
					        {
						        patientNumber = 502391; // Thou can not be resurrected there!
                            }
					        else if ( m_Patient.Region != null && m_Patient.Region.IsPartOf( "Khaldun" ) )
					        {
						        patientNumber = 502391; // Thou can not be resurrected there!
                            }
					        else
					        {
                                Mana -= 50;
						        patientNumber = -1;

						        m_Patient.PlaySound( 0x214 );
						        m_Patient.FixedEffect( 0x376A, 10, 16 );

						        if ( petPatient != null && petPatient.IsDeadPet )
						        {
							        Mobile master = petPatient.ControlMaster;
							        if( master != null && ControlMaster == master )
							        {
								        petPatient.ResurrectPet();

								        for ( int j = 0; j < petPatient.Skills.Length; ++j )
									        petPatient.Skills[j].Base -= 0.1;
							        }
							        else if ( master != null && master.InRange( petPatient, 3 ) )
							        {
								        master.CloseGump( typeof( PetResurrectGump ) );
								        master.SendGump( new PetResurrectGump( ControlMaster, petPatient ) );
                                    }
							        else
							        {
								        bool found = false;

								        List<Mobile> friends = petPatient.Friends;

								        for ( int j = 0; friends != null && j < friends.Count; ++j )
								        {
									        Mobile friend = friends[j];

									        if ( friend.InRange( petPatient, 3 ) )
									        {
										        friend.CloseGump( typeof( PetResurrectGump ) );
										        friend.SendGump( new PetResurrectGump( ControlMaster, petPatient ) );

										        found = true;
										        break;
									        }
								        }
							        }
						        }
						        else
						        {
							        m_Patient.CloseGump( typeof( ResurrectGump ) );
							        m_Patient.SendGump( new ResurrectGump( m_Patient, ControlMaster ) );
						        }
					        }
			            }
                    }
                }
                
            }
        }
        private static Dictionary<Mobile, Timer> m_Shadowed = new Dictionary<Mobile, Timer>();

        private void ShadowBonus(Mobile m, int bonus)
        {
            if (m.AllowedStealthSteps > 0 && m.AllowedStealthSteps < 24)
                m.AllowedStealthSteps += 10;
            if (m == null || m_Shadowed.ContainsKey(m) || Deleted || !Alive )
                return;
            TimeSpan delay = TimeSpan.FromSeconds(60);
            m.AddSkillMod(new TimedSkillMod(m.Skills.Hiding.SkillName, true, 20, delay));
            m.AddSkillMod(new TimedSkillMod(m.Skills.Stealth.SkillName, true, 20, delay));
            int count = (int)Math.Round(delay.TotalSeconds / 1.25);
            Timer timer = new AnimateTimer(m, count);
            m_Shadowed.Add(m, timer);
            timer.Start();
        }
        public static void ShadowBonusRemove(Mobile target)
        {
            if (target != null && m_Shadowed.ContainsKey(target))
            {
                Timer timer = m_Shadowed[target];

                if (timer != null || timer.Running)
                    timer.Stop();

                m_Shadowed.Remove(target);
            }
        }

        private class AnimateTimer : Timer
        {
            private Mobile m_Owner;
            private int m_Count;

            public AnimateTimer(Mobile owner, int count) : base(TimeSpan.Zero, TimeSpan.FromSeconds(1.25))
            {
                m_Owner = owner;
                m_Count = count;
            }

            protected override void OnTick()
            {
                if (m_Owner.Deleted || !m_Owner.Alive || m_Count-- < 0)
                {
                    ShadowBonusRemove(m_Owner);
                }
            }
        }

        private void EatArmor(Mobile defender)
        {
			/* Rune Corruption
				* Start cliloc: 1070846 "The creature magically corrupts your armor!"
				* Effect: All resistances -70 (lowest 0) for 5 seconds
				* End ASCII: "The corruption of your armor has worn off"
				*/
            
			ExpireTimer timer = (ExpireTimer)m_Table[defender];

			if (timer != null )
			{
				timer.DoExpire();
			}
			else
				defender.SendLocalizedMessage( 1070846 ); // The creature magically corrupts your armor!

			List<ResistanceMod> mods = new List<ResistanceMod>();
            double mod;
            if (ControlMaster != null)
                mod = ControlMaster.Skills.Inscribe.Value/100;
            else
                mod = .5;

			if (defender.PhysicalResistance > 0 )
				mods.Add( new ResistanceMod(ResistanceType.Physical, (int)(-(defender.PhysicalResistance * mod / 4) ) ) );

			if (defender.FireResistance > 0 )
				mods.Add( new ResistanceMod(ResistanceType.Fire, (int)(-(defender.FireResistance * mod / 4) ) ));

			if (defender.ColdResistance > 0 )
				mods.Add( new ResistanceMod(ResistanceType.Cold, (int)(-(defender.ColdResistance * mod / 4) ) ));

			if (defender.PoisonResistance > 0 )
				mods.Add( new ResistanceMod(ResistanceType.Poison, (int)(-(defender.PoisonResistance * mod / 4) ) ));

			if (defender.EnergyResistance > 0 )
				mods.Add( new ResistanceMod(ResistanceType.Energy, (int)(-(defender.EnergyResistance * mod / 4) ) ));

			for (int i = 0; i<mods.Count; ++i )
				defender.AddResistanceMod(mods[i] );

			defender.FixedEffect( 0x37B9, 10, 5 );

			timer = new ExpireTimer(defender, mods, TimeSpan.FromSeconds( 2.0 ) );
			timer.Start();
			m_Table[defender] = timer;
        }

        private static Hashtable m_Table = new Hashtable();

        private class ExpireTimer : Timer
        {
            private Mobile m_Mobile;
            private List<ResistanceMod> m_Mods;

            public ExpireTimer(Mobile m, List<ResistanceMod> mods, TimeSpan delay) : base(delay)
            {
                m_Mobile = m;
                m_Mods = mods;
                Priority = TimerPriority.TwoFiftyMS;
            }

            public void DoExpire()
            {
                for (int i = 0; i < m_Mods.Count; ++i)
                    m_Mobile.RemoveResistanceMod(m_Mods[i]);

                Stop();
                m_Table.Remove(m_Mobile);
            }

            protected override void OnTick()
            {
                m_Mobile.SendMessage("The corruption of your armor has worn off");
                DoExpire();
            }
        }

        private class InternalTimer : Timer
        {
            int minDamage;
            int exploDamage;
            private int ExplosionRange; // How long is the blast radius?
            private Mobile m_Mobile, m_From;
            private Point3D m_loc;
            private Map map;
            private int level;
            

            public InternalTimer(Mobile m, Mobile from) : base(TimeSpan.FromSeconds(0.0))
            {
                m_Mobile = m;
                m_From = from;
                m_loc = m_Mobile.Location;
                map = m_Mobile.Map;
                ExplosionRange = 3;
                int total;
                if ((m_From is BaseCreature) && ((BaseCreature)m_From).ControlMaster != null)
                    ExplosionRange += (int)(((BaseCreature)m_From).ControlMaster.Skills[SkillName.Tinkering].Value + 
                    ((BaseCreature)m_From).ControlMaster.Skills[SkillName.Alchemy].Value +
                    ((BaseCreature)m_From).ControlMaster.Skills[SkillName.Poisoning].Value) / 50;
                int alchemyBonus = 0;
                
                if ((((BaseCreature)from).Controlled || ((BaseCreature)from).Summoned) && ((BaseCreature)from).ControlMaster != null)
                {
                    Mobile master = ((BaseCreature)from).ControlMaster;
                    alchemyBonus = AosAttributes.GetValue(m, AosAttribute.EnhancePotions);
                    if (alchemyBonus > 50)
                        alchemyBonus = 50;
                    alchemyBonus += m.Skills.Alchemy.Fixed / 330 * 10;

                    exploDamage = (int)(((master.Skills[SkillName.Alchemy].Value/10.0))*(1+alchemyBonus));
                    minDamage = (int)master.Skills[SkillName.Carpentry].Value;
                    minDamage += (int)master.Skills[SkillName.ArmsLore].Value;
                    minDamage /= 32;

                    total = (int)master.Skills[SkillName.Poisoning].Value;

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
                }
                else
                {
                    minDamage = 20;
                    exploDamage = 20;
                    level = 1;
                }
                Priority = TimerPriority.TwoFiftyMS;
                
            }

            protected override void OnTick()
            {
                
                if (m_From == null)
                    return;
                /*
                if (m_Mobile == null)
                    return;
                m_loc = m_Mobile.Location;
                if (ExplosionRange == null)
                    return;
                if (minDamage == null)
                    return;
                */
                m_Mobile.PlaySound(0x11D);
                if ((Map)map != null)
                {
                    IPooledEnumerable eable = (IPooledEnumerable)map.GetMobilesInRange(m_loc, ExplosionRange);

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
                            if (Utility.RandomDouble() < ((BaseCreature)m_From).ControlMaster.Skills[SkillName.Poisoning].Value / 120.0)
                            {
                                if (Utility.RandomDouble() < ((BaseCreature)m_From).ControlMaster.Skills[SkillName.Poisoning].Value / 120.0)
                                    ++level;
                                ((Mobile)o).ApplyPoison(m_From, Poison.GetPoison(level));
                            }
                        }
                    }
                }
                
                if (m_Mobile != null)
                    AOS.Damage(m_Mobile, m_From, Utility.RandomMinMax(minDamage, minDamage*3), 100, 0, 0, 0, 0);
            }
        }
        
        
        public override void OnDamage( int amount, Mobile from, bool willKill )
		{
            base.OnDamage( amount, from, willKill );
		}

        public override void OnDeath(Container c)
        {
            int alchemyBonus = 0;
            int exploDamage;
            int minDamage;
            int range = (int)((ControlMaster.Skills[SkillName.Tinkering].Value + ControlMaster.Skills[SkillName.Alchemy].Value) / 20);
            if (Hue == 2419)//Dull Copper
            {
                if ((Controlled || Summoned) && (ControlMaster != null))
                {
                    Mobile master = ControlMaster;
                    alchemyBonus = AosAttributes.GetValue(master, AosAttribute.EnhancePotions);
                    if (alchemyBonus > 50)
                        alchemyBonus = 50;
                    alchemyBonus += master.Skills.Alchemy.Fixed / 330 * 10;

                    exploDamage = (int)((1 + (master.Skills[SkillName.Alchemy].Value / 5.0)) * (1 + alchemyBonus));
                    minDamage = (int)master.Skills[SkillName.Carpentry].Value;
                    minDamage += (int)master.Skills[SkillName.ArmsLore].Value;
                    minDamage /= 12;
                }
                else
                {
                    minDamage = 20;
                    exploDamage = 20;
                }
            
                if (ControlMaster != null)
                    foreach (Mobile o in this.GetMobilesInRange(range))
                    {
                        if ((o != ControlMaster) && o != this && (SpellHelper.ValidIndirectTarget(this, (Mobile)o) && CanBeHarmful((Mobile)o, false)) && !(o is Golem) && o.Alive)
                            AOS.Damage(o, this, Utility.RandomMinMax(1, (minDamage + exploDamage)), 80, 20, 0, 0, 0);
                    }
            }
            base.OnDeath(c);
        }

        public override bool BardImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

        #region Pack Animal Methods
        public override bool OnBeforeDeath()
        {
            if (!base.OnBeforeDeath())
                return false;

            PackAnimal.CombineBackpacks(this);

            return true;
        }
        public override DeathMoveResult GetInventoryMoveResultFor(Item item)
        {
            return DeathMoveResult.MoveToCorpse;
        }

        public override bool IsSnoop(Mobile from)
        {
            if (PackAnimal.CheckAccess(this, from))
                return false;

            return base.IsSnoop(from);
        }
        /*
        public override bool OnDragDrop(Mobile from, Item item)
        {
           
            if ( Hue == 0 || Hue == 2213 || Hue == 2425 )
                if (PackAnimal.CheckAccess(this, from))
                {
                    AddToBackpack(item);
                    return true;
                }
            
            return base.OnDragDrop(from, item);
        }
        */
        public override bool CheckNonlocalDrop(Mobile from, Item item, Item target)
        {
            return PackAnimal.CheckAccess(this, from);
        }

        public override bool CheckNonlocalLift(Mobile from, Item item)
        {
            return PackAnimal.CheckAccess(this, from);
        }

        public override void OnDoubleClick(Mobile from)
        {
            PackAnimal.TryPackOpen(this, from);
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);

            PackAnimal.GetContextMenuEntries(this, from, list);
        }
#endregion

        public GolemOverseer( Serial serial ) : base( serial )
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