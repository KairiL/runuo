using System;
using Server;
using Server.Targeting;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Spells;
using Server.Mobiles;

namespace Server.Items
{
	public class CraftedTrap : BaseTrap
	{
		private Mobile m_TrapOwner;
		private int m_UsesRemaining, m_TrapPower, m_ManaCost, m_DamageRange, m_TriggerRange, m_ParalyzeTime;
        private double m_DamageScalar;
        private SkillName m_BonusSkill;
        private Poison m_Poison;
        private Point3D m_PointDest;
        private Map m_MapDest;

        private DateTime lastused = DateTime.Now;
		private TimeSpan delay = TimeSpan.FromSeconds( 7 );

        [CommandProperty(AccessLevel.GameMaster)]
        public Map MapDest
        {
            get
            {
                return m_MapDest;
            }
            set
            {
                m_MapDest = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D PointDest
        {
            get
            {
                return m_PointDest;
            }
            set
            {
                m_PointDest = value;
            }
        }

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
        public SkillName BonusSkill
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
		public override TimeSpan ResetDelay{ get{ return TimeSpan.FromSeconds( 0 ); } }

        public void SetMap(Map map)
        {
            MapDest = map;
        }
        public void SetPoint(Point3D point)
        {
            PointDest = point;
        }
        public override void OnTrigger(Mobile from)
        {
            ArrayList targets = new ArrayList();
            if (from.AccessLevel > AccessLevel.Player)
                return;
            int ManaLoss = ScaleMana(ManaCost);
            if (TrapOwner != null )
                if (TrapOwner.Player && TrapOwner.Map == this.Map && TrapOwner.InRange(Location, 200))
                {
                    if (TrapOwner.Mana >= ManaLoss)
                        TrapOwner.Mana -= ManaLoss;
                    else
                    {
                        ManaLoss -= TrapOwner.Mana;
                        TrapOwner.Mana = 0;
                        TrapOwner.Damage((ManaLoss));
                    }
                }
            else
                return;


            if (this.Visible == false)
                this.Visible = true;

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
                        if (DamageScalar != 0)
                            Spells.SpellHelper.Damage(TimeSpan.FromSeconds(0.5), from, from, (int)DamageScalar*Utility.RandomMinMax(MinDamage, MaxDamage), 100, 0, 0, 0, 0);
                        Mobile m = (Mobile)targets[i];

                        if (Poison != null)
                            m.ApplyPoison(m, m_Poison);
                        if (ParalyzeTime > 0)
                            if (m.Player)
                                m.Paralyze(TimeSpan.FromSeconds(ParalyzeTime / 4));
                            else
                                m.Paralyze(TimeSpan.FromSeconds(ParalyzeTime));

                    }
                if (PointDest != Point3D.Zero)
                    Teleport(from);
                this.UsesRemaining -= 1;

                if (this.UsesRemaining <= 0)
                    this.Delete();
            }
        }

        public bool Teleport(Mobile from)
        {
            if (Factions.Sigil.ExistsOn(from))
            {
                from.SendLocalizedMessage(1061632); // You can't do that while carrying the sigil.
                return false;
            }
            else if (Server.Misc.WeightOverloading.IsOverloaded(from))
            {
                from.SendLocalizedMessage(502359, "", 0x22); // Thou art too encumbered to move.
                return false;
            }
            else if (!SpellHelper.CheckTravel(from, TravelCheckType.TeleportFrom))
            {
                return false;
            }
            else if (!SpellHelper.CheckTravel(from, MapDest, PointDest, TravelCheckType.TeleportTo))
            {
                return false;
            }
            else if (MapDest == null || !MapDest.CanSpawnMobile(PointDest))
            {
                from.SendLocalizedMessage(501942); // That location is blocked.
                return false;
            }
            else if (SpellHelper.CheckMulti(PointDest, MapDest))
            {
                from.SendLocalizedMessage(502831); // Cannot teleport to that spot.
                return false;
            }
            else
            {
                Mobile m = from;
                
                bool sendEffect = (!m.Hidden || m.AccessLevel == AccessLevel.Player);
                if (sendEffect)
                {
                    Effects.SendLocationParticles(EffectItem.Create(Location, m.Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 2023);
                    Effects.SendLocationParticles(EffectItem.Create(PointDest, m.Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 5023);
                }
                else
                    m.FixedParticles(0x376A, 9, 32, 0x13AF, EffectLayer.Waist);

                m.PlaySound(0x1FE);

                IPooledEnumerable eable = m.GetItemsInRange(0);

                foreach (Item item in eable)
                    if (item is Server.Spells.Sixth.ParalyzeFieldSpell.InternalItem || item is Server.Spells.Fifth.PoisonFieldSpell.InternalItem || item is Server.Spells.Fourth.FireFieldSpell.FireFieldItem)
                        item.OnMoveOver(m);

                eable.Free();

                DoTeleport(m);
                m.ProcessDelta();
                return true;
            }
        }

        public virtual int ScaleMana(int mana)
        {
            double scalar = 1.0;

            // Max Lower Mana Cost = 60%
            int lmc = 0;
            if (TrapOwner != null)
                lmc = AosAttributes.GetValue(TrapOwner, AosAttribute.LowerManaCost);
            if (lmc > 60)
                lmc = 60;

            scalar -= (double)lmc / 100;

            return (int)(mana * scalar);
        }

        public override void OnDoubleClick( Mobile from )
		{
			if ( from == TrapOwner )
			{
                from.SendMessage("You conceal the trap.");
                this.Visible = false;
			}

			else if ( from != TrapOwner )
			{

				int trapmaxskill = (int)Math.Round(from.Skills.RemoveTrap.Value) + (int)Math.Round(from.Skills.Tinkering.Value) + 50;
                int bonusmaxskill = (int)Math.Round(from.Skills[BonusSkill].Value);
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

        public virtual void DoTeleport(Mobile m)
        {
            Map map = MapDest;

            if (map == null || map == Map.Internal)
                map = m.Map;

            Point3D p = PointDest;

            if (p == Point3D.Zero)
                p = m.Location;

            Server.Mobiles.BaseCreature.TeleportPets(m, p, map);

            m.MoveToWorld(p, map);
        }

        [Constructable]
        public CraftedTrap() : base( 0x1BC3 )
		{
            Visible = true;
            UsesRemaining = 100;
            Name = "A Trap";
            DamageScalar = 1;
            TriggerRange = 1;
            DamageRange = 0;
            ManaCost = 10;
        }

        public CraftedTrap( Serial serial ) : base( serial )
		{
            Visible = true;
            UsesRemaining = 100;
            Name = "A Trap";
            DamageScalar = 1;
            TriggerRange = 1;
            DamageRange = 0;
            ManaCost = 10;
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
            writer.Write(m_PointDest);
            writer.Write(m_MapDest);
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
                    m_DamageScalar = reader.ReadDouble();
                    m_PointDest = reader.ReadPoint3D();
                    m_MapDest = reader.ReadMap();

                        break;
				}
			}
		}
	}
}