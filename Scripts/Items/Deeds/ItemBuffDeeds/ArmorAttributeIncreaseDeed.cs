using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class ArmorAttributeIncreaseTarget : Target // Create our targeting class (which we derive from the base target class)
	{
		private ArmorAttributeIncreaseDeed m_Deed;

		public ArmorAttributeIncreaseTarget( ArmorAttributeIncreaseDeed deed ) : base( 1, false, TargetFlags.None )
		{
			m_Deed = deed;
		}

		protected override void OnTarget( Mobile from, object target ) // Override the protected OnTarget() for our feature
		{
			if ( m_Deed.Deleted || m_Deed.RootParent != from )
				return;

			
            if (target is BaseArmor && m_Deed.AllowArmor) 
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
				from.SendMessage( String.Format("You cannot add {0} to this item with this deed.", m_Deed.AosAttribute));
			}
		}

        public bool IncreaseArmorAttribute(BaseArmor item)
        {
            switch (m_Deed.AosAttribute)
            {
                case "DurabilityBonus":
                    item.ArmorAttributes.DurabilityBonus += m_Deed.Level;
                    break;
                case "LowerStatReq":
                    item.ArmorAttributes.LowerStatReq += m_Deed.Level;
                    break;
                case "MageArmor":
                    item.ArmorAttributes.MageArmor += m_Deed.Level;
                    break;
                case "SelfRepair":
                    item.ArmorAttributes.SelfRepair += m_Deed.Level;
                    break;
                case "PhysicalBonus":
                    item.PhysicalBonus += m_Deed.Level;
                    break;
                case "FireBonus":
                    item.FireBonus += m_Deed.Level;
                    break;
                case "ColdBonus":
                    item.ColdBonus += m_Deed.Level;
                    break;
                case "PoisonBonus":
                    item.PoisonBonus += m_Deed.Level;
                    break;
                case "EnergyBonus":
                    item.EnergyBonus += m_Deed.Level;
                    break;
                case "DexBonus":
                    item.DexBonus += m_Deed.Level;
                    break;
                case "IntBonus":
                    item.IntBonus += m_Deed.Level;
                    break;
                case "StrBonus":
                    item.StrBonus += m_Deed.Level;
                    break;
                case "MaxHitPoints":
                    item.MaxHitPoints += m_Deed.Level;
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
                case "DurabilityBonus":
                    item.ClothingAttributes.DurabilityBonus += m_Deed.Level;
                    break;
                case "LowerStatReq":
                    item.ClothingAttributes.LowerStatReq += m_Deed.Level;
                    break;
                case "MageArmor":
                    item.ClothingAttributes.MageArmor += m_Deed.Level;
                    break;
                case "SelfRepair":
                    item.ClothingAttributes.SelfRepair += m_Deed.Level;
                    break;
                case "PhysicalBonus":
                    item.Resistances.Physical += m_Deed.Level;
                    break;
                case "FireBonus":
                    item.Resistances.Fire += m_Deed.Level;
                    break;
                case "ColdBonus":
                    item.Resistances.Cold += m_Deed.Level;
                    break;
                case "PoisonBonus":
                    item.Resistances.Poison += m_Deed.Level;
                    break;
                case "EnergyBonus":
                    item.Resistances.Energy += m_Deed.Level;
                    break;
                case "DexBonus":
                    item.Attributes.BonusDex += m_Deed.Level;
                    break;
                case "IntBonus":
                    item.Attributes.BonusInt += m_Deed.Level;
                    break;
                case "StrBonus":
                    item.Attributes.BonusStr += m_Deed.Level;
                    break;
                case "MaxHitPoints":
                    item.MaxHitPoints += m_Deed.Level;
                    break;
                default:
                    return false;
            }
            return true;
        }


    }

	public class ArmorAttributeIncreaseDeed : Item // Create the item class which is derived from the base item class
	{
        private int m_level;
        [CommandProperty(AccessLevel.GameMaster)]
        public int Level
        {
            get {return m_level; }
            set {m_level = value; }
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
			get { return String.Format("a level {0} {1} increase deed for the following: {2}{3}", Level, AosAttribute, 
                                        AllowArmor ? " Armor":"",
                                        AllowClothing ? " Clothing":""); }
		}

		[Constructable]
		public ArmorAttributeIncreaseDeed() : base( 0x14F0 )
		{
			Weight = 1.0;
            Level = 1;
			LootType = LootType.Blessed;
            AosAttribute = "DurabilityBonus";
            AllowArmor = true;
            AllowClothing = false;
		}

		public ArmorAttributeIncreaseDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
            writer.Write( (int) Level );
            writer.Write((bool) AllowArmor );
            writer.Write((bool) AllowClothing );
            writer.Write((string)AosAttribute );


        }

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			LootType = LootType.Blessed;

			int version = reader.ReadInt();
            Level = reader.ReadInt();
            AllowArmor = reader.ReadBool();
            AllowClothing = reader.ReadBool();
            AosAttribute = reader.ReadString();
        }

		public override void OnDoubleClick( Mobile from ) // Override double click of the deed to call our target
		{
			if ( !IsChildOf( from.Backpack ) ) // Make sure its in their pack
			{
				 from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else
			{
				from.SendMessage( "Choose an item to improve" );
				from.Target = new ArmorAttributeIncreaseTarget( this ); // Call our target
			 }
		}	
	}
}