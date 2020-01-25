using System;
using Server.Mobiles;
using Server.Spells;

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
            ManaCost = 20;
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
                from.FixedParticles( 0x3709, 10, 30, 5052, EffectLayer.LeftFoot );
				from.PlaySound( 0x208 );
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