using System;
using Server;

namespace Server.Items
{
	public class ChannelersDefender : GlassSword
	{
		public override int LabelNumber{ get{ return 1113518; } } // Channeler's Defender

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public ChannelersDefender()
		{
			Hue = 95;
            Attributes.DefendChance = 10;
            Attributes.AttackChance = 5;
            Attributes.LowerManaCost = 5;
            Attributes.WeaponSpeed = 20;
            Attributes.CastRecovery = 1;
            Attributes.SpellChanneling = 1;
            WeaponAttributes.HitLowerAttack = 60;
            AosElementDamages.Energy = 100;
        }

		public override void GetDamageTypes( Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct )
		{
			phys = fire = cold = pois = nrgy = chaos = direct = 0;
			nrgy = 100;
		}

		public ChannelersDefender( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}