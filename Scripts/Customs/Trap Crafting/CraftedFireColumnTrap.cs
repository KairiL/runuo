using System;

namespace Server.Items
{
	public class CraftedFireColumnTrap : CraftedTrap
	{
		[Constructable]
		public CraftedFireColumnTrap()
		{
            ItemID = 7025;
            Visible = false;
			Hue = 45;
			UsesRemaining = 1;
            Name = "A fire-column trap";
			TrapPower = 100;
            DamageScalar = 1;
            TriggerRange = 0;
            DamageRange = 0;
            ManaCost = 20;
            BonusSkill = SkillName.Inscribe;
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
            ManaCost = 20;
            BonusSkill = SkillName.Inscribe;
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