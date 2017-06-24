using System;
using System.Collections.Generic;
using Server.Items;
using Server.Spells;
using System.Collections;
using Server.Gumps;

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

					//if ( 0.15 > Utility.RandomDouble() )
					//	PackItem( new OverseerAssembly() );

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
				SetResistance( ResistanceType.Physical, (int)(27*scalar), (int)(32*scalar) );
				SetResistance( ResistanceType.Fire, (int)(29*scalar), (int)(34*scalar) );
				SetResistance( ResistanceType.Cold, (int)(27*scalar), (int)(32*scalar) );
				SetResistance( ResistanceType.Poison, (int)(22*scalar), (int)(27*scalar) );
				SetResistance( ResistanceType.Energy, (int)(27*scalar), (int)(32*scalar) );
				ControlSlots = 3;
				Hue = 2425;
				}
			else if ( met < 0.9 )
				{
				Name = "a verite golem scorpion";
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
				Name = "a valorite golem scorpion";
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
			
			
			SetSkill( SkillName.MagicResist, (80.1*(scalar-met/2)), (100.0*(scalar-met/2)) );
			SetSkill( SkillName.Tactics, (50.1*(scalar-met/2)), (80.0*(scalar-met/2)) );
			SetSkill( SkillName.Wrestling, (40.1* (scalar - met / 2)), (50.0*(scalar-met/2)) );
			SetSkill( SkillName.Anatomy, (40.1* (scalar - met / 2)), (50.1* (scalar - met / 2)) );
			
			SetDamage( (int)(2*(scalar-met*0.8)), (int)(5*(scalar-met*0.8)) );

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
            /*
            Item item = aggressor.FindItemOnLayer(Layer.Helm);

            if (item is OrcishKinMask)
            {
                AOS.Damage(aggressor, 50, 0, 100, 0, 0, 0);
                item.Delete();
                aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                aggressor.PlaySound(0x307);
            }
            */
        }

        private DateTime m_NextBomb;
        private int m_Thrown;
        private double m_Speed;
        public override void OnActionCombat()
        {
            /*
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
            */
        }
        
        public void ThrowBomb(Mobile m)
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
            base.OnThink();
            if (ControlMaster == null)
                return;


            m_SpeedBonus = (int)((ControlMaster.Skills.Carpentry.Value + ControlMaster.Skills.Blacksmith.Value)*4/280);

            if (DateTime.UtcNow < m_NextFlare)
                return;

            if (ControlMaster == null)
                m_NextFlare = DateTime.UtcNow + TimeSpan.FromSeconds(0.5 + 100.0*Utility.RandomDouble());
            else
            {
                m_SpeedBonus = (int)((ControlMaster.Skills.Carpentry.Value + ControlMaster.Skills.Blacksmith.Value) * 4 / 280);
                m_NextFlare = DateTime.UtcNow + TimeSpan.FromSeconds(0.5 + (4.0-m_SpeedBonus)*Utility.RandomDouble());//0.5-4.0 (0.5-1.35) (.05 with hammer)
            }

            this.FixedEffect(0x37C4, 1, 12, 1109, 6);
            this.PlaySound(0x1D3);

            Timer.DelayCall(TimeSpan.FromSeconds(0.25), new TimerCallback(Flare));
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
            
            ArrayList deadList = new ArrayList();
            ArrayList list = new ArrayList();

            foreach (Mobile m_patient in this.GetMobilesInRange(range))
            {
                if (m_patient.Alive && !((BaseCreature)m_patient).IsDeadPet)
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
                if (m_patient.Alive && !((BaseCreature)m_patient).IsDeadPet)
                    list.Add(m_patient);
            }

            amount = (int)(caster.Skills.Inscribe.Value / 20.0);
            if (Mana >= 40)
            {
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
                        m.FixedEffect(0x37C4, 1, 12, 1109, 3); // At player
                        m.Mana += 5 + amount;
                        m.Hits += amount;
                        m.Stam += amount;
                    }
                    else
                    {
                        DoHarmful(m);
                        m.FixedEffect(0x37C4, 1, 12, 1109, 3); // At player
                        m.Mana -= amount;
                        m.Hits -= amount;
                        m.Stam -= amount;
                        Mana += amount;
                        caster.Mana += amount;
                        if (overseerCount >= 5)
                        {
                            ThrowBomb(m);
                            Mana -= 10;
                        }
                    }
                
                }
                Mana -= 50;
            }
            if (Mana >= 50 && caster.Skills.Inscribe.Value >= 80 && overseerCount >= 3)
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
                        m_NextRes = DateTime.UtcNow + TimeSpan.FromSeconds(0.5 + (8.0 - m_SpeedBonus) * Utility.RandomDouble());//0.5-4.0 (0.5-1.35) (0.5 with hammer)
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
        
        private class InternalTimer : Timer
        {
            int minDamage;
            int exploDamage;
            private int ExplosionRange = 3; // How long is the blast radius?
            private Mobile m_Mobile, m_From;
            private Point3D m_loc;
            private Map map;
            

            public InternalTimer(Mobile m, Mobile from) : base(TimeSpan.FromSeconds(0.0))
            {
                m_Mobile = m;
                m_From = from;
                m_loc = m_Mobile.Location;
                map = m_Mobile.Map;
                int alchemyBonus = 0;

                if ((((BaseCreature)from).Controlled || ((BaseCreature)from).Summoned) && ((BaseCreature)from).ControlMaster != null)
                {
                    Mobile master = (((BaseCreature)from).ControlMaster);
                    alchemyBonus = AosAttributes.GetValue(m, AosAttribute.EnhancePotions);
                    if (alchemyBonus > 50)
                        alchemyBonus = 50;
                    alchemyBonus += m.Skills.Alchemy.Fixed / 330 * 10;

                    exploDamage = (int)((20 + (master.Skills[SkillName.Alchemy].Value/30.0))*(1+alchemyBonus));
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
                if (m_From == null)
                    return;
                int ExpRange = (int)ExplosionRange;
                if (((BaseCreature)m_From).ControlMaster!=null)
                    ExpRange += (int)(((BaseCreature)m_From).ControlMaster.Skills[SkillName.Tinkering].Value + ((BaseCreature)m_From).ControlMaster.Skills[SkillName.Alchemy].Value )/ 50;
                m_Mobile.PlaySound(0x11D);
                if ((Map)map != null)
                {
                    IPooledEnumerable eable = (IPooledEnumerable)map.GetMobilesInRange(m_loc, ExpRange);

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
                if (m_Mobile != null)
                    AOS.Damage(m_Mobile, m_From, Utility.RandomMinMax(minDamage, minDamage*3), 100, 0, 0, 0, 0);
            }
        }
        
        
        public override void OnDamage( int amount, Mobile from, bool willKill )
		{
            double total = 0;
            int level = 0;
            int lmc = AosAttributes.GetValue(from, AosAttribute.LowerManaCost);
            int cost = (amount*(100-lmc))/100;
            if ( Controlled || Summoned )
			{
				Mobile master = ( this.ControlMaster );

				if ( master == null )
					master = this.SummonMaster;

				if ( master != null && master.Player )
				{

                    if ( master.Mana >= (int)(cost/3) )
					{
						master.Mana -= (int)(cost / 3);
					}
					else
					{
                        cost -= master.Mana;
						master.Mana = 0;
						master.Damage( (int)(cost / 3) );
					}
                    if (from is BaseCreature)
                    {
                        total = master.Skills[SkillName.Poisoning].Value;
                        if (total >= 100.0)
                            level = 4;
                        else if (total >= 85.0)
                            level = 3;
                        else if (total > 65.0)
                            level = 2;
                        else if (total > 50.0)
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
            
            base.OnDamage( (int)(lmc/3), from, willKill );
		}
        

		public override bool BardImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

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