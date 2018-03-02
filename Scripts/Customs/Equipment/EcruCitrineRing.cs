

namespace Server.Items
{
    public class EcruCitrineRing : BaseRing
    {
        [Constructable]
        public EcruCitrineRing() : base(0x1F09)
        {
            Weight = 0.1;
            Name = "An Ecru Citrine Ring";
            int maxProps = CraftUtil.GetBonusProps(3)+2;
            switch (Utility.Random(2))
            {
                case 0:
                    if (Utility.RandomDouble() < .20)
                        Attributes.EnhancePotions = 50;
                    else
                        Attributes.EnhancePotions = 5;
                    break;
                default:
                    Attributes.BonusStr = 5;
                    break;
            }


            BaseRunicTool.ApplyAttributesTo(this, maxProps, 0, 100);

        }

        public EcruCitrineRing(Serial serial) : base(serial)
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