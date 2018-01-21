

namespace Server.Items
{
    public class WhitePearlBracelet : BaseBracelet
    {
        [Constructable]
        public WhitePearlBracelet() : base(0x1086)
        {
            Weight = 0.1;
            Name = "A White Pearl Bracelet";
            int maxProps = CraftUtil.GetBonusProps(6);
            switch (Utility.Random(4))
            {
                case 0:
                    Attributes.LowerRegCost = (1 + Utility.Random(2) )*10;
                    break;
                case 1:
                    Attributes.CastSpeed = 1;
                    break;
                case 2:
                    Attributes.CastRecovery = Utility.Random(1, 3);
                    break;
                default:
                    Attributes.NightSight = 1;
                    break;
            }

            BaseRunicTool.ApplyAttributesTo(this, maxProps, 0, 100);

        }

        public WhitePearlBracelet(Serial serial) : base(serial)
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