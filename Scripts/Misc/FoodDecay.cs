using System;
using Server.Network;
using Server;

namespace Server.Misc
{
	public class FoodDecayTimer : Timer
	{
		public static void Initialize()
		{
			new FoodDecayTimer().Start();
		}

		public FoodDecayTimer() : base( TimeSpan.FromMinutes( 30 ), TimeSpan.FromMinutes( 30 ) )
		{
			Priority = TimerPriority.OneMinute;
		}

		protected override void OnTick()
		{
			FoodDecay();			
		}

		public static void FoodDecay()
		{
			foreach ( NetState state in NetState.Instances )
			{
				HungerDecay( state.Mobile );
				ThirstDecay( state.Mobile );
			}
		}

		public static void HungerDecay( Mobile m )
		{
			if ( m != null && m.Hunger >= 1 )
            {
				m.Hunger -= 1;
                if (m.Hunger <= 5)
                    m.SendMessage("You are starving!");
                else if (m.Hunger <= 10)
                    m.SendMessage("You are starting to feel hungry.");
            }
            else
                m.SendMessage("You are starving!");
            
		}

		public static void ThirstDecay( Mobile m )
		{
			if ( m != null && m.Thirst >= 1 )
            {
				m.Thirst -= 1;
                if (m.Thirst <= 5)
                    m.SendMessage("You are dehydrated!");
                else if (m.Thirst <= 10)
                    m.SendMessage("You are starting to feel parched.");

            }
            else
                m.SendMessage("You are parched!");
		}
	}
}