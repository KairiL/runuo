using System;
using Server;
using Server.Targeting;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;

namespace Server.Items
{
	public class CraftedBoltTrap : CraftedTrap
	{
        
		[Constructable]
		public CraftedBoltTrap() : base( 0x1F18 )
		{
			Visible = false;
			Hue = 543;
			UsesRemaining = 1;
            Name = "A bolt-firing trap";
			TrapPower = 100;
            TriggerRange = 4;
            DamageRange = 4;
            ManaCost = (int)TrapPower / 10;
        }
        
		public override void OnTrigger( Mobile from )
		{
            Effects.SendMovingEffect(this, from, 7166, 5, 0, false, false);
            Effects.PlaySound(Location, Map, 564);
            base.OnTrigger(from);
		}
        
		public CraftedBoltTrap( Serial serial ) : base( serial )
		{
            Visible = false;
            Hue = 543;
            UsesRemaining = 1;
            this.Name = "A bolt-firing trap";
            TrapPower = 100;
            TriggerRange = 4;
            DamageRange = 4;
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