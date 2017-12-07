using System;
using Server;
using Server.Spells;
using Server.Regions;

namespace Server.Items
{
    public class SuperSonicSandals : Sandals
    {
        public override int ArtifactRarity { get { return 12; } }

        public override int InitMinHits { get { return 255; } }
        public override int InitMaxHits { get { return 255; } }

        public Point3D GetDestination(Mobile from)
        {
            Point3D to = from.Location;


            int x = from.X, y = from.Y;
            int z = from.Z;
            switch (from.Direction & Direction.Mask)
            {
                case Direction.North:
                    y-=2; 
                    break;
                case Direction.Right:
                    x+=2;
                    y-=2;
                    break;
                case Direction.East:
                    x+=2;
                    break;
                case Direction.Down:
                    x+=2;
                    y+=2;
                    break;
                case Direction.South:
                    y+=2;
                    break;
                case Direction.Left:
                    x-=2;
                    y+=2;
                    break;
                case Direction.West:
                    x-=2;
                    break;
                case Direction.Up:
                    x-=2;
                    y-=2;
                    break;
            }

            to.X = x;
            to.Y = y;
            to.Z = z;
            

            return to;
        }

        public bool Sprint(Mobile Caster, IPoint3D p)
        {
            IPoint3D orig = p;
            Map map = Caster.Map;

            SpellHelper.GetSurfaceTop(ref p);

            Point3D from = Caster.Location;
            Point3D to = new Point3D(p);

            if (Server.Misc.WeightOverloading.IsOverloaded(Caster))
            {
                Caster.SendLocalizedMessage(502359, "", 0x22); // Thou art too encumbered to move.
                return false;
            }
            else if (!SpellHelper.CheckTravel(Caster, TravelCheckType.TeleportFrom))
            {
                return false;
            }
            else if (!SpellHelper.CheckTravel(Caster, map, to, TravelCheckType.TeleportTo))
            {
                return false;
            }
            else if (map == null || !map.CanSpawnMobile(p.X, p.Y, p.Z))
            {
                Caster.SendLocalizedMessage(501942); // That location is blocked.
                return false;
            }
            else if (SpellHelper.CheckMulti(to, map))
            {
                Caster.SendLocalizedMessage(502831); // Cannot teleport to that spot.
                return false;
            }
            else if (Region.Find(to, map).GetRegion(typeof(HouseRegion)) != null)
            {
                Caster.SendLocalizedMessage(502829); // Cannot teleport to that spot.
                return false;
            }
            else if ( !Caster.Map.LineOfSight(Caster, to) )
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen
                return false;
            }
            else if ( Parent != Caster )
            {
                Caster.SendMessage( "You must equip the item to use it" );
                return false;
            }
            else 
            {
                SpellHelper.Turn(Caster, orig);

                Mobile m = Caster;

                m.Location = to;
                m.ProcessDelta();

                /* no particles seems cooler?
                if (m.Player)
                {
                    Effects.SendLocationParticles(EffectItem.Create(from, m.Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 2023);
                    Effects.SendLocationParticles(EffectItem.Create(to, m.Map, EffectItem.DefaultDuration), 0x3728, 10, 10, 5023);
                }
                else
                {
                    m.FixedParticles(0x376A, 9, 32, 0x13AF, EffectLayer.Waist);
                }
                */

                m.PlaySound(0x1FE); // Maybe find a better sound?

                IPooledEnumerable eable = m.GetItemsInRange(0);

                foreach (Item item in eable)
                {
                    if (item is Server.Spells.Sixth.ParalyzeFieldSpell.InternalItem || item is Server.Spells.Fifth.PoisonFieldSpell.InternalItem || item is Server.Spells.Fourth.FireFieldSpell.FireFieldItem)
                        item.OnMoveOver(m);
                }

                eable.Free();
                return true;
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            int SprintDamage = 50;

            if (Sprint(from, GetDestination(from) as IPoint3D))
                if (from.Stam >= SprintDamage)
                    from.Stam -= SprintDamage;
                else
                {
                    from.Damage(SprintDamage - from.Stam);
                    from.Stam = 0;
                    from.SendMessage( "You hurt from exhaustion" );
                }
        }

        [Constructable]
        public SuperSonicSandals()
        {
            Weight = 285.7;
            Name = "Super Sonic Sandals";
            Attributes.BonusDex = 10;
            Attributes.Luck = 100;
            LootType = LootType.Cursed;
        }

        public SuperSonicSandals(Serial serial) : base(serial)
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