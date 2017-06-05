using System;
using Server;

namespace Server.Items
{
    public class PrismTicket : TempItem
    {
        private Point3D m_PointDest;
        private Map m_MapDest;
        private Point3D m_RegionCenter;
        private int m_RegionRange;
        

        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D PointDest
        {
            get { return m_PointDest; }
            set { m_PointDest = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Map MapDest
        {
            get { return m_MapDest; }
            set { m_MapDest = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Point3D RegionCenter
        {
            get { return m_RegionCenter; }
            set { m_RegionCenter = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int RegionRange
        {
            get { return m_RegionRange; }
            set { m_RegionRange = value; InvalidateProperties(); }
        }

        [Constructable]
        public PrismTicket()
            : this(TimeSpan.FromHours(12))
        {
        }

        [Constructable]
        public PrismTicket(int lifeSpan)
            : this(TimeSpan.FromSeconds(lifeSpan))
        {
        }

        public PrismTicket(TimeSpan lifeSpan) : base(0x14EF, lifeSpan)
        {
            Name = "Prism of Light Admission Ticket";
            LootType = LootType.Blessed;
            Hue = 150;
            //MapDest = Felucca;//Remove this line if you have Tram enabled
            PointDest = new Point3D(3785, 1097, 14); //Default teleport location, prism palace entrance
            RegionCenter = new Point3D(6526, 145, 14);//Center of region in which ticket can be used
            RegionRange = 100;//Range from center of region in which ticket can be used
        }

        public PrismTicket(Serial serial) : base(serial)
        {
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!from.InRange(RegionCenter, RegionRange))
                from.SendMessage("It doesn't do anything");
            else
            {
                from.SendMessage("Come visit us again!");
                from.Location = PointDest;
            }
        }
    }    
}