using System;
using Server;
using Server.Mobiles;
using Server.Targeting;
using Server.Items;
using Server.Gumps;

namespace Server.Engines.Craft
{
	public class Repair
	{
		public Repair()
		{
		}

		public static void Do( Mobile from, CraftSystem craftSystem, BaseTool tool )
		{
			from.Target = new InternalTarget( craftSystem, tool );
			from.SendLocalizedMessage( 1044276 ); // Target an item to repair.
		}

        public static void Do(Mobile from, CraftSystem craftSystem, RepairDeed deed)
        {
            from.Target = new InternalTarget(craftSystem, deed);
            from.SendLocalizedMessage(1044276); // Target an item to repair.
        }

        private class InternalTarget : Target
		{
			private CraftSystem m_CraftSystem;
			private BaseTool m_Tool;
            private RepairDeed m_Deed;

            public InternalTarget( CraftSystem craftSystem, BaseTool tool ) :  base ( 2, false, TargetFlags.None )
			{
				m_CraftSystem = craftSystem;
				m_Tool = tool;
			}

            public InternalTarget(CraftSystem craftSystem, RepairDeed deed) : base(2, false, TargetFlags.None)
            {
                m_CraftSystem = craftSystem;
                m_Deed = deed;
            }

            private static void EndGolemRepair( object state )
			{
				((Mobile)state).EndAction( typeof( Golem ) );
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				int number;

				if ( m_CraftSystem is DefTinkering )
				{
					if ( targeted is Golem )
					{
						Golem g = (Golem)targeted;
						int damage = g.HitsMax - g.Hits;
						if ( g.IsDeadBondedPet )
						{
							if ( from.Skills[SkillName.Tinkering].Value > 79.9 )
								{
								if ( !g.Map.CanFit( g.Location, 16, false, false ) )
									{
									number = 501042; // Target can not be resurrected at that location.
									}
								else 
									{
									double chance = ((from.Skills[SkillName.Tinkering].Value - 68.0) / 50.0);
									if ( chance > Utility.RandomDouble() )
										{
										Mobile master = g.ControlMaster;
										if ( master != null && master.InRange( g, 3 ) )
											{
											number = 503255; // You are able to resurrect the creature.
											master.SendGump( new PetResurrectGump( from, g ) );
											}
										else
											number = 1049670; // The pet's owner must be nearby to attempt resurrection.
										}
									else
										number = 503256; // You fail to resurrect the creature.
									}
								}
							else
								number = 503256; // You fail to resurrect the creature.
						}
						
						else if ( damage <= 0 )
						{
							number = 500423; // That is already in full repair.
						}
						else
						{
							double skillValue = from.Skills[SkillName.Tinkering].Value;

							if ( skillValue < 60.0 )
							{
								number = 1044153; // You don't have the required skills to attempt this item.
							}
							else if ( !from.CanBeginAction( typeof( Golem ) ) )
							{
								number = 501789; // You must wait before trying again.
							}
							else
							{
								if ( damage > (int)(skillValue * 0.3) )
									damage = (int)(skillValue * 0.3);//max 33

								damage += 30;

								if ( !from.CheckSkill( SkillName.Tinkering, 0.0, 100.0 ) )
									damage /= 2;

								Container pack = from.Backpack;

								if ( pack != null )
								{
									int v = pack.ConsumeUpTo( typeof( IronIngot ), (damage+4)/5 );

									if ( v > 0 )
									{
										g.Hits += (v*5+(int)(from.Skills[SkillName.Blacksmith].Value / 5));//max 36.8 w/o ancient hammer
                                        g.Hits += (int)(from.Skills[SkillName.Tailoring].Value / 5);//max24
                                        g.Hits += (int)(from.Skills[SkillName.ArmsLore].Value / 5);//max24

                                        number = 1044279; // You repair the item.

										from.BeginAction( typeof( Golem ) );
										Timer.DelayCall( TimeSpan.FromSeconds( 5.0 ), new TimerStateCallback( EndGolemRepair ), from );
									}
									else
									{
										number = 1044037; // You do not have sufficient metal to make that.
									}
								}
								else
								{
									number = 1044037; // You do not have sufficient metal to make that.
								}
							}
						}
					}
					else
					{
						number = 500426; // You can't repair that.
					}
				}
				else if ( targeted is BaseWeapon )
				{
					BaseWeapon weapon = (BaseWeapon)targeted;
					SkillName skill = m_CraftSystem.MainSkill;
					int toWeaken = 0;

					if ( Core.AOS )
					{
						toWeaken = 1;
					}
					else if ( skill != SkillName.Tailoring )
					{
						double skillLevel = from.Skills[skill].Base;

						if ( skillLevel >= 90.0 )
							toWeaken = 1;
						else if ( skillLevel >= 70.0 )
							toWeaken = 2;
						else
							toWeaken = 3;
					}

					if ( m_CraftSystem.CraftItems.SearchForSubclass( weapon.GetType() ) == null )
					{
						number = 1044277; // That item cannot be repaired.
					}
					else if ( !weapon.IsChildOf( from.Backpack ) )
					{
						number = 1044275; // The item must be in your backpack to repair it.
					}
					else if ( weapon.MaxHitPoints <= 0 || weapon.HitPoints == weapon.MaxHitPoints )
					{
						number = 1044281; // That item is in full repair
					}
					else if ( weapon.MaxHitPoints <= toWeaken )
					{
						number = 500424; // You destroyed the item.
						m_CraftSystem.PlayCraftEffect( from );
						weapon.Delete();
					}
					else if ( from.CheckSkill( skill, -285.0, 100.0 ) )
					{
						number = 1044279; // You repair the item.
						m_CraftSystem.PlayCraftEffect( from );
						weapon.MaxHitPoints -= toWeaken;
						weapon.HitPoints = weapon.MaxHitPoints;
					}
					else
					{
						number = 1044280; // You fail to repair the item.
						m_CraftSystem.PlayCraftEffect( from );
						weapon.MaxHitPoints -= toWeaken;
						weapon.HitPoints -= toWeaken;
					}

					if ( weapon.MaxHitPoints <= toWeaken )
						from.SendLocalizedMessage( 1044278 ); // That item has been repaired many times, and will break if repairs are attempted again.
				}
				else if ( targeted is BaseArmor )
				{
					BaseArmor armor = (BaseArmor)targeted;
					SkillName skill = m_CraftSystem.MainSkill;
					int toWeaken = 0;

					if ( Core.AOS )
					{
						toWeaken = 1;
					}
					else if ( skill != SkillName.Tailoring )
					{
						double skillLevel = from.Skills[skill].Base;

						if ( skillLevel >= 90.0 )
							toWeaken = 1;
						else if ( skillLevel >= 70.0 )
							toWeaken = 2;
						else
							toWeaken = 3;
					}

					if ( m_CraftSystem.CraftItems.SearchForSubclass( armor.GetType() ) == null )
					{
						number = 1044277; // That item cannot be repaired.
					}
					else if ( !armor.IsChildOf( from.Backpack ) )
					{
						number = 1044275; // The item must be in your backpack to repair it.
					}
					else if ( armor.MaxHitPoints <= 0 || armor.HitPoints == armor.MaxHitPoints )
					{
						number = 1044281; // That item is in full repair
					}
					else if ( armor.MaxHitPoints <= toWeaken )
					{
						number = 500424; // You destroyed the item.
						m_CraftSystem.PlayCraftEffect( from );
						armor.Delete();
					}
					else if ( from.CheckSkill( skill, -285.0, 100.0 ) )
					{
						number = 1044279; // You repair the item.
						m_CraftSystem.PlayCraftEffect( from );
						armor.MaxHitPoints -= toWeaken;
						armor.HitPoints = armor.MaxHitPoints;
					}
					else
					{
						number = 1044280; // You fail to repair the item.
						m_CraftSystem.PlayCraftEffect( from );
						armor.MaxHitPoints -= toWeaken;
						armor.HitPoints -= toWeaken;
					}

					if ( armor.MaxHitPoints <= toWeaken )
						from.SendLocalizedMessage( 1044278 ); // That item has been repaired many times, and will break if repairs are attempted again.
				}
				else if ( targeted is Item )
				{
					number = 1044277; // That item cannot be repaired.
				}
				else
				{
					number = 500426; // You can't repair that.
				}

				CraftContext context = m_CraftSystem.GetContext( from );

				from.SendGump( new CraftGump( from, m_CraftSystem, m_Tool, number ) );
			}
		}
	}
}