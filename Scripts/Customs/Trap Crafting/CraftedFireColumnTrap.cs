using System;

namespace Server.Items
{
	public class CraftedFireColumnTrap : CraftedTrap
	{
		[Constructable]
		public CraftedFireColumnTrap() : base( 0x1B71 )
		{
			Visible = false;
			Hue = 45;
			UsesRemaining = 1;
            Name = "A fire-column trap";
			TrapPower = 100;
            DamageScalar = 1;
            TriggerRange = 0;
            DamageRange = 0;
            ManaCost = (int)TrapPower / 10;
        }
        
		public CraftedFireColumnTrap( Serial serial ) : base( serial )
		{
            Visible = false;
            Hue = 45;
            UsesRemaining = 1;
            Name = "A fire-column trap";
            TrapPower = 100;
            DamageScalar = 1;
            TriggerRange = 0;
            DamageRange = 0;
            ManaCost = (int)TrapPower / 10;
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}