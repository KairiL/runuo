using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Spells;
using Server.Mobiles;

namespace Server.Items
{
	public class CraftedElectricTrap : CraftedTrap
	{
		[Constructable]
		public CraftedElectricTrap()
		{
            ItemID = 3629;
            Visible = false;
			Hue = 6;
			UsesRemaining = 1;
            Name = "A static-jolt trap";
			TrapPower = 100;
            DamageScalar = 1;
            TriggerRange = 1;
            DamageRange = 1;
            ManaCost = 20;
            BonusSkill = SkillName.Inscribe;
            Delay = TimeSpan.FromSeconds(5);
        }

		public CraftedElectricTrap( Serial serial ) : base( serial )
		{
            ItemID = 3629;
            Visible = false;
            Hue = 6;
            UsesRemaining = 1;
            Name = "A static-jolt trap";
            TrapPower = 100;
            DamageScalar = .1;
            TriggerRange = 1;
            DamageRange = 1;
            ManaCost = 20;
            BonusSkill = SkillName.Inscribe;
            Delay = TimeSpan.FromSeconds(5);
        }

		public override void OnTrigger( Mobile from )
		{
             if (TrapOwner != null  && TrapOwner.Player && TrapOwner.CanBeHarmful(from, false) && 
                    from != TrapOwner && SpellHelper.ValidIndirectTarget(TrapOwner, (Mobile)from) &&
                    (!(from is BaseCreature) || ((BaseCreature)from).ControlMaster != TrapOwner))
            {
                from.BoltEffect( 0 );
                base.OnTrigger(from);
            }
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