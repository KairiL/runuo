using System; 
using System.Collections;
using Server.Network; 
using Server.Mobiles; 
using Server.Items; 
using Server.Gumps;

namespace Server.Items
{
	
	public class CraftedTrapComponents : Item
	{

		private bool m_Armed;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Armed
		{
			get
			{
				return m_Armed;
			}
			set
			{
				m_Armed = value;
			}
		}


		[Constructable]
		public CraftedTrapComponents() : base( 0xB7D )
		{
			Name = "Components for a Poison trap";
			ItemID = 7867;
			Weight = 5;
			Hue = 272;
			Armed = false;
		}


		public override void OnDoubleClick( Mobile from ) 
		{ 
			if ( this.Armed == false )
			{
				this.Armed = true;
				from.SendMessage("This trap is now ARMED. Double-clicking these components again will place the trap.");
				this.Name = "Components for a Poison trap [ARMED]";
			}

			else if ( this.Armed == true )
			{
				Map map = from.Map;
				Point3D m_pnt = from.Location;

				int x = from.X;
				int y = from.Y;
				int z = from.Z;

				ArrayList trapshere = CheckTrap( m_pnt, map, 5 );
				if ( trapshere.Count > 0 )
				{
					from.SendMessage( "There is already a trap here." ); 
					return;
				}

				double poisonskill = from.Skills.Poisoning.Value;
                int trapskill = (int)Math.Round(from.Skills.Tinkering.Value) + (int)(from.Skills.Poisoning.Value);
                int trapmod = trapskill - 50;
                int trapuses = (int)(from.Skills.Tailoring.Value + (from.Skills.Carpentry.Value + (from.Skills.ArmsLore.Value + trapskill) / 2) / 4) / 2 + Utility.RandomMinMax(1, 3);
                int rangeBonus = (int)(from.Skills.Fletching.Value * 2 + from.Skills.ArmsLore.Value) / 100;
                int radiusBonus = (int)(from.Skills.Alchemy.Value + from.Skills.Blacksmith.Value + from.Skills.Tinkering.Value + from.Skills.Poisoning.Value + 50) / 100;
                double delayBonus  = (from.Skills.Blacksmith.Value + from.Skills.Carpentry.Value) / 100.0;

                if (from.Skills.Blacksmith.Value >= 120)
                    delayBonus += 1;

                if (from.Skills.Blacksmith.Value >= 135)
                    radiusBonus += 1;

                if (from.Skills.Blacksmith.Value >= 150)
                    delayBonus += 1;

                if (from.Skills.Blacksmith.Value >= 180)
                    radiusBonus += 1;

                CreateTrap(map, x, y, z, from, trapmod, poisonskill, trapskill, trapuses, rangeBonus, radiusBonus, delayBonus);
		
				this.Delete();
			}

		}


        protected void CreateTrap(Map map, int x, int y, int z, Mobile from, int trapmod, double poisonskill, int trapskill, int trapuses, 
            int rangeBonus, int radiusBonus, double delayBonus)
        {
            CraftedTrap trap = new CraftedTrap();

            trap.TrapOwner = from;
            trap.TrapPower += trapmod;
            trap.TriggerRange += rangeBonus;
            trap.DamageRange += radiusBonus;
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


        public static ArrayList CheckTrap( Point3D pnt, Map map, int range )
		{
			ArrayList traps = new ArrayList();

            IPooledEnumerable eable = map.GetItemsInRange(pnt, 1);
            foreach (Item trap in eable)
            {
                if ((trap != null) && (trap is BaseTrap))
                    traps.Add((BaseTrap)trap);
            }
            eable = map.GetItemsInRange( pnt, range );
			foreach ( Item trap in eable ) 
			{ 
				if ( ( trap != null ) && ( trap is BaseTrap ) )
					traps.Add( (BaseTrap)trap ); 
			} 
			eable.Free();

			return traps;
		}


		public CraftedTrapComponents( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
			writer.Write( m_Armed );
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{

					m_Armed = reader.ReadBool();

					break;
				}
			}
		}
	}
}