using System; 
using System.Collections;
using Server.Network; 
using Server.Mobiles; 
using Server.Items; 
using Server.Gumps;

namespace Server.Items
{
	
	public class CraftedPoisonGasComponents : CraftedTrapComponents
	{

		[Constructable]
		public CraftedPoisonGasComponents() : base()
		{
			Name = "Components for a Poison trap";
			ItemID = 7867;
			Weight = 5;
			Hue = 272;
			Armed = false;
		}

        protected void CreateTrap(Map map, int x, int y, int z, Mobile from, int trapmod, double poisonskill, int trapskill, int trapuses,
            int rangeBonus, int radiusBonus, double delayBonus)
        {
            CraftedPoisonGasTrap trap = new CraftedPoisonGasTrap();

            trap.TrapOwner = from;
            trap.TrapPower += trapmod;
            trap.TriggerRange += rangeBonus;
            trap.DamageRange += radiusBonus*2;
            trap.UsesRemaining += trapuses;
            if (delayBonus > 0)
                trap.Delay = (TimeSpan.FromSeconds((trap.Delay.TotalSeconds) / delayBonus));

            if (poisonskill <= 0.0)
                trap.Poison = null;
            else
            if (poisonskill <= 19.9)
            {
                trap.Poison = Poison.Lesser;
                from.SendMessage("Poison Type: Lesser");
            }
            else
            if (poisonskill >= 20 && poisonskill <= 39.9)
            {
                trap.Poison = Poison.Regular;
                from.SendMessage("Poison Type: Regular");
            }
            else
            if (poisonskill >= 40 && poisonskill <= 59.9)
            {
                trap.Poison = Poison.Greater;
                from.SendMessage("Poison Type: Greater");
            }
            else
            if (poisonskill >= 60 && poisonskill <= 79.9)
            {
                trap.Poison = Poison.Deadly;
                from.SendMessage("Poison Type: Deadly");
            }
            else
            if (poisonskill >= 100)
            {
                trap.Poison = Poison.Lethal;
                from.SendMessage("Poison Type: Lethal");
            }

            trap.MoveToWorld(new Point3D(x, y, z), map);

            from.SendMessage("You have configured the trap and concealed it at your location.");
        }

		public CraftedPoisonGasComponents( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					break;
				}
			}
		}
	}
}