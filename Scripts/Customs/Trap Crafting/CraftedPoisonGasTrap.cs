using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Spells;

namespace Server.Items
{
	public class CraftedPoisonGasTrap : CraftedTrap
	{
        
		[Constructable]
		public CraftedPoisonGasTrap() : base( 0x1848 )
		{
            DamageType = "Poison";
            ItemID = 6216;
            Visible = false;
			Hue = 272;
			UsesRemaining = 1;
            Name = "A poison-gas trap";
			Poison = Poison.Regular;
            DamageScalar = 0;
            TriggerRange = 2;
            DamageRange = 3;
            ManaCost = 11;
            BonusSkill = SkillName.Poisoning;
            Delay = TimeSpan.FromSeconds(5);
        }

		public override void OnTrigger( Mobile from )
		{
			Effects.SendLocationParticles( EffectItem.Create( Location, Map, EffectItem.DefaultDuration ), 0x3914, 10, 30, 5052 );
			Effects.PlaySound( Location, Map, 0x231 );
            base.OnTrigger(from);
        }

		public CraftedPoisonGasTrap( Serial serial ) : base( serial )
		{
            DamageType = "Poison";
            ItemID = 6216;
            Visible = false;
            Hue = 272;
            UsesRemaining = 1;
            Name = "A poison-gas trap";
            Poison = Poison.Regular;
            DamageScalar = 0;
            TriggerRange = 2;
            DamageRange = 3;
            ManaCost = 11;
            BonusSkill = SkillName.Poisoning;
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