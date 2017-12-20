using System;
using System.Net;
using Server;
using Server.Accounting;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{

	public class DreadHornCombiner : Item
	{

		[Constructable]
		public DreadHornCombiner() : this( null )
		{
		}
		
		[Constructable]
		public DreadHornCombiner( string name ) : base( 4644 )
		{
			Name = "Statue Of The Fey";
            Movable = false;
		}
		
		public DreadHornCombiner( Serial serial ) : base( serial )
		{
		}
		
		public override void OnDoubleClick( Mobile from )
		{
            base.OnDoubleClick(from);

            Item bc = from.Backpack.FindItemByType(typeof(BlightedCotton));
            Item cc = from.Backpack.FindItemByType(typeof(IrkBrain));
            Item jc = from.Backpack.FindItemByType(typeof(LissithSilk));
            Item pc = from.Backpack.FindItemByType(typeof(SabrixEye));
            Item sc = from.Backpack.FindItemByType(typeof(ThornyBriar));

            if ( ( cc == null || cc.Amount < 1 ) || 
                 ( bc == null || bc.Amount < 1 ) || 
                 ( jc == null || jc.Amount < 1 ) || 
                 ( pc == null || pc.Amount < 1 ) || 
                 ( sc == null || sc.Amount < 1 ) )
            {
                from.SendMessage("You do not have all the required items");
            }
            else
            {
                from.Backpack.ConsumeTotal(typeof(BlightedCotton), 1);
                from.Backpack.ConsumeTotal(typeof(IrkBrain), 1);
                from.Backpack.ConsumeTotal(typeof(LissithSilk), 1);
                from.Backpack.ConsumeTotal(typeof(SabrixEye), 1);
                from.Backpack.ConsumeTotal(typeof(ThornyBriar), 1);
                from.AddToBackpack(new DreadhornKey());
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