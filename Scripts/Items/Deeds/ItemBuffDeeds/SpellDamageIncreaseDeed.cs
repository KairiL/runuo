using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class SpellDamageIncreaseTarget : Target // Create our targeting class (which we derive from the base target class)
	{
		private SpellDamageIncreaseDeed m_Deed;

		public SpellDamageIncreaseTarget( SpellDamageIncreaseDeed deed ) : base( 1, false, TargetFlags.None )
		{
			m_Deed = deed;
		}

		protected override void OnTarget( Mobile from, object target ) // Override the protected OnTarget() for our feature
		{
			if ( m_Deed.Deleted || m_Deed.RootParent != from )
				return;

			if ( target is BaseJewel )
			{
				BaseJewel item = (BaseJewel)target;
                if (item.LootType == LootType.Cursed)
                {
                    from.SendMessage("You cannot enhance that item further");
                    return;
                }
                item.LootType = LootType.Cursed;
                item.Attributes.SpellDamage += m_Deed.Level;
				from.SendMessage( "You increase the items spell damage... at a cost." );

				m_Deed.Delete(); // Delete the deed
			}
            else if (target is Spellbook)
            {
                Spellbook item = (Spellbook)target;
                if (item.LootType == LootType.Cursed)
                {
                    from.SendMessage("You cannot enhance that item further");
                    return;
                }
                item.LootType = LootType.Cursed;
                item.Attributes.SpellDamage += m_Deed.Level;
                from.SendMessage("You increase the items spell damage... at a cost.");

                m_Deed.Delete(); // Delete the deed
            }

			else
			{
				from.SendMessage( "You can only add spell damage to jewelry or spellbooks" );
			}
		}
	}

	public class SpellDamageIncreaseDeed : Item // Create the item class which is derived from the base item class
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
			get { return String.Format("a level {0} spell damage increase deed", Level); }
		}

		[Constructable]
		public SpellDamageIncreaseDeed() : base( 0x14F0 )
		{
			Weight = 1.0;
            Level = 1;
			LootType = LootType.Blessed;
		}

		public SpellDamageIncreaseDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
            writer.Write( (int) Level);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			LootType = LootType.Blessed;

			int version = reader.ReadInt();
            Level = reader.ReadInt();
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
                from.SendMessage("What would you like to enhance?");
                from.Target = new SpellDamageIncreaseTarget( this ); // Call our target
			 }
		}	
	}
}