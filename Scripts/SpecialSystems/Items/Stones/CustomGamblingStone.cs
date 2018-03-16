using System;

namespace Server.Items
{
	public class CustomGamblingStone : Item
	{
        private int m_Cost = 250;

        [CommandProperty(AccessLevel.GameMaster)]
        public int Cost
        {
            get
            {
                return m_Cost;
            }
            set
            {
                m_Cost = value;
                InvalidateProperties();
            }
        }

        private double m_PayoutPercent = .80;

        [CommandProperty(AccessLevel.GameMaster)]
        public double PayoutPerccent
        {
            get
            {
                return m_PayoutPercent;
            }
            set
            {
                m_PayoutPercent = value;
                InvalidateProperties();
            }
        }

        private int m_GamblePot = 0;

        [CommandProperty(AccessLevel.GameMaster)]
        public int GamblePot
        {
            get
            {
                return m_GamblePot;
            }
            set
            {
                m_GamblePot = value;
                InvalidateProperties();
            }
        }

        private bool m_CursePrize = true;

        [CommandProperty(AccessLevel.GameMaster )]
        public bool CursePrize
        {
            get
            {
                return m_CursePrize;
            }
            set
            {
                m_CursePrize = value;
                InvalidateProperties();
            }
        }

        private Type m_GrandPrizeType = null;

        [CommandProperty(AccessLevel.GameMaster)]
        public Type GrandPrizeType
        {
            get 
            {
                return m_GrandPrizeType;
            }
            set
            {
                try
                {
                    Item prize = Loot.Construct(value);
                    prize.Delete();
                    m_GrandPrizeType = value;
                }
                catch
                {
                    m_GrandPrizeType = null;
                }
               InvalidateProperties();
            }
        }
        private double m_SmallChance = .05;

        [CommandProperty(AccessLevel.GameMaster)]
        public double SmallChance
        {
            get
            {
                return m_SmallChance;
            }
            set
            {
                m_SmallChance = value;
                InvalidateProperties();
            }
        }

        private int m_SmallPrize = 1000;

        [CommandProperty(AccessLevel.GameMaster)]
        public int SmallPrize
        {
            get
            {
                return m_SmallPrize;
            }
            set
            {
                m_SmallPrize = value;
                InvalidateProperties();
            }
        }

        private double m_BigChance = .01;

        [CommandProperty(AccessLevel.GameMaster)]
        public double BigChance
        {
            get
            {
                return m_BigChance;
            }
            set
            {
                m_BigChance = value;
                InvalidateProperties();
            }
        }

        private int m_BigPrize = 2500;

        [CommandProperty(AccessLevel.GameMaster)]
        public int BigPrize
        {
            get
            {
                return m_BigPrize;
            }
            set
            {
                m_BigPrize = value;
                InvalidateProperties();
            }
        }

        private double m_JackpotChance = .0001;

        [CommandProperty(AccessLevel.GameMaster)]
        public double JackpotChance
        {
            get
            {
                return m_JackpotChance;
            }
            set
            {
                m_JackpotChance = value;
                InvalidateProperties();
            }
        }

        private double m_GrandPrizeChance = .00001;

        [CommandProperty(AccessLevel.GameMaster)]
        public double GrandPrizeChance
        {
            get
            {
                return m_GrandPrizeChance;
            }
            set
            {
                m_GrandPrizeChance = value;
                InvalidateProperties();
            }
        }

        private int m_GrandPrizeValue = 2000000;

        [CommandProperty(AccessLevel.GameMaster)]
        public int GrandPrizeValue
        {
            get
            {
                return m_GrandPrizeValue;
            }
            set
            {
                m_GrandPrizeValue = value;
                InvalidateProperties();
            }
        }

        private string m_PrizeName = "Nothing!";

        [CommandProperty(AccessLevel.GameMaster)]
        public string PrizeName
        {
            get
            {
                return m_PrizeName;
            }
            set
            {
                m_PrizeName = value;
                InvalidateProperties();
            }
        }
        [CommandProperty(AccessLevel.GameMaster)]
        public int JackpotContribution
        { 
            get
            { 
                return ((int)(( Cost - SmallPrize*SmallChance - BigPrize*BigChance - GrandPrizeValue*GrandPrizeChance)* PayoutPerccent));
            }
        }
        public override string DefaultName
		{
			get { return "a gambling stone"; }
		}

		[Constructable]
		public CustomGamblingStone()
			: base( 0xED4 )
		{
			Movable = false;
			Hue = 0x56;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add("Jackpot: {0}gp \nCost: {1}gp\nGrand Prize: {2}", m_GamblePot, m_Cost, m_PrizeName );
            //list.Add ( "Cost: {0}gp", m_Cost);
            //list.Add ( "Grand Prize: {0}", m_PrizeName );
        }

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );
            base.LabelTo( from, "Cost: {0}gp", m_Cost );
			base.LabelTo( from, "Jackpot: {0}gp", m_GamblePot );
            base.LabelTo( from, "Grand Prize: {0}", m_PrizeName ); 
		}

		public override void OnDoubleClick( Mobile from )
		{
			Container pack = from.Backpack;
            bool winner = false;

			if( pack != null && pack.ConsumeTotal( typeof( Gold ), Cost ) )
			{
                if (JackpotContribution > 0)
				m_GamblePot += JackpotContribution;
				InvalidateProperties();
                double roll = Utility.RandomDouble();

                if (Utility.RandomDouble() < GrandPrizeChance && GrandPrizeType != null)
                { 
                    Item prize = Loot.Construct(GrandPrizeType);
                    if (CursePrize)
                        prize.LootType = LootType.Cursed;

                    if (from == null || prize == null)
                        return;

                    Container backpack = from.Backpack;

                    if (backpack == null || !backpack.TryDropItem(from, prize, false))
                        prize.Delete();
                    else
                    {
                        from.SendMessage(0x35, "You won the grand prize, a brand new {0}!", PrizeName);
                        winner = true;
                    }

                }
				if(Utility.RandomDouble() < JackpotChance) // Jackpot
				{
					int maxCheck = 1000000;

					from.SendMessage( 0x35, "You win the {0}gp jackpot!", m_GamblePot );

					while( m_GamblePot > maxCheck )
					{
						from.AddToBackpack( new BankCheck( maxCheck ) );

						m_GamblePot -= maxCheck;
					}

					from.AddToBackpack( new BankCheck( m_GamblePot ) );

					m_GamblePot = 2500;
                    winner = true;
                }
				if(Utility.RandomDouble() < BigChance ) // Chance for gold
				{
					from.SendMessage( 0x35, "You win {0}gp!", BigPrize );
					from.AddToBackpack( new BankCheck( BigPrize ) );
                    winner = true;
                }
				if(Utility.RandomDouble() <= SmallChance ) // Another chance for gold
				{
					from.SendMessage( 0x35, "You win {0}gp!", SmallPrize );
					from.AddToBackpack( new BankCheck( SmallPrize ) );
                    winner = true;
                }
				if (!winner)
				{
					from.SendMessage( 0x22, "You lose!" );
				}
			}
			else
			{
				from.SendMessage( 0x22, "You need at least {0}gp in your backpack to use this.", Cost );
			}
		}

		public CustomGamblingStone( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version

            writer.Write( (int)m_Cost );
			writer.Write( (double)m_PayoutPercent );
            writer.Write( (int)m_GamblePot );
            writer.Write( (bool)m_CursePrize );
            if (m_GrandPrizeType != null)
                writer.Write( m_GrandPrizeType.Name );
            else
                writer.Write( "null" );
            writer.Write( (double)m_SmallChance );
            writer.Write( (int)m_SmallPrize );
            writer.Write( (double)m_BigChance );
            writer.Write( (int)m_BigPrize );
            writer.Write( (double)m_JackpotChance );
            writer.Write( (double)m_GrandPrizeChance );
            writer.Write( (int)m_GrandPrizeValue );
            writer.Write( (string)m_PrizeName );


		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch( version )
			{
				case 0:
					{
                        m_Cost = reader.ReadInt();
                        m_PayoutPercent = reader.ReadDouble();
						m_GamblePot = reader.ReadInt();
                        m_CursePrize = reader.ReadBool();
                        m_GrandPrizeType = Type.GetType(reader.ReadString());
                        m_SmallChance = reader.ReadDouble();
                        m_SmallPrize = reader.ReadInt();
                        m_BigChance = reader.ReadDouble();
                        m_BigPrize = reader.ReadInt();
                        m_JackpotChance = reader.ReadDouble();
                        m_GrandPrizeChance = reader.ReadDouble();
                        m_GrandPrizeValue = reader.ReadInt();
                        m_PrizeName = reader.ReadString();

						break;
					}
			}
		}
	}
}