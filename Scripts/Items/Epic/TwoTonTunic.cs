using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
    public class TwoTonTunic : PlateChest
    {
        public override int ArtifactRarity { get { return 12; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        [Constructable]
        public TwoTonTunic()
        {
            Weight = 285.7;
            Hue = 0x8E;
            Name = "Two-Ton Tunic";
            PhysicalBonus = 20;
            FireBonus = 15;
            ColdBonus = 15;
            PoisonBonus = 15;
            EnergyBonus = 15;
            Attributes.BonusHits = 10;
            Attributes.ReflectPhysical = 15;
            Attributes.BonusDex = -20;
            Attributes.Luck = 200;
            Attributes.DefendChance = 15;
            LootType = LootType.Cursed;
        }

        public override bool OnEquip(Mobile from)
        {
            return (from.CantWalk = base.OnEquip(from));
        }

        public override void OnRemoved(IEntity parent)
        {
            if (parent is PlayerMobile)
                ((PlayerMobile)parent).CantWalk = false;
            else if (parent is BaseCreature)
                ((BaseCreature)parent).CantWalk = false;
            base.OnRemoved(parent);

        }

            public TwoTonTunic(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (version < 1)
                PhysicalBonus = 0;
        }
    }
}