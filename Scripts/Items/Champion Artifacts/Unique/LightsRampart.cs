using System;
using Server;

namespace Server.Items
{
	public class LightsRampart : MetalShield
	{
		public override int LabelNumber{ get{ return 1112399; } } //Light's Rampart [Replica]

        public override int InitMinHits { get { return 150; } }
        public override int InitMaxHits { get { return 150; } }

        public override bool CanFortify { get { return false; } }

        public override int BasePhysicalResistance { get { return 4; } }
        public override int BaseFireResistance { get { return 5; } }
        public override int BaseColdResistance { get { return 13; } }
        public override int BasePoisonResistance { get { return 3; } }
        public override int BaseEnergyResistance { get { return 3; } }

        [Constructable]
		public LightsRampart()
		{
            Hue = 1272;
            Attributes.DefendChance = 20;
			Attributes.SpellChanneling = 1;
		}

		public LightsRampart( Serial serial ) : base( serial )
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