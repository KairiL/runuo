using System;
using System.Net;
using Server;
using Server.Accounting;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{

	public class CrystalCombiner : Item
	{

		[Constructable]
		public CrystalCombiner() : this( null )
		{
		}
		
		[Constructable]
		public CrystalCombiner( string name ) : base( 0x2210 )
		{
			Name = "Crystal Combiner";
            Movable = false;
		}
		
		public CrystalCombiner( Serial serial ) : base( serial )
		{
		}
		
		public override void OnDoubleClick( Mobile from )
		{
            base.OnDoubleClick(from);

            Item bc = from.Backpack.FindItemByType(typeof(BrokenCrystals));
            Item cc = from.Backpack.FindItemByType(typeof(CrushedCrystalPieces));
            Item jc = from.Backpack.FindItemByType(typeof(JaggedCrystals));
            Item pc = from.Backpack.FindItemByType(typeof(PiecesOfCrystal));
            Item sc = from.Backpack.FindItemByType(typeof(ScatteredCrystals));
            Item sh = from.Backpack.FindItemByType(typeof(ShatteredCrystals));

            if ( ( cc == null || cc.Amount < 1 ) || 
                 ( bc == null || bc.Amount < 1 ) || 
                 ( jc == null || jc.Amount < 1 ) || 
                 ( pc == null || pc.Amount < 1 ) || 
                 ( sc == null || sc.Amount < 1 ) || 
                 ( sh == null || sh.Amount < 1 ) )
            {
                from.SendMessage("You do not have all the required items");
            }
            else
            {
                from.Backpack.ConsumeTotal(typeof(BrokenCrystals), 1);
                from.Backpack.ConsumeTotal(typeof(CrushedCrystalPieces), 1);
                from.Backpack.ConsumeTotal(typeof(JaggedCrystals), 1);
                from.Backpack.ConsumeTotal(typeof(PiecesOfCrystal), 1);
                from.Backpack.ConsumeTotal(typeof(ScatteredCrystals), 1);
                from.Backpack.ConsumeTotal(typeof(ShatteredCrystals), 1);
                from.AddToBackpack(new ShimmeringEffusionKey());
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