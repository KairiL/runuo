using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an abyssal abomination corpse")]
    public class AbyssalAbomination : BaseCreature
    {
        [Constructable]
        public AbyssalAbomination()
            : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an Abyssal abomination";
            Body = 742;
            Hue = 769;
            BaseSoundID = 0x451;

            SetStr(200, 230);
            SetDex(81, 100);
            SetInt(230);

            SetHits(600, 750);

            SetDamage(13, 17);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Fire, 50);

            SetResistance(ResistanceType.Physical, 30, 35);
            SetResistance(ResistanceType.Fire, 100);
            SetResistance(ResistanceType.Cold, 50, 55);
            SetResistance(ResistanceType.Poison, 60, 65);
            SetResistance(ResistanceType.Energy, 77, 80);

            SetSkill(SkillName.EvalInt, 100.1, 130.0);
            SetSkill(SkillName.Magery, 99.1, 130.0);
            SetSkill(SkillName.Meditation, 100.0);
            SetSkill(SkillName.MagicResist, 85.1, 90.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Wrestling, 84.1, 90.0);

            Fame = 36000;
            Karma = -18000;

            VirtualArmor = 54;
        }

        public AbyssalAbomination(Serial serial)
            : base(serial)
        {
        }

        public override bool BardImmune { get { return false; } }
        public override bool Unprovokable { get { return true; } }

        public override Poison PoisonImmune { get { return Poison.Lethal; } }
        public override WeaponAbility GetWeaponAbility()
        {
            return Utility.RandomBool() ? WeaponAbility.MortalStrike : WeaponAbility.WhirlwindAttack;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
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

            if (this.BaseSoundID == 357)
                this.BaseSoundID = 0x451;
        }
    }
}