using System;
using Server; 
namespace Server.Items
{
    public class JaggedCrystals : Item
    {
        [Constructable]
        public JaggedCrystals() : this( 1 )
        {}
        [Constructable]
        public JaggedCrystals( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
        {}
        [Constructable]

        ///////////The hexagon value ont he line below is the ItemID
        public JaggedCrystals( int amount ) : base( 8765 )
        {


            ///////////Item name
            Name = "Jagged Crystals";

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

        public JaggedCrystals( Serial serial ) : base( serial )
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
