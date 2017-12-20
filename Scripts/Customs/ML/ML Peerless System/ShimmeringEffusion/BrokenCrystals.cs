using System;
using Server; 
namespace Server.Items
{
    public class BrokenCrystals : Item
    {
        [Constructable]
        public BrokenCrystals() : this( 1 )
        {}
        [Constructable]
        public BrokenCrystals( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
        {}
        [Constructable]

        ///////////The value on the line below is the ItemID
        public BrokenCrystals( int amount ) : base( 8767 )
        {


            ///////////Item name
            Name = "Broken Crystals";

            ///////////Item hue
            Hue = 1150;

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

        public BrokenCrystals( Serial serial ) : base( serial )
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
