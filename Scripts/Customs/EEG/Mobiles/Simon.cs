using System;
using Server;
using Server.Items;
using Server.Spells;

namespace Server.Mobiles
{
    [CorpseName("Simon's corpse")]
    public class Simon : BaseCreature
    {
        public enum SimonState
        {
            waiting,//Cantwalk = true, Chill, wait for double click to initialize
            initialize,//teleport to 'Hitler East' room, cantwalk true, 
            learn,//para, clear record buffer, start combo + record combo list, record(subjectID, sessionID, learn, 0, 0)
            test,//unpara, IsInvulnerable false, respond to damage source if wrong spell, record(subjectID, sessionID, test, #correct spells, #spells total)
            reflex//announce attack, charge up until double click/attack, record(subjectID, sessionID, reflex, reflex times[]?)
        }

        public override WeaponAbility GetWeaponAbility()
        {
            return Utility.RandomBool() ? WeaponAbility.MortalStrike : WeaponAbility.WhirlwindAttack;
        }

        public override bool IgnoreYoungProtection { get { return Core.ML; } }
        public PlayerMobile m_Opponent;
        public SimonState m_State;
        public bool m_EEGOn;
        public Spell[] Combo;
        public int ComboLength;
        public int NumWrong;
        public int ComboIndex = 1;
        public int OpponentX = 6038;
        public int OpponentY = 406;
        public int OpponentZ = 0;
        public int MyX = 6035;
        public int MyY = 403;
        public int MyZ = 0;

        [Constructable]
        public Simon() : base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "Simon";
            Body = 400;
            BaseSoundID = 0x451;

            Blessed = true;
            EEGOn = false;
            CantWalk = true;
            State = SimonState.waiting;

            SetStr(401, 420);
            SetDex(81, 90);
            SetInt(401, 420);

            SetHits(6000);

            SetDamage(13, 17);

            SetDamageType(ResistanceType.Physical, 50);
            SetDamageType(ResistanceType.Poison, 50);

            SetResistance(ResistanceType.Physical, 0);
            SetResistance(ResistanceType.Fire, 0);
            SetResistance(ResistanceType.Cold, 0);
            SetResistance(ResistanceType.Poison, 0);
            SetResistance(ResistanceType.Energy, 0);

            SetSkill(SkillName.EvalInt, 200.0);
            SetSkill(SkillName.Magery, 112.6, 117.5);
            SetSkill(SkillName.Meditation, 200.0);
            SetSkill(SkillName.MagicResist, 117.6, 120.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Wrestling, 84.1, 88.0);

            Fame = 26000;
            Karma = -26000;

            VirtualArmor = 54;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!(from is PlayerMobile))
                return;
            
            if (State == SimonState.waiting)
            {
                State = SimonState.initialize;
                Opponent = (PlayerMobile)from;
                Initialize();
            }
            else if (State == SimonState.reflex)
                return;//Do reflex stuff instead of just allowing damage?
            else if (State == SimonState.initialize)
                return;
            else if (State == SimonState.learn)
                return;
            else if (State == SimonState.test)
                return;
            else
                return;
            
        }

        public virtual void AlterSpellDamageTo(Spell spell, Mobile to, Mobile from, ref int damage)
        {
            if (State == SimonState.test && Combo[ComboIndex] != null)
                if (Combo[ComboIndex] != spell)
                {
                    AOS.Damage(to, from, damage, 20, 20, 20, 20, 20);
                    damage = 0;
                    ComboIndex++;
                    NumWrong++;
                    if (Combo[ComboIndex] == null)
                    {
                        State = SimonState.reflex;
                        return;
                    }
                }
                else
                {
                    ComboIndex++;
                    return;
                }
            else if (State == SimonState.reflex)
                return;
            else
                damage = 0;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.UltraRich, 2);
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            if (!Summoned && !NoKillAwards && DemonKnight.CheckArtifactChance(this))
                DemonKnight.DistributeArtifact(this);
        }

        public override bool BardImmune { get { return true; } }
        public override bool Unprovokable { get { return true; } }
        public override bool AreaPeaceImmune { get { return true; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }
        public override int TreasureMapLevel { get { return 1; } }

        public bool EEGOn
        {
            get
            {
                return m_EEGOn;
            }
            set
            {
                m_EEGOn = value;
            }
        }

        public SimonState State
        {
            get
            {
                return m_State;
            }
            set
            {
                m_State = value;
            }
        }

        public PlayerMobile Opponent
        {
            get
            {
                return m_Opponent;
            }
            set
            {
                m_Opponent = value;
            }
        }

        public void Initialize()
        {
            Opponent.CantWalk = true;
            CantWalk = true;
            Opponent.X = OpponentX;
            Opponent.Y = OpponentY;
            Opponent.Z = OpponentZ;
            X = MyX;
            Y = MyY;
            Z = MyZ;
            //Start EEG buffer
            State = SimonState.learn;
            Learn();
        }

        public void Learn()
        { 
            return;
        }

        public Simon( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 357 )
				BaseSoundID = 0x451;
		}
	}
}