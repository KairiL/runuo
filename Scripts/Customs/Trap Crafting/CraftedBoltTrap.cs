using System;
using Server;
using Server.Targeting;
using System.Collections;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Spells;
using Server.Mobiles;

namespace Server.Items
{
	public class CraftedBoltTrap : CraftedTrap
	{
        [Constructable]
		public CraftedBoltTrap()
		{
            DamageType = "Physical";
            ItemID = 7960;
			Visible = false;
			Hue = 543;
			UsesRemaining = 1;
            Name = "A bolt-firing trap";
            TriggerRange = 3;
            DamageRange = 0;
            ManaCost = 5;
            ParalyzeTime = 0;
            DamageScalar = .5;
            BonusSkill = SkillName.Fletching;
            Delay = TimeSpan.FromSeconds(3);
        }
        
		public override void OnTrigger( Mobile from )
		{
             if (TrapOwner != null  && TrapOwner.Player && TrapOwner.CanBeHarmful(from, false) && 
                    from != TrapOwner && SpellHelper.ValidIndirectTarget(TrapOwner, (Mobile)from) &&
                    (!(from is BaseCreature) || ((BaseCreature)from).ControlMaster != TrapOwner))
            {
                Effects.SendMovingEffect(this, from, 7166, 5, 0, false, false);
                Effects.PlaySound(Location, Map, 564);
                base.OnTrigger(from);
            }
		}
        
		public CraftedBoltTrap( Serial serial ) : base( serial )
		{
            DamageType = "Physical";
            ItemID = 7960;
            Visible = false;
            Hue = 543;
            UsesRemaining = 1;
            Name = "A bolt-firing trap";
            TriggerRange = 3;
            DamageRange = 0;
            ManaCost = 10;
            DamageScalar = .5;
            BonusSkill = SkillName.Fletching;
            Delay = TimeSpan.FromSeconds(3);
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