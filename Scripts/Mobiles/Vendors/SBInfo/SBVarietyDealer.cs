using System; 
using System.Collections.Generic; 
using Server.Items; 

namespace Server.Mobiles 
{ 
	public class SBVarietyDealer : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBVarietyDealer()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( Bandage ), 10, 20, 0xE21, 0 ) );

				Add( new GenericBuyInfo( typeof( BlankScroll ), 10, 999, 0x0E34, 0 ) );
                Add(new GenericBuyInfo(typeof(Bottle), 10, 100, 0xF0E, 0));

                Add( new GenericBuyInfo( typeof( NightSightPotion ), 30, 10, 0xF06, 0 ) );
				Add( new GenericBuyInfo( typeof( AgilityPotion ), 30, 10, 0xF08, 0 ) );
				Add( new GenericBuyInfo( typeof( StrengthPotion ), 30, 10, 0xF09, 0 ) );
				Add( new GenericBuyInfo( typeof( RefreshPotion ), 30, 10, 0xF0B, 0 ) );
				Add( new GenericBuyInfo( typeof( LesserCurePotion ), 30, 10, 0xF07, 0 ) );
				Add( new GenericBuyInfo( typeof( LesserHealPotion ), 30, 10, 0xF0C, 0 ) );
				Add( new GenericBuyInfo( typeof( LesserPoisonPotion ), 30, 10, 0xF0A, 0 ) );
				Add( new GenericBuyInfo( typeof( LesserExplosionPotion ), 42, 10, 0xF0D, 0 ) );

				Add( new GenericBuyInfo( typeof( Bolt ), 12, Utility.Random( 30, 60 ), 0x1BFB, 0 ) );
				Add( new GenericBuyInfo( typeof( Arrow ), 6, Utility.Random( 30, 60 ), 0xF3F, 0 ) );

				Add( new GenericBuyInfo( typeof( BlackPearl ),10, 999, 0xF7A, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Bloodmoss ), 10, 999, 0xF7B, 0 ) ); 
				Add( new GenericBuyInfo( typeof( MandrakeRoot ), 6, 999, 0xF86, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Garlic ), 6, 999, 0xF84, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Ginseng ), 6, 999, 0xF85, 0 ) ); 
				Add( new GenericBuyInfo( typeof( Nightshade ), 6, 999, 0xF88, 0 ) ); 
				Add( new GenericBuyInfo( typeof( SpidersSilk ), 6, 999, 0xF8D, 0 ) ); 
				Add( new GenericBuyInfo( typeof( SulfurousAsh ), 6, 999, 0xF8C, 0 ) );

                Add(new GenericBuyInfo(typeof(TinkersTools), 14, 20, 0x1EBC, 0));
                Add(new GenericBuyInfo(typeof(Board), 6, 20, 0x1BD7, 0));
                Add(new GenericBuyInfo(typeof(IronIngot), 10, 16, 0x1BF2, 0));

                Add( new GenericBuyInfo( typeof( BreadLoaf ), 14, 10, 0x103B, 0 ) );
				Add( new GenericBuyInfo( typeof( Backpack ), 30, 20, 0x9B2, 0 ) );

				Type[] types = Loot.RegularScrollTypes;

				int circles = 3;

				for ( int i = 0; i < circles*8 && i < types.Length; ++i )
				{
					int itemID = 0x1F2E + i;

					if ( i == 6 )
						itemID = 0x1F2D;
					else if ( i > 6 )
						--itemID;

					Add( new GenericBuyInfo( types[i], 12 + ((i / 8) * 10), 20, itemID, 0 ) );
				}

				if ( Core.AOS )
				{
					Add( new GenericBuyInfo( typeof( BatWing ), 6, 999, 0xF78, 0 ) );
					Add( new GenericBuyInfo( typeof( GraveDust ), 6, 999, 0xF8F, 0 ) );
					Add( new GenericBuyInfo( typeof( DaemonBlood ), 12, 999, 0xF7D, 0 ) );
					Add( new GenericBuyInfo( typeof( NoxCrystal ), 12, 999, 0xF8E, 0 ) );
					Add( new GenericBuyInfo( typeof( PigIron ), 10, 999, 0xF8A, 0 ) );

					Add( new GenericBuyInfo( typeof( NecromancerSpellbook ), 115, 10, 0x2253, 0 ) );
				}

				Add( new GenericBuyInfo( typeof( RecallRune ), 15, 10, 0x1f14, 0 ) );
				Add( new GenericBuyInfo( typeof( Spellbook ), 18, 10, 0xEFA, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( Bandage ), 6 );

				Add( typeof( BlankScroll ), 6 );

				Add( typeof( NightSightPotion ), 14 );
				Add( typeof( AgilityPotion ), 14 );
				Add( typeof( StrengthPotion ), 14 );
				Add( typeof( RefreshPotion ), 14 );
				Add( typeof( LesserCurePotion ), 14 );
				Add( typeof( LesserHealPotion ), 14 );
				Add( typeof( LesserPoisonPotion ), 14 );
				Add( typeof( LesserExplosionPotion ), 20 );

				Add( typeof( Bolt ), 7 );
				Add( typeof( Arrow ), 4 );

				Add( typeof( BlackPearl ), 6 );
				Add( typeof( Bloodmoss ), 6 );
				Add( typeof( MandrakeRoot ), 4 );
				Add( typeof( Garlic ), 4 );
				Add( typeof( Ginseng ), 4 );
				Add( typeof( Nightshade ), 4 );
				Add( typeof( SpidersSilk ), 4 );
				Add( typeof( SulfurousAsh ), 4 );

				Add( typeof( BreadLoaf ), 8 );
				Add( typeof( Backpack ), 7 );
				Add( typeof( RecallRune ), 8 );
				Add( typeof( Spellbook ), 9 );
				Add( typeof( BlankScroll ), 3 );

				if ( Core.AOS )
				{
					Add( typeof( BatWing ), 4 );
					Add( typeof( GraveDust ), 4 );
					Add( typeof( DaemonBlood ), 7 );
					Add( typeof( NoxCrystal ), 7 );
					Add( typeof( PigIron ), 6 );
				}

				Type[] types = Loot.RegularScrollTypes;

				for ( int i = 0; i < types.Length; ++i )
					Add( types[i], ((i / 8) + 2) * 5 );
			}
		}
	}
}