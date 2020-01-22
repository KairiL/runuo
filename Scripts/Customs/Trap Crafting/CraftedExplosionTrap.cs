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
            DamageType = "Fire";
            ItemID = 11220;
			Visible = false;
			Hue = 254;
			UsesRemaining = 1;
            Name = "An explosion trap";
			TrapPower = 100;
            DamageScalar = .2;
            TriggerRange = 3;
            DamageRange = 5;
            ManaCost = 20;
            ParalyzeTime = 0;
            BonusSkill = SkillName.Alchemy;
            Delay = TimeSpan.FromSeconds(5);
        }
    
		public CraftedExplosionTrap( Serial serial ) : base( serial )
		{
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