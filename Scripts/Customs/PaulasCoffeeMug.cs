using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class PaulasCoffeeMug : Item
	{
		[Constructable]
		public PaulasCoffeeMug() : base( 0xFFF )
		{
		}

		public PaulasCoffeeMug( Serial serial ) : base( serial )
		{
		}

        public override void OnDoubleClick(Mobile from)
        {
            if (Deleted)
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
            from.SendMessage("You feel completely satiated");
            from.Hunger = 20;
            from.Thirst = 20;
        }
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}