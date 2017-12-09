using System;
using Server;
using Server.Spells;
using Server.Regions;
using Server.Targeting;
using System.Collections;
using Server.Items;
using Server.Misc;
using Server.Mobiles;

namespace Server.Items
{
    public class GoopyGauntlets : PlateGloves
    {
        public override int ArtifactRarity { get { return 12; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        
        public override void OnDoubleClick(Mobile from)
        {
            //TODO: Checks
            if (Parent != from)
            {
                from.SendMessage("You must equip the item to use it");
            }
            else if (!from.CanBeginAction(typeof(GoopyGauntlets)))
            {
                from.SendMessage("You have to wait a few moments before you can shoot more goop!"); // You have to wait a few moments before you can use another bola!
            }
            else if (!HasFreeHands(from))
            {
                from.SendLocalizedMessage(1040015); // Your hands must be free to use this
            }
            else if (from.BeginAction(typeof(GoopyGauntlets)))
            {
                Timer.DelayCall(TimeSpan.FromSeconds(3.0), new TimerStateCallback(ReleaseGoopLock), from);
                from.Target = new InternalTarget();
            }

        }

        private static void ReleaseGoopLock(object state)
        {
            ((Mobile)state).EndAction(typeof(GoopyGauntlets));
        }

        private static bool HasFreeHands(Mobile from)
        {
            Item one = from.FindItemOnLayer(Layer.OneHanded);
            Item two = from.FindItemOnLayer(Layer.TwoHanded);

            if (Core.SE)
            {
                Container pack = from.Backpack;

                if (pack != null)
                {
                    if (one != null && one.Movable)
                    {
                        pack.DropItem(one);
                        one = null;
                    }

                    if (two != null && two.Movable)
                    {
                        pack.DropItem(two);
                        two = null;
                    }
                }
            }
            else if (Core.AOS)
            {
                if (one != null && one.Movable)
                {
                    from.AddToBackpack(one);
                    one = null;
                }

                if (two != null && two.Movable)
                {
                    from.AddToBackpack(two);
                    two = null;
                }
            }

            return (one == null && two == null);
        }


        [Constructable]
        public GoopyGauntlets()
        {
            Weight = 5.0;
            Name = "Goopy Gauntlets";
            Attributes.BonusDex = -10;
            Attributes.Luck = 150;
            LootType = LootType.Cursed;
            Hue = 0x850;
        }

        [DispellableField]
        public class GoopItem : Item
        {
            private Timer m_Timer;
            private DateTime m_End;
            private Mobile m_Caster;
            private int m_Damage;
            private double m_StickChance= .5;

            public override bool BlocksFit { get { return true; } }

            public GoopItem( int itemID, IPoint3D loc, Mobile caster, Map map, TimeSpan duration, int val )
				: this( itemID, loc, caster, map, duration, val, 0 )
			{
			}

			public GoopItem( int itemID, IPoint3D loc, Mobile caster, Map map, TimeSpan duration, int val, int damage ) : base( itemID )
			{
                Point3D p = new Point3D(loc.X, loc.Y, loc.Z);

                bool canFit = SpellHelper.AdjustField(ref p, map, 12, false);

                Visible = false;
                Movable = false;

                MoveToWorld(p, map);

                m_Caster = caster;

                m_Damage = damage;

                m_End = DateTime.UtcNow + duration;

                m_Timer = new InternalTimer(this, TimeSpan.FromSeconds(Math.Abs(val) * 0.2), caster.InLOS(this), canFit);
                m_Timer.Start();
            }

            public override bool OnMoveOff(Mobile m)
            {
                return Utility.RandomDouble() > m_StickChance;
            }

            public override bool OnMoveOver(Mobile m)
            {
                return Utility.RandomDouble() > m_StickChance;
            }
            public override void OnAfterDelete()
            {
                base.OnAfterDelete();

                if (m_Timer != null)
                    m_Timer.Stop();
            }

            public GoopItem(Serial serial) : base( serial )
			{
            }
            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);

                writer.Write((int)0); // version

                writer.Write(m_Damage);
                writer.Write(m_Caster);
                writer.WriteDeltaTime(m_End);
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);

                int version = reader.ReadInt();
                
                m_Damage = reader.ReadInt();
                m_Caster = reader.ReadMobile();
                m_End = reader.ReadDeltaTime();
                m_Timer = new InternalTimer(this, TimeSpan.Zero, true, true);
                m_Timer.Start();
            }

            private class InternalTimer : Timer
            {
                private GoopItem m_Item;
                private bool m_InLOS, m_CanFit;

                private static Queue m_Queue = new Queue();

                public InternalTimer(GoopItem item, TimeSpan delay, bool inLOS, bool canFit) : base(delay, TimeSpan.FromSeconds(1.0))
                {
                    m_Item = item;
                    m_InLOS = inLOS;
                    m_CanFit = canFit;

                    Priority = TimerPriority.FiftyMS;
                }
                protected override void OnTick()
                {
                    if (m_Item.Deleted)
                        return;

                    if (!m_Item.Visible)
                    {
                        if (m_InLOS && m_CanFit)
                            m_Item.Visible = true;
                        else
                            m_Item.Delete();

                        if (!m_Item.Deleted)
                        {
                            m_Item.ProcessDelta();
                            Effects.SendLocationParticles(EffectItem.Create(m_Item.Location, m_Item.Map, EffectItem.DefaultDuration), 0x376A, 9, 10, 5029);
                        }
                    }
                    else if (DateTime.UtcNow > m_Item.m_End)
                    {
                        m_Item.Delete();
                        Stop();
                    }
                    else
                    {
                        Map map = m_Item.Map;
                        Mobile caster = m_Item.m_Caster;

                        if (map != null && caster != null)
                        {
                            foreach (Mobile m in m_Item.GetMobilesInRange(0))
                            {
                                if ((m.Z + 16) > m_Item.Z && (m_Item.Z + 12) > m.Z && (!Core.AOS || m != caster) && SpellHelper.ValidIndirectTarget(caster, m) && caster.CanBeHarmful(m, false))
                                    m_Queue.Enqueue(m);
                            }

                            while (m_Queue.Count > 0)
                            {
                                int damage = m_Item.m_Damage;
                                if (damage > 0)
                                {
                                    Mobile m = (Mobile)m_Queue.Dequeue();

                                    if (SpellHelper.CanRevealCaster(m))
                                        caster.RevealingAction();

                                    caster.DoHarmful(m);

                                
                                    if (!Core.AOS && m.CheckSkill(SkillName.MagicResist, 0.0, 30.0))
                                    {
                                        damage = 1;

                                        m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy.
                                    }

                                    AOS.Damage(m, caster, damage, 0, 100, 0, 0, 0);
                                    m.PlaySound(0x208);
                                
                                    if (m is BaseCreature)
                                        ((BaseCreature)m).OnHarmfulSpell(caster);
                                }
                            }
                        }
                    }
                }
            }
            
        }

        private class InternalTarget : Target
        {
            public InternalTarget() : base(10, true, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (!from.CanSee(targeted))
                {
                    from.SendLocalizedMessage(500237); // Target can not be seen.
                }
                else
                {
                    int duration = 10;
                    new GoopItem(0xCC3, (IPoint3D)targeted, from, from.Map, TimeSpan.FromSeconds(duration), 1, 0);//TODO: add skill based damage & duration
                }
                
            }
        }
        public GoopyGauntlets(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            
        }
    }
}