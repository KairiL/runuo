using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;
using Server.Spells;

namespace Server.Mobiles
{
	[CorpseName( "a Monstrous Interred Grizzle corpse" )]
	public class MonstrousInterredGrizzle : BaseCreature
	{
        private static Hashtable m_Table;
        [Constructable]
		public MonstrousInterredGrizzle() : base( AIType.AI_Necro, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Monstrous Interred Grizzle";
			Body = 259;			
			BaseSoundID = 589;

			SetStr( 1207, 1329 );
			SetDex( 121, 170 );
			SetInt( 595, 707 );

			SetHits( 10000, 10000 );

			SetDamage( 5, 11 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Energy, 75 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 70, 90 );
			SetResistance( ResistanceType.Cold, 55, 65 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 65, 75 );

			SetSkill( SkillName.EvalInt, 105.0, 120.0 );
			SetSkill( SkillName.Magery, 120.0, 120.0 );
			SetSkill( SkillName.SpiritSpeak, 120.0 );
			SetSkill( SkillName.Necromancy, 120.0 );
			SetSkill( SkillName.MagicResist, 100.0, 120.0);
			SetSkill( SkillName.Tactics, 110.1, 120.0 );
			SetSkill( SkillName.Wrestling, 103.4, 115.4 );

			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 80;
		
		    switch ( Utility.Random( 4 ))
            {                                   
            	case 0: AddItem( new CrimsonCinture() ); break;      	
			}

            switch ( Utility.Random( 8 ))
            {                                   
            	case 0: AddItem( new Tomb1() ); break;
            	case 1: AddItem( new Tomb2 () ); break;
				case 2: AddItem( new Tomb3() ); break;
            	case 3: AddItem( new Tomb4 () ); break;        	
			}
			switch ( Utility.Random( 12 ))
            {                                   
            	case 0: AddItem( new GrizzleSummoner() ); break;
				case 1: AddItem( new SkullPike2() ); break;
       	
			}
			switch ( Utility.Random( 64 ))
            {                                   
            	case 0: AddItem( new PlateOfHonorArms() ); break;
            	case 1: AddItem( new PlateOfHonorChest() ); break;
				case 2: AddItem( new PlateOfHonorGloves () ); break;
            	case 3: AddItem( new PlateOfHonorGorget () ); break;       
				case 4: AddItem( new PlateOfHonorHelm () ); break;		
				case 5: AddItem( new PlateOfHonorLegs () ); break;		
				case 6: AddItem( new AcolyteArms() ); break;
            	case 7: AddItem( new AcolyteChest() ); break;
				case 8: AddItem( new AcolyteGloves () ); break;	
				case 9: AddItem( new AcolyteLegs () ); break;	
				case 10: AddItem( new EvocaricusSword() ); break;
            	case 11: AddItem( new MalekisHonor() ); break;
				case 12: AddItem( new GrizzleArms() ); break;
            	case 13: AddItem( new GrizzleChest() ); break;
				case 14: AddItem( new GrizzleGloves () ); break;       
				case 15: AddItem( new GrizzleHelm () ); break;		
				case 16: AddItem( new PlateOfHonorLegs () ); break;
				case 17: AddItem( new MageArmorArms() ); break;
            	case 18: AddItem( new MageArmorChest() ); break;
				case 19: AddItem( new MageArmorGloves () ); break;	
				case 20: AddItem( new MageArmorLegs () ); break;                    
				case 21: AddItem( new MyrmidonArms() ); break;
            	case 22: AddItem( new MyrmidonChest() ); break;
				case 23: AddItem( new MyrmidonGloves () ); break;
            	case 24: AddItem( new MyrmidonGorget () ); break;       
				case 25: AddItem( new MyrmidonHelm () ); break;		
				case 26: AddItem( new MyrmidonLegs () ); break;		
				case 27: AddItem( new DeathEssenceArms() ); break;
            	case 28: AddItem( new DeathEssenceChest() ); break;
				case 29: AddItem( new DeathEssenceGloves () ); break;      
				case 30: AddItem( new DeathEssenceHelm () ); break;		
				case 31: AddItem( new DeathEssenceLegs () ); break;		
            }
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
            Timer.DelayCall(TimeSpan.FromMinutes(30.0), new TimerStateCallback(DeletePeerless), this);
        }

        public void DeletePeerless(object state)
        {
            Mobile from = (Mobile)state;
            from.Delete();
        }

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

        public override void GenerateLoot()
		{
            //AddLoot(LootPack.SuperBoss, 2);
            AddLoot(LootPack.Epic, 2);
            AddLoot(LootPack.HighScrolls, Utility.RandomMinMax(6, 60));

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
                case 0: AddItem(new GrizzleArms()); break;
                case 1: AddItem(new GrizzleChest()); break;
                case 2: AddItem(new GrizzleGloves()); break;
                case 3: AddItem(new GrizzleHelm()); break;
                case 4: AddItem(new GrizzleLegs()); break;
                case 5: AddItem(new GrizzleArms()); break;
                case 6: AddItem(new GrizzledMareStatuette()); break;

            }
            switch (Utility.Random(3))//common
            {
                case 0: AddItem(new TombstoneOfTheDamned()); break;
                case 1: AddItem(new GlobOfMonsterousInterredGrizzle()); break;
                case 2: AddItem(new GrizzledBones()); break;

            }
            switch (Utility.Random(5))//uncommon
            {
                case 0: AddItem(new MonsterousInterredGrizzleMaggots()); break;
                case 1: AddItem(new GrizzledSkullCollection()); break;
            }
        }

		public override int Meat{ get{ return 1; } }
        public override bool BardImmune { get { return false; } }
        public override int TreasureMapLevel{ get{ return 5; } }
        public override bool BleedImmune{ get{ return true; } }
		public override bool Unprovokable{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
        public override bool ReacquireOnMovement { get { return true; } }

        public override void OnDeath( Container c )
		{
			c.DropItem( new SkullPike() );
			c.DropItem( new GrizzledBones  () );

			base.OnDeath( c );
		}
		
		public void DrainLife()
		{
			ArrayList list = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 10 ) )
			{
				if ( m == this || !CanBeHarmful( m ) )
					continue;

				if ( m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team) )
					list.Add( m );
				else if ( m.Player )
					list.Add( m );
			}

			foreach ( Mobile m in list )
			{
				DoHarmful( m );

				m.FixedParticles( 0x374A, 10, 15, 5013, 0x496, 0, EffectLayer.Waist );
				m.PlaySound( 0x231 );

				m.SendMessage( "Your Life is Mine to feed on!" );

				int toDrain = Utility.RandomMinMax( 15, 40 );

				Hits += toDrain;
				m.Damage( toDrain, this );
			}
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.1 >= Utility.RandomDouble() )
				DrainLife();

            List<string> EffectList = new List<string>();
            EffectList.Add("CastSpeed");
            EffectList.Add("CastRecovery");
            EffectList.Add("WeaponSpeed");
            if (Utility.RandomDouble() < .15)
            {
                SpellHelper.AddAosBuff(this, defender, EffectList,
                              TimeSpan.FromSeconds(30), -1, false,
                              false, true, -60);
                this.CacophonicAttack(defender);
            }
        }

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.1 >= Utility.RandomDouble() )
				DrainLife();

            List<string> EffectList = new List<string>();
            EffectList.Add("CastSpeed");
            EffectList.Add("CastRecovery");
            EffectList.Add("WeaponSpeed");
            if (Utility.RandomDouble() < .15)
            {
                SpellHelper.AddAosBuff(this, attacker, EffectList,
                              TimeSpan.FromSeconds(30), -1, false,
                              false, true, -60);
                this.CacophonicAttack(attacker);
            }
        }

        public static bool UnderCacophonicAttack(Mobile from)
        {
            if (m_Table == null)
                m_Table = new Hashtable();

            return m_Table[from] != null;
        }

        #region CacophonicAttack

        public virtual void CacophonicAttack(Mobile to)
        {
            if (m_Table == null)
                m_Table = new Hashtable();

            if (to.Alive && to.Player && m_Table[to] == null)
            {
                to.Send(SpeedControl.WalkSpeed);
                to.SendLocalizedMessage(1072069); // A cacophonic sound lambastes you, suppressing your ability to move.
                to.PlaySound(0x584);

                m_Table[to] = Timer.DelayCall(TimeSpan.FromSeconds(30), new TimerStateCallback(EndCacophonic_Callback), to);
            }
        }

        public virtual void CacophonicEnd(Mobile from)
        {
            if (m_Table == null)
                m_Table = new Hashtable();

            m_Table[from] = null;

            from.Send(SpeedControl.Disable);
        }

        private void EndCacophonic_Callback(object state)
        {
            if (state is Mobile)
                this.CacophonicEnd((Mobile)state);
        }


        #endregion
        private int RandomPoint(int mid)
        {
            return (mid + Utility.RandomMinMax(-2, 2));
        }



        public virtual Point3D GetSpawnPosition(int range)
        {
            return GetSpawnPosition(Location, Map, range);
        }

        public virtual Point3D GetSpawnPosition(Point3D from, Map map, int range)
        {
            if (map == null)
                return from;

            Point3D loc = new Point3D((RandomPoint(X)), (RandomPoint(Y)), Z);

            loc.Z = Map.GetAverageZ(loc.X, loc.Y);

            return loc;
        }

        public override void OnThink()
        {
            base.OnThink();
            //if (Utility.RandomDouble() < .15)
            //    DropOoze();
            RandoTarget(this);
        }

        public virtual void DropOoze()
        {
            int amount = Utility.RandomMinMax(1, 3);
            bool corrosive = Utility.RandomBool();

            for (int i = 0; i < amount; i++)
            {
                Item ooze = new StainedOoze(corrosive);
                Point3D p = new Point3D(Location);

                for (int j = 0; j < 5; j++)
                {
                    p = GetSpawnPosition(2);
                    bool found = false;

                    foreach (Item item in Map.GetItemsInRange(p, 0))
                        if (item is StainedOoze)
                        {
                            found = true;
                            break;
                        }

                    if (!found)
                        break;
                }

                ooze.MoveToWorld(p, Map);
            }

            if (Combatant != null)
            {
                if (corrosive)
                    Combatant.SendLocalizedMessage(1072071); // A corrosive gas seeps out of your enemy's skin!
                else
                    Combatant.SendLocalizedMessage(1072072); // A poisonous gas seeps out of your enemy's skin!
            }
        }

        public override bool OnBeforeDeath()
        {
            //DropOoze();

            return base.OnBeforeDeath();
        }

        public MonstrousInterredGrizzle( Serial serial ) : base( serial )
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