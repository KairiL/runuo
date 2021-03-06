// By Nerun

using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{

	public class PLichRing : BaseRing
	{
		[Constructable]
		public PLichRing() : base( 0x108a )
		{
			Weight = 1.0;
			Name = "The Primeval Lich Ring";
			Attributes.CastRecovery = 9;
			Attributes.CastSpeed = 12;
            Attributes.SpellChanneling = 14;
			LootType = LootType.Blessed;
		}

		public PLichRing( Serial serial ) : base( serial )
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