using System;
using Server;

namespace Server.Items
{
	public class ThrashersTail : Item
	{
		public override int LabelNumber{ get{ return 1074230; } } // Thrasher's Tail

		[Constructable]
		public ThrashersTail() : base( 0x1A9D )
		{
			LootType = LootType.Blessed;
			Hue = 0x455;
            Timer.DelayCall(TimeSpan.FromHours(3.0), new TimerStateCallback(DeleteKey), this);
        }

        public void DeleteKey(object state)
        {
            Item from = (Item)state;
            from.Delete();
        }

        public ThrashersTail( Serial serial ) : base( serial )
		{
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

