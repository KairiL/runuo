using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Prompts;
using Server.Network;
using Server.ContextMenus;
using Server.Multis;

namespace Server.Mobiles
{

	public class TailorRumor
	{
		private string m_Message;
		private string m_Keyword;

		public string Message{ get{ return m_Message; } set{ m_Message = value; } }
		public string Keyword{ get{ return m_Keyword; } set{ m_Keyword = value; } }

		public TailorRumor( string message, string keyword )
		{
			m_Message = message;
			m_Keyword = keyword;
		}

		public static TailorRumor Deserialize( GenericReader reader )
		{
			if ( !reader.ReadBool() )
				return null;

			return new TailorRumor( reader.ReadString(), reader.ReadString() );
		}

		public static void Serialize( GenericWriter writer, TailorRumor rumor )
		{
			if ( rumor == null )
			{
				writer.Write( false );
			}
			else
			{
				writer.Write( true );
				writer.Write( rumor.m_Message );
				writer.Write( rumor.m_Keyword );
			}
		}
	}

	public class ManageTailorEntry : ContextMenuEntry
	{
		private Mobile m_From;
		private PlayerTailor m_Tailor;

		public ManageTailorEntry( Mobile from, PlayerTailor tailor ) : base( 6151, 12 )
		{
			m_From = from;
			m_Tailor = tailor;
		}

		public override void OnClick()
		{
			m_Tailor.BeginManagement( m_From );
		}
	}

	public class PlayerTailor : Tailor
	{
		private Mobile m_Owner;
		private BaseHouse m_House;
		private string m_TipMessage;

		private TailorRumor[] m_Rumors;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}

		public BaseHouse House
		{
			get{ return m_House; }
			set
			{
				if ( m_House != null )
					m_House.PlayerBarkeepers.Remove( this );

				if ( value != null )
					value.PlayerBarkeepers.Add( this );

				m_House = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string TipMessage
		{
			get{ return m_TipMessage; }
			set{ m_TipMessage = value; }
		}

		public override bool IsActiveBuyer{ get{ return false; } }
		public override bool IsActiveSeller{ get{ return ( m_SBInfos.Count > 0 ); } }

		public override bool DisallowAllMoves{ get{ return true; } }
		public override bool NoHouseRestrictions{ get{ return true; } }

		public TailorRumor[] Rumors{ get{ return m_Rumors; } }

		public override VendorShoeType ShoeType{ get{ return Utility.RandomBool() ? VendorShoeType.ThighBoots : VendorShoeType.Boots; } }

		public override bool GetGender()
		{
			return false; // always starts as male
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new HalfApron( Utility.RandomBrightHue() ) );

			Container pack = this.Backpack;

			if ( pack != null )
				pack.Delete();
		}

		public override void InitBody()
		{
			base.InitBody();

			if ( BodyValue == 0x340 || BodyValue == 0x402 )
				Hue = 0;
			else
				Hue = 0x83F4; // hue is not random

			Container pack = this.Backpack;

			if ( pack != null )
				pack.Delete();
		}

		public PlayerTailor( Mobile owner, BaseHouse house ) : base()
		{
			m_Owner = owner;
			House = house;
			m_Rumors = new TailorRumor[3];

			LoadSBInfo();
		}

		public override bool HandlesOnSpeech(Mobile from)
		{
			if ( InRange( from, 3 ) )
				return true;

			return base.HandlesOnSpeech (from);
		}

		private Timer m_NewsTimer;

		private void ShoutNews_Callback( object state )
		{
			object[] states = (object[])state;
			TownCrierEntry tce = (TownCrierEntry)states[0];
			int index = (int)states[1];

			if ( index < 0 || index >= tce.Lines.Length )
			{
				if ( m_NewsTimer != null )
					m_NewsTimer.Stop();

				m_NewsTimer = null;
			}
			else
			{
				PublicOverheadMessage( MessageType.Regular, 0x3B2, false, tce.Lines[index] );
				states[1] = index + 1;
			}
		}

		public override void OnAfterDelete()
		{
			base.OnAfterDelete();

			House = null;
		}

		public override bool OnBeforeDeath()
		{
			if ( !base.OnBeforeDeath() )
				return false;

			Item shoes = this.FindItemOnLayer( Layer.Shoes );

			if ( shoes is Sandals )
				shoes.Hue = 0;

			return true;
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			base.OnSpeech( e );

			if ( !e.Handled && InRange( e.Mobile, 3 ) )
			{
				if ( m_NewsTimer == null && e.HasKeyword( 0x30 ) ) // *news*
				{
					TownCrierEntry tce = GlobalTownCrierEntryList.Instance.GetRandomEntry();

					if ( tce == null )
					{
						PublicOverheadMessage( MessageType.Regular, 0x3B2, 1005643 ); // I have no news at this time.
					}
					else
					{
						m_NewsTimer = Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 3.0 ), new TimerStateCallback( ShoutNews_Callback ), new object[]{ tce, 0 } );

						PublicOverheadMessage( MessageType.Regular, 0x3B2, 502978 ); // Some of the latest news!
					}
				}

				for ( int i = 0; i < m_Rumors.Length; ++i )
				{
					TailorRumor rumor = m_Rumors[i];

					if ( rumor == null )
						continue;

					string keyword = rumor.Keyword;

					if ( keyword == null || (keyword = keyword.Trim()).Length == 0 )
						continue;

					if ( Insensitive.Equals( keyword, e.Speech ) )
					{
						string message = rumor.Message;

						if ( message == null || (message = message.Trim()).Length == 0 )
							continue;

						PublicOverheadMessage( MessageType.Regular, 0x3B2, false, message );
					}
				}
			}
		}

		public override bool CheckGold( Mobile from, Item dropped )
		{
			if ( dropped is Gold )
			{
				Gold g = (Gold)dropped;

				if ( g.Amount > 50 )
				{
					PrivateOverheadMessage( MessageType.Regular, 0x3B2, false, "I cannot accept so large a tip!", from.NetState );
				}
				else
				{
					string tip = m_TipMessage;

					if ( tip == null || (tip = tip.Trim()).Length == 0 )
					{
						PrivateOverheadMessage( MessageType.Regular, 0x3B2, false, "It would not be fair of me to take your money and not offer you information in return.", from.NetState );
					}
					else
					{
						Direction = GetDirectionTo( from );
						PrivateOverheadMessage( MessageType.Regular, 0x3B2, false, tip, from.NetState );

						g.Delete();
						return true;
					}
				}
			}

			return false;
		}

		public bool IsOwner( Mobile from )
		{
			if ( from == null || from.Deleted || this.Deleted )
				return false;

			if ( from.AccessLevel > AccessLevel.GameMaster )
				return true;

			return ( m_Owner == from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			if ( IsOwner( from ) && from.InLOS( this ) )
				list.Add( new ManageTailorEntry( from, this ) );
		}

		public void BeginManagement( Mobile from )
		{
			if ( !IsOwner( from ) )
				return;

			from.SendGump( new TailorGump( from, this ) );
		}

		public void Dismiss()
		{
			Delete();
		}

		public void BeginChangeTitle( Mobile from )
		{
			from.SendGump( new TailorTitleGump( from, this ) );
		}

		public void EndChangeTitle( Mobile from, string title, bool vendor )
		{
			this.Title = title;

			LoadSBInfo();
		}

		public void CancelChangeTitle( Mobile from )
		{
			from.SendGump( new TailorGump( from, this ) );
		}

		public void BeginChangeAppearance( Mobile from )
		{
			from.CloseGump( typeof( PlayerVendorCustomizeGump ) );
			from.SendGump( new PlayerVendorCustomizeGump( this, from ) );
		}

		public void ChangeGender( Mobile from )
		{
			Female = !Female;

			if ( Female )
			{
				Body = 401;
				Name = NameList.RandomName( "female" );

				FacialHairItemID = 0;
			}
			else
			{
				Body = 400;
				Name = NameList.RandomName( "male" );
			}
		}

		private List<SBInfo> m_SBInfos = new List<SBInfo>(); 
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } } 

		public override void InitSBInfo()
		{
			if ( Title == "the tailor" || Title == "the tailor" )
			{
				if ( m_SBInfos.Count == 0 )
					m_SBInfos.Add( new SBTailor() );
			}
			else
			{
				m_SBInfos.Clear();
			}
		}

		public PlayerTailor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version;

			writer.Write( (Item) m_House );

			writer.Write( (Mobile) m_Owner );

			writer.WriteEncodedInt( (int) m_Rumors.Length );

			for ( int i = 0; i < m_Rumors.Length; ++i )
				TailorRumor.Serialize( writer, m_Rumors[i] );

			writer.Write( (string) m_TipMessage );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					House = (BaseHouse) reader.ReadItem();

					goto case 0;
				}
				case 0:
				{
					m_Owner = reader.ReadMobile();

					m_Rumors = new TailorRumor[reader.ReadEncodedInt()];

					for ( int i = 0; i < m_Rumors.Length; ++i )
						m_Rumors[i] = TailorRumor.Deserialize( reader );

					m_TipMessage = reader.ReadString();

					break;
				}
			}

			if ( version < 1 )
				Timer.DelayCall( TimeSpan.Zero, new TimerCallback( UpgradeFromVersion0 ) );
		}

		private void UpgradeFromVersion0()
		{
			House = BaseHouse.FindHouseAt( this );
		}
	}

	public class TailorTitleGump : Gump
	{
		private Mobile m_From;
		private PlayerTailor m_Tailor;

		private class Entry
		{
			public string m_Description;
			public string m_Title;
			public bool m_Vendor;

			public Entry( string desc ) : this( desc, String.Format( "the {0}", desc.ToLower() ), false )
			{
			}

			public Entry( string desc, bool vendor ) : this( desc, String.Format( "the {0}", desc.ToLower() ), vendor )
			{
			}

			public Entry( string desc, string title, bool vendor )
			{
				m_Description = desc;
				m_Title = title;
				m_Vendor = vendor;
			}
		}

		private static Entry[] m_Entries = new Entry[]
			{
				new Entry( "Alchemist" ),
				new Entry( "Animal Tamer" ),
				new Entry( "Apothecary" ),
				new Entry( "Artist" ),
				new Entry( "Baker", true ),
				new Entry( "Bard" ),
				new Entry( "Barkeep", "the barkeeper", true ),
				new Entry( "Beggar" ),
				new Entry( "Tailor" ),
				new Entry( "Bounty Hunter" ),
				new Entry( "Brigand" ),
				new Entry( "Butler" ),
				new Entry( "Carpenter" ),
				new Entry( "Chef", true ),
				new Entry( "Commander" ),
				new Entry( "Curator" ),
				new Entry( "Drunkard" ),
				new Entry( "Farmer" ),
				new Entry( "Fisherman" ),
				new Entry( "Gambler" ),
				new Entry( "Gypsy" ),
				new Entry( "Herald" ),
				new Entry( "Herbalist" ),
				new Entry( "Hermit" ),
				new Entry( "Innkeeper", true ),
				new Entry( "Jailor" ),
				new Entry( "Jester" ),
				new Entry( "Librarian" ),
				new Entry( "Mage" ),
				new Entry( "Mercenary" ),
				new Entry( "Merchant" ),
				new Entry( "Messenger" ),
				new Entry( "Miner" ),
				new Entry( "Monk" ),
				new Entry( "Noble" ),
				new Entry( "Paladin" ),
				new Entry( "Peasant" ),
				new Entry( "Pirate" ),
				new Entry( "Prisoner" ),
				new Entry( "Prophet" ),
				new Entry( "Ranger" ),
				new Entry( "Sage" ),
				new Entry( "Sailor" ),
				new Entry( "Scholar" ),
				new Entry( "Scribe" ),
				new Entry( "Sentry" ),
				new Entry( "Servant" ),
				new Entry( "Shepherd" ),
				new Entry( "Soothsayer" ),
				new Entry( "Stoic" ),
				new Entry( "Storyteller" ),
				new Entry( "Tailor" ),
				new Entry( "Thief" ),
				new Entry( "Tinker" ),
				new Entry( "Town Crier" ),
				new Entry( "Treasure Hunter" ),
				new Entry( "Waiter", true ),
				new Entry( "Warrior" ),
				new Entry( "Watchman" ),
				new Entry( "No Title", null, false )
			};

		private void RenderBackground()
		{
			AddPage( 0 );

			AddBackground( 30, 40, 585, 410, 5054 );

			AddImage( 30, 40, 9251 );
			AddImage( 180, 40, 9251 );
			AddImage( 30, 40, 9253 );
			AddImage( 30, 130, 9253 );
			AddImage( 598, 40, 9255 );
			AddImage( 598, 130, 9255 );
			AddImage( 30, 433, 9257 );
			AddImage( 180, 433, 9257 );
			AddImage( 30, 40, 9250 );
			AddImage( 598, 40, 9252 );
			AddImage( 598, 433, 9258 );
			AddImage( 30, 433, 9256 );

			AddItem( 30, 40, 6816 );
			AddItem( 30, 125, 6817 );
			AddItem( 30, 233, 6817 );
			AddItem( 30, 341, 6817 );
			AddItem( 580, 40, 6814 );
			AddItem( 588, 125, 6815 );
			AddItem( 588, 233, 6815 );
			AddItem( 588, 341, 6815 );

			AddImage( 560, 20, 1417 );
			AddItem( 580, 44, 4033 );

			AddBackground( 183, 25, 280, 30, 5054 );

			AddImage( 180, 25, 10460 );
			AddImage( 434, 25, 10460 );

			AddHtml( 223, 32, 200, 40, "BARKEEP CUSTOMIZATION MENU", false, false );
			AddBackground( 243, 433, 150, 30, 5054 );

			AddImage( 240, 433, 10460 );
			AddImage( 375, 433, 10460 );

			AddImage( 80, 398, 2151 );
			AddItem( 72, 406, 2543 );

			AddHtml( 110, 412, 180, 25, "sells food and drink", false, false );
		}

		private void RenderPage( Entry[] entries, int page )
		{
			AddPage( 1 + page );

			AddHtml( 430, 70, 180, 25, String.Format( "Page {0} of {1}", page + 1, (entries.Length + 19) / 20 ), false, false );

			for ( int count = 0, i = (page * 20); count < 20 && i < entries.Length; ++count, ++i )
			{
				Entry entry = entries[i];

				AddButton( 80 + ((count / 10) * 260), 100 + ((count % 10) * 30), 4005, 4007, 2 + i, GumpButtonType.Reply, 0 );
				AddHtml( 120 + ((count / 10) * 260), 100 + ((count % 10) * 30), entry.m_Vendor ? 148 : 180, 25, entry.m_Description, true, false );

				if ( entry.m_Vendor )
				{
					AddImage( 270 + ((count / 10) * 260), 98 + ((count % 10) * 30), 2151 );
					AddItem( 262 + ((count / 10) * 260), 106 + ((count % 10) * 30), 2543 );
				}
			}

			AddButton( 340, 400, 4005, 4007, 0, GumpButtonType.Page, 1 + ((page + 1) % ((entries.Length + 19) / 20)) );
			AddHtml( 380, 400, 180, 25, "More Job Titles", false, false );

			AddButton( 338, 437, 4014, 4016, 1, GumpButtonType.Reply, 0 );
			AddHtml( 290, 440, 35, 40, "Back", false, false );
		}

		public TailorTitleGump( Mobile from, PlayerTailor tailor ) : base( 0, 0 )
		{
			m_From = from;
			m_Tailor = tailor;

			from.CloseGump( typeof( TailorGump ) );
			from.CloseGump( typeof( TailorTitleGump ) );

			Entry[] entries = m_Entries;

			RenderBackground();

			int pageCount = (entries.Length + 19) / 20;

			for ( int i = 0; i < pageCount; ++i )
				RenderPage( entries, i );
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			int buttonID = info.ButtonID;

			if ( buttonID > 0 )
			{
				--buttonID;

				if ( buttonID > 0 )
				{
					--buttonID;

					if ( buttonID >= 0 && buttonID < m_Entries.Length )
						m_Tailor.EndChangeTitle( m_From, m_Entries[buttonID].m_Title, m_Entries[buttonID].m_Vendor );
				}
				else
				{
					m_Tailor.CancelChangeTitle( m_From );
				}
			}
		}
	}

	public class TailorGump : Gump
	{
		private Mobile m_From;
		private PlayerTailor m_Tailor;

		public void RenderBackground()
		{
			AddPage( 0 );

			AddBackground( 30, 40, 585, 410, 5054 );

			AddImage( 30, 40, 9251 );
			AddImage( 180, 40, 9251 );
			AddImage( 30, 40, 9253 );
			AddImage( 30, 130, 9253 );
			AddImage( 598, 40, 9255 );
			AddImage( 598, 130, 9255 );
			AddImage( 30, 433, 9257 );
			AddImage( 180, 433, 9257 );
			AddImage( 30, 40, 9250 );
			AddImage( 598, 40, 9252 );
			AddImage( 598, 433, 9258 );
			AddImage( 30, 433, 9256 );

			AddItem( 30, 40, 6816 );
			AddItem( 30, 125, 6817 );
			AddItem( 30, 233, 6817 );
			AddItem( 30, 341, 6817 );
			AddItem( 580, 40, 6814 );
			AddItem( 588, 125, 6815 );
			AddItem( 588, 233, 6815 );
			AddItem( 588, 341, 6815 );

			AddBackground( 183, 25, 280, 30, 5054 );

			AddImage( 180, 25, 10460 );
			AddImage( 434, 25, 10460 );
			AddImage( 560, 20, 1417 );

			AddHtml( 223, 32, 200, 40, "BARKEEP CUSTOMIZATION MENU", false, false );
			AddBackground( 243, 433, 150, 30, 5054 );

			AddImage( 240, 433, 10460 );
			AddImage( 375, 433, 10460 );
		}

		public void RenderCategories()
		{
			AddPage( 1 );

			AddButton( 130, 200, 4005, 4007, 0, GumpButtonType.Page, 8 );
			AddHtml( 170, 200, 200, 40, "Customize your barkeep", false, false );

			AddButton( 130, 280, 4005, 4007, 0, GumpButtonType.Page, 3 );
			AddHtml( 170, 280, 200, 40, "Dismiss your barkeep", false, false );

			AddButton( 338, 437, 4014, 4016, 0, GumpButtonType.Reply, 0 );
			AddHtml( 290, 440, 35, 40, "Back", false, false );

			AddItem( 574, 43, 5360 );
		}

		public void RenderDismissConfirmation()
		{
			AddPage( 3 );

			AddHtml( 170, 160, 380, 20, "Are you sure you want to dismiss your tailor?", false, false );

			AddButton( 205, 280, 4005, 4007, GetButtonID( 0, 0 ), GumpButtonType.Reply, 0 );
			AddHtml( 240, 280, 100, 20,@"Yes", false, false );

			AddButton( 395, 280, 4005, 4007, 0, GumpButtonType.Reply, 0 );
			AddHtml( 430, 280, 100, 20, "No", false, false );

			AddButton( 338, 437, 4014, 4016, 0, GumpButtonType.Page, 1 );
			AddHtml( 290, 440, 35, 40, "Back", false, false );

			AddItem( 574, 43, 5360 );
			AddItem( 584, 34, 6579 );
		}
        

		private int GetButtonID( int type, int index )
		{
			return 1 + (index * 6) + type;
		}

		private void RenderAppearanceCategories()
		{
			AddPage( 8 );

			AddButton( 130, 120, 4005, 4007, GetButtonID( 5, 0 ), GumpButtonType.Reply, 0 );
			AddHtml( 170, 120, 120, 20, "Title", false, false );

			if ( m_Tailor.BodyValue != 0x340 && m_Tailor.BodyValue != 0x402 ) {			
				AddButton( 130, 200, 4005, 4007, GetButtonID( 5, 1 ), GumpButtonType.Reply, 0 );
				AddHtml( 170, 200, 120, 20, "Appearance", false, false );

				AddButton( 130, 280, 4005, 4007, GetButtonID( 5, 2 ), GumpButtonType.Reply, 0 );
				AddHtml( 170, 280, 120, 20, "Male / Female", false, false );

				AddButton( 338, 437, 4014, 4016, 0, GumpButtonType.Page, 1 );
				AddHtml( 290, 440, 35, 40, "Back", false, false );
			}

			AddItem( 580, 44, 4033 );
		}

		public TailorGump( Mobile from, PlayerTailor tailor ) : base( 0, 0 )
		{
			m_From = from;
			m_Tailor = tailor;

			from.CloseGump( typeof( TailorGump ) );
			from.CloseGump( typeof( TailorTitleGump ) );

			RenderBackground();
			RenderCategories();
			RenderDismissConfirmation();
			RenderAppearanceCategories();
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( !m_Tailor.IsOwner( m_From ) )
				return;

			int index = info.ButtonID - 1;

			if ( index < 0 )
				return;

			int type = index % 6;
			index /= 6;

			switch ( type )
			{
				case 0: // Controls
				{
					switch ( index )
					{
						case 0: // Dismiss
						{
							m_Tailor.Dismiss();
							break;
						}
					}

					break;
				}
				case 1: // Change message
				{
					break;
				}
				case 2: // Remove message
				{
					break;
				}
				case 3: // Change tip
				{
					break;
				}
				case 4: // Remove tip
				{
					break;
				}
				case 5: // Appearance category selection
				{
					switch ( index )
					{
						case 0: m_Tailor.BeginChangeTitle( m_From ); break;
						case 1: m_Tailor.BeginChangeAppearance( m_From ); break;
						case 2: m_Tailor.ChangeGender( m_From ); break;
					}

					break;
				}
			}
		}
	}
}