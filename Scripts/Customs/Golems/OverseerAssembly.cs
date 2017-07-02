using System;
using Server;
using Server.Mobiles;
using Server.Spells;
using Server.Items;
using Server.Targeting;
using Server.Engines.Craft;


namespace Server.Items
{
	public class OverseerAssembly : Item
	{
		
		[Constructable]
		public OverseerAssembly() : base( 0x1EA8 )
		{
			Weight = 5.0;
			Hue = 1102;
			Name = "clockwork overseer assembly";
		}

		public OverseerAssembly( Serial serial ) : base( serial )
		{
		}

		

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
				return;
			}

			double tinkerSkill = from.Skills[SkillName.Tinkering].Value;

			if ( tinkerSkill < 60.0 )
			{
				from.SendMessage( "You must have at least 60.0 skill in tinkering to construct a golem." );
				return;
			}
			
			from.SendMessage( "What metal do you wish to use for your golem?" );
			
			from.Target = new IngotTarget( this ); //This shit make the other shit work
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
		
		private class IngotTarget : Target
		{
			private OverseerAssembly m_ass;

			public IngotTarget(OverseerAssembly ass ) : base( 2, false, TargetFlags.None )
			{
			m_ass = ass;
			}
		
			protected override void OnTarget( Mobile from, Object targeted )
			{   
				double tinkerSkill = from.Skills[SkillName.Tinkering].Value;
                double smithSkill = from.Skills[SkillName.Blacksmith].Value;
                double armsSkill = from.Skills[SkillName.ArmsLore].Value;
                double carpentrySkill = from.Skills[SkillName.Carpentry].Value;
                //double bowSkill = from.Skills[SkillName.Bowcraft].Value;
                //double alchemySkill = from.Skills[SkillName.Alchemy].Value;
                double tailoringSkill = from.Skills[SkillName.Tailoring].Value;
                double synergyBonus = (smithSkill + armsSkill + tailoringSkill + carpentrySkill)/625.0;
                Type typ;
                double metal;
				
				if ( targeted is IronIngot)
					{metal = 0.1; typ = typeof( IronIngot );}
				else if ( targeted is DullCopperIngot && tinkerSkill > 59.9)
					{metal = 0.2; typ = typeof( DullCopperIngot );}
				else if ( targeted is ShadowIronIngot && tinkerSkill > 64.9)
					{metal = 0.3; typ = typeof( ShadowIronIngot );}
				else if ( targeted is CopperIngot && tinkerSkill > 69.9)
					{metal = 0.4; typ = typeof( CopperIngot );}
				else if ( targeted is BronzeIngot && tinkerSkill > 74.9)
					{metal = 0.5; typ = typeof( BronzeIngot );}
				else if ( targeted is GoldIngot && tinkerSkill > 79.9)
					{metal = 0.6; typ = typeof( GoldIngot );}
				else if ( targeted is AgapiteIngot && tinkerSkill > 84.9)
					{metal = 0.7; typ = typeof( AgapiteIngot );}
				else if ( targeted is VeriteIngot && tinkerSkill > 89.9)
					{metal = 0.8; typ = typeof( VeriteIngot );}
				else if ( targeted is ValoriteIngot && tinkerSkill > 94.9)
					{metal = 0.9; typ = typeof( ValoriteIngot );}
				else 
					{
					from.SendMessage("You havent got the required skill for that kind of iron");
				 	return;
					}
				if ( metal >= 0.7 && (from.Followers + 3) > from.FollowersMax )
					{
					from.SendLocalizedMessage( 1049607 ); // You have too many followers to control that creature.
					return;
					}
				else if ( (from.Followers + 2) > from.FollowersMax )
					{
					from.SendLocalizedMessage( 1049607 ); // You have too many followers to control that creature.
					return;
					}
					
				double scalar;

				if ( tinkerSkill >= 100.0 )
					scalar = 1.0 + metal;
				else if ( tinkerSkill >= 90.0 )
					scalar = 0.9 + metal;
				else if ( tinkerSkill >= 80.0 )
					scalar = 0.8 + metal;
				else if ( tinkerSkill >= 70.0 )
					scalar = 0.7 + metal;
				else
					scalar = 0.6 + metal;

                scalar += synergyBonus;//max .7 without ancient hammer, .768 with.

                Container pack = from.Backpack;
				if ( pack == null )
					return;
								
				int res = pack.ConsumeTotal(
					new Type[]
					{
						typeof( PowerCrystal ),
						typ,
						typeof( Gears ),
                        typeof( GreaterExplosionPotion ),
                        typeof( Bolt ),
                        typeof( Board ),
                        typeof( Leather ),
                    },
					new int[]
					{
						1,
						500,
						50,
                        50,
                        200,
                        50,
                        50
                    } );

				switch ( res )
				{
					case 0:
					{
						from.SendMessage( "You must have a power crystal to construct the golem." );
						break;
					}
					case 1:
					{
						from.SendMessage( "You must have 500 ingots to construct the golem." );
						break;
					}
					case 2:
					{
						from.SendMessage( "You must have 50 gears to construct the golem." );
						break;
					}
                    case 3:
                    {
                        from.SendMessage("You must have 50 greater explosion potions to construct the golem.");
                        break;
                    }
                    case 4:
                    {
                        from.SendMessage("You must have 200 bolts to construct the golem.");
                        break;
                    }
                    case 5:
                    {
                        from.SendMessage("You must have 50 boards to construct the golem.");
                        break;
                    }
                    case 6:
                    {
                        from.SendMessage("You must have 50 leather to construct the golem.");
                        break;
                    }

                    default:
					{
						Golem g = new GolemOverseer( 1, scalar, metal );

						if ( g.SetControlMaster( from ) )
						{
							m_ass.Delete();

							g.MoveToWorld( from.Location, from.Map );
							from.PlaySound( 0x241 );
						}

						break;
					}
				}

			}
		}







	}

}