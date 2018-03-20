using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class WeaponAttributeIncreaseTarget : Target // Create our targeting class (which we derive from the base target class)
	{
		private WeaponAttributeIncreaseDeed m_Deed;

		public WeaponAttributeIncreaseTarget(WeaponAttributeIncreaseDeed deed ) : base( 1, false, TargetFlags.None )
		{
			m_Deed = deed;
		}

		protected override void OnTarget( Mobile from, object target ) // Override the protected OnTarget() for our feature
		{
			if ( m_Deed.Deleted || m_Deed.RootParent != from )
				return;

			if (target is BaseWeapon && m_Deed.AllowWeps) 
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
            else if (target is ElvenGlasses && m_Deed.AllowGlasses) 
            {
                ElvenGlasses item = (ElvenGlasses)target;
                if (item.LootType == LootType.Cursed)
                {
                    from.SendMessage("You cannot enhance that item further.");
                    return;
                }
                if (IncreaseGlassesAttribute(item))
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

        public bool IncreaseWeaponAttribute(BaseWeapon item)
        {
            switch (m_Deed.AosAttribute)
            {
                case "DurabilityBonus":
                    item.WeaponAttributes.DurabilityBonus += m_Deed.Level;
                    break;
                case "HitColdArea":
                    item.WeaponAttributes.HitColdArea += m_Deed.Level;
                    break;
                case "HitDispel":
                    item.WeaponAttributes.HitDispel += m_Deed.Level;
                    break;
                case "HitEnergyArea":
                    item.WeaponAttributes.HitEnergyArea += m_Deed.Level;
                    break;
                case "HitFireArea":
                    item.WeaponAttributes.HitFireArea += m_Deed.Level;
                    break;
                case "HitFireball":
                    item.WeaponAttributes.HitFireball += m_Deed.Level;
                    break;
                case "HitHarm":
                    item.WeaponAttributes.HitHarm += m_Deed.Level;
                    break;
                case "HitLeechHits":
                    item.WeaponAttributes.HitLeechHits += m_Deed.Level;
                    break;
                case "HitLeechMana":
                    item.WeaponAttributes.HitLeechMana += m_Deed.Level;
                    break;
                case "HitLeechStam":
                    item.WeaponAttributes.HitLeechStam += m_Deed.Level;
                    break;
                case "HitLightning":
                    item.WeaponAttributes.HitLightning += m_Deed.Level;
                    break;
                case "HitLowerAttack":
                    item.WeaponAttributes.HitLowerAttack += m_Deed.Level;
                    break;
                case "HitLowerDefend":
                    item.WeaponAttributes.HitLowerDefend += m_Deed.Level;
                    break;
                case "HitMagicArrow":
                    item.WeaponAttributes.HitMagicArrow += m_Deed.Level;
                    break;
                case "HitPhysicalArea":
                    item.WeaponAttributes.HitPhysicalArea += m_Deed.Level;
                    break;
                case "HitPoisonArea":
                    item.WeaponAttributes.HitPoisonArea += m_Deed.Level;
                    break;
                case "LowerStatReq":
                    item.WeaponAttributes.LowerStatReq += m_Deed.Level;
                    break;
                case "MageWeapon":
                    item.WeaponAttributes.MageWeapon += m_Deed.Level;
                    break;
                case "ResistColdBonus":
                    item.WeaponAttributes.ResistColdBonus += m_Deed.Level;
                    break;
                case "ResistEnergyBonus":
                    item.WeaponAttributes.ResistEnergyBonus += m_Deed.Level;
                    break;
                case "ResistFireBonus":
                    item.WeaponAttributes.ResistFireBonus += m_Deed.Level;
                    break;
                case "ResistPhysicalBonus":
                    item.WeaponAttributes.ResistPhysicalBonus += m_Deed.Level;
                    break;
                case "ResistPoisonBonus":
                    item.WeaponAttributes.ResistPoisonBonus += m_Deed.Level;
                    break;
                case "SelfRepair":
                    item.WeaponAttributes.SelfRepair += m_Deed.Level;
                    break;
                case "UseBestSkill":
                    item.WeaponAttributes.UseBestSkill += m_Deed.Level;
                    break;
                case "MaxRange":
                    if (item.MaxRange >= 6)
                        item.MaxRange += m_Deed.Level * 2;
                    else
                        item.MaxRange += m_Deed.Level;
                    break;
                case "MinDamage":
                    item.MinDamage += m_Deed.Level;
                    break;
                case "MaxDamage":
                    item.MaxDamage += m_Deed.Level;
                    break;
                case "MaxHitPoints":
                    item.MaxHitPoints += m_Deed.Level;
                    break;
                case "Speed":
                    item.Speed -= (float)((m_Deed.Level)/4.0);
                    if (item.Speed < (float).25)
                        item.Speed = (float).25;
                    break;
                case "Cold":
                    if (item.AosElementDamages.Physical >= m_Deed.Level)
                    {
                        item.AosElementDamages.Cold += m_Deed.Level;
                        item.AosElementDamages.Physical -= m_Deed.Level;
                    }
                    else
                    {
                        item.AosElementDamages.Cold += item.AosElementDamages.Physical;
                        item.AosElementDamages.Physical = 0;
                    }
                    break;
                case "Energy":
                    if (item.AosElementDamages.Physical >= m_Deed.Level)
                    {
                        item.AosElementDamages.Energy += m_Deed.Level;
                        item.AosElementDamages.Physical -= m_Deed.Level;
                    }
                    else
                    {
                        item.AosElementDamages.Energy += item.AosElementDamages.Physical;
                        item.AosElementDamages.Physical = 0;
                    }
                    break;
                case "Fire":
                    if (item.AosElementDamages.Physical >= m_Deed.Level)
                    {
                        item.AosElementDamages.Fire += m_Deed.Level;
                        item.AosElementDamages.Physical -= m_Deed.Level;
                    }
                    else
                    {
                        item.AosElementDamages.Fire += item.AosElementDamages.Physical;
                        item.AosElementDamages.Physical = 0;
                    }
                    break;
                case "Poison":
                    if (item.AosElementDamages.Physical >= m_Deed.Level)
                    {
                        item.AosElementDamages.Poison += m_Deed.Level;
                        item.AosElementDamages.Physical -= m_Deed.Level;
                    }
                    else
                    {
                        item.AosElementDamages.Poison += item.AosElementDamages.Physical;
                        item.AosElementDamages.Physical = 0;
                    }
                    break;
                case "Chaos":
                    if (item.AosElementDamages.Physical >= m_Deed.Level)
                    {
                        item.AosElementDamages.Chaos += m_Deed.Level;
                        item.AosElementDamages.Physical -= m_Deed.Level;
                    }
                    else
                    {
                        item.AosElementDamages.Chaos += item.AosElementDamages.Physical;
                        item.AosElementDamages.Physical = 0;
                    }
                    break;
                case "Direct":
                    if (item.AosElementDamages.Physical >= m_Deed.Level)
                    {
                        item.AosElementDamages.Direct += m_Deed.Level;
                        item.AosElementDamages.Physical -= m_Deed.Level;
                    }
                    else
                    {
                        item.AosElementDamages.Direct += item.AosElementDamages.Physical;
                        item.AosElementDamages.Physical = 0;
                    }
                    break;
                case "FollowersBonus":
                    item.FollowersBonus += m_Deed.Level;
                    break;
                default:
                    return false;
            }
            return true;
        }

        public bool IncreaseGlassesAttribute(ElvenGlasses item)
        {
            switch (m_Deed.AosAttribute)
            {
                case "DurabilityBonus":
                    item.WeaponAttributes.DurabilityBonus += m_Deed.Level;
                    break;
                case "HitColdArea":
                    item.WeaponAttributes.HitColdArea += m_Deed.Level;
                    break;
                case "HitDispel":
                    item.WeaponAttributes.HitDispel += m_Deed.Level;
                    break;
                case "HitEnergyArea":
                    item.WeaponAttributes.HitEnergyArea += m_Deed.Level;
                    break;
                case "HitFireArea":
                    item.WeaponAttributes.HitFireArea += m_Deed.Level;
                    break;
                case "HitFireball":
                    item.WeaponAttributes.HitFireball += m_Deed.Level;
                    break;
                case "HitHarm":
                    item.WeaponAttributes.HitHarm += m_Deed.Level;
                    break;
                case "HitLeechHits":
                    item.WeaponAttributes.HitLeechHits += m_Deed.Level;
                    break;
                case "HitLeechMana":
                    item.WeaponAttributes.HitLeechMana += m_Deed.Level;
                    break;
                case "HitLeechStam":
                    item.WeaponAttributes.HitLeechStam += m_Deed.Level;
                    break;
                case "HitLightning":
                    item.WeaponAttributes.HitLightning += m_Deed.Level;
                    break;
                case "HitLowerAttack":
                    item.WeaponAttributes.HitLowerAttack += m_Deed.Level;
                    break;
                case "HitLowerDefend":
                    item.WeaponAttributes.HitLowerDefend += m_Deed.Level;
                    break;
                case "HitMagicArrow":
                    item.WeaponAttributes.HitMagicArrow += m_Deed.Level;
                    break;
                case "HitPhysicalArea":
                    item.WeaponAttributes.HitPhysicalArea += m_Deed.Level;
                    break;
                case "HitPoisonArea":
                    item.WeaponAttributes.HitPoisonArea += m_Deed.Level;
                    break;
                case "LowerStatReq":
                    item.WeaponAttributes.LowerStatReq += m_Deed.Level;
                    break;
                case "MageWeapon":
                    item.WeaponAttributes.MageWeapon += m_Deed.Level;
                    break;
                case "ResistColdBonus":
                    item.WeaponAttributes.ResistColdBonus += m_Deed.Level;
                    break;
                case "ResistEnergyBonus":
                    item.WeaponAttributes.ResistEnergyBonus += m_Deed.Level;
                    break;
                case "ResistFireBonus":
                    item.WeaponAttributes.ResistFireBonus += m_Deed.Level;
                    break;
                case "ResistPhysicalBonus":
                    item.WeaponAttributes.ResistPhysicalBonus += m_Deed.Level;
                    break;
                case "ResistPoisonBonus":
                    item.WeaponAttributes.ResistPoisonBonus += m_Deed.Level;
                    break;
                case "SelfRepair":
                    item.WeaponAttributes.SelfRepair += m_Deed.Level;
                    break;
                case "UseBestSkill":
                    item.WeaponAttributes.UseBestSkill += m_Deed.Level;
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

	public class WeaponAttributeIncreaseDeed : Item // Create the item class which is derived from the base item class
	{
        private int m_level;
        [CommandProperty(AccessLevel.GameMaster)]
        public int Level
        {
            get {return m_level; }
            set {m_level = value; }
        }

        private bool m_allowweps;
        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowWeps
        {
            get { return m_allowweps; }
            set { m_allowweps = value; }
        }

        private bool m_allowglasses;
        [CommandProperty(AccessLevel.GameMaster)]
        public bool AllowGlasses
        {
            get { return m_allowglasses; }
            set { m_allowglasses = value; }
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
                                        AllowWeps ? " Weapons":"",
                                        AllowGlasses ? " Glasses":""); }
		}

		[Constructable]
		public WeaponAttributeIncreaseDeed() : base( 0x14F0 )
		{
			Weight = 1.0;
            Level = 1;
			LootType = LootType.Blessed;
            AosAttribute = "HitLeechMana";
            AllowWeps = true;
            AllowGlasses = true;
		}

		public WeaponAttributeIncreaseDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
            writer.Write( (int) Level );
            writer.Write((bool) AllowWeps );
            writer.Write((bool) AllowGlasses );
            writer.Write((string) AosAttribute );

        }

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			LootType = LootType.Blessed;

			int version = reader.ReadInt();
            Level = reader.ReadInt();
            AllowWeps = reader.ReadBool();
            AllowGlasses = reader.ReadBool();
            AosAttribute = reader.ReadString();
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
				from.Target = new WeaponAttributeIncreaseTarget( this ); // Call our target
			 }
		}	
	}
}