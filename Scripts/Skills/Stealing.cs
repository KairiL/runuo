using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.Factions;
using Server.Spells.Seventh;
using Server.Spells.Fifth;
using Server.Spells.Necromancy;
using Server.Spells;
using Server.Spells.Ninjitsu;

namespace Server.SkillHandlers
{
	public class Stealing
	{
		public static void Initialize()
		{
			SkillInfo.Table[33].Callback = new SkillUseCallback( OnUse );
		}

		public static readonly bool ClassicMode = false;
		public static readonly bool SuspendOnMurder = false;

		public static bool IsInGuild( Mobile m )
		{
			return ( m is PlayerMobile && ((PlayerMobile)m).NpcGuild == NpcGuild.ThievesGuild );
		}

		public static bool IsInnocentTo( Mobile from, Mobile to )
		{
			return ( Notoriety.Compute( from, (Mobile)to ) == Notoriety.Innocent );
		}


		private class StealingTarget : Target
		{
			private Mobile m_Thief;

            private bool m_Caught;
            
			public StealingTarget( Mobile thief ) : base ( 1, false, TargetFlags.None )
			{
				m_Thief = thief;

				AllowNonlocal = true;
			}
			private Item TryStealItem( Item toSteal )
			{
				Item stolen = null;

				object root = toSteal.RootParent;

				StealableArtifactsSpawner.StealableInstance si = null;
				if ( toSteal.Parent == null || !toSteal.Movable )
					si = StealableArtifactsSpawner.GetStealableInstance( toSteal );

				if ( !IsEmptyHanded( m_Thief ) )
				{
					m_Thief.SendLocalizedMessage( 1005584 ); // Both hands must be free to steal.
				}
				else if ( m_Thief.Region.IsPartOf( typeof( Engines.ConPVP.SafeZone ) ) )
				{
					m_Thief.SendMessage( "You may not steal in this area." );
				}
				else if ( root is Mobile && ((Mobile)root).Player && !IsInGuild( m_Thief ) )
				{
					m_Thief.SendLocalizedMessage( 1005596 ); // You must be in the thieves guild to steal from other players.
				}
				else if ( SuspendOnMurder && root is Mobile && ((Mobile)root).Player && IsInGuild( m_Thief ) && m_Thief.Kills > 0 )
				{
					m_Thief.SendLocalizedMessage( 502706 ); // You are currently suspended from the thieves guild.
				}
				else if ( root is BaseVendor && ((BaseVendor)root).IsInvulnerable )
				{
					m_Thief.SendLocalizedMessage( 1005598 ); // You can't steal from shopkeepers.
				}
				else if ( root is PlayerVendor )
				{
					m_Thief.SendLocalizedMessage( 502709 ); // You can't steal from vendors.
				}
				else if ( !m_Thief.CanSee( toSteal ) )
				{
					m_Thief.SendLocalizedMessage( 500237 ); // Target can not be seen.
				}
				else if ( m_Thief.Backpack == null || !m_Thief.Backpack.CheckHold( m_Thief, toSteal, false, true ) )
				{
					m_Thief.SendLocalizedMessage( 1048147 ); // Your backpack can't hold anything else.
				}
				#region Sigils
				else if ( toSteal is Sigil )
				{						
					PlayerState pl = PlayerState.Find( m_Thief );
					Faction faction = ( pl == null ? null : pl.Faction );

					Sigil sig = (Sigil) toSteal;

					if ( !m_Thief.InRange( toSteal.GetWorldLocation(), 1 ) )
					{
						m_Thief.SendLocalizedMessage( 502703 ); // You must be standing next to an item to steal it.
					}
					else if ( root != null ) // not on the ground
					{
						m_Thief.SendLocalizedMessage( 502710 ); // You can't steal that!
					}
					else if ( faction != null )
					{
						if ( !m_Thief.CanBeginAction( typeof( IncognitoSpell ) ) )
						{
							m_Thief.SendLocalizedMessage( 1010581 ); //	You cannot steal the sigil when you are incognito
						}
						else if ( DisguiseTimers.IsDisguised( m_Thief ) )
						{
							m_Thief.SendLocalizedMessage( 1010583 ); //	You cannot steal the sigil while disguised
						}
						else if ( !m_Thief.CanBeginAction( typeof( PolymorphSpell ) ) )
						{
							m_Thief.SendLocalizedMessage( 1010582 ); //	You cannot steal the sigil while polymorphed				
						}
						else if( TransformationSpellHelper.UnderTransformation( m_Thief ) )
						{
							m_Thief.SendLocalizedMessage( 1061622 ); // You cannot steal the sigil while in that form.
						}
						else if ( AnimalForm.UnderTransformation( m_Thief ) )
						{
							m_Thief.SendLocalizedMessage( 1063222 ); // You cannot steal the sigil while mimicking an animal.
						}
						else if ( pl.IsLeaving )
						{
							m_Thief.SendLocalizedMessage( 1005589 ); // You are currently quitting a faction and cannot steal the town sigil
						}
						else if ( sig.IsBeingCorrupted && sig.LastMonolith.Faction == faction )
						{
							m_Thief.SendLocalizedMessage( 1005590 ); //	You cannot steal your own sigil
						}
						else if ( sig.IsPurifying )
						{
							m_Thief.SendLocalizedMessage( 1005592 ); // You cannot steal this sigil until it has been purified
						}
						else if ( m_Thief.CheckTargetSkill( SkillName.Stealing, toSteal, 80.0, 80.0 ) )
						{
							if ( Sigil.ExistsOn( m_Thief ) )
							{
								m_Thief.SendLocalizedMessage( 1010258 ); //	The sigil has gone back to its home location because you already have a sigil.
							}
							else if ( m_Thief.Backpack == null || !m_Thief.Backpack.CheckHold( m_Thief, sig, false, true ) )
							{
								m_Thief.SendLocalizedMessage( 1010259 ); //	The sigil has gone home because your backpack is full
							}
							else
							{
								if ( sig.IsBeingCorrupted )
									sig.GraceStart = DateTime.UtcNow; // begin grace period

								m_Thief.SendLocalizedMessage( 1010586 ); // YOU STOLE THE SIGIL!!!   (woah, calm down now)

								if ( sig.LastMonolith != null && sig.LastMonolith.Sigil != null ) {
									sig.LastMonolith.Sigil = null;
									sig.LastStolen = DateTime.UtcNow;
								}

								return sig;
							}
						}
						else
						{
							m_Thief.SendLocalizedMessage( 1005594 ); //	You do not have enough skill to steal the sigil
						}
					}
					else
					{
						m_Thief.SendLocalizedMessage( 1005588 ); //	You must join a faction to do that
					}
				}
				#endregion
				else if ( si == null && ( toSteal.Parent == null || !toSteal.Movable ) )
				{
					m_Thief.SendLocalizedMessage( 502710 ); // You can't steal that!
				}
				else if ( !toSteal.Insured && (toSteal.LootType == LootType.Newbied || toSteal.CheckBlessed( root ) ) )
				{
					m_Thief.SendLocalizedMessage( 502710 ); // You can't steal that!
				}
				else if ( Core.AOS && si == null && toSteal is Container )
				{
					m_Thief.SendLocalizedMessage( 502710 ); // You can't steal that!
				}
				else if ( !m_Thief.InRange( toSteal.GetWorldLocation(), 1 ) )
				{
					m_Thief.SendLocalizedMessage( 502703 ); // You must be standing next to an item to steal it.
				}
				else if ( si != null && m_Thief.Skills[SkillName.Stealing].Value < 100.0 )
				{
					m_Thief.SendLocalizedMessage( 1060025, "", 0x66D ); // You're not skilled enough to attempt the theft of this item.
				}
				else if ( toSteal.Parent is Mobile )
				{
					m_Thief.SendLocalizedMessage( 1005585 ); // You cannot steal items which are equiped.
				}
				else if ( root == m_Thief )
				{
					m_Thief.SendLocalizedMessage( 502704 ); // You catch yourself red-handed.
				}
				else if ( root is Mobile && ((Mobile)root).AccessLevel > AccessLevel.Player )
				{
					m_Thief.SendLocalizedMessage( 502710 ); // You can't steal that!
				}
				else if ( root is Mobile && !m_Thief.CanBeHarmful( (Mobile)root ) )
				{
				}
				else if ( root is Corpse )
				{
					m_Thief.SendLocalizedMessage( 502710 ); // You can't steal that!
				}
				else
				{
					double w = toSteal.Weight + toSteal.TotalWeight;

					if ( w > 10 )
					{
						m_Thief.SendMessage( "That is too heavy to steal." );
					}
					else
					{
						if ( toSteal.Stackable && toSteal.Amount > 1 )
						{
							int maxAmount = (int)((m_Thief.Skills[SkillName.Stealing].Value / 10.0) / toSteal.Weight);

							if ( maxAmount < 1 )
								maxAmount = 1;
							else if ( maxAmount > toSteal.Amount )
								maxAmount = toSteal.Amount;

							int amount = Utility.RandomMinMax( 1, maxAmount );

							if ( amount >= toSteal.Amount )
							{
								int pileWeight = (int)Math.Ceiling( toSteal.Weight * toSteal.Amount );
								pileWeight *= 10;

								if ( m_Thief.CheckTargetSkill( SkillName.Stealing, toSteal, pileWeight - 22.5, pileWeight + 27.5 ) )
									stolen = toSteal;
							}
							else
							{
								int pileWeight = (int)Math.Ceiling( toSteal.Weight * amount );
								pileWeight *= 10;

								if ( m_Thief.CheckTargetSkill( SkillName.Stealing, toSteal, pileWeight - 22.5, pileWeight + 27.5 ) )
								{
									stolen = Mobile.LiftItemDupe( toSteal, toSteal.Amount - amount );

									if ( stolen == null )
										stolen = toSteal;
								}
							}
						}
						else
						{
							int iw = (int)Math.Ceiling( w );
							iw *= 10;

							if ( m_Thief.CheckTargetSkill( SkillName.Stealing, toSteal, iw - 22.5, iw + 27.5 ) )
								stolen = toSteal;
						}

						if ( stolen != null )
						{
							m_Thief.SendLocalizedMessage( 502724 ); // You succesfully steal the item.

							if ( si != null )
							{
								toSteal.Movable = true;
								si.Item = null;
							}
						}
						else
						{
							m_Thief.SendLocalizedMessage( 502723 ); // You fail to steal the item.
						}

						m_Caught = ( m_Thief.Skills[SkillName.Stealing].Value < Utility.Random( 150 ) );
					}
				}

				return stolen;
			}

			protected override void OnTarget( Mobile from, object target )
			{
				from.RevealingAction();

				Item stolen = null;
                
				object root = null;

				if ( target is Item )
				{
					root = ((Item)target).RootParent;
                    if (root != from)//Change to if (true) if this script fucks up
                        stolen = TryStealItem((Item)target);
                    else
                    {
                        m_Thief.SendMessage("Who would you like to reverse pickpocket?");
                        m_Thief.Target = new Stealing.StealingTarget.UnstealingTarget(m_Thief, (Item)target );
                    }
				}
				else if ( target is Mobile )
				{
					Container pack = ((Mobile)target).Backpack;

					if ( pack != null && pack.Items.Count > 0 )
					{
						int randomIndex = Utility.Random( pack.Items.Count );

						root = target;
						stolen = TryStealItem( pack.Items[randomIndex] );
					}
				} 
				else 
				{
					m_Thief.SendLocalizedMessage( 502710 ); // You can't steal that!
				}

				if ( stolen != null )
				{
					from.AddToBackpack( stolen );

					if ( !( stolen is Container || stolen.Stackable ) ) { // do not return stolen containers or stackable items
						StolenItem.Add( stolen, m_Thief, root as Mobile );
					}
				}

				if ( m_Caught )
				{
					if ( root == null )
					{
						m_Thief.CriminalAction( false );
					}
					else if ( root is Corpse && ((Corpse)root).IsCriminalAction( m_Thief ) )
					{
						m_Thief.CriminalAction( false );
					}
					else if ( root is Mobile )
					{
						Mobile mobRoot = (Mobile)root;

						if ( !IsInGuild( mobRoot ) && IsInnocentTo( m_Thief, mobRoot ) )
							m_Thief.CriminalAction( false );

						string message = String.Format( "You notice {0} trying to steal from {1}.", m_Thief.Name, mobRoot.Name );

						foreach ( NetState ns in m_Thief.GetClientsInRange( 8 ) )
						{
							if ( ns.Mobile != m_Thief )
								ns.Mobile.SendMessage( message );
						}
					}
				}
				else if ( root is Corpse && ((Corpse)root).IsCriminalAction( m_Thief ) )
				{
					m_Thief.CriminalAction( false );
				}

				if ( root is Mobile && ((Mobile)root).Player && m_Thief is PlayerMobile && IsInnocentTo( m_Thief, (Mobile)root ) && !IsInGuild( (Mobile)root ) )
				{
					PlayerMobile pm = (PlayerMobile)m_Thief;

					pm.PermaFlags.Add( (Mobile)root );
					pm.Delta( MobileDelta.Noto );
				}
			}
            private class UnstealingTarget : Target
            {
                private Item m_toUnsteal;

                private Mobile m_Thief; 

                private bool m_Caught;

                public UnstealingTarget(Mobile thief, Item toUnsteal) : base(1, false, TargetFlags.None)
                {
                    m_toUnsteal = toUnsteal;
                    m_Thief = thief;
                    AllowNonlocal = true;
                }

                private Item TryUnstealItem(Mobile target)
                {
                    m_Thief.RevealingAction();
                    Item unstolen = null;

                    if (!IsEmptyHanded(m_Thief))
                    {
                        m_Thief.SendLocalizedMessage(1005584); // Both hands must be free to steal.
                    }
                    else if (m_Thief.Region.IsPartOf(typeof(Engines.ConPVP.SafeZone)))
                    {
                        m_Thief.SendMessage("You may not steal in this area.");
                    }
                    else if (target.Player && !IsInGuild(m_Thief))
                    {
                        m_Thief.SendLocalizedMessage(1005596); // You must be in the thieves guild to steal from other players.
                    }
                    else if (SuspendOnMurder && target.Player && IsInGuild(m_Thief) && m_Thief.Kills > 0)
                    {
                        m_Thief.SendLocalizedMessage(502706); // You are currently suspended from the thieves guild.
                    }
                    else if (target is BaseVendor && ((BaseVendor)target).IsInvulnerable)
                    {
                        m_Thief.SendLocalizedMessage(1005598); // You can't steal from shopkeepers.
                    }
                    else if (target is PlayerVendor)
                    {
                        m_Thief.SendLocalizedMessage(502709); // You can't steal from vendors.
                    }
                    else if (!m_Thief.CanSee(target))
                    {
                        m_Thief.SendLocalizedMessage(500237); // Target can not be seen.
                    }
                    else if (m_Thief.Backpack == null || !target.Backpack.CheckHold(target, m_toUnsteal, false, true))
                    {
                        m_Thief.SendMessage("Their backpack can't hold anything else"); // Your backpack can't hold anything else.
                    }
                    else if (m_toUnsteal is Sigil)
                    {
                        m_Thief.SendMessage("You can't unsteal a sigil!");
                    }
                    else if (m_toUnsteal.Parent == null || !m_toUnsteal.Movable)
                    {
                        m_Thief.SendLocalizedMessage(502710); // You can't steal that!
                    }
                    else if (m_toUnsteal.LootType == LootType.Newbied || m_toUnsteal.CheckBlessed(m_Thief))
                    {
                        m_Thief.SendLocalizedMessage(502710); // You can't steal that!
                    }
                    else if (Core.AOS && m_toUnsteal is Container)
                    {
                        m_Thief.SendLocalizedMessage(502710); // You can't steal that!
                    }
                    else if (!m_Thief.InRange(target.Location, 1))
                    {
                        m_Thief.SendMessage("You must be next to a target to reverse pickpocket them."); // You must be standing next to an item to steal it.
                    }
                    else if (target == m_Thief)
                    {
                        m_Thief.SendLocalizedMessage(502704); // You catch yourself red-handed.
                    }
                    else if (target.AccessLevel > AccessLevel.Player)
                    {
                        m_Thief.SendLocalizedMessage(502710); // You can't steal that!
                    }
                    else if (!m_Thief.CanBeHarmful((Mobile)target))
                    {
                    }
                    else
                    {
                        double w = m_toUnsteal.Weight + m_toUnsteal.TotalWeight;

                        if (w > 10)
                        {
                            m_Thief.SendMessage("That is too heavy to steal.");
                        }
                        else
                        {
                            if (m_toUnsteal.Stackable && m_toUnsteal.Amount > 1)
                            {
                                int maxAmount = (int)((m_Thief.Skills[SkillName.Stealing].Value / 10.0) / m_toUnsteal.Weight);

                                if (maxAmount < 1)
                                    maxAmount = 1;
                                else if (maxAmount > m_toUnsteal.Amount)
                                    maxAmount = m_toUnsteal.Amount;

                                int amount = Utility.RandomMinMax(1, maxAmount);

                                if (amount >= m_toUnsteal.Amount)
                                {
                                    int pileWeight = (int)Math.Ceiling(m_toUnsteal.Weight * m_toUnsteal.Amount);
                                    pileWeight *= 10;

                                    if (m_Thief.CheckTargetSkill(SkillName.Stealing, m_toUnsteal, pileWeight - 22.5, pileWeight + 27.5))
                                    {
                                        if (m_toUnsteal.Insured)
                                            m_toUnsteal.Insured = false;
                                        unstolen = m_toUnsteal;
                                    }
                                }
                                else
                                {
                                    int pileWeight = (int)Math.Ceiling(m_toUnsteal.Weight * amount);
                                    pileWeight *= 10;

                                    if (m_Thief.CheckTargetSkill(SkillName.Stealing, m_toUnsteal, pileWeight - 22.5, pileWeight + 27.5))
                                    {
                                        unstolen = Mobile.LiftItemDupe(m_toUnsteal, m_toUnsteal.Amount - amount);

                                        if (unstolen == null)
                                            unstolen = m_toUnsteal;
                                    }
                                }
                            }
                            else
                            {
                                int iw = (int)Math.Ceiling(w);
                                iw *= 10;

                                if (m_Thief.CheckTargetSkill(SkillName.Stealing, m_toUnsteal, iw - 22.5, iw + 27.5))
                                    if (m_toUnsteal.Insured)
                                        m_toUnsteal.Insured = false;
                                unstolen = m_toUnsteal;
                            }

                            if (unstolen != null)
                            {
                                m_Thief.SendMessage("You successfully reverse pickpocket the item.");
                                target.AddToBackpack(unstolen);

                                if (!(unstolen is Container || unstolen.Stackable))
                                { // do not return stolen containers or stackable items
                                    StolenItem.Add(unstolen, target, m_Thief as Mobile);
                                }
                            }
                            else
                            {
                                m_Thief.SendMessage("You fail to reverse pickpocket the item.");
                            }
                        }
                        m_Caught = (m_Thief.Skills[SkillName.Stealing].Value < Utility.Random(150));
                    }
                    return unstolen;
                }
               

                protected override void OnTarget(Mobile from, object target )
                {
                    //target
                    //m_Thief
                    Item unstolen = null;
                    if (!(target is Mobile))
                    {
                        m_Thief.SendMessage("You can only reverse pickpocket creatures");
                    }
                    else
                    {
                        unstolen = TryUnstealItem((Mobile)target);
                    }
                    if (unstolen != null)
                    {
                        if (!(unstolen is Container || unstolen.Stackable))
                        { // do not return stolen containers or stackable items
                            StolenItem.Add(unstolen, m_Thief as Mobile, target as Mobile);
                        }
                    }
                    if (m_Caught)
                    {
                        
                        Mobile mobRoot = (Mobile)target;

                        if (!IsInGuild(mobRoot) && IsInnocentTo(m_Thief, mobRoot))
                            m_Thief.CriminalAction(false);

                        string message = String.Format("You notice {0} trying to reverse pickpocket {1}.", m_Thief.Name, mobRoot.Name);

                        foreach (NetState ns in m_Thief.GetClientsInRange(8))
                        {
                            if (ns.Mobile != m_Thief)
                                ns.Mobile.SendMessage(message);
                        }
                    }
                    else if (target is Corpse && ((Corpse)target).IsCriminalAction(m_Thief))
                    {
                        m_Thief.CriminalAction(false);
                    }

                    if (target is Mobile && ((Mobile)target).Player && m_Thief is PlayerMobile && IsInnocentTo(m_Thief, (Mobile)target) && !IsInGuild((Mobile)target))
                    {
                        PlayerMobile pm = (PlayerMobile)m_Thief;

                        pm.PermaFlags.Add((Mobile)target);
                        pm.Delta(MobileDelta.Noto);
                    }
                }
            }
        }

		public static bool IsEmptyHanded( Mobile from )
		{
			if ( from.FindItemOnLayer( Layer.OneHanded ) != null )
				return false;

			if ( from.FindItemOnLayer( Layer.TwoHanded ) != null )
				return false;

			return true;
		}

		public static TimeSpan OnUse( Mobile m )
		{
			if ( !IsEmptyHanded( m ) )
			{
				m.SendLocalizedMessage( 1005584 ); // Both hands must be free to steal.
			}
			else if ( m.Region.IsPartOf( typeof( Engines.ConPVP.SafeZone ) ) )
			{
				m.SendMessage( "You may not steal in this area." );
			}
			else
			{
				m.Target = new Stealing.StealingTarget( m );
				m.RevealingAction();

				m.SendLocalizedMessage( 502698 ); // Which item do you want to steal?
			}

			return TimeSpan.FromSeconds( 10.0 );
		}
	}

	public class StolenItem
	{
		public static readonly TimeSpan StealTime = TimeSpan.FromMinutes( 2.0 );

		private Item m_Stolen;
		private Mobile m_Thief;
		private Mobile m_Victim;
		private DateTime m_Expires;

		public Item Stolen{ get{ return m_Stolen; } }
		public Mobile Thief{ get{ return m_Thief; } }
		public Mobile Victim{ get{ return m_Victim; } }
		public DateTime Expires{ get{ return m_Expires; } }

		public bool IsExpired{ get{ return ( DateTime.UtcNow >= m_Expires ); } }

		public StolenItem( Item stolen, Mobile thief, Mobile victim )
		{
			m_Stolen = stolen;
			m_Thief = thief;
			m_Victim = victim;

			m_Expires = DateTime.UtcNow + StealTime;
		}

		private static Queue<StolenItem> m_Queue = new Queue<StolenItem>();

		public static void Add( Item item, Mobile thief, Mobile victim )
		{
			Clean();

			m_Queue.Enqueue( new StolenItem( item, thief, victim ) );
		}

		public static bool IsStolen( Item item )
		{
			Mobile victim = null;

			return IsStolen( item, ref victim );
		}

		public static bool IsStolen( Item item, ref Mobile victim )
		{
			Clean();

			foreach ( StolenItem si in m_Queue )
			{
				if ( si.m_Stolen == item && !si.IsExpired )
				{
					victim = si.m_Victim;
					return true;
				}
			}

			return false;
		}

		public static void ReturnOnDeath( Mobile killed, Container corpse )
		{
			Clean();

			foreach ( StolenItem si in m_Queue )
			{
				if ( si.m_Stolen.RootParent == corpse && si.m_Victim != null && !si.IsExpired )
				{
					if ( si.m_Victim.AddToBackpack( si.m_Stolen ) )
						si.m_Victim.SendLocalizedMessage( 1010464 ); // the item that was stolen is returned to you.
					else
						si.m_Victim.SendLocalizedMessage( 1010463 ); // the item that was stolen from you falls to the ground.

					si.m_Expires = DateTime.UtcNow; // such a hack
				}
			}
		}

		public static void Clean()
		{
			while ( m_Queue.Count > 0 )
			{
				StolenItem si = m_Queue.Peek();

				if ( si.IsExpired )
					m_Queue.Dequeue();
				else
					break;
			}
		}
	}
}