using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class SkillIncreaseTarget : Target // Create our targeting class (which we derive from the base target class)
	{
		private SkillIncreaseDeed m_Deed;

		public SkillIncreaseTarget( SkillIncreaseDeed deed ) : base( 1, false, TargetFlags.None )
		{
			m_Deed = deed;
		}

		protected override void OnTarget( Mobile from, object target ) // Override the protected OnTarget() for our feature
		{
			if ( m_Deed.Deleted || m_Deed.RootParent != from )
				return;

			if ( target is BaseJewel && m_Deed.AllowJewels && (!(target is BaseEarrings) || m_Deed.AllowEarrings))
            {
                BaseJewel item = (BaseJewel)target;
                if (item.LootType == LootType.Cursed)
                {
                    from.SendMessage("You cannot enhance that item further.");
                    return;
                }
                if (IncreaseJewelAttribute(item))
                {
                    from.SendMessage(String.Format( "You increase the items {0}... at a cost.", m_Deed.BonusSkill ) );
                    item.LootType = LootType.Cursed;
                    m_Deed.Delete(); // Delete the deed
                }
                else
                { 
                    from.SendMessage(String.Format("{0} cannot be applied to that item with this deed.", m_Deed.BonusSkill));
                    return;
                }
            }
            else if (target is Spellbook && m_Deed.AllowBooks) 
            {
                Spellbook item = (Spellbook)target;
                if (item.LootType == LootType.Cursed)
                {
                    from.SendMessage("You cannot enhance that item further.");
                    return;
                }
                if (IncreaseBookAttribute(item))
                {
                    from.SendMessage(String.Format("You increase the items {0}... at a cost.", m_Deed.BonusSkill));
                    item.LootType = LootType.Cursed;
                    m_Deed.Delete(); // Delete the deed
                }
                else
                {
                    from.SendMessage(String.Format("{0} cannot be applied to that item with this deed.", m_Deed.BonusSkill));
                    return;
                }
            }
            else if (target is BaseWeapon && m_Deed.AllowWeps) 
            {
                BaseWeapon item = (BaseWeapon)target;
                if (item.LootType == LootType.Cursed)
                {
                    from.SendMessage("You cannot enhance that item further.");
                    return;
                }
                if (IncreaseWeaponAttribute(item))
                {
                    from.SendMessage(String.Format("You increase the items {0}... at a cost.", m_Deed.BonusSkill));
                    item.LootType = LootType.Cursed;
                    m_Deed.Delete(); // Delete the deed
                }
                else
                {
                    from.SendMessage(String.Format("{0} cannot be applied to that item with this deed.", m_Deed.BonusSkill));
                    return;
                }
            }
            else if (target is BaseArmor && m_Deed.AllowArmor) 
            {
                BaseArmor item = (BaseArmor)target;
                if (item.LootType == LootType.Cursed)
                {
                    from.SendMessage("You cannot enhance that item further.");
                    return;
                }
                if (IncreaseArmorAttribute(item))
                {
                    from.SendMessage(String.Format("You increase the items {0}... at a cost.", m_Deed.BonusSkill));
                    item.LootType = LootType.Cursed;
                    m_Deed.Delete(); // Delete the deed
                }
                else
                {
                    from.SendMessage(String.Format("{0} cannot be applied to that item with this deed.", m_Deed.BonusSkill));
                    return;
                }
            }
            else if (target is BaseClothing && m_Deed.AllowClothing)
            {
                BaseClothing item = (BaseClothing)target;
                if (item.LootType == LootType.Cursed)
                {
                    from.SendMessage("You cannot enhance that item further.");
                    return;
                }
                if (IncreaseClothingAttribute(item))
                {
                    from.SendMessage(String.Format("You increase the items {0}... at a cost.", m_Deed.BonusSkill));
                    item.LootType = LootType.Cursed;
                    m_Deed.Delete(); // Delete the deed
                }
                else
                {
                    from.SendMessage(String.Format("{0} cannot be applied to that item with this deed.", m_Deed.BonusSkill));
                    return;
                }
            }


            else
			{
				from.SendMessage( String.Format("You cannot add {0} to this item with this deed.", m_Deed.BonusSkill));
			}
		}

        public bool IncreaseJewelAttribute(BaseJewel item)
        {
            if ( item.SkillBonuses.Skill_1_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_1_Value == 0)
            {
                item.SkillBonuses.Skill_1_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_1_Value += m_Deed.Level;
            }
            else if (item.SkillBonuses.Skill_2_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_2_Value == 0)
            {
                item.SkillBonuses.Skill_2_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_2_Value += m_Deed.Level;
            }
            else if (item.SkillBonuses.Skill_3_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_3_Value == 0)
            {
                item.SkillBonuses.Skill_3_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_3_Value += m_Deed.Level;
            }
            else if (item.SkillBonuses.Skill_4_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_4_Value == 0)
            {
                item.SkillBonuses.Skill_4_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_4_Value += m_Deed.Level;
            }
            else if (item.SkillBonuses.Skill_5_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_5_Value == 0)
            {
                item.SkillBonuses.Skill_5_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_5_Value += m_Deed.Level;
            }
            else
                return false;
            return true;
        }

        public bool IncreaseBookAttribute(Spellbook item)
        {
            if (item.SkillBonuses.Skill_1_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_1_Value == 0)
            {
                item.SkillBonuses.Skill_1_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_1_Value += m_Deed.Level;
            }
            else if (item.SkillBonuses.Skill_2_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_2_Value == 0)
            {
                item.SkillBonuses.Skill_2_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_2_Value += m_Deed.Level;
            }
            else if (item.SkillBonuses.Skill_3_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_3_Value == 0)
            {
                item.SkillBonuses.Skill_3_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_3_Value += m_Deed.Level;
            }
            else if (item.SkillBonuses.Skill_4_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_4_Value == 0)
            {
                item.SkillBonuses.Skill_4_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_4_Value += m_Deed.Level;
            }
            else if (item.SkillBonuses.Skill_5_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_5_Value == 0)
            {
                item.SkillBonuses.Skill_5_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_5_Value += m_Deed.Level;
            }
            else
                return false;
            return true;
        }

        public bool IncreaseWeaponAttribute(BaseWeapon item)
        {
            if (item.SkillBonuses.Skill_1_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_1_Value == 0)
            {
                item.SkillBonuses.Skill_1_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_1_Value += m_Deed.Level;
            }
            else if (item.SkillBonuses.Skill_2_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_2_Value == 0)
            {
                item.SkillBonuses.Skill_2_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_2_Value += m_Deed.Level;
            }
            else if (item.SkillBonuses.Skill_3_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_3_Value == 0)
            {
                item.SkillBonuses.Skill_3_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_3_Value += m_Deed.Level;
            }
            else if (item.SkillBonuses.Skill_4_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_4_Value == 0)
            {
                item.SkillBonuses.Skill_4_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_4_Value += m_Deed.Level;
            }
            else if (item.SkillBonuses.Skill_5_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_5_Value == 0)
            {
                item.SkillBonuses.Skill_5_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_5_Value += m_Deed.Level;
            }
            else
                return false;
            return true;
        }

        public bool IncreaseArmorAttribute(BaseArmor item)
        {
            if (item.SkillBonuses.Skill_1_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_1_Value == 0)
            {
                item.SkillBonuses.Skill_1_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_1_Value += m_Deed.Level;
            }
            else if (item.SkillBonuses.Skill_2_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_2_Value == 0)
            {
                item.SkillBonuses.Skill_2_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_2_Value += m_Deed.Level;
            }
            else if (item.SkillBonuses.Skill_3_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_3_Value == 0)
            {
                item.SkillBonuses.Skill_3_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_3_Value += m_Deed.Level;
            }
            else if (item.SkillBonuses.Skill_4_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_4_Value == 0)
            {
                item.SkillBonuses.Skill_4_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_4_Value += m_Deed.Level;
            }
            else if (item.SkillBonuses.Skill_5_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_5_Value == 0)
            {
                item.SkillBonuses.Skill_5_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_5_Value += m_Deed.Level;
            }
            else
                return false;
            return true;
        }

        public bool IncreaseClothingAttribute(BaseClothing item)
        {
            if (item.SkillBonuses.Skill_1_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_1_Value == 0)
            {
                item.SkillBonuses.Skill_1_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_1_Value += m_Deed.Level;
            }
            else if (item.SkillBonuses.Skill_2_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_2_Value == 0)
            {
                item.SkillBonuses.Skill_2_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_2_Value += m_Deed.Level;
            }
            else if (item.SkillBonuses.Skill_3_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_3_Value == 0)
            {
                item.SkillBonuses.Skill_3_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_3_Value += m_Deed.Level;
            }
            else if (item.SkillBonuses.Skill_4_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_4_Value == 0)
            {
                item.SkillBonuses.Skill_4_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_4_Value += m_Deed.Level;
            }
            else if (item.SkillBonuses.Skill_5_Name == m_Deed.BonusSkill || item.SkillBonuses.Skill_5_Value == 0)
            {
                item.SkillBonuses.Skill_5_Name = m_Deed.BonusSkill;
                item.SkillBonuses.Skill_5_Value += m_Deed.Level;
            }
            else
                return false;
            return true;
        }


    }

	public class SkillIncreaseDeed : Item // Create the item class which is derived from the base item class
	{
        private int m_level;
        [CommandProperty(AccessLevel.GameMaster)]
        public int Level
        {
            get {return m_level; }
            set {m_level = value; }
        }

        private SkillName m_bonusskill;
        [CommandProperty(AccessLevel.GameMaster)]
        public SkillName BonusSkill
        {
            get { return m_bonusskill; }
            set { m_bonusskill = value; }
        }

        private bool m_allowjewels;
        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowJewels
        {
            get { return m_allowjewels; }
            set { m_allowjewels = value; }
        }

        private bool m_allowearrings;
        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowEarrings
        {
            get { return m_allowearrings; }
            set { m_allowearrings = value; }
        }

        private bool m_allowbooks;
        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowBooks
        {
            get { return m_allowbooks; }
            set { m_allowbooks = value; }
        }

        private bool m_allowweps;
        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowWeps
        {
            get { return m_allowweps; }
            set { m_allowweps = value; }
        }

        private bool m_allowarmor;
        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowArmor
        {
            get { return m_allowarmor; }
            set { m_allowarmor = value; }
        }

        private bool m_allowclothing;
        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowClothing
        {
            get { return m_allowclothing; }
            set { m_allowclothing = value; }
        }

        private string m_aosattribute;
        [CommandProperty(AccessLevel.GameMaster)]
        public string AosAttribute
        {
            get { return m_aosattribute; }
            set { m_aosattribute = value; }
        }


        public override string DefaultName
		{
			get { return String.Format("a level {0} {1} increase deed for the following: {2}{3}{4}{5}{6}{7}", Level, BonusSkill, 
                                        AllowJewels ? " Jewelry" : "",
                                        AllowEarrings ? " Earrings":"",
                                        AllowBooks ? " Spellbooks":"",
                                        AllowWeps ? " Weapons":"",
                                        AllowArmor ? " Armor":"",
                                        AllowClothing ? " Clothing":""); }
		}

		[Constructable]
		public SkillIncreaseDeed() : base( 0x14F0 )
		{
			Weight = 1.0;
            Level = 1;
			LootType = LootType.Blessed;
            BonusSkill = SkillName.Alchemy;
            AllowJewels = true;
            AllowEarrings = false;
            AllowBooks = false;
            AllowWeps = false;
            AllowArmor = false;
            AllowClothing = false;
		}

		public SkillIncreaseDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
            writer.Write( (int) Level );
            writer.WriteEncodedInt((int) BonusSkill );
            writer.Write( (bool) AllowJewels );
            writer.Write( (bool) AllowEarrings );
            writer.Write((bool) AllowBooks );
            writer.Write((bool) AllowWeps );
            writer.Write((bool) AllowArmor );
            writer.Write((bool) AllowClothing );

        }

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			LootType = LootType.Blessed;

			int version = reader.ReadInt();
            Level = reader.ReadInt();
            reader.ReadEncodedInt();
            AllowJewels = reader.ReadBool();
            AllowEarrings = reader.ReadBool();
            AllowBooks = reader.ReadBool();
            AllowWeps = reader.ReadBool();
            AllowArmor = reader.ReadBool();
            AllowClothing = reader.ReadBool();
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
				from.SendMessage( "Choose an item to improve" );
				from.Target = new SkillIncreaseTarget( this ); // Call our target
			 }
		}	
	}
}