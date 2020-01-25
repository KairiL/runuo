using System; 
using System.Collections;
using Server.Network; 
using Server.Mobiles; 
using Server.Items; 
using Server.Gumps;

namespace Server.Items
{
	
	public class CraftedTeleporterComponents : CraftedTrapComponents
	{

		[Constructable]
		public CraftedTeleporterComponents() : base()
		{
			Name = "Components for a teleporter trap";
			ItemID = 7867;
			Weight = 5;
			Hue = 6;
			Armed = false;
		}


		public override void OnDoubleClick( Mobile from ) 
		{ 
			if ( this.Armed == false )
			{
				this.Armed = true;
				from.SendMessage("This trap is now ARMED. Double-clicking these components again will place the trap.");
				this.Name = "Components for a teleporter trap [ARMED]";
			}

			else if ( this.Armed == true )
			{
				Map map = from.Map;
				Point3D m_pnt = from.Location;

				int x = from.X;
				int y = from.Y;
				int z = from.Z;

				ArrayList trapshere = CheckTrap( m_pnt, map, 3 );
				if ( trapshere.Count > 0 )
				{
					from.SendMessage( "There is already a trap here." ); 
					return;
				}

				int trapskill = (int)Math.Round(from.Skills.Tinkering.Value) + (int)Math.Round(from.Skills.Inscribe.Value);
				int trapmod = trapskill - 50;
				int trapuses = (int)(from.Skills.Tailoring.Value + (from.Skills.Carpentry.Value + (from.Skills.ArmsLore.Value + trapskill) / 2) / 4) / 2 + Utility.RandomMinMax(1, 3);
                int delayBonus = (int)(from.Skills.Blacksmith.Value + from.Skills.Carpentry.Value) / 100;

                if (from.Skills.Blacksmith.Value >= 120)
                    delayBonus += 1;

                CraftedTeleporter trap = new CraftedTeleporter(); 

				trap.TrapOwner = from;
				trap.TrapPower += trapmod;
				trap.UsesRemaining += trapuses;
                trap.Delay -= TimeSpan.FromSeconds(delayBonus);

                trap.MoveToWorld( new Point3D( x, y, z ), map );

				from.SendMessage("You have configured the trap and concealed it at your location.");
		
				this.Delete();
			}

		}

		public override void CreateTrap(Map map, int x, int y, int z, Mobile from, int trapmod, double poisonskill, int trapskill, int trapuses, 
            int rangeBonus, int radiusBonus, double delayBonus)
			{
			CraftedTeleporter trap = new CraftedTeleporter();

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
		public CraftedTeleporterComponents( Serial serial ) : base( serial )
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