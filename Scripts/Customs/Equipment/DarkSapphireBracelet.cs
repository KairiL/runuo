

namespace Server.Items
{
    public class DarkSapphireBracelet : BaseBracelet
    {
        [Constructable]
        public DarkSapphireBracelet() : base(0x1086)
        {
            Weight = 0.1;
            Name = "A Brilliant Amber Bracelet";
            int maxProps = CraftUtil.GetBonusProps(4) + 1;
            if (Utility.RandomDouble() > .5)
                Resistances.Cold = 10;
            else
                Attributes.RegenMana = 2;

            BaseRunicTool.ApplyAttributesTo(this, maxProps, 0, 100);

        }

        public DarkSapphireBracelet(Serial serial) : base(serial)
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