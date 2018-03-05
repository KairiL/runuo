using System;
using Server;

namespace Server.Items
{
	public class CrystallineRing : SilverRing
	{

		[Constructable]
		public CrystallineRing()
		{
            Name = "Crystalline Ring";
            Hue = 0x480;
            SkillBonuses.SetValues(0, SkillName.Magery, 10);
            SkillBonuses.SetValues(1, SkillName.Focus, 10);
            Attributes.RegenHits = 5;
			Attributes.RegenMana = 3;
			Attributes.SpellDamage = 20;
		}

		public CrystallineRing( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}