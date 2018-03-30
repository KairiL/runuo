using System;
using Server;
using Server.Items;
using System.Collections.Generic;
using Server.Spells;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a bone demon corpse" )]
	public class BigBen : BaseCreature
	{
		[Constructable]
		public BigBen() : base( AIType.AI_NecromageEpic, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Big Ben";
			Body = 576;
			BaseSoundID = 0x47E;
            Hue = 11;

			SetStr( 1 );
			SetDex( 1 );
			SetInt( 1 );

			SetHits( 50000 );

			SetDamage( 0 );

			SetDamageType( ResistanceType.Physical, 0 );
            SetDamageType( ResistanceType.Fire, 0 );
            SetDamageType(ResistanceType.Cold, 0);
            SetDamageType( ResistanceType.Poison, 0 );
            SetDamageType(ResistanceType.Energy, 0);

            SetResistance( ResistanceType.Physical, 200 );
			SetResistance( ResistanceType.Fire, 200 );
			SetResistance( ResistanceType.Cold, 200 );
			SetResistance( ResistanceType.Poison, 200 );
			SetResistance( ResistanceType.Energy, 200 );

			VirtualArmor = 44;
            
            AddMonkRobe();

            CantWalk = true;
            Direction = Direction.Left;
		}

        public void AddMonkRobe()
        { 
            MonkRobe item = new MonkRobe();
            item.Hue = 0;
            item.LootType = LootType.Blessed;
            item.Name = "A Phantom Robe";
            item.Resistances.Direct = 200;
            AddItem(item);
        }

		public override void GenerateLoot()
		{
            AddLoot(LootPack.Epic, 1);
        }
        private int RoomRange = 50;

        List<Mobile> Victims = new List<Mobile>();
        List<int> VictimTimes = new List<int>();

        public override void OnThink()
        { 
            bool phantomAlive = false;
            double newVictimRate = .05;
            int tickStartTime = 800; //~5 ticks per second
            int newVictimNumber = 0;
            List <Mobile> mobilesInRoom = new List<Mobile>(GetMobilesInRange(RoomRange));
            List <Mobile> enemiesInRoom = new List<Mobile>();

            //Direction = Direction.Left;
            if (mobilesInRoom.Count != 0)
                for ( int i = mobilesInRoom.Count-1; i>=0; i-- )
                    if ((mobilesInRoom[i] != null))
                        if (mobilesInRoom[i] is Phantom)
                            phantomAlive = true;
                        else if ( (mobilesInRoom[i] != this) && (SpellHelper.ValidIndirectTarget(mobilesInRoom[i], this) &&
                                 mobilesInRoom[i].CanBeHarmful(this, false) ) )
                            enemiesInRoom.Add(mobilesInRoom[i]);
            
            if (enemiesInRoom.Count != 0)
                if (phantomAlive && Utility.RandomDouble() < newVictimRate)
                {
                    newVictimNumber = Utility.Random(enemiesInRoom.Count);
                    if ( !Victims.Contains(enemiesInRoom[newVictimNumber]) )
                    {
                        Victims.Add(enemiesInRoom[newVictimNumber]);
                        VictimTimes.Add(tickStartTime);
                        enemiesInRoom[newVictimNumber].PublicOverheadMessage(MessageType.Emote, EmoteHue, false, "*TICK*");
                        enemiesInRoom[newVictimNumber].SendMessage("An overwhelming sense of doom comes over you!");
                    }
                }
            
            if (!phantomAlive)
                Reset();
            else if (!Paralyzed)
                Tick();
                    
        }


        public void Reset()
        { 
            Victims.Clear();
            VictimTimes.Clear();
        }
        public void Tick()
        { 
            List<int> KilledVictimcs;
            int len = Victims.Count;
            if (len == 0)
                return;
            for (int i=len-1; i>=0; i--)
            { 
                if (Victims[i] == null || Victims[i].Deleted || Victims[i].Map != Map || 
                    !InRange(Victims[i], RoomRange) || !Victims[i].Alive || !Victims[i].CanBeHarmful(this, false) ||
                    !SpellHelper.ValidIndirectTarget(Victims[i], this))
                {
                    Victims.RemoveAt(i);
                    VictimTimes.RemoveAt(i);
                }
                else
                {
                    VictimTimes[i]--;
                    if (VictimTimes[i] % 5 == 0)
                        Victims[i].PublicOverheadMessage(MessageType.Emote, EmoteHue, false, String.Format("*{0}*", VictimTimes[i]));
                    if (VictimTimes[i] <= 0)
                    { 
                        Victims[i].Kill();
                        if (Victims[i]!=null)
                            Victims[i].Location = new Point3D(2044,235,13); // Kick out of area
                        Victims.RemoveAt(i);
                        VictimTimes.RemoveAt(i);
                    }            
                }
            }
        }

        public override void Animate(int action, int frameCount, int repeatCount, bool forward, bool repeat, int delay)
        {
        }

        public override bool CanBeHarmful(Mobile target, bool message, bool ignoreOurBlessedness)
        {
            return false;
        }

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
           Hits = 50000;
        }

        public override void AlterMeleeDamageFrom(Mobile from, ref int damage)
        {
            Hits = 50000;
            damage = 0;
        }

        public override void AlterSpellDamageFrom(Mobile from, ref int damage)
        {
            Hits = 50000;
            damage = 0;
        }

        public override bool BardImmune { get { return true; } }
		public override bool Unprovokable { get { return true; } }
		public override bool AreaPeaceImmune { get { return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Godly; } }
        public override bool ShowFameTitle { get { return false; } }
        public override bool AlwaysMurderer { get { return true; } }
        public override bool DisallowAllMoves { get { return true; } }

        public BigBen( Serial serial ) : base( serial )
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