using System;
using Server;
using Server.Targeting;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Spells.Necromancy;

namespace Server.Items
{
	public class CraftedTrap : BaseTrap
	{

		private Mobile m_TrapOwner;
		private int m_UsesRemaining, m_TrapPower, m_ManaCost, m_DamageRange, m_TriggerRange, m_ParalyzeTime;
        private double m_DamageScalar;
        private Skill m_BonusSkill;
        private Poison m_Poison;

        private DateTime lastused = DateTime.Now;
		private TimeSpan delay = TimeSpan.FromSeconds( 7 );

        [CommandProperty(AccessLevel.GameMaster)]
        public double DamageScalar
        {
            get
            {
                return m_DamageScalar;
            }
            set
            {
                m_DamageScalar = value;
            }
        }

        [CommandProperty( AccessLevel.GameMaster )]
		public Mobile TrapOwner
		{
			get
			{
				return m_TrapOwner;
			}
			set
			{
				m_TrapOwner = value;
			}
		}

        [CommandProperty(AccessLevel.GameMaster)]
        public int ManaCost
        {
            get
            {
                return m_ManaCost;
            }
            set
            {
                m_ManaCost = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Poison Poison
        {
            get { return m_Poison; }
            set { m_Poison = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Skill BonusSkill
        {
            get
            {
                return m_BonusSkill;
            }
            set
            {
                m_BonusSkill = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int ParalyzeTime
        {
            get
            {
                return m_ParalyzeTime;
            }
            set
            {
                m_ParalyzeTime = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int TriggerRange
        {
            get
            {
                return m_TriggerRange;
            }
            set
            {
                m_TriggerRange = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int DamageRange
        {
            get
            {
                return m_DamageRange;
            }
            set
            {
                m_DamageRange = value;
            }
        }

        [CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get
			{
				return m_UsesRemaining;
			}
			set
			{
				m_UsesRemaining = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int TrapPower
		{
			get
			{
				return m_TrapPower;
			}
			set
			{
				m_TrapPower = value;
			}
		}

		public override bool PassivelyTriggered{ get{ return true; } }
		public override TimeSpan PassiveTriggerDelay{ get{ return TimeSpan.FromSeconds( 10 ); } }
		public int PassiveTriggerRange{ get{ return m_TriggerRange; } }
		public override TimeSpan ResetDelay{ get{ return TimeSpan.FromSeconds( 2 ); } }

        public override void OnTrigger(Mobile from)
        {
            ArrayList targets = new ArrayList();
            if (from.AccessLevel > AccessLevel.Player)
                return;

            if (from == TrapOwner)
                return;

            if (this.Visible == false)
                this.Visible = true;

            Effects.SendMovingEffect(this, from, 7166, 5, 0, false, false);
            Effects.PlaySound(Location, Map, 564);

            int MinDamage = 40 * (TrapPower / 100);
            int MaxDamage = 60 * (TrapPower / 100);

            if (MinDamage < 5)
                MinDamage = 5;

            if (MaxDamage < 10)
                MaxDamage = 10;

            if (from.Alive)
            {
                IPooledEnumerable eable = this.Map.GetMobilesInRange(new Point3D(Location), DamageRange);

                foreach (Mobile m in eable)
                    targets.Add(m);

                eable.Free();
                if (targets.Count > 0)
                    for (int i = 0; i < targets.Count; ++i)
                    {
                        Spells.SpellHelper.Damage(TimeSpan.FromSeconds(0.5), from, from, Utility.RandomMinMax(MinDamage, MaxDamage), 100, 0, 0, 0, 0);
                        Mobile m = (Mobile)targets[i];

                        if (Poison != null)
                            m.ApplyPoison(m, m_Poison);

                        if (m.Player)
                            m.Paralyze(TimeSpan.FromSeconds(ParalyzeTime / 4));
                        else
                            m.Paralyze(TimeSpan.FromSeconds(ParalyzeTime));

                    }

                this.UsesRemaining -= 1;

                if (this.UsesRemaining <= 0)
                    this.Delete();
            }
        }

        public virtual int ScaleMana(int mana)
        {
            double scalar = 1.0;

            // Max Lower Mana Cost = 60%
            int lmc = AosAttributes.GetValue(m_TrapOwner, AosAttribute.LowerManaCost);
            if (lmc > 60)
                lmc = 60;

            scalar -= (double)lmc / 100;

            return (int)(mana * scalar);
        }

        public override void OnDoubleClick( Mobile from )
		{
			if ( from == TrapOwner )
			{
				from.SendMessage( "You conceal the trap." );
				this.Visible = false;
			}

			else if ( from != TrapOwner )
			{

				int trapmaxskill = (int)Math.Round(from.Skills.RemoveTrap.Value) + (int)Math.Round(from.Skills.Tinkering.Value) + 50;
                int bonusmaxskill = (int)Math.Round(BonusSkill.Value);
				int trapminskill = trapmaxskill - 20;
				int trappower = this.TrapPower;
				int trapcheck = Utility.RandomMinMax(trapminskill, trapmaxskill);

				if ( trappower > trapmaxskill )
				{
                    from.SendMessage("You have no chance of removing this trap.");
					return;
				}

				if ( lastused + delay > DateTime.Now ) 
				{
					from.SendMessage("You must wait 7 seconds between uses.");
					return;
				}
				else
					lastused = DateTime.Now;

				if ( trapcheck >= trappower )
				{
					this.Delete();
					from.SendMessage("You successfully remove the trap.");
				}
				else
				{
					from.SendMessage("You fail to remove the trap.");
					
					if ( 0.5 >= Utility.RandomDouble())
					{
						from.SendMessage("You accidently trigger one of the trap's components. It damages you before you can deactive it.");
						Spells.SpellHelper.Damage( TimeSpan.FromSeconds( 0.5 ), from, from, Utility.RandomMinMax( 15, 25 ), 100, 0, 0, 0, 0 );
					}
				}
			}
						
		}

		public CraftedTrap( Serial serial ) : base( serial )
		{
            DamageScalar = 1;
            TriggerRange = 1;
            DamageRange = 0;
            ManaCost = (int)TrapPower / 10;
        }

        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_TrapOwner );
           	writer.Write( m_UsesRemaining );
            writer.Write( m_TrapPower );
            writer.Write(m_ManaCost);
            writer.Write(m_DamageRange);
            writer.Write(m_TriggerRange);
            writer.Write(m_ParalyzeTime);
            Poison.Serialize(m_Poison, writer);
            writer.Write(m_DamageScalar);
        }

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_TrapOwner = reader.ReadMobile();
					m_UsesRemaining = reader.ReadInt();
					m_TrapPower = reader.ReadInt();
                    m_ManaCost = reader.ReadInt();
                    m_DamageRange = reader.ReadInt();
                    m_TriggerRange = reader.ReadInt();
                    m_ParalyzeTime = reader.ReadInt();
                    m_Poison = Poison.Deserialize(reader);
                    m_DamageScalar = reader.ReadFloat();

                    break;
				}
			}
		}
	}
}