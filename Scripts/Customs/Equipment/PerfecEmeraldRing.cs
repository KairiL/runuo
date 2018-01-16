namespace Server.Items
{
    public class PerfectEmeraldRing : SilverRing
    {
        [Constructable]
        public PerfectEmeraldRing()
        {
            Weight = 0.1;
            Name = "A Perfect Emerald Ring";
            int maxProps = CraftUtil.GetBonusProps(5);
            switch (Utility.Random(2))
            {
                case 0:
                    Resistances.Poison = 10;
                    break;
                default:
                    Attributes.LowerManaCost = 10;
                    break;
            }
            
            BaseRunicTool.ApplyAttributesTo(this, maxProps, 0, 80);

        }

        

        public PerfectEmeraldRing(Serial serial) : base(serial)
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