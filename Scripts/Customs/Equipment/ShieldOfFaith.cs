using System;
using Server;
using Server.Spells;
using Server.Network;

namespace Server.Items
{
	public class ShieldOfFaith : WoodenKiteShield
	{
		//public override int LabelNumber{ get{ return 1061101; } } // Arcane Shield 
		public override int ArtifactRarity{ get{ return 777; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }
        public int Push
        {
            get{ return m_Push; }
            set{ m_Push = value; }
        }

        public int Pull
        {
            get { return m_Pull; }
            set { m_Pull = value; }
        }

        private int m_Push;
        private int m_Pull;

        [Constructable]
		public ShieldOfFaith()
		{
            Push = 14;
            LootType = LootType.Blessed;
            Name = "The Shield of Faith";
			ItemID = 0x2B01;
			Hue = 0x556;
			Attributes.NightSight = 1;
			Attributes.SpellChanneling = 1;
			Attributes.DefendChance = 45;
			Attributes.CastSpeed = 1;
            Attributes.Luck = 400;
            SkillBonuses.SetValues(0, SkillName.Hiding, 100);
            SkillBonuses.SetValues(0, SkillName.Poisoning, 100);
            SkillBonuses.SetValues(0, SkillName.Necromancy, 100);
            //TODO skill bonuses
        }

		public ShieldOfFaith( Serial serial ) : base( serial )
		{
		}

        public override void OnDoubleClick(Mobile from)
        {
            if ( Deleted )
                return;
            Point3D loc = GetWorldLocation();

            if (!from.InLOS(loc) || !from.InRange(loc, 2))
            {
                from.LocalOverheadMessage(Server.Network.MessageType.Regular, 0x3E9, 1019045); // I can't reach that
                return;
            }
            else if (!this.IsAccessibleTo(from))
            {
                from.PublicOverheadMessage(MessageType.Regular, 0x3E9, 1061637); // You are not allowed to access this.
                return;
            }
            DoPulls(from);
            DoPushes(from);
            from.Hidden = true;
            from.Poison = null;
            from.Hits += 20;
            from.Stam += 20;
        }

        private void DoPulls(Mobile from)
        {
            Direction dir;
            int dist;
            if (Pull > 0)
                foreach (Mobile m_target in GetMobilesInRange(Pull))
                {
                    if ((m_target != from) && (SpellHelper.ValidIndirectTarget(from, (Mobile)m_target) && from.CanBeHarmful((Mobile)m_target, false)))
                    {
                        m_target.Paralyzed = false;
                        m_target.Frozen = false;

                        if (m_target.Spell != null)
                            m_target.Spell.OnCasterHurt();

                        m_target.Location = from.Location;
                    }
                }
        }

        
        private void DoPushes(Mobile from)
        {
            Direction dir;
            int dist;
            if (Push > 0)
                foreach (Mobile m_target in GetMobilesInRange(Push))
                    if ((m_target != from) && (SpellHelper.ValidIndirectTarget(from, (Mobile)m_target) && from.CanBeHarmful((Mobile)m_target, false)))
                    {
                        if (m_target.Spell != null)
                            m_target.Spell.OnCasterHurt();

                        m_target.Direction = from.GetDirectionTo(m_target);
                        m_target.Move(m_target.Direction);
                    }
        }
        /*
        public override void AppendChildNameProperties(ObjectPropertyList list)
        {
            base.AppendChildNameProperties(list);
            int prop;

            if ((prop = Push) != 0)
                list.Add(1060416, prop.ToString()); // hit cold area ~1_val~%
        }
        */
        public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( Attributes.NightSight == 0 )
				Attributes.NightSight = 1;
		}
	}
}