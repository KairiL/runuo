using System;
using Server; 
namespace Server.Items
{
    public class ScatteredCrystals : Item
    {
        [Constructable]
        public ScatteredCrystals() : this( 1 )
        {}
        [Constructable]
        public ScatteredCrystals( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
        {}
        [Constructable]

        ///////////The value on the line below is the ItemID
        public ScatteredCrystals( int amount ) : base( 8762 )
        {
            ///////////Item name
            Name = "Scattered Crystals";

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

        public ScatteredCrystals( Serial serial ) : base( serial )
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
