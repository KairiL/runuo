using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("an undead gargoyle corpse")]
    public class UndeadGargoyle : BaseCreature
    {
        [Constructable]
        public UndeadGargoyle()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "an undead gargoyle";
            Body = 722; // TODO: update body
            BaseSoundID = 372;

            SetStr(250, 350);
            SetDex(120, 140);
            SetInt(250, 350);

            SetHits(200, 300);

            SetDamage(15, 27);

            SetDamageType(ResistanceType.Cold, 60);
            SetDamageType(ResistanceType.Energy, 40);


            SetResistance(ResistanceType.Physical, 40, 50);
            SetResistance(ResistanceType.Fire, 30, 40);
            SetResistance(ResistanceType.Cold, 50, 60);
            SetResistance(ResistanceType.Poison, 50, 60);
            SetResistance(ResistanceType.Energy, 40, 50);

            SetSkill(SkillName.MagicResist, 100.0, 120.0);
            SetSkill(SkillName.Magery, 120.0);
            SetSkill(SkillName.EvalInt, 90, 110.0);
            SetSkill(SkillName.Tactics, 60.0, 70);
            SetSkill(SkillName.Wrestling, 60.0, 70.0);

            Fame = 18000;
            Karma = -18000;

            VirtualArmor = 32;
        }

        public UndeadGargoyle(Serial serial)
            : base(serial)
        {
        }
        public override OppositionGroup OppositionGroup { get { return OppositionGroup.FeyAndUndead; } }

        public override int TreasureMapLevel
        {
            get
            {
                return 1;
            }
        }
        public override int Meat
        {
            get
            {
                return 1;
            }
        }
        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
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