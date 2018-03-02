

namespace Server.Items
{
    public class FireRubyBracelet : BaseBracelet
    {
        [Constructable]
        public FireRubyBracelet() : base(0x1086)
        {
            Weight = 0.1;
            Name = "A Brilliant Amber Bracelet";
            int maxProps = CraftUtil.GetBonusProps(4) + 1;
            if (Utility.RandomDouble() > .5)
                Resistances.Fire = 10;
            else
                Attributes.RegenHits = 2;

            BaseRunicTool.ApplyAttributesTo(this, maxProps, 0, 100);

        }

        public FireRubyBracelet(Serial serial) : base(serial)
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