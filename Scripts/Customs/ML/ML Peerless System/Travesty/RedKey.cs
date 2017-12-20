/* This file was created with
Ilutzio's Questmaker. Enjoy! */
using System;using Server;namespace Server.Items
{
    public class RedKey : Item
    {
        [Constructable]
        public RedKey() : this( 1 )
        {}
        [Constructable]
        public RedKey( int amountFrom, int amountTo ) : this( Utility.RandomMinMax( amountFrom, amountTo ) )
        {}
        [Constructable]

        ///////////The hexagon value ont he line below is the ItemID
        public RedKey( int amount ) : base( 4112 )
        {


            ///////////Item name
            Name = "A Red Key";

            ///////////Item hue
            Hue = 2;

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

        public RedKey( Serial serial ) : base( serial )
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
