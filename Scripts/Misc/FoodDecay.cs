using System;
using Server.Network;
using Server;
using Server.Mobiles;

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
            if ( m == null )
                return;
            if ( m is PlayerMobile || (m is BaseCreature && (((BaseCreature)m).ControlMaster != null && !(m is Golem))))
			    if ( m != null && m.Hunger >= 1 )
				    m.Hunger -= 1;
		}

		public static void ThirstDecay( Mobile m )
		{
            if (m == null)
                return;
            if (m is PlayerMobile || (m is BaseCreature && (((BaseCreature)m).ControlMaster != null && !(m is Golem))))
                if ( m != null && m.Thirst >= 1 )
				    m.Thirst -= 1;
		}
	}
}