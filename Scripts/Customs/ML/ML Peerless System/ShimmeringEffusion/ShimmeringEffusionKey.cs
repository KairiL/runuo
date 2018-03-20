using System;
using System.Net;
using Server;
using Server.Accounting;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Regions;
using System.Collections;
using Server.Engines.PartySystem;

namespace Server.Items
{

	public class ShimmeringEffusionKey : Item
	{

		[Constructable]
		public ShimmeringEffusionKey() : this( null )
		{
		}
		
		[Constructable]
		public ShimmeringEffusionKey( string name ) : base( 0x2258 )
		{
			Name = "Shimmering Effusion Teleporter. Dont Spawn Here till you use this!";
			LootType = LootType.Blessed;
            Timer.DelayCall(TimeSpan.FromHours(3.0), new TimerStateCallback(DeleteKey), this);
        }

        public void DeleteKey(object state)
        {
            Item from = (Item)state;
            from.Delete();
        }

        public ShimmeringEffusionKey( Serial serial ) : base( serial )
		{
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			ArrayList list = new ArrayList();
			
			foreach ( Mobile m in World.Mobiles.Values )
			{
				if ( m is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)m;
					
					if ( bc is ShimmeringEffusion )
						list.Add( bc );
				}
			}
			if ( list.Count > 0 )
				from.SendMessage( "A Party is Already in Battle With Shimmering Effusion. Please Wait" );
			else
			{
				from.SendGump( new ShimmeringEffusionGump( from, this ) );
			}
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            Timer.DelayCall(TimeSpan.FromHours(3.0), new TimerStateCallback(DeleteKey), this);
        }
	}

	
	
	public class ShimmeringEffusionGump : Gump
	{
	    private Mobile m_Mobile;
	    private Item m_Deed;
		
	    public ShimmeringEffusionGump( Mobile from, Item deed ) : base( 30, 20 )
	    {
		    m_Mobile = from;
		    m_Deed = deed;
						
		    Account a = from.Account as Account;
			
		    AddImageTiled(  54, 33, 369, 400, 2624 );
		    AddAlphaRegion( 54, 33, 369, 400 );
		    AddImageTiled( 416, 39, 44, 389, 203 );			
		    AddImage( 97, 49, 9005 );
		    AddImageTiled( 58, 39, 29, 390, 10460 );
		    AddImageTiled( 412, 37, 31, 389, 10460 );
		    AddLabel( 140, 60, 0x34, "Destroy Shimmering Effusion" );
		    AddImage( 430, 9, 10441);
		    AddImageTiled( 40, 38, 17, 391, 9263 );
		    AddImage( 6, 25, 10421 );
		    AddImage( 34, 12, 10420 );
		    AddImageTiled( 94, 25, 342, 15, 10304 );
		    AddImageTiled( 40, 427, 415, 16, 10304 );
		    AddImage( -10, 314, 10402 );
		    AddImage( 56, 150, 10411 );
		    AddImage( 136, 84, 96 );
		    AddImage( 215, 150, 5536 );

		    AddButton( 225, 390, 0xF7, 0xF8, 1, GumpButtonType.Reply, 0 );
	    }
		
	    public override void OnResponse( NetState state, RelayInfo info )
	    {
		    Mobile from = state.Mobile;
			
		    switch( info.ButtonID )
		    {
			    case 0:
			    {
				    from.CloseGump( typeof(ShimmeringEffusionGump) );
				    break;
			    }
			    case 1: //Case uses the ActionIDs define above. Case 1 defines the actions for the button with the action id 1 
			    {
				    Party party = Party.Get( from );

				    if( party != null )
				    {
					    for( int i = 0; i < party.Count; i++ )
					    {
						    Mobile m = party[ i ].Mobile;

						    if( Utility.InRange( from.Location, m.Location, 6 ) )
						    {
							    m.MoveToWorld( new Point3D( 6538, 117, -20 ), Map.Felucca );
						    }
					    }
				    }
				    else
				    {
					    from.MoveToWorld( new Point3D(6538, 117, -20), Map.Felucca);
                    }

				    ShimmeringEffusion dh = new ShimmeringEffusion();
                    dh.MoveToWorld( new Point3D(6510, 119, -20), Map.Felucca);
				    m_Deed.Delete();

                    break;
			    }
			}
		}
	}
}