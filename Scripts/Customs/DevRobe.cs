using System;
using Server;

namespace Server.Items
{
	[Flipable( 0x1F03, 0x1F04 )]
	public class DevRobe : Robe
	{

		[Constructable]
		public DevRobe()
		{
			Weight = 3.0;

			
            Attributes.AttackChance = 100;
            Attributes.BonusDex = 100;
            Attributes.BonusHits = 100;
            Attributes.BonusInt = 100;
            Attributes.BonusMana = 100;
            Attributes.BonusStam = 100;
            Attributes.BonusStr = 100;
            Attributes.CastRecovery = 10;
            Attributes.CastSpeed = 10;
            Attributes.DefendChance = 100;
            Attributes.EnhancePotions = 100;
            Attributes.LowerManaCost = 100;
            Attributes.LowerRegCost = 100;
            Attributes.Luck = 4000;
            Attributes.NightSight = 1;
            Attributes.ReflectPhysical = 100;
            Attributes.RegenHits = 100;
            Attributes.RegenMana = 100;
            Attributes.RegenStam = 100;
            Attributes.SpellDamage = 100;
            Attributes.WeaponDamage = 100;
            Attributes.WeaponSpeed = 100;
            Resistances.Cold = 100;
            Resistances.Energy = 100;
            Resistances.Fire = 100;
            Resistances.Physical = 100;
            Resistances.Poison = 100;
            Resistances.Direct = 100;
        }

		public DevRobe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}