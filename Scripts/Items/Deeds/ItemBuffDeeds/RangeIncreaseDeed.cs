using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class RangeIncreaseTarget : Target // Create our targeting class (which we derive from the base target class)
	{
		private RangeIncreaseDeed m_Deed;

		public RangeIncreaseTarget( RangeIncreaseDeed deed ) : base( 1, false, TargetFlags.None )
		{
			m_Deed = deed;
		}

		protected override void OnTarget( Mobile from, object target ) // Override the protected OnTarget() for our feature
		{
			if ( m_Deed.Deleted || m_Deed.RootParent != from )
				return;

			if ( target is BaseWeapon )
			{
				BaseWeapon item = (BaseWeapon)target;

				item.LootType = LootType.Cursed;
                if (item.MaxRange >= 6)
                    item.MaxRange += m_Deed.Level * 2;
                else
                    item.MaxRange += m_Deed.Level;
				from.SendMessage( "You increase the items range... at a cost." );

				m_Deed.Delete(); // Delete the deed

			}
			else
			{
				from.SendMessage( "You can only add range to weapons" );
			}
		}
	}

	public class RangeIncreaseDeed : Item // Create the item class which is derived from the base item class
	{
        private int m_level;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Level
        {
            get {return m_level; }
            set {m_level = value; }
        }
		public override string DefaultName
		{
			get { return String.Format("a level {0} range increase deed", Level); }
		}

		[Constructable]
		public RangeIncreaseDeed() : base( 0x14F0 )
		{
			Weight = 1.0;
            Level = 1;
			LootType = LootType.Blessed;
		}

		public RangeIncreaseDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			LootType = LootType.Blessed;

			int version = reader.ReadInt();
		}

		public override bool DisplayLootType{ get{ return false; } }

		public override void OnDoubleClick( Mobile from ) // Override double click of the deed to call our target
		{
			if ( !IsChildOf( from.Backpack ) ) // Make sure its in their pack
			{
				 from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else
			{
				from.SendLocalizedMessage( 1005018 ); // What would you like to bless? (Clothes Only)
				from.Target = new RangeIncreaseTarget( this ); // Call our target
			 }
		}	
	}
}