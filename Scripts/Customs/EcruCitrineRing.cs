

namespace Server.Items
{
    public class EcruCitrineRing : BaseRing
    {
        [Constructable]
        public EcruCitrineRing() : base(0x1F09)
        {
            Weight = 0.1;

            BaseRunicTool.ApplyAttributesTo(this, 5, 0, 100);

            if (Utility.RandomDouble()<.05)
                Attributes.EnhancePotions = 50;
            else
                Attributes.EnhancePotions = 5;
            Attributes.BonusStr = 5;


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