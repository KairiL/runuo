

namespace Server.Items
{
    public class BlueDiamondRing : BaseRing
    {
        [Constructable]
        public BlueDiamondRing() : base(0x1F09)
        {
            Weight = 0.1;
            Name = "A Blue Diamond Ring";
            int maxProps = CraftUtil.GetBonusProps(5);
            switch (Utility.Random(4))
            {
                case 0:
                    Attributes.LowerRegCost = 10;
                    break;
                case 1:
                    Attributes.CastSpeed = 1;
                    break;
                case 2:
                    Attributes.CastRecovery = Utility.Random(1,3);
                    break;
                default:
                    Attributes.SpellDamage = 5;
                    break;
            }

            BaseRunicTool.ApplyAttributesTo(this, maxProps, 0, 100);

        }

        public BlueDiamondRing(Serial serial) : base(serial)
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