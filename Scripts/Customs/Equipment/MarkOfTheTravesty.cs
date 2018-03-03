using System;
using Server;

namespace Server.Items
{
	public class MarkOfTheTravesty : SavageMask
	{

        public override int BasePhysicalResistance { get { return 8; } }
        public override int BaseFireResistance { get { return 5; } }
        public override int BaseColdResistance { get { return 11; } }
        public override int BasePoisonResistance { get { return 20; } }
        public override int BaseEnergyResistance { get { return 15; } }
        public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 20; } }

		[Constructable]
		public MarkOfTheTravesty()
		{
			Hue = 0x455;
            Name = "Mark of Travesty";
			Attributes.BonusMana = 8;
			Attributes.RegenHits = 3;
            ClothingAttributes.SelfRepair = 3;
            AddRandomSkillPair();

        }

        protected void AddRandomSkillPair()
        { 
            int numpairs = 16;
            switch(Utility.Random(numpairs))
            {
                default:
                case 0:
                    SkillBonuses.SetValues(0, SkillName.Anatomy, 10);
                    SkillBonuses.SetValues(1, SkillName.Healing, 10);
                    break;
                case 1:
                    SkillBonuses.SetValues(0, SkillName.AnimalLore, 10);
                    SkillBonuses.SetValues(1, SkillName.AnimalTaming, 10);
                    break;
                case 16:
                    SkillBonuses.SetValues(0, SkillName.Archery, 10);
                    SkillBonuses.SetValues(1, SkillName.Archery, 10);
                    break;
                case 2:
                    SkillBonuses.SetValues(0, SkillName.Archery, 10);
                    SkillBonuses.SetValues(1, SkillName.Tactics, 10);
                    break;
                case 3:
                    SkillBonuses.SetValues(0, SkillName.Bushido, 10);
                    SkillBonuses.SetValues(1, SkillName.Parry, 10);
                    break;
                case 4:
                    SkillBonuses.SetValues(0, SkillName.Chivalry, 10);
                    SkillBonuses.SetValues(1, SkillName.MagicResist, 10);
                    break;
                case 5:
                    SkillBonuses.SetValues(0, SkillName.Discordance, 10);
                    SkillBonuses.SetValues(1, SkillName.Musicianship, 10);
                    break;
                case 6:
                    SkillBonuses.SetValues(0, SkillName.EvalInt, 10);
                    SkillBonuses.SetValues(1, SkillName.Magery, 10);
                    break;
                case 7:
                    SkillBonuses.SetValues(0, SkillName.Fencing, 10);
                    SkillBonuses.SetValues(1, SkillName.Tactics, 10);
                    break;
                case 8:
                    SkillBonuses.SetValues(0, SkillName.Macing, 10);
                    SkillBonuses.SetValues(1, SkillName.Tactics, 10);
                    break;
                case 9:
                    SkillBonuses.SetValues(0, SkillName.Necromancy, 10);
                    SkillBonuses.SetValues(1, SkillName.SpiritSpeak, 10);
                    break;
                case 10:
                    SkillBonuses.SetValues(0, SkillName.Ninjitsu, 10);
                    SkillBonuses.SetValues(1, SkillName.Stealth, 10);
                    break;
                case 11:
                    SkillBonuses.SetValues(0, SkillName.Peacemaking, 10);
                    SkillBonuses.SetValues(1, SkillName.Musicianship, 10);
                    break;
                case 12:
                    SkillBonuses.SetValues(0, SkillName.Provocation, 10);
                    SkillBonuses.SetValues(1, SkillName.Musicianship, 10);
                    break;
                case 13:
                    SkillBonuses.SetValues(0, SkillName.Swords, 10);
                    SkillBonuses.SetValues(1, SkillName.Tactics, 10);
                    break;
                case 14:
                    SkillBonuses.SetValues(0, SkillName.Stealth, 10);
                    SkillBonuses.SetValues(1, SkillName.Stealing, 10);
                    break;
                case 15:
                    SkillBonuses.SetValues(0, SkillName.Tinkering, 10);
                    SkillBonuses.SetValues(1, SkillName.Alchemy, 10);
                    break;
            }
        }
		public MarkOfTheTravesty( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					Resistances.Physical = 0;
					break;
				}
			}
		}
	}
}