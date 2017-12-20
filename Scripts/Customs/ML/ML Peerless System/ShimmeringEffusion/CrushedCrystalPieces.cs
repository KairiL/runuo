using System;
using Server; 
namespace Server.Items
{
    public class CrushedCrystalPieces : Item
    {
        [Constructable]
        public CrushedCrystalPieces() : this( 1 )
        {}
        [Constructable]
        public CrushedCrystalPieces( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
        {}
        [Constructable]

        ///////////The hexagon value ont he line below is the ItemID
        public CrushedCrystalPieces( int amount ) : base( 8764 )
        {


            ///////////Item name
            Name = "Crushed Crystal Pieces";

            ///////////Item hue
            Hue = 0;

            ///////////Stackable
            Stackable = false;

            ///////////Weight of one item
            Weight = 0.01;
            Amount = amount;
            LootType = LootType.Blessed;

            Timer.DelayCall(TimeSpan.FromHours(3.0), new TimerStateCallback(DeleteKey), this);
        }

        public void DeleteKey(object state)
        {
            Item from = (Item)state;
            from.Delete();
        }

        public CrushedCrystalPieces( Serial serial ) : base( serial )
        {}
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
