using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Targeting;
using Server.Spells;

namespace Server.Items
{
	public class CraftedPoisonGasTrap : BaseTrap
	{

		private Mobile m_TrapOwner;
		private int m_UsesRemaining;
		private Poison m_Poison;

		private DateTime lastused = DateTime.Now;
		private TimeSpan delay = TimeSpan.FromSeconds( 7 );

		[CommandProperty( AccessLevel.GameMaster )]
		public Poison Poison
		{
			get{ return m_Poison; }
			set{ m_Poison = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile TrapOwner
		{
			get
			{
				return m_TrapOwner;
			}
			set
			{
				m_TrapOwner = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int UsesRemaining
		{
			get
			{
				return m_UsesRemaining;
			}
			set
			{
				m_UsesRemaining = value;
			}
		}



		[Constructable]
		public CraftedPoisonGasTrap() : base( 0x1848 )
		{
			this.Visible = false;
			this.Hue = 272;
			this.UsesRemaining = 1;
            		this.Name = "A poison-gas trap";
			this.Poison = Poison.Regular;
		}

		public override bool PassivelyTriggered{ get{ return true; } }
		public override TimeSpan PassiveTriggerDelay{ get{ return TimeSpan.FromSeconds( 2.0 ); } }
		public override int PassiveTriggerRange{ get{ return 2; } }
		public override TimeSpan ResetDelay{ get{ return TimeSpan.FromSeconds( 10 ); } }

		public override void OnTrigger( Mobile from )
		{
			if ( from.AccessLevel > AccessLevel.Player )
				return;

			if ( from == TrapOwner )
				return;

			if ( this.Visible == false )
				this.Visible = true;

			Effects.SendLocationParticles( EffectItem.Create( Location, Map, EffectItem.DefaultDuration ), 0x3914, 10, 30, 5052 );
			Effects.PlaySound( Location, Map, 0x231 );

			if ( from.Alive )
			{
				
				ArrayList targets = new ArrayList();
				IPooledEnumerable eable = this.Map.GetMobilesInRange( new Point3D( Location ), 2 );

				foreach ( Mobile m in eable )
				{
					targets.Add( m );
		
				}

				eable.Free();
				if ( targets.Count > 0 )
				{

					for ( int i = 0; i < targets.Count; ++i )
					{
						Mobile m = (Mobile)targets[i];
						m.ApplyPoison( m, m_Poison );
					}
				}
			}


			this.UsesRemaining -= 1;

			if ( this.UsesRemaining <= 0 )
				this.Delete();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from == TrapOwner )
			{
				from.SendMessage( "You conceal the trap." );
				this.Visible = false;
			}

			else if ( from != TrapOwner )
			{

				int trapmaxskill = (int)Math.Round(from.Skills.RemoveTrap.Value) + 50;
				int trapminskill = trapmaxskill - 20;
				int trapcheck = Utility.RandomMinMax(trapminskill, trapmaxskill);
				int trappower = 0;
				Poison pos = this.Poison;

				if ( pos == Poison.Lesser )
				{
					trappower += 30;
				}

				if ( pos == Poison.Regular )
				{
					trappower += 60;
				}

				if ( pos == Poison.Greater )
				{
					trappower += 90;
				}

				if ( pos == Poison.Deadly )
				{
					trappower += 120;
				}

				if ( pos == Poison.Lethal )
				{
					trappower += 150;
				}

				if ( trappower > trapmaxskill )
				{
                    			from.SendMessage("You have no chance of removing this trap.");
					return;
				}

				if ( lastused + delay > DateTime.Now ) 
				{
					from.SendMessage("You must wait 7 seconds between uses.");
					return;
				}
				else
					lastused = DateTime.Now;

				if ( trapcheck >= trappower )
				{
					this.Delete();
					from.SendMessage("You successfully remove the trap.");
				}
				else
				{
					from.SendMessage("You fail to remove the trap.");
					
					if ( 0.5 >= Utility.RandomDouble())
					{
						from.SendMessage("You accidently trigger one of the trap's components. It damages you before you can deactive it.");
						Spells.SpellHelper.Damage( TimeSpan.FromSeconds( 0.5 ), from, from, Utility.RandomMinMax( 15, 25 ), 100, 0, 0, 0, 0 );
					}
				}
			}
		}

		public CraftedPoisonGasTrap( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_TrapOwner );
           		writer.Write( m_UsesRemaining);
                	Poison.Serialize( m_Poison, writer );

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_TrapOwner = reader.ReadMobile();
					m_UsesRemaining = reader.ReadInt();
					m_Poison = Poison.Deserialize( reader );

					break;
				}
			}
		}
	}
}