using System;

namespace Server.Items
{
	public class CraftedFireColumnTrap : BaseTrap
	{

		private Mobile m_TrapOwner;
		private int m_UsesRemaining, m_TrapPower;

		private DateTime lastused = DateTime.Now;
		private TimeSpan delay = TimeSpan.FromSeconds( 7 );

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

		[CommandProperty( AccessLevel.GameMaster )]
		public int TrapPower
		{
			get
			{
				return m_TrapPower;
			}
			set
			{
				m_TrapPower = value;
			}
		}

		[Constructable]
		public CraftedFireColumnTrap() : base( 0x1B71 )
		{
			this.Visible = false;
			this.Hue = 45;
			this.UsesRemaining = 1;
            this.Name = "A fire-column trap";
			this.TrapPower = 100;
		}

		public override bool PassivelyTriggered{ get{ return true; } }
		public override TimeSpan PassiveTriggerDelay{ get{ return TimeSpan.FromSeconds( 2.0 ); } }
		public override int PassiveTriggerRange{ get{ return 0; } }
		public override TimeSpan ResetDelay{ get{ return TimeSpan.FromSeconds( 10 ); } }

		public override void OnTrigger( Mobile from )
		{
			if ( from.AccessLevel > AccessLevel.Player )
				return;

			if ( from == TrapOwner )
				return;

			if ( this.Visible == false )
				this.Visible = true;

			Effects.SendLocationParticles( EffectItem.Create( Location, Map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 5052 );
			Effects.PlaySound( Location, Map, 0x225 );

           		int MinDamage = 20 * (TrapPower / 100);
            		int MaxDamage = 40 * (TrapPower / 100);

			if ( MinDamage < 5 )
				MinDamage = 5;

			if ( MaxDamage < 10 )
				MaxDamage = 10;

			if ( from.Alive && CheckRange( from.Location, 0 ) )

				Spells.SpellHelper.Damage( TimeSpan.FromSeconds( 0.5 ), from, from, Utility.RandomMinMax( MinDamage, MaxDamage ), 0, 100, 0, 0, 0 );

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
				int trappower = this.TrapPower;
				int trapcheck = Utility.RandomMinMax(trapminskill, trapmaxskill);

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

		public CraftedFireColumnTrap( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_TrapOwner );
           		writer.Write( m_UsesRemaining);
                writer.Write(m_TrapPower);

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
					m_TrapPower = reader.ReadInt();

					break;
				}
			}
		}
	}
}