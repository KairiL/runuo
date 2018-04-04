using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ScorpionBag : Bag
	{
		[Constructable]
		public ScorpionBag()
		{
			DropItem( new Gears   ( 50 ) );
			DropItem( new Leather    ( 50 ) );
			DropItem( new Board       ( 50 ) );
			DropItem( new Bolt      ( 200 ) );
            DropItem(new GreaterExplosionPotion( 50 ) ); 
			DropItem( new PowerCrystal   ( 1 ) );
			DropItem( new ScorpionAssembly ( 1 ) );
		}

		public ScorpionBag( Serial serial ) : base( serial )
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