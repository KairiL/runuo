using System; 
using System.Collections;
using Server.Network; 
using Server.Mobiles; 
using Server.Items; 
using Server.Gumps;

namespace Server.Items
{
	
	public class CraftedFireColumnComponents : CraftedTrapComponents
	{

		[Constructable]
		public CraftedFireColumnComponents() : base()
		{
			Name = "Components for a Fire Column Trap";
			ItemID = 7867;
			Weight = 5;
			Hue = 45;
			Armed = false;
		}

        public override void CreateTrap(Map map, int x, int y, int z, Mobile from, int trapmod, double poisonskill, int trapskill, int trapuses,
   int rangeBonus, int radiusBonus, double delayBonus)
        {
            CraftedFireColumnTrap trap = new CraftedFireColumnTrap();

            trap.TrapOwner = from;
            trap.TrapPower += trapmod;
            trap.TriggerRange += rangeBonus;
            trap.DamageRange += radiusBonus;
            trap.UsesRemaining += trapuses;
            if (delayBonus > 0)
                trap.Delay = (TimeSpan.FromSeconds((trap.Delay.TotalSeconds) / delayBonus));

            trap.Poison = null;

            trap.MoveToWorld(new Point3D(x, y, z), map);

            from.SendMessage("You have configured the trap and concealed it at your location.");
        }

		public CraftedFireColumnComponents( Serial serial ) : base( serial )
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