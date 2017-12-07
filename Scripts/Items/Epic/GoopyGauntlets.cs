using System;
using Server;
using Server.Spells;
using Server.Regions;

namespace Server.Items
{
    public class GoopyGauntlets : Sandals
    {
        public override int ArtifactRarity { get { return 12; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        
        public override void OnDoubleClick(Mobile from)
        {
            //TODO: Checks
            Caster.Target = new InternalTarget(this);

        }

        public class InternalTarget : Target
        {

        }

        [Constructable]
        public GoopyGauntlets()
        {
            Weight = 5.0;
            Name = "Goopy Gauntlets";
            Attributes.BonusDex = -10;
            Attributes.Luck = 150;
            LootType = LootType.Cursed;
        }

        public GoopyGauntlets(Serial serial) : base(serial)
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
            
        }
    }
}