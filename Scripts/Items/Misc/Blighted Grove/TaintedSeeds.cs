using System;
using Server;

namespace Server.Items
{
	public class TaintedSeeds : Item
	{
		public override int LabelNumber{ get{ return 1074233; } } // Tainted Seeds

		[Constructable]
		public TaintedSeeds() : base( 0xDFA )
		{
			LootType = LootType.Blessed;
            Hue = 1161;
            Weight = 0.01;
            Timer.DelayCall(TimeSpan.FromHours(3.0), new TimerStateCallback(DeleteKey), this);
        }

        public void DeleteKey(object state)
        {
            Item from = (Item)state;
            from.Delete();
        }

        public TaintedSeeds( Serial serial ) : base( serial )
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

