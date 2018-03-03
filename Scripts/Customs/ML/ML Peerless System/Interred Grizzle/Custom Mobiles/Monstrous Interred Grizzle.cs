using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;


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

			SetStr( 488, 620 );
			SetDex( 121, 170 );
			SetInt( 800, 800 );

			SetHits( 10000, 11000 );

			SetDamage( 25, 28 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Energy, 75 );

			SetResistance( ResistanceType.Physical, 80, 90 );
			SetResistance( ResistanceType.Fire, 70, 80 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 50, 60 );

			SetSkill( SkillName.EvalInt, 105.0, 110.0 );
			SetSkill( SkillName.Magery, 120.0, 120.0 );
			SetSkill( SkillName.SpiritSpeak, 120.0 );
			SetSkill( SkillName.Necromancy, 120.0 );
			SetSkill( SkillName.MagicResist, 120.0, 120.0);
			SetSkill( SkillName.Tactics, 110.1, 120.0 );
			SetSkill( SkillName.Wrestling, 120.1, 150.0 );

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
            Timer.DelayCall(TimeSpan.FromMinutes(10.0), new TimerStateCallback(DeletePeerless), this);
        }

        public void DeletePeerless(object state)
        {
            Mobile from = (Mobile)state;
            from.Delete();
        }

        public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.MedScrolls, 2 );
            
        }

		public override int Meat{ get{ return 1; } }
		public override int TreasureMapLevel{ get{ return 5; } }
        public override bool BleedImmune{ get{ return true; } }
        public override bool AutoDispel{ get{ return true; } }
		public override bool Unprovokable{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		
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

				int toDrain = Utility.RandomMinMax( 10, 40 );

				Hits += toDrain;
				m.Damage( toDrain, this );
			}
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.1 >= Utility.RandomDouble() )
				DrainLife();

            if (Utility.RandomDouble() < 0.15)
                this.CacophonicAttack(defender);
        }

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.1 >= Utility.RandomDouble() )
				DrainLife();

            if (Utility.RandomDouble() < 0.15)
                this.CacophonicAttack(attacker);
        }

        public static bool UnderCacophonicAttack(Mobile from)
        {
            if (m_Table == null)
                m_Table = new Hashtable();

            return m_Table[from] != null;
        }

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
		}
	}
}