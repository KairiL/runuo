using System;
using Server;

namespace Server.Items
{
	[Flipable( 0x2684, 0x2683 )]
	public class ShroudOfTheCondemned : HoodedShroudOfShadows
    {

        public override int InitMinHits { get { return 150; } }
        public override int InitMaxHits { get { return 150; } }

        public override bool CanFortify { get { return false; } }

        [Constructable]
		public ShroudOfTheCondemned() : this(1910 )
		{
		}

		[Constructable]
		public ShroudOfTheCondemned( int hue ) : base( 0x2684 )
		{
            Name = "Shroud of the Condemned";
			LootType = LootType.Blessed;
			Weight = 3.0;
            Hue = 1910;
            Attributes.BonusHits = 3;
            Attributes.BonusInt = 5;
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendLocalizedMessage( sender.FailMessage );
			return false;
		}

		public ShroudOfTheCondemned( Serial serial ) : base( serial )
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
