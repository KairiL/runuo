using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class AttributeIncreaseTarget : Target // Create our targeting class (which we derive from the base target class)
	{
		private AttributeIncreaseDeed m_Deed;

		public AttributeIncreaseTarget( AttributeIncreaseDeed deed ) : base( 1, false, TargetFlags.None )
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
                    from.SendMessage(String.Format( "You increase the items {0}... at a cost.", m_Deed.AosAttribute ) );
                    item.LootType = LootType.Cursed;
                    m_Deed.Delete(); // Delete the deed
                }
                else
                { 
                    from.SendMessage(String.Format( "{0} cannot be applied to that item with this deed.", m_Deed.AosAttribute ));
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
                    from.SendMessage(String.Format("You increase the items {0}... at a cost.", m_Deed.AosAttribute));
                    item.LootType = LootType.Cursed;
                    m_Deed.Delete(); // Delete the deed
                }
                else
                {
                    from.SendMessage(String.Format("{0} cannot be applied to that item with this deed.", m_Deed.AosAttribute));
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
                    from.SendMessage(String.Format("You increase the items {0}... at a cost.", m_Deed.AosAttribute));
                    item.LootType = LootType.Cursed;
                    m_Deed.Delete(); // Delete the deed
                }
                else
                {
                    from.SendMessage(String.Format("{0} cannot be applied to that item with this deed.", m_Deed.AosAttribute));
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
                    from.SendMessage(String.Format("You increase the items {0}... at a cost.", m_Deed.AosAttribute));
                    item.LootType = LootType.Cursed;
                    m_Deed.Delete(); // Delete the deed
                }
                else
                {
                    from.SendMessage(String.Format("{0} cannot be applied to that item with this deed.", m_Deed.AosAttribute));
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
                    from.SendMessage(String.Format("You increase the items {0}... at a cost.", m_Deed.AosAttribute));
                    item.LootType = LootType.Cursed;
                    m_Deed.Delete(); // Delete the deed
                }
                else
                {
                    from.SendMessage(String.Format("{0} cannot be applied to that item with this deed.", m_Deed.AosAttribute));
                    return;
                }
            }


            else
			{
				from.SendMessage( String.Format("You cannot add {0} to this item with this deed", m_Deed.AosAttribute));
			}
		}

        public bool IncreaseJewelAttribute(BaseJewel item)
        {
            switch (m_Deed.AosAttribute)
            {
                case "AttackChance":
                    item.Attributes.AttackChance += m_Deed.Level;
                    break;
                case "BonusDex":
                    item.Attributes.BonusDex += m_Deed.Level;
                    break;
                case "BonusHits":
                    item.Attributes.BonusHits += m_Deed.Level;
                    break;
                case "BonusInt":
                    item.Attributes.BonusHits += m_Deed.Level;
                    break;
                case "BonusMana":
                    item.Attributes.BonusMana += m_Deed.Level;
                    break;
                case "BonusStam":
                    item.Attributes.BonusStam += m_Deed.Level;
                    break;
                case "BonusStr":
                    item.Attributes.BonusStr += m_Deed.Level;
                    break;
                case "CastRecovery":
                    item.Attributes.CastRecovery += m_Deed.Level;
                    break;
                case "CastSpeed":
                    item.Attributes.CastSpeed += m_Deed.Level;
                    break;
                case "DefendChance":
                    item.Attributes.DefendChance += m_Deed.Level;
                    break;
                case "EnhancePotions":
                    item.Attributes.EnhancePotions += m_Deed.Level;
                    break;
                case "IncreasedKarmaLoss":
                    item.Attributes.IncreasedKarmaLoss += m_Deed.Level;
                    break;
                case "LowerManaCost":
                    item.Attributes.LowerManaCost += m_Deed.Level;
                    break;
                case "LowerRegCost":
                    item.Attributes.LowerRegCost += m_Deed.Level;
                    break;
                case "Luck":
                    item.Attributes.Luck += m_Deed.Level;
                    break;
                case "NightSight":
                    item.Attributes.NightSight += m_Deed.Level;
                    break;
                case "ReflectPhysical":
                    item.Attributes.ReflectPhysical += m_Deed.Level;
                    break;
                case "RegenHits":
                    item.Attributes.RegenHits += m_Deed.Level;
                    break;
                case "RegenMana":
                    item.Attributes.RegenMana += m_Deed.Level;
                    break;
                case "SpellChanneling":
                    item.Attributes.SpellChanneling += m_Deed.Level;
                    break;
                case "SpellDamage":
                    item.Attributes.SpellDamage += m_Deed.Level;
                    break;
                case "WeaponDamage":
                    item.Attributes.WeaponDamage += m_Deed.Level;
                    break;
                case "WeaponSpeed":
                    item.Attributes.WeaponSpeed += m_Deed.Level;
                    break;
                default:
                    return false;
            }
            return true;
        }

        public bool IncreaseBookAttribute(Spellbook item)
        {
            switch (m_Deed.AosAttribute)
            {
                case "AttackChance":
                    item.Attributes.AttackChance += m_Deed.Level;
                    break;
                case "BonusDex":
                    item.Attributes.BonusDex += m_Deed.Level;
                    break;
                case "BonusHits":
                    item.Attributes.BonusHits += m_Deed.Level;
                    break;
                case "BonusInt":
                    item.Attributes.BonusHits += m_Deed.Level;
                    break;
                case "BonusMana":
                    item.Attributes.BonusMana += m_Deed.Level;
                    break;
                case "BonusStam":
                    item.Attributes.BonusStam += m_Deed.Level;
                    break;
                case "BonusStr":
                    item.Attributes.BonusStr += m_Deed.Level;
                    break;
                case "CastRecovery":
                    item.Attributes.CastRecovery += m_Deed.Level;
                    break;
                case "CastSpeed":
                    item.Attributes.CastSpeed += m_Deed.Level;
                    break;
                case "DefendChance":
                    item.Attributes.DefendChance += m_Deed.Level;
                    break;
                case "EnhancePotions":
                    item.Attributes.EnhancePotions += m_Deed.Level;
                    break;
                case "IncreasedKarmaLoss":
                    item.Attributes.IncreasedKarmaLoss += m_Deed.Level;
                    break;
                case "LowerManaCost":
                    item.Attributes.LowerManaCost += m_Deed.Level;
                    break;
                case "LowerRegCost":
                    item.Attributes.LowerRegCost += m_Deed.Level;
                    break;
                case "Luck":
                    item.Attributes.Luck += m_Deed.Level;
                    break;
                case "NightSight":
                    item.Attributes.NightSight += m_Deed.Level;
                    break;
                case "ReflectPhysical":
                    item.Attributes.ReflectPhysical += m_Deed.Level;
                    break;
                case "RegenHits":
                    item.Attributes.RegenHits += m_Deed.Level;
                    break;
                case "RegenMana":
                    item.Attributes.RegenMana += m_Deed.Level;
                    break;
                case "SpellChanneling":
                    item.Attributes.SpellChanneling += m_Deed.Level;
                    break;
                case "SpellDamage":
                    item.Attributes.SpellDamage += m_Deed.Level;
                    break;
                case "WeaponDamage":
                    item.Attributes.WeaponDamage += m_Deed.Level;
                    break;
                case "WeaponSpeed":
                    item.Attributes.WeaponSpeed += m_Deed.Level;
                    break;
                default:
                    return false;
            }
            return true;
        }

        public bool IncreaseWeaponAttribute(BaseWeapon item)
        {
            switch (m_Deed.AosAttribute)
            {
                case "AttackChance":
                    item.Attributes.AttackChance += m_Deed.Level;
                    break;
                case "BonusDex":
                    item.Attributes.BonusDex += m_Deed.Level;
                    break;
                case "BonusHits":
                    item.Attributes.BonusHits += m_Deed.Level;
                    break;
                case "BonusInt":
                    item.Attributes.BonusHits += m_Deed.Level;
                    break;
                case "BonusMana":
                    item.Attributes.BonusMana += m_Deed.Level;
                    break;
                case "BonusStam":
                    item.Attributes.BonusStam += m_Deed.Level;
                    break;
                case "BonusStr":
                    item.Attributes.BonusStr += m_Deed.Level;
                    break;
                case "CastRecovery":
                    item.Attributes.CastRecovery += m_Deed.Level;
                    break;
                case "CastSpeed":
                    item.Attributes.CastSpeed += m_Deed.Level;
                    break;
                case "DefendChance":
                    item.Attributes.DefendChance += m_Deed.Level;
                    break;
                case "EnhancePotions":
                    item.Attributes.EnhancePotions += m_Deed.Level;
                    break;
                case "IncreasedKarmaLoss":
                    item.Attributes.IncreasedKarmaLoss += m_Deed.Level;
                    break;
                case "LowerManaCost":
                    item.Attributes.LowerManaCost += m_Deed.Level;
                    break;
                case "LowerRegCost":
                    item.Attributes.LowerRegCost += m_Deed.Level;
                    break;
                case "Luck":
                    item.Attributes.Luck += m_Deed.Level;
                    break;
                case "NightSight":
                    item.Attributes.NightSight += m_Deed.Level;
                    break;
                case "ReflectPhysical":
                    item.Attributes.ReflectPhysical += m_Deed.Level;
                    break;
                case "RegenHits":
                    item.Attributes.RegenHits += m_Deed.Level;
                    break;
                case "RegenMana":
                    item.Attributes.RegenMana += m_Deed.Level;
                    break;
                case "SpellChanneling":
                    item.Attributes.SpellChanneling += m_Deed.Level;
                    break;
                case "SpellDamage":
                    item.Attributes.SpellDamage += m_Deed.Level;
                    break;
                case "WeaponDamage":
                    item.Attributes.WeaponDamage += m_Deed.Level;
                    break;
                case "WeaponSpeed":
                    item.Attributes.WeaponSpeed += m_Deed.Level;
                    break;
                default:
                    return false;
            }
            return true;
        }

        public bool IncreaseArmorAttribute(BaseArmor item)
        {
            switch (m_Deed.AosAttribute)
            {
                case "AttackChance":
                    item.Attributes.AttackChance += m_Deed.Level;
                    break;
                case "BonusDex":
                    item.Attributes.BonusDex += m_Deed.Level;
                    break;
                case "BonusHits":
                    item.Attributes.BonusHits += m_Deed.Level;
                    break;
                case "BonusInt":
                    item.Attributes.BonusHits += m_Deed.Level;
                    break;
                case "BonusMana":
                    item.Attributes.BonusMana += m_Deed.Level;
                    break;
                case "BonusStam":
                    item.Attributes.BonusStam += m_Deed.Level;
                    break;
                case "BonusStr":
                    item.Attributes.BonusStr += m_Deed.Level;
                    break;
                case "CastRecovery":
                    item.Attributes.CastRecovery += m_Deed.Level;
                    break;
                case "CastSpeed":
                    item.Attributes.CastSpeed += m_Deed.Level;
                    break;
                case "DefendChance":
                    item.Attributes.DefendChance += m_Deed.Level;
                    break;
                case "EnhancePotions":
                    item.Attributes.EnhancePotions += m_Deed.Level;
                    break;
                case "IncreasedKarmaLoss":
                    item.Attributes.IncreasedKarmaLoss += m_Deed.Level;
                    break;
                case "LowerManaCost":
                    item.Attributes.LowerManaCost += m_Deed.Level;
                    break;
                case "LowerRegCost":
                    item.Attributes.LowerRegCost += m_Deed.Level;
                    break;
                case "Luck":
                    item.Attributes.Luck += m_Deed.Level;
                    break;
                case "NightSight":
                    item.Attributes.NightSight += m_Deed.Level;
                    break;
                case "ReflectPhysical":
                    item.Attributes.ReflectPhysical += m_Deed.Level;
                    break;
                case "RegenHits":
                    item.Attributes.RegenHits += m_Deed.Level;
                    break;
                case "RegenMana":
                    item.Attributes.RegenMana += m_Deed.Level;
                    break;
                case "SpellChanneling":
                    item.Attributes.SpellChanneling += m_Deed.Level;
                    break;
                case "SpellDamage":
                    item.Attributes.SpellDamage += m_Deed.Level;
                    break;
                case "WeaponDamage":
                    item.Attributes.WeaponDamage += m_Deed.Level;
                    break;
                case "WeaponSpeed":
                    item.Attributes.WeaponSpeed += m_Deed.Level;
                    break;
                default:
                    return false;
            }
            return true;
        }

        public bool IncreaseClothingAttribute(BaseClothing item)
        {
            switch (m_Deed.AosAttribute)
            {
                case "AttackChance":
                    item.Attributes.AttackChance += m_Deed.Level;
                    break;
                case "BonusDex":
                    item.Attributes.BonusDex += m_Deed.Level;
                    break;
                case "BonusHits":
                    item.Attributes.BonusHits += m_Deed.Level;
                    break;
                case "BonusInt":
                    item.Attributes.BonusHits += m_Deed.Level;
                    break;
                case "BonusMana":
                    item.Attributes.BonusMana += m_Deed.Level;
                    break;
                case "BonusStam":
                    item.Attributes.BonusStam += m_Deed.Level;
                    break;
                case "BonusStr":
                    item.Attributes.BonusStr += m_Deed.Level;
                    break;
                case "CastRecovery":
                    item.Attributes.CastRecovery += m_Deed.Level;
                    break;
                case "CastSpeed":
                    item.Attributes.CastSpeed += m_Deed.Level;
                    break;
                case "DefendChance":
                    item.Attributes.DefendChance += m_Deed.Level;
                    break;
                case "EnhancePotions":
                    item.Attributes.EnhancePotions += m_Deed.Level;
                    break;
                case "IncreasedKarmaLoss":
                    item.Attributes.IncreasedKarmaLoss += m_Deed.Level;
                    break;
                case "LowerManaCost":
                    item.Attributes.LowerManaCost += m_Deed.Level;
                    break;
                case "LowerRegCost":
                    item.Attributes.LowerRegCost += m_Deed.Level;
                    break;
                case "Luck":
                    item.Attributes.Luck += m_Deed.Level;
                    break;
                case "NightSight":
                    item.Attributes.NightSight += m_Deed.Level;
                    break;
                case "ReflectPhysical":
                    item.Attributes.ReflectPhysical += m_Deed.Level;
                    break;
                case "RegenHits":
                    item.Attributes.RegenHits += m_Deed.Level;
                    break;
                case "RegenMana":
                    item.Attributes.RegenMana += m_Deed.Level;
                    break;
                case "SpellChanneling":
                    item.Attributes.SpellChanneling += m_Deed.Level;
                    break;
                case "SpellDamage":
                    item.Attributes.SpellDamage += m_Deed.Level;
                    break;
                case "WeaponDamage":
                    item.Attributes.WeaponDamage += m_Deed.Level;
                    break;
                case "WeaponSpeed":
                    item.Attributes.WeaponSpeed += m_Deed.Level;
                    break;
                default:
                    return false;
            }
            return true;
        }


    }

	public class AttributeIncreaseDeed : Item // Create the item class which is derived from the base item class
	{
        private int m_level;
        [CommandProperty(AccessLevel.GameMaster)]
        public int Level
        {
            get {return m_level; }
            set {m_level = value; }
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
			get { return String.Format("a level {0} {1} increase deed for the following: {2}{3}{4}{5}{6}{7}", Level, AosAttribute, 
                                        AllowJewels ? " Jewelry" : "",
                                        AllowEarrings ? " Earrings":"",
                                        AllowBooks ? " Spellbooks":"",
                                        AllowWeps ? " Weapons":"",
                                        AllowArmor ? " Armor":"",
                                        AllowClothing ? " Clothing":""); }
		}

		[Constructable]
		public AttributeIncreaseDeed() : base( 0x14F0 )
		{
			Weight = 1.0;
            Level = 1;
			LootType = LootType.Blessed;
            AosAttribute = "Luck";
            AllowJewels = true;
            AllowEarrings = false;
            AllowBooks = false;
            AllowWeps = false;
            AllowArmor = false;
            AllowClothing = false;
		}

		public AttributeIncreaseDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
            writer.Write( (int) Level );
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
				from.Target = new AttributeIncreaseTarget( this ); // Call our target
			 }
		}	
	}
}