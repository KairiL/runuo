using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
    public class TinkerBell : HolidayBell
    {
        [Constructable]
        public TinkerBell()
		{
            Name = "A Tinker Bell";
        }
        public TinkerBell(Serial serial) : base( serial )
		{
            Name = "A Tinker Bell";
        }
        public override void OnDoubleClick(Mobile from)
        {
            from.SendMessage("What would you like to jingle at?");
            from.Target = new InternalTarget();

        }
        private class InternalTarget : Target
        {
            public InternalTarget() : base(20, false, TargetFlags.None)
            {
            }
            protected override void OnTarget(Mobile from, object target)
            {
                if (target is CraftedTeleporter)
                {
                    if (((CraftedTeleporter)target).TrapOwner == from)
                    {
                        ((CraftedTeleporter)target).SetMap(from.Map);
                        ((CraftedTeleporter)target).SetPoint(from.Location);
                        from.SendMessage("You have linked the teleporter to your current location");
                    }
                    else
                        from.SendMessage("*jingle jingle*");
                }
                else
                    from.SendMessage("*jingle jingle*");
            }
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