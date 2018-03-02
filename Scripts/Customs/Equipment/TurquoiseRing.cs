

namespace Server.Items
{
    public class TurquoiseRing : BaseRing
    {
        [Constructable]
        public TurquoiseRing() : base(0x1F09)
        {
            Weight = 0.1;
            Name = "A Turquoise Ring";
            int maxProps = CraftUtil.GetBonusProps(3)+2;
            if ( Utility.RandomDouble() > .95 )
                Attributes.WeaponSpeed = 5;
            else
                Attributes.WeaponDamage = 15;
            BaseRunicTool.ApplyAttributesTo(this, maxProps, 0, 90);

        }

        public TurquoiseRing(Serial serial) : base(serial)
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