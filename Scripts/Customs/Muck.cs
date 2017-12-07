using System;

namespace Server.Items
{
	public class Muck : Item
	{
        private double m_StickChance = .75;

		[Constructable]
		public Muck() : base(0xCC3)
		{
			Movable = false;
		}

        public override bool OnMoveOff(Mobile m)
        {
            return Utility.RandomDouble() > m_StickChance;
        }

        public override bool OnMoveOver(Mobile m)
        {
            return Utility.RandomDouble() > m_StickChance;
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public double StickChance
        {
            get
            {
                return m_StickChance;
            }
            set
            {
                m_StickChance = value;
            }
        }

        public Muck(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
            writer.Write(StickChance);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
            StickChance = reader.ReadDouble();
		}
	}
}