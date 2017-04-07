using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Spells;

namespace Server.Items
{
	public class CraftedExplosionTrap : CraftedTrap
	{
		
		[Constructable]
		public CraftedExplosionTrap()
		{
            ItemID = 11220;
			Visible = false;
			Hue = 254;
			UsesRemaining = 1;
            Name = "An explosion trap";
			TrapPower = 100;
            DamageScalar = 1.5;
            TriggerRange = 1;
            DamageRange = 2;
            ManaCost = 40;
            BonusSkill = SkillName.Alchemy;
        }
    
		public CraftedExplosionTrap( Serial serial ) : base( serial )
		{
            Visible = false;
            Hue = 254;
            UsesRemaining = 1;
            Name = "An explosion trap";
            TrapPower = 100;
            DamageScalar = 1.5;
            TriggerRange = 1;
            DamageRange = 2;
            ManaCost = 40;
            BonusSkill = SkillName.Alchemy;
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