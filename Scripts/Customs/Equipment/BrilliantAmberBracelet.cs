

namespace Server.Items
{
    public class BrilliantAmberBracelet : BaseBracelet
    {
        [Constructable]
        public BrilliantAmberBracelet() : base(0x1086)
        {
            Weight = 0.1;
            Name = "A Brilliant Amber Bracelet";
            int maxProps = CraftUtil.GetBonusProps(4)+1;
            switch (Utility.Random(4))
            {
                case 0:
                    Attributes.LowerRegCost = Utility.Random(1, 20);
                    break;
                case 1:
                    Attributes.CastSpeed = 1;
                    break;
                case 2:
                    Attributes.CastRecovery = Utility.Random(1, 3);
                    break;
                default:
                    Attributes.SpellDamage = Utility.Random(1, 3);
                    break;
            }

            BaseRunicTool.ApplyAttributesTo(this, maxProps, 0, 100);

        }

        public BrilliantAmberBracelet(Serial serial) : base(serial)
        {
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