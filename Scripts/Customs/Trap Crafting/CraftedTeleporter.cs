using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Spells;

namespace Server.Items
{
	public class CraftedTeleporter : CraftedTrap
	{
		[Constructable]
		public CraftedTeleporter()
		{
            Visible = true;
			UsesRemaining = 100;
            Name = "A teleporter";
            ManaCost = 4;
            DamageRange = 0;
            TriggerRange = 0;
            ParalyzeTime = 0;
            DamageScalar = 0;
            BonusSkill = SkillName.Inscribe;
        }

		public CraftedTeleporter( Serial serial ) : base( serial )
		{
            Visible = true;
            UsesRemaining = 100;
            Name = "A teleporter";
            ManaCost = 4;
            DamageRange = 0;
            TriggerRange = 0;
            ParalyzeTime = 0;
            DamageScalar = 0;
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