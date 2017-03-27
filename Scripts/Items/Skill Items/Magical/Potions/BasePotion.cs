using System;
using Server;
using Server.Engines.Craft;
using System.Collections.Generic;

namespace Server.Items
{
	public enum PotionEffect
	{
		Nightsight,
		CureLesser,
		Cure,
		CureGreater,
		Agility,
		AgilityGreater,
		Strength,
		StrengthGreater,
		PoisonLesser,
		Poison,
		PoisonGreater,
		PoisonDeadly,
		Refresh,
		RefreshTotal,
		HealLesser,
		Heal,
		HealGreater,
		ExplosionLesser,
		Explosion,
		ExplosionGreater,
		Conflagration,
		ConflagrationGreater,
		MaskOfDeath,		// Mask of Death is not available in OSI but does exist in cliloc files
		MaskOfDeathGreater,	// included in enumeration for compatability if later enabled by OSI
		ConfusionBlast,
		ConfusionBlastGreater,
		Invisibility,
		Parasitic,
		Darkglow,
	}

	public abstract class BasePotion : Item, ICraftable, ICommodity
	{
		private PotionEffect m_PotionEffect;
        private int m_LessPoisoned;
        private int m_RegPoisoned;
        private int m_GreatPoisoned;
        private int m_DeadlyPoisoned;
        private int m_LethalPoisoned;
        private Mobile m_Poisoner;

        [CommandProperty(AccessLevel.GameMaster)]
        public int LessPoisoned
        {
            get { return m_LessPoisoned; }
            set { m_LessPoisoned = value; InvalidateProperties(); }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int RegPoisoned
        {
            get { return m_RegPoisoned; }
            set { m_RegPoisoned = value; InvalidateProperties(); }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int GreatPoisoned
        {
            get { return m_GreatPoisoned; }
            set { m_GreatPoisoned = value; InvalidateProperties(); }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int DeadlyPoisoned
        {
            get { return m_DeadlyPoisoned; }
            set { m_DeadlyPoisoned = value; InvalidateProperties(); }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int LethalPoisoned
        {
            get { return m_LethalPoisoned; }
            set { m_LethalPoisoned = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Poisoner
        {
            get { return m_Poisoner; }
            set { m_Poisoner = value; }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int TotalPoisoned
        {
            get { return LethalPoisoned + DeadlyPoisoned + GreatPoisoned + RegPoisoned + LessPoisoned; }
        }
        public int GetRandomPoisoned( )
        {
            int potnum = Utility.Random(1, Amount);
            if (potnum<LessPoisoned)//Target potion is lesser poisoned
            {
                return 0;
            }
            else
            {
                potnum -= LessPoisoned;
                if (potnum < RegPoisoned)//target potion is regular poisoned
                    return 1;
                else
                {
                    potnum -= RegPoisoned;
                    if (potnum < GreatPoisoned)//target potion is greater poisoned
                        return 2;
                    else
                    {
                        potnum -= GreatPoisoned;
                        if (potnum < DeadlyPoisoned)//target potion is deadly poisoned
                            return 3;
                        else
                        {
                            potnum -= DeadlyPoisoned;
                            if (potnum < LethalPoisoned)//target potion is lethal poisoned
                                return 4;
                            else
                                return -1;
                        }
                    }
                }
            }
        }

        public PotionEffect PotionEffect
		{
			get
			{
				return m_PotionEffect;
			}
			set
			{
				m_PotionEffect = value;
				InvalidateProperties();
			}
		}

		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return (Core.ML); } }

		public override int LabelNumber{ get{ return 1041314 + (int)m_PotionEffect; } }

		public BasePotion( int itemID, PotionEffect effect ) : base( itemID )
		{
			m_PotionEffect = effect;

			Stackable = Core.ML;
			Weight = 1.0;
		}

		public BasePotion( Serial serial ) : base( serial )
		{
		}

		public virtual bool RequireFreeHand{ get{ return true; } }

		public static bool HasFreeHand( Mobile m )
		{
			Item handOne = m.FindItemOnLayer( Layer.OneHanded );
			Item handTwo = m.FindItemOnLayer( Layer.TwoHanded );

			if ( handTwo is BaseWeapon )
				handOne = handTwo;
			if ( handTwo is BaseRanged )
			{
				BaseRanged ranged = (BaseRanged) handTwo;
				
				if ( ranged.Balanced )
					return true;
			}

			return ( handOne == null || handTwo == null );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			if ( from.InRange( this.GetWorldLocation(), 1 ) )
			{
				if (!RequireFreeHand || HasFreeHand(from))
				{
					if (this is BaseExplosionPotion && Amount > 1)
					{
						BasePotion pot = (BasePotion)Activator.CreateInstance(this.GetType());

						if (pot != null)
						{
							Amount--;

							if (from.Backpack != null && !from.Backpack.Deleted)
							{
								from.Backpack.DropItem(pot);
							}
							else
							{
								pot.MoveToWorld(from.Location, from.Map);
							}
							pot.Drink( from );
						}
					}
					else
					{
						this.Drink( from );
					}
				}
				else
				{
					from.SendLocalizedMessage(502172); // You must have a free hand to drink a potion.
				}
			}
			else
			{
				from.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version
			writer.Write( (int) m_PotionEffect );
            writer.Write( (int) LessPoisoned );
            writer.Write( (int) RegPoisoned );
            writer.Write( (int) GreatPoisoned );
            writer.Write( (int) DeadlyPoisoned );
            writer.Write( (int) LethalPoisoned );
        //Poison.Serialize(m_Poison, writer);
        //writer.Write((int)m_PoisonCharges);
        //writer.Write((Mobile)m_Poisoner);
    }

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
                case 2:
                {
                    m_PotionEffect = (PotionEffect)reader.ReadInt();
                        LessPoisoned = reader.ReadInt();
                        RegPoisoned = reader.ReadInt();
                        GreatPoisoned = reader.ReadInt();
                        DeadlyPoisoned = reader.ReadInt();
                        LethalPoisoned = reader.ReadInt();
                    //m_PoisonCharges = null;
                    //m_Poisoner = null;
                    break;
                }
				case 1:
                case 0:
				{
					m_PotionEffect = (PotionEffect)reader.ReadInt();
					break;
				}
			}

			if( version ==  0 )
				Stackable = Core.ML;
		}

		public abstract void Drink( Mobile from );

		public static void PlayDrinkEffect( Mobile m )
		{
			m.RevealingAction();

			m.PlaySound( 0x2D6 );

			#region Dueling
			if ( !Engines.ConPVP.DuelContext.IsFreeConsume( m ) )
				m.AddToBackpack( new Bottle() );
			#endregion

			if ( m.Body.IsHuman && !m.Mounted )
				m.Animate( 34, 5, 1, true, false, 0 );
		}

		public static int EnhancePotions( Mobile m )
		{
			int EP = AosAttributes.GetValue( m, AosAttribute.EnhancePotions );
			int skillBonus = m.Skills.Alchemy.Fixed / 330 * 10;

			if ( Core.ML && EP > 50 && m.AccessLevel <= AccessLevel.Player )
				EP = 50;

			return ( EP + skillBonus );
		}

		public static TimeSpan Scale( Mobile m, TimeSpan v )
		{
			if ( !Core.AOS )
				return v;

			double scalar = 1.0 + ( 0.01 * EnhancePotions( m ) );

			return TimeSpan.FromSeconds( v.TotalSeconds * scalar );
		}

		public static double Scale( Mobile m, double v )
		{
			if ( !Core.AOS )
				return v;

			double scalar = 1.0 + ( 0.01 * EnhancePotions( m ) );

			return v * scalar;
		}

		public static int Scale( Mobile m, int v )
		{
			if ( !Core.AOS )
				return v;

			return AOS.Scale( v, 100 + EnhancePotions( m ) );
		}

        public static void DupePoisoned(BasePotion newItem, BasePotion oldItem)
        {
            int LessTotal = oldItem.LessPoisoned + newItem.LessPoisoned;
            int RegTotal = oldItem.RegPoisoned + newItem.RegPoisoned; 
            int GreatTotal = oldItem.GreatPoisoned + newItem.GreatPoisoned;
            int DeadlyTotal = oldItem.DeadlyPoisoned + newItem.DeadlyPoisoned;
            int LethalTotal = oldItem.LethalPoisoned + newItem.LethalPoisoned;
            int AmountTotal = oldItem.Amount + newItem.Amount;

            oldItem.LessPoisoned = (int)(1.0 * LessTotal * oldItem.Amount/AmountTotal);
            oldItem.RegPoisoned = (int)(1.0 * RegTotal * oldItem.Amount / AmountTotal);
            oldItem.GreatPoisoned = (int)(1.0 * GreatTotal * oldItem.Amount / AmountTotal);
            oldItem.DeadlyPoisoned = (int)(1.0 * DeadlyTotal * oldItem.Amount / AmountTotal);
            oldItem.LethalPoisoned = (int)(1.0 * LethalTotal * oldItem.Amount / AmountTotal);

            newItem.LessPoisoned = (int)(1.0 * LessTotal * newItem.Amount / AmountTotal);
            newItem.RegPoisoned = (int)(1.0 * RegTotal * newItem.Amount / AmountTotal);
            newItem.GreatPoisoned = (int)(1.0 * GreatTotal * newItem.Amount / AmountTotal);
            newItem.DeadlyPoisoned = (int)(1.0 * DeadlyTotal * newItem.Amount / AmountTotal);
            newItem.LethalPoisoned = (int)(1.0 * LethalTotal * newItem.Amount / AmountTotal);

            LessTotal -= oldItem.LessPoisoned + newItem.LessPoisoned;
            RegTotal -= oldItem.RegPoisoned + newItem.RegPoisoned;
            GreatTotal -= oldItem.GreatPoisoned + newItem.GreatPoisoned;
            DeadlyTotal -= oldItem.DeadlyPoisoned + newItem.DeadlyPoisoned;
            LethalTotal -= oldItem.LethalPoisoned + newItem.LethalPoisoned;
            for (int i = 0; i < LessTotal; i++)
                if (oldItem.TotalPoisoned < oldItem.Amount)
                    ++oldItem.LessPoisoned;
                else
                    ++newItem.LessPoisoned;
            for (int i = 0; i < RegTotal; i++)
                if (oldItem.TotalPoisoned < oldItem.Amount)
                    ++oldItem.RegPoisoned;
                else
                    ++newItem.RegPoisoned;
            for (int i = 0; i < GreatTotal; i++)
                if (oldItem.TotalPoisoned < oldItem.Amount)
                    ++oldItem.GreatPoisoned;
                else
                    ++newItem.GreatPoisoned;
            for (int i = 0; i < DeadlyTotal; i++)
                if (oldItem.TotalPoisoned < oldItem.Amount)
                    ++oldItem.DeadlyPoisoned;
                else
                    ++newItem.DeadlyPoisoned;
            for (int i = 0; i < LethalTotal; i++)
                if (oldItem.TotalPoisoned < oldItem.Amount)
                    ++oldItem.LethalPoisoned;
                else
                    ++newItem.LethalPoisoned;

        }

        public override bool StackWith( Mobile from, Item dropped, bool playSound )
		{
            bool Stacks = false;
            if (dropped is BasePotion && ((BasePotion)dropped).m_PotionEffect == m_PotionEffect)
            {
                Stacks = base.StackWith(from, dropped, playSound);
                if (Stacks)
                {
                    LethalPoisoned += ((BasePotion)dropped).LethalPoisoned;
                    DeadlyPoisoned += ((BasePotion)dropped).DeadlyPoisoned;
                    GreatPoisoned += ((BasePotion)dropped).GreatPoisoned;
                    RegPoisoned += ((BasePotion)dropped).RegPoisoned;
                    LessPoisoned += ((BasePotion)dropped).LessPoisoned;
                }
                return Stacks;
                
            }
			return false;
		}

		#region ICraftable Members

		public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			if ( craftSystem is DefAlchemy )
			{
				Container pack = from.Backpack;

				if ( pack != null )
				{
					if ( (int) PotionEffect >= (int) PotionEffect.Invisibility )
						return 1;

					List<PotionKeg> kegs = pack.FindItemsByType<PotionKeg>();

					for ( int i = 0; i < kegs.Count; ++i )
					{
						PotionKeg keg = kegs[i];

						if ( keg == null )
							continue;

						if ( keg.Held <= 0 || keg.Held >= 100 )
							continue;

						if ( keg.Type != PotionEffect )
							continue;

						++keg.Held;

						Consume();
						from.AddToBackpack( new Bottle() );

						return -1; // signal placed in keg
					}
				}
			}

			return 1;
		}

		#endregion
	}
}