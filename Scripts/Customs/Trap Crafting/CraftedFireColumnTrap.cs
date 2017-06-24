using System;

namespace Server.Items
{
	public class CraftedFireColumnTrap : CraftedTrap
	{
        [Constructable]
		public CraftedFireColumnTrap()
		{
            DamageType = "Fire";
            ItemID = 7025;
            Visible = false;
			Hue = 45;
			UsesRemaining = 1;
            Name = "A fire-column trap";
			TrapPower = 100;
            DamageScalar = 1.5;
            TriggerRange = 0;
            DamageRange = 0;
            ManaCost = 10;
            BonusSkill = SkillName.Inscribe;
            Delay = TimeSpan.FromSeconds(5);
        }
        
		public CraftedFireColumnTrap( Serial serial ) : base( serial )
		{
            DamageType = "Fire";
            Visible = false;
            Hue = 45;
            UsesRemaining = 1;
            Name = "A fire-column trap";
            TrapPower = 100;
            DamageScalar = 1.5;
            TriggerRange = 0;
            DamageRange = 0;
            ManaCost = 10;
            BonusSkill = SkillName.Inscribe;
            Delay = TimeSpan.FromSeconds(5);
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