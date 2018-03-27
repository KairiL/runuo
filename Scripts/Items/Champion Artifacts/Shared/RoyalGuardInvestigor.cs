using System;

namespace Server.Items
{
    public class RoyalGuardInvestigator : Cloak, ITokunoDyable
	{
		public int LabelNumber{ get{ return 1112409; } } // Royal Guard Investigator

        public override int InitMinHits { get { return 150; } }
        public override int InitMaxHits { get { return 150; } }

        public override bool CanFortify { get { return false; } }

        [Constructable]
		public RoyalGuardInvestigator() : base()
		{
			Hue = 0xD;
			
			SkillBonuses.Skill_1_Name = SkillName.Stealth;
			SkillBonuses.Skill_1_Value = 20;
		}

		public RoyalGuardInvestigator( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}
	}
}

