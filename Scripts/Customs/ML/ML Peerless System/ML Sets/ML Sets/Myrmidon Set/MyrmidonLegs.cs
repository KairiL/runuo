using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x13DA, 0x13E1 )]
	public class MyrmidonLegs : BaseArmor
	{
		public override int LabelNumber{ get{ return 1074306; } }
		public override int BasePhysicalResistance{ get{ return 7; } }
		public override int BaseFireResistance{ get{ return 7; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 3; } }

		public override int InitMinHits{ get{ return 35; } }
		public override int InitMaxHits{ get{ return 45; } }

		public override int AosStrReq{ get{ return 35; } }
		public override int OldStrReq{ get{ return 35; } }

		public override int ArmorBase{ get{ return 16; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Studded; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.Half; } }

		[Constructable]
		public MyrmidonLegs() : base( 0x13DA )
		{
			Weight = 7.0;
			Attributes.BonusStr = 1;
			Attributes.BonusHits = 2;
		}
		
		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			
			list.Add( 1072376, "6" );
			
			if ( this.Parent is Mobile )
			{
				if ( this.Hue == 0x2CA )
				{
					list.Add( 1072377 );
					list.Add( 1073489, "500" );
					list.Add( 1060441 );
					list.Add( 1060450, "3" );
				}
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( this.Hue == 0x0 )
			{
				list.Add( 1072378 );
				list.Add( 1072382, "3" );
				list.Add( 1072383, "3" );
				list.Add( 1072384, "3" );
				list.Add( 1072385, "3" );
				list.Add( 1072386, "3" );
				list.Add( 1060450, "3" );
				list.Add( 1073489, "500" );
				list.Add( 1060441 );
			}
		}

		public override bool OnEquip( Mobile from )
		{
			
			Item shirt = from.FindItemOnLayer( Layer.InnerTorso );
			Item glove = from.FindItemOnLayer( Layer.Gloves );
			Item neck = from.FindItemOnLayer( Layer.Neck );
			Item helm = from.FindItemOnLayer( Layer.Helm );
			Item arms = from.FindItemOnLayer( Layer.Arms );
			
			if ( shirt != null && shirt.GetType() == typeof( MyrmidonChest ) && glove != null && glove.GetType() == typeof( MyrmidonGloves ) && neck != null && neck.GetType() == typeof( MyrmidonGorget ) && helm != null && helm.GetType() == typeof( MyrmidonHelm ) && arms != null && arms.GetType() == typeof( MyrmidonArms ) )
			{
				Effects.PlaySound( from.Location, from.Map, 503 );
				from.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );

				Hue = 0x2CA;
				ArmorAttributes.SelfRepair = 3;
				PhysicalBonus = 3;
				FireBonus = 3;
				ColdBonus = 3;
				PoisonBonus = 3;
				EnergyBonus = 3;


				MyrmidonChest chest = from.FindItemOnLayer( Layer.InnerTorso ) as MyrmidonChest;
				MyrmidonGloves gloves = from.FindItemOnLayer( Layer.Gloves ) as MyrmidonGloves;
				MyrmidonGorget gorget = from.FindItemOnLayer( Layer.Neck ) as MyrmidonGorget;
				MyrmidonHelm helmet = from.FindItemOnLayer( Layer.Helm ) as MyrmidonHelm;
				MyrmidonArms arm = from.FindItemOnLayer( Layer.Arms ) as MyrmidonArms;

				chest.Hue = 0x2CA;
				chest.Attributes.NightSight = 1;
				chest.Attributes.Luck = 500;
				chest.ArmorAttributes.SelfRepair = 3;
				chest.PhysicalBonus = 3;
				chest.FireBonus = 3;
				chest.ColdBonus = 3;
				chest.PoisonBonus = 3;
				chest.EnergyBonus = 3;
				
				gloves.Hue = 0x2CA;
				gloves.ArmorAttributes.SelfRepair = 3;
				gloves.PhysicalBonus = 3;
				gloves.FireBonus = 3;
				gloves.ColdBonus = 3;
				gloves.PoisonBonus = 3;
				gloves.EnergyBonus = 3;

				gorget.Hue = 0x2CA;
				gorget.ArmorAttributes.SelfRepair = 3;
				gorget.PhysicalBonus = 3;
				gorget.FireBonus = 3;
				gorget.ColdBonus = 3;
				gorget.PoisonBonus = 3;
				gorget.EnergyBonus = 3;

				helmet.Hue = 0x2CA;
				helmet.ArmorAttributes.SelfRepair = 3;
				helmet.PhysicalBonus = 3;
				helmet.FireBonus = 3;
				helmet.ColdBonus = 3;
				helmet.PoisonBonus = 3;
				helmet.EnergyBonus = 3;
				
				arm.Hue = 0x2CA;
				arm.ArmorAttributes.SelfRepair = 3;
				arm.PhysicalBonus = 3;
				arm.FireBonus = 3;
				arm.ColdBonus = 3;
				arm.PoisonBonus = 3;
				arm.EnergyBonus = 3;
				
						
				from.SendLocalizedMessage( 1072391 );
			}
			this.InvalidateProperties();
			return base.OnEquip( from );							
		}

		public override void OnRemoved( IEntity parent )
		{
			if ( parent is Mobile )
			{
				Mobile m = ( Mobile )parent;
				Hue = 0x0;
				ArmorAttributes.SelfRepair = 0;
				PhysicalBonus = 0;
				FireBonus = 0;
				ColdBonus = 0;
				PoisonBonus = 0;
				EnergyBonus = 0;
				if ( m.FindItemOnLayer( Layer.InnerTorso ) is MyrmidonChest && m.FindItemOnLayer( Layer.Gloves ) is MyrmidonGloves && m.FindItemOnLayer( Layer.Arms ) is MyrmidonArms && m.FindItemOnLayer( Layer.Neck ) is MyrmidonGorget && m.FindItemOnLayer( Layer.Helm ) is MyrmidonHelm )
				{
					MyrmidonChest chest = m.FindItemOnLayer( Layer.InnerTorso ) as MyrmidonChest;
					chest.Hue = 0x0;
					chest.Attributes.NightSight = 0;
					chest.Attributes.Luck = 0;
					chest.ArmorAttributes.SelfRepair = 0;
					chest.PhysicalBonus = 0;
					chest.FireBonus = 0;
					chest.ColdBonus = 0;
					chest.PoisonBonus = 0;
					chest.EnergyBonus = 0;

					MyrmidonGloves gloves = m.FindItemOnLayer( Layer.Gloves ) as MyrmidonGloves;
					gloves.Hue = 0x0;
					gloves.ArmorAttributes.SelfRepair = 0;
					gloves.PhysicalBonus = 0;
					gloves.FireBonus = 0;
					gloves.ColdBonus = 0;
					gloves.PoisonBonus = 0;
					gloves.EnergyBonus = 0;
					
					MyrmidonArms arm = m.FindItemOnLayer( Layer.Arms ) as MyrmidonArms;
					arm.Hue = 0x0;
					arm.ArmorAttributes.SelfRepair = 0;
					arm.PhysicalBonus = 0;
					arm.FireBonus = 0;
					arm.ColdBonus = 0;
					arm.PoisonBonus = 0;
					arm.EnergyBonus = 0;

					MyrmidonGorget gorget = m.FindItemOnLayer( Layer.Neck ) as MyrmidonGorget;
					gorget.Hue = 0x0;
					gorget.ArmorAttributes.SelfRepair = 0;
					gorget.PhysicalBonus = 0;
					gorget.FireBonus = 0;
					gorget.ColdBonus = 0;
					gorget.PoisonBonus = 0;
					gorget.EnergyBonus = 0;

					MyrmidonHelm helmet = m.FindItemOnLayer( Layer.Helm ) as MyrmidonHelm;
					helmet.Hue = 0x0;
					helmet.ArmorAttributes.SelfRepair = 0;
					helmet.PhysicalBonus = 0;
					helmet.FireBonus = 0;
					helmet.ColdBonus = 0;
					helmet.PoisonBonus = 0;
					helmet.EnergyBonus = 0;
				}
				this.InvalidateProperties();
			}
			base.OnRemoved( parent );
		}

		public MyrmidonLegs( Serial serial ) : base( serial )
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