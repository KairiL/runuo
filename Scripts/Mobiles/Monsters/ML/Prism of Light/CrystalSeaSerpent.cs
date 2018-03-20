using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Mobiles
{
	[CorpseName( "a crystal sea serpent corpse" )]
	public class CrystalSeaSerpent : SeaSerpent
	{
		[Constructable]
		public CrystalSeaSerpent()
		{
			Name = "a crystal sea serpent";
			Hue = 0x47E;

			SetStr( 250, 450 );
			SetDex( 100, 150 );
			SetInt( 90, 190 );

			SetHits( 230, 330 );

			SetDamage( 10, 18 );

			SetDamageType( ResistanceType.Physical, 10 );
			SetDamageType( ResistanceType.Cold, 45 );
			SetDamageType( ResistanceType.Energy, 45 );

			SetResistance( ResistanceType.Physical, 50, 70 );
			SetResistance( ResistanceType.Fire, 0 );
			SetResistance( ResistanceType.Cold, 70, 90 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 60, 80 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if ( Utility.RandomDouble() < 0.05 )
				c.DropItem( new CrushedCrystalPieces() );

			if ( Utility.RandomDouble() < 0.1 )
				c.DropItem( new IcyHeart() );

			if ( Utility.RandomDouble() < 0.1 )
				c.DropItem( new LuckyDagger() );
		}

        public override void OnThink()
        {
            base.OnThink();
            RadiateCold();
        }

        public void RadiateCold()
        {
            int ColdRange = 5;
            double ColdRate = .10;//(ThinkRate/ColdRate = Avg HitRate = .2/.10 = 2 seconds)
            int MinDamage = 1;
            int MaxDamage = 30;
            if (Utility.RandomDouble() < ColdRate)
                foreach (Mobile m_target in GetMobilesInRange(ColdRange))
                    if ((m_target != this) && (SpellHelper.ValidIndirectTarget(this, (Mobile)m_target) &&
                            CanBeHarmful((Mobile)m_target, false)))
                    {
                        AOS.Damage(m_target, this, (int)(Utility.RandomMinMax(MinDamage, MaxDamage) - 2 * GetDistanceToSqrt(m_target)), 0, 0, 100, 0, 0);
                    }
        }

        public CrystalSeaSerpent( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
