using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using System.Reflection;
using Server.Misc;
using Server.Spells;
using Server.Spells.Third;
using Server.Spells.Sixth;
using Server.Items;
using Server.Targeting;
using Server.Commands;

namespace Server.Mobiles
{
	[CorpseName( "a changeling corpse" )]
	public class Changeling : BaseCreature
	{
		public virtual string DefaultName{ get{ return "a changeling"; } }
		public virtual int DefaultHue{ get{ return 0; } }

		[Constructable]
		public Changeling()
			: base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = DefaultName;
			Body = 264;
			Hue = DefaultHue;

			SetStr( 36, 105 );
			SetDex( 212, 262 );
			SetInt( 317, 399 );

			SetHits( 201, 211 );
			SetStam( 212, 262 );
			SetMana( 317, 399 );

			SetDamage( 9, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 81, 90 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 40, 49 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 43, 50 );

			SetSkill( SkillName.Wrestling, 10.4, 12.5 );
			SetSkill( SkillName.Tactics, 101.1, 108.3 );
			SetSkill( SkillName.MagicResist, 121.6, 132.2 );
			SetSkill( SkillName.Magery, 91.6, 99.5 );
			SetSkill( SkillName.EvalInt, 91.5, 98.8 );
			SetSkill( SkillName.Meditation, 91.7, 98.5 );

			Fame = 15000;
			Karma = -15000;

			PackScroll( 1, 7 );
			PackItem( new Arrow( 35 ) );
			PackItem( new Bolt( 25 ) );
			PackGem( 2 );

			PackArcaneScroll( 0, 1 );
		}

		public override bool ShowFameTitle{ get{ return false; } }
		public override bool InitialInnocent{ get{ return ( m_MorphedInto != null ); } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.AosRich, 3 );
		}

		public override int GetAngerSound() { return 0x46E; }
		public override int GetIdleSound() { return 0x470; }
		public override int GetAttackSound() { return 0x46D; }
		public override int GetHurtSound() { return 0x471; }
		public override int GetDeathSound() { return 0x46F; }

		private Mobile m_MorphedInto;
		private DateTime m_LastMorph;
		private DateTime m_NextFireRing;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile MorphedInto
		{
			get { return m_MorphedInto; }
			set
			{
				if ( value == this )
					value = null;

				if ( m_MorphedInto != value )
				{
					Revert();

					if ( value != null )
					{
						Morph( value );
						m_LastMorph = DateTime.UtcNow;
					}

					m_MorphedInto = value;
					Delta( MobileDelta.Noto );
				}
			}
		}

        private void RandoTarget(Mobile from)
        {
            double SwitchRate = .05;
            int PullRange = 10;
            foreach (Mobile m_target in GetMobilesInRange(PullRange))
                if ((m_target != from) && (SpellHelper.ValidIndirectTarget(from, (Mobile)m_target) && from.CanBeHarmful((Mobile)m_target, false)))
                {
                    if (Utility.RandomDouble() < SwitchRate)
                        from.Combatant = m_target;
                }
        }

        public override void OnThink()
		{
			base.OnThink();

			if ( Combatant != null )
			{
				if ( m_NextFireRing <= DateTime.UtcNow && Utility.RandomDouble() < 0.02 )
				{
					FireRing();
					m_NextFireRing = DateTime.UtcNow + TimeSpan.FromMinutes( 2 );
				}

				if ( /*Combatant.Player &&*/ m_MorphedInto != Combatant && Utility.RandomDouble() < 0.05 )
					MorphedInto = Combatant;

                if (Skills.Discordance.Value >= 100.0)
                    Suppress(Combatant);
                if (Skills.Provocation.Value >= 100.0)
                    Provoke(Combatant);
                if (Skills.Peacemaking.Value >= 100.0)
                    Peace(Combatant);
            }

            RandoTarget(this);
            
		}

        #region Suppress
        private static Dictionary<Mobile, Timer> m_Suppressed = new Dictionary<Mobile, Timer>();
        private DateTime m_NextSuppress;
        
        public void Suppress(Mobile target)
        {
            if (target == null || m_Suppressed.ContainsKey(target) || Deleted || !Alive || m_NextSuppress > DateTime.UtcNow || 0.1 < Utility.RandomDouble())
                return;

            TimeSpan delay = TimeSpan.FromSeconds(Utility.RandomMinMax(20, 80));

            if (!target.Hidden && CanBeHarmful(target))
            {
                target.SendLocalizedMessage(1072061); // You hear jarring music, suppressing your strength.

                for (int i = 0; i < target.Skills.Length; i++)
                {
                    Skill s = target.Skills[i];

                    target.AddSkillMod(new TimedSkillMod(s.SkillName, true, s.Base * -0.28, delay));
                }

                int count = (int)Math.Round(delay.TotalSeconds / 1.25);
                Timer timer = new AnimateTimer(target, count);
                m_Suppressed.Add(target, timer);
                timer.Start();

                PlaySound(0x58C);
            }

            m_NextSuppress = DateTime.UtcNow + TimeSpan.FromSeconds(10);
        }

        public static void SuppressRemove(Mobile target)
        {
            if (target != null && m_Suppressed.ContainsKey(target))
            {
                Timer timer = m_Suppressed[target];

                if (timer != null || timer.Running)
                    timer.Stop();

                m_Suppressed.Remove(target);
            }
        }

        private class AnimateTimer : Timer
        {
            private Mobile m_Owner;
            private int m_Count;

            public AnimateTimer(Mobile owner, int count) : base(TimeSpan.Zero, TimeSpan.FromSeconds(1.25))
            {
                m_Owner = owner;
                m_Count = count;
            }

            protected override void OnTick()
            {
                if (m_Owner.Deleted || !m_Owner.Alive || m_Count-- < 0)
                {
                    SuppressRemove(m_Owner);
                }
                else
                    m_Owner.FixedParticles(0x376A, 1, 32, 0x15BD, EffectLayer.Waist);
            }
        }
        #endregion

        #region Provoke
        private DateTime m_NextProvoke;

        public void Provoke(Mobile target)
        {
            if (target == null || Deleted || !Alive || m_NextProvoke > DateTime.UtcNow || 0.05 < Utility.RandomDouble())
                return;

            foreach (Mobile m in GetMobilesInRange(RangePerception))
            {
                if (m is BaseCreature)
                {
                    BaseCreature c = (BaseCreature)m;

                    if (c == this || c == target || c.Unprovokable || c.IsParagon || c.BardProvoked || c.AccessLevel != AccessLevel.Player || !c.CanBeHarmful(target))
                        continue;

                    c.Provoke(this, target, true);

                    if (target.Player)
                        target.SendLocalizedMessage(1072062); // You hear angry music, and start to fight.

                    PlaySound(0x58A);
                    break;
                }
            }

            m_NextProvoke = DateTime.UtcNow + TimeSpan.FromSeconds(10);
        }
        #endregion

        #region Peace
        private DateTime m_NextPeace;

        public void Peace(Mobile target)
        {
            if (target == null || Deleted || !Alive || m_NextPeace > DateTime.UtcNow || 0.1 < Utility.RandomDouble())
                return;

            PlayerMobile p = target as PlayerMobile;

            if (p != null && p.PeacedUntil < DateTime.UtcNow && !p.Hidden && CanBeHarmful(p))
            {
                p.PeacedUntil = DateTime.UtcNow + TimeSpan.FromMinutes(1);
                p.SendLocalizedMessage(500616); // You hear lovely music, and forget to continue battling!
                p.FixedParticles(0x376A, 1, 32, 0x15BD, EffectLayer.Waist);
                p.Combatant = null;

                PlaySound(0x58D);
            }

            m_NextPeace = DateTime.UtcNow + TimeSpan.FromSeconds(10);
        }
        #endregion

        public override bool CheckIdle()
		{
			bool idle = base.CheckIdle();

			if ( idle && m_MorphedInto != null && DateTime.UtcNow - m_LastMorph > TimeSpan.FromSeconds( 60 ) )
				MorphedInto = null;

			return idle;
		}

		private void FireEffects( int itemID, int[] offsets )
		{
			for ( int i = 0; i < offsets.Length; i += 2 )
			{
				Point3D p = Location;

				p.X += offsets[i];
				p.Y += offsets[i + 1];

				if ( SpellHelper.AdjustField( ref p, Map, 12, false ) )
					Effects.SendLocationEffect( p, Map, itemID, 50 );
			}
		}

		private static readonly int[] m_FireNorth = new int[]
		{
			-1, -1,
			1, -1,
			-1, 2,
			1, 2
		};

		private static readonly int[] m_FireEast = new int[]
		{
			-1, 0,
			2, 0
		};

		protected virtual void FireRing()
		{
			FireEffects( 0x3E27, m_FireNorth );
			FireEffects( 0x3E31, m_FireEast );
		}

		protected virtual void Morph( Mobile m )
		{
            
			Body = m.Body;
			Hue = m.Hue;
			Female = m.Female;
			Name = m.Name;
			NameHue = m.NameHue;
			Title = m.Title;
			Kills = m.Kills;
			HairItemID = m.HairItemID;
			HairHue = m.HairHue;
			FacialHairItemID = m.FacialHairItemID;
			FacialHairHue = m.FacialHairHue;

            MorphSkills(m);


            foreach ( Item item in m.Items )
			{
                Item newItem;
                Type t;
                ConstructorInfo c;
                object o;
                if ( item.Layer != Layer.Backpack && item.Layer != Layer.Mount && item.Layer != Layer.Bank )
                {
                    t = item.GetType();
                    c = t.GetConstructor(Type.EmptyTypes);

                    if (c != null)
                    {
                        o = c.Invoke(null);
                        if (o != null && o is Item)
                        {
                            //TODO: How to copy ASHammer
                            newItem = (Item)o;
                            Dupe.CopyProperties(newItem, item);
                            newItem.LootType = LootType.Newbied;
                            item.OnAfterDuped(newItem);
                            newItem.Parent = null;
                            AddItem(newItem);
                            newItem.InvalidateProperties();
                        }
                    }
                }
            }

			PlaySound( 0x511 );
			FixedParticles( 0x376A, 1, 14, 5045, EffectLayer.Waist );
		}

        protected virtual void MorphSkills( Mobile m )
        {
            if (Skills.Alchemy.Base < m.Skills.Alchemy.Base)
                Skills.Alchemy.Base = m.Skills.Alchemy.Base;
            if (Skills.Anatomy.Base < m.Skills.Anatomy.Base)
                Skills.Anatomy.Base = m.Skills.Anatomy.Base;
            if (Skills.ItemID.Base < m.Skills.ItemID.Base)
                Skills.ItemID.Base = m.Skills.ItemID.Base;
            if (Skills.ArmsLore.Base < m.Skills.ArmsLore.Base)
                Skills.ArmsLore.Base = m.Skills.ArmsLore.Base;
            if (Skills.Parry.Base < m.Skills.Parry.Base )
                Skills.Parry.Base = m.Skills.Parry.Base;
            if (Skills.Begging.Base < m.Skills.Begging.Base)
                Skills.Begging.Base = m.Skills.Begging.Base;
            if (Skills.Blacksmith.Base < m.Skills.Blacksmith.Base)
                Skills.Blacksmith.Base = m.Skills.Blacksmith.Base;
            if (Skills.Fletching.Base < m.Skills.Fletching.Base )
                Skills.Fletching.Base = m.Skills.Fletching.Base;
            if (Skills.Peacemaking.Base < m.Skills.Peacemaking.Base )
                Skills.Peacemaking.Base = m.Skills.Peacemaking.Base;
            if (Skills.Camping.Base < m.Skills.Camping.Base)
                Skills.Camping.Base = m.Skills.Camping.Base;
            if (Skills.Carpentry.Base < m.Skills.Carpentry.Base )
                Skills.Carpentry.Base = m.Skills.Carpentry.Base;
            if (Skills.Cartography.Base < m.Skills.Cartography.Base)
                Skills.Cartography.Base = m.Skills.Cartography.Base;
            if (Skills.Cooking.Base < m.Skills.Cooking.Base )
                Skills.Cooking.Base = m.Skills.Cooking.Base;
            if (Skills.DetectHidden.Base < m.Skills.DetectHidden.Base)
                Skills.DetectHidden.Base = m.Skills.DetectHidden.Base;
            if (Skills.Discordance.Base < m.Skills.Discordance.Base )
                Skills.Discordance.Base = m.Skills.Discordance.Base;
            if (Skills.EvalInt.Base < m.Skills.EvalInt.Base )
                Skills.EvalInt.Base = m.Skills.EvalInt.Base;
            if (Skills.Healing.Base < m.Skills.Healing.Base )
                Skills.Healing.Base = m.Skills.Healing.Base;
            if (Skills.Fishing.Base < m.Skills.Fishing.Base )
                Skills.Fishing.Base = m.Skills.Fishing.Base;
            if (Skills.Forensics.Base < m.Skills.Forensics.Base )
                Skills.Forensics.Base = m.Skills.Forensics.Base;
            if (Skills.Herding.Base < m.Skills.Herding.Base)
                Skills.Herding.Base = m.Skills.Herding.Base;
            if (Skills.Hiding.Base < m.Skills.Hiding.Base)
                Skills.Hiding.Base = m.Skills.Hiding.Base;
            if (Skills.Provocation.Base < m.Skills.Provocation.Base)
                Skills.Provocation.Base = m.Skills.Provocation.Base;
            if (Skills.Inscribe.Base < m.Skills.Inscribe.Base )
                Skills.Inscribe.Base = m.Skills.Inscribe.Base;
            if (Skills.Lockpicking.Base < m.Skills.Lockpicking.Base )
                Skills.Lockpicking.Base = m.Skills.Lockpicking.Base;
            if (Skills.Magery.Base < m.Skills.Magery.Base )
                Skills.Magery.Base = m.Skills.Magery.Base;
            if (Skills.MagicResist.Base < m.Skills.MagicResist.Base )
                Skills.MagicResist.Base = m.Skills.MagicResist.Base;
            if (Skills.Tactics.Base < m.Skills.Tactics.Base )
                Skills.Tactics.Base = m.Skills.Tactics.Base;
            if (Skills.Snooping.Base < m.Skills.Snooping.Base )
                Skills.Snooping.Base = m.Skills.Snooping.Base;
            if (Skills.Musicianship.Base < m.Skills.Musicianship.Base )
                Skills.Musicianship.Base = m.Skills.Musicianship.Base;
            if (Skills.Poisoning.Base < m.Skills.Poisoning.Base )
                Skills.Poisoning.Base = m.Skills.Poisoning.Base;
            if (Skills.Archery.Base < m.Skills.Archery.Base )
                Skills.Archery.Base = m.Skills.Archery.Base;
            if (Skills.SpiritSpeak.Base < m.Skills.SpiritSpeak.Base )
                Skills.SpiritSpeak.Base = m.Skills.SpiritSpeak.Base;
            if (Skills.Stealing.Base < m.Skills.Stealing.Base )
                Skills.Stealing.Base = m.Skills.Stealing.Base;
            if (Skills.Tailoring.Base < m.Skills.Tailoring.Base )
                Skills.Tailoring.Base = m.Skills.Tailoring.Base;
            if (Skills.AnimalTaming.Base < m.Skills.AnimalTaming.Base )
                Skills.AnimalTaming.Base = m.Skills.AnimalTaming.Base;
            if (Skills.TasteID.Base < m.Skills.TasteID.Base )
                Skills.TasteID.Base = m.Skills.TasteID.Base;
            if (Skills.Tinkering.Base < m.Skills.Tinkering.Base)
                Skills.Tinkering.Base = m.Skills.Tinkering.Base;
            if (Skills.Tracking.Base < m.Skills.Tracking.Base )
                Skills.Tracking.Base = m.Skills.Tracking.Base;
            if (Skills.Veterinary.Base < m.Skills.Veterinary.Base )
                Skills.Veterinary.Base = m.Skills.Veterinary.Base;
            if (Skills.Swords.Base < m.Skills.Swords.Base )
                Skills.Swords.Base = m.Skills.Swords.Base;
            if (Skills.Macing.Base < m.Skills.Macing.Base )
                Skills.Macing.Base = m.Skills.Macing.Base;
            if (Skills.Fencing.Base < m.Skills.Fencing.Base )
                Skills.Fencing.Base = m.Skills.Fencing.Base;
            if (Skills.Wrestling.Base < m.Skills.Wrestling.Base )
                Skills.Wrestling.Base = m.Skills.Wrestling.Base;
            if (Skills.Mining.Base < m.Skills.Mining.Base) 
                Skills.Mining.Base = m.Skills.Mining.Base;
            if (Skills.Meditation.Base < m.Skills.Meditation.Base )
                Skills.Meditation.Base = m.Skills.Meditation.Base;
            if (Skills.Stealth.Base < m.Skills.Stealth.Base )
                Skills.Stealth.Base = m.Skills.Stealth.Base;
            if (Skills.RemoveTrap.Base < m.Skills.RemoveTrap.Base )
                Skills.RemoveTrap.Base = m.Skills.RemoveTrap.Base;
            if (Skills.Necromancy.Base < m.Skills.Necromancy.Base )
                Skills.Necromancy.Base = m.Skills.Necromancy.Base;
            if (Skills.Focus.Base < m.Skills.Focus.Base )
                Skills.Focus.Base = m.Skills.Focus.Base;
            if (Skills.Chivalry.Base < m.Skills.Chivalry.Base)
                Skills.Chivalry.Base = m.Skills.Chivalry.Base;
            if (Skills.Chivalry.Base < m.Skills.Chivalry.Base )
                Skills.Chivalry.Base = m.Skills.Chivalry.Base;
            if (Skills.Ninjitsu.Base < m.Skills.Ninjitsu.Base )
                Skills.Ninjitsu.Base = m.Skills.Ninjitsu.Base;
            if (Skills.Mysticism.Base < m.Skills.Mysticism.Base)
                Skills.Mysticism.Base = m.Skills.Mysticism.Base;
            if (Skills.Imbuing.Base < m.Skills.Imbuing.Base )
                Skills.Imbuing.Base = m.Skills.Imbuing.Base;
            if (Skills.Spellweaving.Base < m.Skills.Spellweaving.Base)
                Skills.Spellweaving.Base = m.Skills.Spellweaving.Base;
            if (Skills.Throwing.Base < m.Skills.Throwing.Base )
                Skills.Throwing.Base = m.Skills.Throwing.Base;
        }
		protected virtual void Revert()
		{
			Body = 264;
			Hue = ( IsParagon && DefaultHue == 0 ) ? Paragon.Hue : DefaultHue;
			Female = false;
			Name = DefaultName;
			NameHue = -1;
			Title = null;
			Kills = 0;
			HairItemID = 0;
			HairHue = 0;
			FacialHairItemID = 0;
			FacialHairHue = 0;

			DeleteClonedItems();
            //Skills
			PlaySound( 0x511 );
			FixedParticles( 0x376A, 1, 14, 5045, EffectLayer.Waist );
		}

		public void DeleteClonedItems()
		{
			for ( int i = Items.Count - 1; i >= 0; --i )
			{
				Item item = Items[i];

				if ( item.LootType == LootType.Newbied )
					item.Delete();
			}

			if ( Backpack != null )
			{
				for ( int i = Backpack.Items.Count - 1; i >= 0; --i )
				{
					Item item = Backpack.Items[i];

					if (item.LootType == LootType.Newbied)
                        item.Delete();
				}
			}
		}

		public override void OnAfterDelete()
		{
			DeleteClonedItems();

			base.OnAfterDelete();
		}

		public override void ClearHands()
		{
		}

		public Changeling( Serial serial )
			: base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			writer.Write( ( m_MorphedInto != null ) );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( reader.ReadBool() )
				ValidationQueue<Changeling>.Add( this );
		}

		public void Validate()
		{
			Revert();
		}
        
		private class ClonedItem : Item
		{
			public ClonedItem( Item item )
				: base( item.ItemID )
			{
				Name = item.Name;
				Weight = item.Weight;
				Hue = item.Hue;
				Layer = item.Layer;
				Movable = false;
                    
			}

			public ClonedItem( Serial serial )
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
}
