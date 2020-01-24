using System; 
using System.Collections;
using Server.Network; 
using Server.Mobiles; 
using Server.Items; 
using Server.Gumps;

namespace Server.Items
{
	
	public class CraftedBoltComponents : CraftedTrapComponents
	{
		[Constructable]
		public CraftedBoltComponents() : base()
		{
			Name = "Components for a Bolt shooter";
			ItemID = 7867;
			Weight = 5;
			Hue = 543;
			Armed = false;
		}

        public override void CreateTrap(Map map, int x, int y, int z, Mobile from, int trapmod, double poisonskill, int trapskill, int trapuses, 
            int rangeBonus, int radiusBonus, double delayBonus)
        {
            CraftedBoltTrap trap = new CraftedBoltTrap();

            trap.TrapOwner = from;
            trap.TrapPower += trapmod;
            trap.TriggerRange += rangeBonus*3;
            trap.DamageRange += radiusBonus;
            trap.UsesRemaining += trapuses;
            if (delayBonus > 0)
                trap.Delay = (TimeSpan.FromSeconds((trap.Delay.TotalSeconds) / delayBonus));

            trap.Poison = null;

            trap.MoveToWorld(new Point3D(x, y, z), map);

            from.SendMessage("You have configured the trap and concealed it at your location.");

        }

		public CraftedBoltComponents( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					break;
				}
			}
		}
	}
}