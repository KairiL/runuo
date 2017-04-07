using System; 
using System.Collections;
using Server.Network; 
using Server.Mobiles; 
using Server.Items; 
using Server.Gumps;

namespace Server.Items
{
	
	public class CraftedTeleporterComponents : Item
	{

		private bool m_Armed;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Armed
		{
			get
			{
				return m_Armed;
			}
			set
			{
				m_Armed = value;
			}
		}


		[Constructable]
		public CraftedTeleporterComponents() : base( 0x1EBB)
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

				ArrayList trapshere = CheckTrap( m_pnt, map, 0 );
				if ( trapshere.Count > 0 )
				{
					from.SendMessage( "There is already a trap here." ); 
					return;
				}

				int trapskill = (int)Math.Round(from.Skills.Tinkering.Value) + (int)Math.Round(from.Skills.Inscribe.Value);
				int trapmod = trapskill - 50;
				int trapuses = (trapskill / 25);

				CraftedTeleporter trap = new CraftedTeleporter(); 

				trap.TrapOwner = from;
				trap.TrapPower += trapmod;
				trap.UsesRemaining += trapuses;				

				trap.MoveToWorld( new Point3D( x, y, z ), map );

				from.SendMessage("You have configured the trap and concealed it at your location.");
		
				this.Delete();
			}

		}

		public static ArrayList CheckTrap( Point3D pnt, Map map, int range )
		{
			ArrayList traps = new ArrayList();

			IPooledEnumerable eable = map.GetItemsInRange( pnt, range );
			foreach ( Item trap in eable ) 
			{ 
				if ( ( trap != null ) && ( trap is BaseTrap ) )
					traps.Add( (BaseTrap)trap ); 
			} 
			eable.Free();

			return traps;
		}


		public CraftedTeleporterComponents( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
			writer.Write( m_Armed );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{

					m_Armed = reader.ReadBool();

					break;
				}
			}
		}
	}
}