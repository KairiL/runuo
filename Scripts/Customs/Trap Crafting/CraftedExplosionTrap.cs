using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Spells;
using Server.Mobiles;

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

		public override void OnTrigger( Mobile from )
		{
             if (TrapOwner != null  && TrapOwner.Player && TrapOwner.CanBeHarmful(from, false) && 
                    from != TrapOwner && SpellHelper.ValidIndirectTarget(TrapOwner, (Mobile)from) &&
                    (!(from is BaseCreature) || ((BaseCreature)from).ControlMaster != TrapOwner))
            {
                from.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
                from.PlaySound( 0x307 );
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