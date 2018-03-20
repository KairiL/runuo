using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Mobiles
{
	[CorpseName( "an ice elemental corpse" )]
	public class IceElemental : BaseCreature
	{
		[Constructable]
		public IceElemental () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an ice elemental";
			Body = 161;
			BaseSoundID = 268;

			SetStr( 156, 185 );
			SetDex( 96, 115 );
			SetInt( 171, 192 );

			SetHits( 94, 111 );

			SetDamage( 10, 21 );

			SetDamageType( ResistanceType.Physical, 25 );
			SetDamageType( ResistanceType.Cold, 75 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.EvalInt, 10.5, 60.0 );
			SetSkill( SkillName.Magery, 10.5, 60.0 );
			SetSkill( SkillName.MagicResist, 30.1, 80.0 );
			SetSkill( SkillName.Tactics, 70.1, 100.0 );
			SetSkill( SkillName.Wrestling, 60.1, 100.0 );

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 40;

			PackItem( new BlackPearl() );
			PackReg( 3 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average, 2 );
			AddLoot( LootPack.Gems, 2 );
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

        public override bool BleedImmune{ get{ return true; } }

		public IceElemental( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}