using System;
using Server;

namespace Server.Items
{
	public class SCExecutionersAxe : ExecutionersAxe
	{
		[Constructable]
		public SCExecutionersAxe()
		{
            Attributes.SpellChanneling = 1;
            Attributes.CastSpeed = 2;
            Attributes.CastRecovery = 6;
            LootType = LootType.Blessed;
		}

		public SCExecutionersAxe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}