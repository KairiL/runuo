using System;
using Server;
using Server.Items;
using Server.Spells;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "a lady's corpse" )]
	public class LadyMelisande : BaseCreature
	{

        private bool m_SpawnedSatyrs;
        [Constructable]
		public LadyMelisande() : base( AIType.AI_NecromageEpic, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Lady Melisande";
			Body = 0x102;
			BaseSoundID = 451;

			SetStr( 487 );
			SetDex( 400, 400 );
			SetInt( 1680 );

			SetHits( 30000 );	

			SetDamage( 11, 18 );
			
			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 45, 58 );
			SetResistance( ResistanceType.Fire, 39, 43 );
			SetResistance( ResistanceType.Cold, 55, 61 );
			SetResistance( ResistanceType.Poison, 69, 74 );
			SetResistance( ResistanceType.Energy, 75, 77 );
			
			SetSkill( SkillName.Wrestling, 135);
			SetSkill( SkillName.Tactics, 100.1, 101.9 );
			SetSkill( SkillName.MagicResist, 120 );
			SetSkill( SkillName.Magery, 120 );
			SetSkill( SkillName.EvalInt, 120 );
			SetSkill( SkillName.Meditation, 120 );
			SetSkill( SkillName.Necromancy, 120 );
			SetSkill( SkillName.SpiritSpeak, 120 );
            SetSkill( SkillName.Anatomy, 100 );
            SetSkill( SkillName.DetectHidden, 100 );
            SetSkill( SkillName.Focus, 78.6 );

			Fame = 18000;
			Karma = -18000;

			VirtualArmor = 50;
            switch (Utility.Random(3))
            {
                case 0: AddItem(new SquirrelSummoner()); break;
            }
            switch (Utility.Random(3))
            {
                case 0: AddItem(new MelisandraHairDye()); break;
                case 1: AddItem(new MelisandesCorrodedHatchet()); break;
                case 2: AddItem(new CrimsonCinture()); break;
            }

            switch (Utility.Random(11))
            {
                case 0: AddItem(new YamandonIdol()); break;
                case 1: AddItem(new WandererIdol()); break;
                case 2: AddItem(new AbyssmalIdol()); break;
                case 3: AddItem(new AncientWyrmIdol()); break;
                case 4: AddItem(new BoneDemonIdol()); break;
                case 5: AddItem(new GamanIdol()); break;
                case 6: AddItem(new DeathwatchBeetleIdol()); break;
                case 7: AddItem(new HiryuIdol()); break;
                case 8: AddItem(new LadyOfTheSnowIdol()); break;
                case 9: AddItem(new RuneBeetleIdol()); break;
                case 10: AddItem(new ShadowKnightIdol()); break;
            }
            switch (Utility.Random(64))
            {
                case 0: AddItem(new PlateOfHonorArms()); break;
                case 1: AddItem(new PlateOfHonorChest()); break;
                case 2: AddItem(new PlateOfHonorGloves()); break;
                case 3: AddItem(new PlateOfHonorGorget()); break;
                case 4: AddItem(new PlateOfHonorHelm()); break;
                case 5: AddItem(new PlateOfHonorLegs()); break;
                case 6: AddItem(new AcolyteArms()); break;
                case 7: AddItem(new AcolyteChest()); break;
                case 8: AddItem(new AcolyteGloves()); break;
                case 9: AddItem(new AcolyteLegs()); break;
                case 10: AddItem(new EvocaricusSword()); break;
                case 11: AddItem(new MalekisHonor()); break;
                case 12: AddItem(new GrizzleArms()); break;
                case 13: AddItem(new GrizzleChest()); break;
                case 14: AddItem(new GrizzleGloves()); break;
                case 15: AddItem(new GrizzleHelm()); break;
                case 16: AddItem(new PlateOfHonorLegs()); break;
                case 17: AddItem(new MageArmorArms()); break;
                case 18: AddItem(new MageArmorChest()); break;
                case 19: AddItem(new MageArmorGloves()); break;
                case 20: AddItem(new MageArmorLegs()); break;
                case 21: AddItem(new MyrmidonArms()); break;
                case 22: AddItem(new MyrmidonChest()); break;
                case 23: AddItem(new MyrmidonGloves()); break;
                case 24: AddItem(new MyrmidonGorget()); break;
                case 25: AddItem(new MyrmidonHelm()); break;
                case 26: AddItem(new MyrmidonLegs()); break;
                case 27: AddItem(new DeathEssenceArms()); break;
                case 28: AddItem(new DeathEssenceChest()); break;
                case 29: AddItem(new DeathEssenceGloves()); break;
                case 30: AddItem(new DeathEssenceHelm()); break;
                case 31: AddItem(new DeathEssenceLegs()); break;
            }
            PackItem( new GnarledStaff() );
			PackNecroReg( 50, 80 );
            Timer.DelayCall(TimeSpan.FromMinutes(30.0), new TimerStateCallback(DeletePeerless), this);
        }

        public void DeletePeerless(object state)
        {
            Mobile from = (Mobile)state;
            from.Delete();
        }

        public override void GenerateLoot()
        {
            //AddLoot(LootPack.AosUltraRich);
            //AddLoot(LootPack.AosSuperBoss);
            AddLoot(LootPack.Epic, 2);

            for (int i = 0; i < 8; i++)
            {
                switch (Utility.Random(6))
                {
                    case 0: AddItem(new Corruption()); break;
                    case 1: AddItem(new Taint()); break;
                    case 2: AddItem(new Blight()); break;
                    case 3: AddItem(new Putrefaction()); break;
                    case 4: AddItem(new Muculent()); break;
                    case 5: AddItem(new Scourge()); break;
                }
            }
            switch (Utility.Random(10))//rare
            {
                case 0: AddItem(new MelisandesCorrodedHatchet()); break;
            }
            switch (Utility.Random(3))//common
            {
                case 0: AddItem(new EternallyCorruptTree()); break;
                case 1: AddItem(new DiseasedBark()); break;
            }
            switch (Utility.Random(5))//uncommon
            {
                case 0: AddItem(new MelisandesHairDye()); break;
                //case 1: AddItem(new AlbinoSquirrel()); break;
            }

            AddItem(new MelisandesFermentedWine());
        }
        public override bool AutoDispel { get { return true; } }
        public override bool CanRummageCorpses{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 4; } }

        private void RandoTarget(Mobile from)
        {
            double SwitchRate = .01;
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
            RandoTarget(this);
        }

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);

            if (!m_SpawnedSatyrs)
            {
                SpawnSatyrs();
                m_SpawnedSatyrs = true;
            }
            List<string> EffectList = new List<string>();
            EffectList.Add("CastSpeed"); 
            EffectList.Add("AttackChance"); 
            EffectList.Add("DefendChance");
            EffectList.Add("WeaponSpeed");
            if (Utility.RandomDouble() < .15)
            {
                SpellHelper.AddAosBuff(this, defender, EffectList,
                              TimeSpan.FromSeconds(30), 3, false,
                              false, true, -60);
                string args = String.Format("{0}\t{1}", 60, 5);
                BuffInfo.AddBuff(defender, new BuffInfo(BuffIcon.AuraOfNausea, 1153424, 1075803, TimeSpan.FromSeconds(30), defender, args.ToString()));
            }
        }

        public override void OnGotMeleeAttack(Mobile attacker)
        {
            base.OnGotMeleeAttack(attacker);

            if (!m_SpawnedSatyrs)
            {
                SpawnSatyrs();
                m_SpawnedSatyrs = true;
            }

            List<string> EffectList = new List<string>();
            EffectList.Add("CastSpeed");
            EffectList.Add("AttackChance");
            EffectList.Add("DefendChance");
            EffectList.Add("WeaponSpeed");
            if (Utility.RandomDouble() < .15)
            {
                SpellHelper.AddAosBuff(this, attacker, EffectList,
                              TimeSpan.FromSeconds(30), 3, false,
                              false, true, -60);
                string args = String.Format("{0}\t{1}", 60, 5);
                BuffInfo.AddBuff(attacker, new BuffInfo(BuffIcon.AuraOfNausea, 1153424, 1075803, TimeSpan.FromSeconds(30), attacker, args.ToString()));
            }
        }

        public void SpawnSatyrs()
        {
            Map map = this.Map;

            if (map == null)
                return;

            int newSatyrs = 5;

            for (int i = 0; i < newSatyrs; ++i)
            {
                BaseCreature satyr = new EvilSatyr();

                satyr.Team = this.Team;

                bool validLocation = false;
                Point3D loc = this.Location;

                for (int j = 0; !validLocation && j < 10; ++j)
                {
                    int x = X + Utility.Random(3) - 1;
                    int y = Y + Utility.Random(3) - 1;
                    int z = map.GetAverageZ(x, y);

                    if (validLocation = map.CanFit(x, y, this.Z, 16, false, false))
                        loc = new Point3D(x, y, Z);
                    else if (validLocation = map.CanFit(x, y, z, 16, false, false))
                        loc = new Point3D(x, y, z);
                }

                satyr.MoveToWorld(loc, map);
            
            }
        }

        public override void OnDeath(Container c)
        {
            c.DropItem(new CorruptedTree());
            c.DropItem(new CorruptedTree());
            c.DropItem(new DiseasedBark());

            base.OnDeath(c);
        }

        public LadyMelisande( Serial serial ) : base( serial )
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
            Timer.DelayCall(TimeSpan.FromMinutes(30.0), new TimerStateCallback(DeletePeerless), this);
        }
	}
}