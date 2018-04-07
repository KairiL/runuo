using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a phantom corpse")]
    public class DarkPhantom : BaseCreature
    {
        [Constructable]
        public DarkPhantom() : base(AIType.AI_NecromageEpic, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a phantom";
            Body = 606;
            BaseSoundID = 0x47E;
            Hue = 1;

            SetStr(1000);
            SetDex(151, 175);
            SetInt(171, 220);

            SetHits(30000);

            SetDamage(34, 36);

            SetDamageType(ResistanceType.Physical, 20);
            SetDamageType(ResistanceType.Fire, 20);
            SetDamageType(ResistanceType.Cold, 20);
            SetDamageType(ResistanceType.Poison, 20);
            SetDamageType(ResistanceType.Energy, 20);

            SetResistance(ResistanceType.Physical, 200);
            SetResistance(ResistanceType.Fire, 200);
            SetResistance(ResistanceType.Cold, 200);
            SetResistance(ResistanceType.Poison, 200);
            SetResistance(ResistanceType.Energy, 200);

            SetSkill(SkillName.Necromancy, 120, 130.0);
            SetSkill(SkillName.SpiritSpeak, 120.0, 140.0);

            SetSkill(SkillName.DetectHidden, 80.0);
            SetSkill(SkillName.EvalInt, 200.0);
            SetSkill(SkillName.Magery, 112.6, 117.5);
            SetSkill(SkillName.Meditation, 200.0);
            SetSkill(SkillName.MagicResist, 200.1, 240.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Wrestling, 120.0, 140.0);

            Fame = 20000;
            Karma = -20000;

            VirtualArmor = 44;

            AddMonkRobe();
        }

        public void AddMonkRobe()
        {
            MonkRobe item = new MonkRobe();
            item.Hue = 1;
            item.LootType = LootType.Blessed;
            item.Name = "A Phantom Robe";
            item.Resistances.Direct = 200;//Doesn't seem to work
            AddItem(item);
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Epic, 2);
        }

        public override void OnThink()
        {
            double ColorChangeChance = .05;
            //double DarkChance = .05;
            base.OnThink();
            if (Hue == 1 && Utility.RandomDouble() < ColorChangeChance)
                ChangeColor();
            //else if (Hue != 1 && Utility.RandomDouble() < DarkChance)
            //    GoDark();
        }

        public virtual void ChangeColor()
        {
            Hue = 34700;
            SetResistance(ResistanceType.Physical, 0);
            SetResistance(ResistanceType.Fire, 0);
            SetResistance(ResistanceType.Cold, 0);
            SetResistance(ResistanceType.Poison, 0);
            SetResistance(ResistanceType.Energy, 0);
        }

        public void GoDark()
        {
            Hue = 1;
            SetResistance(ResistanceType.Physical, 200);
            SetResistance(ResistanceType.Fire, 200);
            SetResistance(ResistanceType.Cold, 200);
            SetResistance(ResistanceType.Poison, 200);
            SetResistance(ResistanceType.Energy, 200);
        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            int reflectAmount = 50;
            /*
            if (Hue == 1)
                return;
            */
            double ChangeChance = .05;
            if (Hue == 1)
            {
                Hits += amount*2;
                AOS.Damage(from, reflectAmount, 20, 20, 20, 20, 20);
            }
            else
            {
                base.OnDamage(amount, from, willKill);
                if (Utility.RandomDouble() < ChangeChance)
                    GoDark();
            }
        }

        public override void CheckReflect(Mobile caster, ref bool reflect)
        {
            reflect = (Hue == 1);
        }

        public override void AlterMeleeDamageFrom(Mobile from, ref int damage)
        {
            if (Hue == 1)
            {
                AOS.Damage(from, damage, 100, 0, 0, 0, 0);
            }
            /*
            if (damage < 20 )
            {
                damage = 0;
                Hits += 20;
            }
            */
        }

        public override void AlterSpellDamageFrom(Mobile from, ref int damage)
        {
            /*
            if (damage < 20)
            {
                damage = 0;
                Hits += 20;
            }
            */
        }

        public override bool BardImmune { get { return false; } }
        public override bool Unprovokable { get { return true; } }
        public override bool AreaPeaceImmune { get { return false; } }
        public override Poison PoisonImmune { get { return Poison.Godly; } }
        public override int TreasureMapLevel { get { return 5; } }
        public override bool ShowFameTitle { get { return false; } }
        public override bool AlwaysMurderer { get { return true; } }
        public override bool BleedImmune { get { return true; } }
        public override bool BreathImmune { get { return true; } }

        public DarkPhantom(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}