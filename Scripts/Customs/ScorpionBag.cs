using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ScorpionBag : Bag
	{
		[Constructable]
		public ScorpionBag() : this( 50 )
		{
		}

		[Constructable]
		public ScorpionBag( int amount )
		{
			DropItem( new Gears   ( 50 ) );
			DropItem( new Leather    ( 50 ) );
			DropItem( new Board       ( 50 ) );
			DropItem( new Bolt      ( 200 ) );
            for (int i=0; i<50; i++)
            { 
                DropItem(new GreaterExplosionPotion(1)); 
            }
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