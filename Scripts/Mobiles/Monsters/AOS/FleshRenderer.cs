using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Mobiles
{
	[CorpseName( "a fleshrenderer corpse" )]
	public class FleshRenderer : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
            switch (Utility.Random(3))
            {
                default:
                case 0: return WeaponAbility.Dismount;
                case 1: return WeaponAbility.ParalyzingBlow;
                case 2: return WeaponAbility.MortalStrike;
            }
		}

		public override bool IgnoreYoungProtection { get { return Core.ML; } }

		[Constructable]
		public FleshRenderer() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a fleshrenderer";
			Body = 315;

			SetStr( 401, 460 );
			SetDex( 201, 210 );
			SetInt( 221, 260 );

			SetHits( 4500 );

			SetDamage( 16, 20 );

			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Poison, 20 );

			SetResistance( ResistanceType.Physical, 80, 90 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 70, 80 );

			SetSkill( SkillName.DetectHidden, 80.0 );
			SetSkill( SkillName.MagicResist, 155.1, 160.0 );
			SetSkill( SkillName.Meditation, 100.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 23000;
			Karma = -23000;

			VirtualArmor = 24;
		}

        public override void OnThink()
        {
            base.OnThink();
            if (Utility.RandomDouble() > .95)
                DoPulls(this);
        }

        private void DoPulls(Mobile from)
        {
            Direction dir;
            int NumPulls = 1;
            int PullRange = 10;
            foreach (Mobile m_target in GetMobilesInRange(PullRange))
                if ((m_target != from) && (SpellHelper.ValidIndirectTarget(from, (Mobile)m_target) && from.CanBeHarmful((Mobile)m_target, false)))
                {
                    if (m_target.Spell != null)
                        m_target.Spell.OnCasterHurt();

                    m_target.Direction = m_target.GetDirectionTo(from);
                    for (int i = 0; i<NumPulls; i++)
                        m_target.Move(m_target.Direction);
                }
        }

        public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 2 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if ( !Summoned && !NoKillAwards && DemonKnight.CheckArtifactChance( this ) )
				DemonKnight.DistributeArtifact( this );
		}

		public override bool AutoDispel{ get{ return true; } }
		public override bool BardImmune { get { return !Core.SE; } }
		public override bool Unprovokable { get { return Core.SE; } }
		public override bool AreaPeaceImmune { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override int TreasureMapLevel{ get{ return 1; } }

		public override int GetAttackSound()
		{
			return 0x34C;
		}

		public override int GetHurtSound()
		{
			return 0x354;
		}

		public override int GetAngerSound()
		{
			return 0x34C;
		}

		public override int GetIdleSound()
		{
			return 0x34C;
		}

		public override int GetDeathSound()
		{
			return 0x354;
		}

		public FleshRenderer( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 660 )
				BaseSoundID = -1;
		}
	}
}