using System;
using System.Net;
using Server;
using Server.Accounting;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{

	public class KeyCombiner : Item
	{

		[Constructable]
		public KeyCombiner() : this( null )
		{
		}
		
		[Constructable]
		public KeyCombiner( string name ) : base( 0xB35 )
		{
			Name = "Key Combiner";
            Movable = false;
		}
		
		public KeyCombiner( Serial serial ) : base( serial )
		{
		}
		
		public override void OnDoubleClick( Mobile from )
		{
            base.OnDoubleClick(from);

            Item bk = from.Backpack.FindItemByType(typeof(BlueKey));
            Item rk = from.Backpack.FindItemByType(typeof(RedKey));
            Item yk = from.Backpack.FindItemByType(typeof(YellowKey));

            if ( ( bk == null || bk.Amount < 1 ) || 
                 ( rk == null || rk.Amount < 1 ) || 
                 ( yk == null || yk.Amount < 1 ) )
            {
                from.SendMessage("You do not have all the required items");
            }
            else
            {
                from.Backpack.ConsumeTotal(typeof(BlueKey), 1);
                from.Backpack.ConsumeTotal(typeof(RedKey), 1);
                from.Backpack.ConsumeTotal(typeof(YellowKey), 1);
                from.AddToBackpack(new TravestyKey());
            }
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