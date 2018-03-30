using System;
using System.Collections.Generic;
using Server.Items;
using Server.Spells;
//using System.Collections;
using Server.ContextMenus;
using Server.Spells.Spellweaving;

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

				Fame = 3500;
				Karma = -3500;

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
				SetHits( (int)(100*(scalar+0.7)), (int)(140*(scalar+0.7)) );
            Container pack = Backpack;
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
			
			SetDamage( (int)(4*(scalar-met*0.8)), (int)(10*(scalar-met*0.8)) );

			Fame = 10;
			Karma = 10;
						
			
		}

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

        public override bool OnDragDrop(Mobile from, Item item)
        {
            if (CheckFeed(from, item))
                return true;

            if (PackAnimal.CheckAccess(this, from))
            {
                AddToBackpack(item);
                return true;
            }

            return base.OnDragDrop(from, item);
        }

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

        private DateTime m_NextBomb = DateTime.UtcNow;
        private int m_Thrown = 0;
        private double m_Speed = 6;
        private int m_Range = 10;
        public override void OnActionCombat()
        {
            Mobile combatant = Combatant;
            Mobile m;
            if (combatant == null || combatant.Deleted || combatant.Map != Map || !InRange(combatant, m_Range) || !CanBeHarmful(combatant) || !InLOS(combatant))
                return;

            if (DateTime.UtcNow >= m_NextBomb)
            {
                ThrowBomb(combatant);
                m_Thrown++;
                m_Range = 10;
                m_Speed = 6;
                if ((Controlled || Summoned) && (ControlMaster != null))
                {
                    m = ControlMaster;
                    m_Speed = (int)((m.Skills[SkillName.Fletching].Value * 3 + m.Skills[SkillName.Carpentry].Value + m.Skills[SkillName.Blacksmith].Value) / 58.0);
                    m_Range = 4 + (int)(m.Skills[SkillName.Fletching].Value / 10.0);
                }

                if (0.75 >= Utility.RandomDouble() && (m_Thrown % 2) == 1) // 75% chance to quickly throw another bomb
                    m_NextBomb = DateTime.UtcNow + TimeSpan.FromSeconds((2.1 - m_Speed/5.0)*2);
                else
                    m_NextBomb = DateTime.UtcNow + TimeSpan.FromSeconds(6.0 + (6.0 * Utility.RandomDouble()) - m_Speed); // 6-12 seconds avg 9 (0-2 avg .666 with +60 hammer)

                if (m_Thrown > 32768)
                    m_Thrown -= 32768;
            }
            base.OnActionCombat();
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
            private int ExplosionRange = 2; // How long is the blast radius?
            private Mobile m_Mobile, m_From;
            private Point3D m_loc;
            private Map map;
            

            public InternalTimer(Mobile m, Mobile from) : base(TimeSpan.FromSeconds(0.0))
            {
                m_Mobile = m;
                m_From = from;
                m_loc = m_Mobile.Location;
                map = m_Mobile.Map;
                double alchemyBonus = 0;
                double sdi = 0;
                int dmgInc = 0;
                if ((((BaseCreature)from).Controlled || ((BaseCreature)from).Summoned) && ((BaseCreature)from).ControlMaster != null)
                {
                    Mobile master = (((BaseCreature)from).ControlMaster);
                    alchemyBonus = AosAttributes.GetValue(master, AosAttribute.EnhancePotions);
                    sdi = AosAttributes.GetValue(master, AosAttribute.SpellDamage);
                    dmgInc = AosAttributes.GetValue(master, AosAttribute.WeaponDamage);
                    //if (alchemyBonus > 50)
                    //    alchemyBonus = 50;
                    sdi+= ArcaneEmpowermentSpell.GetSpellBonus(master, false);

                    TransformContext context = TransformationSpellHelper.GetContext(master);

                    if (context != null && context.Spell is ReaperFormSpell)
                        sdi += ((ReaperFormSpell)context.Spell).SpellDamageBonus;

                    sdi += (int)(master.Skills.Inscribe.Fixed + (1000 * (int)(master.Skills.Inscribe.Fixed / 100))) / 100;
                    alchemyBonus += master.Skills.Alchemy.Fixed / 330 * 10;
                    sdi += m.Int / 10;

                    exploDamage = (int)(((master.Skills[SkillName.Alchemy].Value / 2.5)) * (1 + (alchemyBonus + sdi) / 100));
                    minDamage = (int)master.Skills[SkillName.Carpentry].Value;
                    minDamage += (int)master.Skills[SkillName.ArmsLore].Value;
                    minDamage /= 6;
                    minDamage *= (100 + dmgInc);
                    minDamage /= 100;
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
                int ExpRange = ExplosionRange;
                if (((BaseCreature)m_From).ControlMaster!=null)
                {
                    ExpRange += (int)(((BaseCreature)m_From).ControlMaster.Skills[SkillName.Tinkering].Value + ((BaseCreature)m_From).ControlMaster.Skills[SkillName.Alchemy].Value )/ 50;
                    if (Utility.RandomDouble() > AosAttributes.GetValue(((BaseCreature)m_From).ControlMaster, AosAttribute.EnhancePotions))
                        ExpRange += 1;
                    if (Utility.RandomDouble() > AosAttributes.GetValue(((BaseCreature)m_From).ControlMaster, AosAttribute.SpellDamage))
                        ExpRange += 1;
                }
                m_Mobile.PlaySound(0x11D);

                if ((Map)map != null)
                {
                    IPooledEnumerable eable = (IPooledEnumerable)map.GetMobilesInRange(m_loc, ExpRange);

                    foreach (object o in eable)
                    {
                        if ( (o is Mobile) && (o != m_From) && (m_Mobile == null || (SpellHelper.ValidIndirectTarget(m_From, (Mobile)o) && m_From.CanBeHarmful((Mobile)o, false))) &&
                            ((Mobile)o).InLOS(m_Mobile))
                        {
                            if (o is PlayerMobile)
                                AOS.Damage((Mobile)o, m_From, Utility.RandomMinMax(0, exploDamage/4), 0, 100, 0, 0, 0);
                            else if (o is BaseCreature && ((BaseCreature)m_From).Controlled)
                                if (((BaseCreature)m_From).ControlMaster != ((BaseCreature)o).ControlMaster)
                                    AOS.Damage((Mobile)o, m_From, Utility.RandomMinMax(0, exploDamage), 0, 100, 0, 0, 0);
                            else if (o is BaseCreature)
                                AOS.Damage((Mobile)o, m_From, Utility.RandomMinMax(0, exploDamage), 0, 100, 0, 0, 0);
                            m_From.DoHarmful((Mobile)o);
                        }
                    }
                }
                if (m_Mobile != null)
                    if (m_Mobile is PlayerMobile)
                        AOS.Damage(m_Mobile, m_From, Utility.RandomMinMax(1, minDamage), 100, 0, 0, 0, 0);
                    else
                        AOS.Damage(m_Mobile, m_From, Utility.RandomMinMax(minDamage, minDamage*3), 100, 0, 0, 0, 0);
                    m_From.DoHarmful(m_Mobile);
            }
        }
        /*
        public override void OnDamage( int amount, Mobile from, bool willKill )
		{
            base.OnDamage( amount, from, willKill );
		}
        */
        public override void CheckReflect(Mobile caster, ref bool reflect)
        {
        }
        public virtual void AlterMeleeDamageFrom(Mobile from, ref int damage)
        {
        }
        public virtual void AlterMeleeDamageTo(Mobile to, ref int damage)
        {
        }

        public override bool BardImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public GolemScorpion( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); //version = 1
            writer.Write(DateTime.UtcNow);
            writer.Write((int)m_Thrown);
            writer.Write((double)m_Speed);
            writer.Write((int) m_Range);
        }

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            if (version == 1 )
            { 
                m_NextBomb = reader.ReadDateTime();
                m_Thrown = reader.ReadInt();
                m_Speed = reader.ReadDouble();
                m_Range = reader.ReadInt();
            }
		}
	}
}