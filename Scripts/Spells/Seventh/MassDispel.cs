using System;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Ninjitsu;

namespace Server.Spells.Seventh
{
	public class MassDispelSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Mass Dispel", "Vas An Ort",
				263,
				9002,
				Reagent.Garlic,
				Reagent.MandrakeRoot,
				Reagent.BlackPearl,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Seventh; } }

		public MassDispelSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( IPoint3D p )
		{
			if ( !Caster.CanSee( p ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckSequence() )
			{
				SpellHelper.Turn( Caster, p );

				SpellHelper.GetSurfaceTop( ref p );

				List<Mobile> targets = new List<Mobile>();

				Map map = Caster.Map;

				if ( map != null )
				{
					IPooledEnumerable eable = map.GetMobilesInRange( new Point3D( p ), 8 );

					foreach ( Mobile m in eable )
                    {
						if ( (m is BaseCreature) && (m as BaseCreature).IsDispellable && Caster.CanBeHarmful( m, false ) || 
                            TransformationSpellHelper.UnderTransformation(m) || !m.CanBeginAction(typeof(PolymorphSpell)) ||
                            AnimalForm.UnderTransformation(Caster) || (TransformationSpellHelper.GetContext(m) != null))
							targets.Add( m );

                        
                    }

					eable.Free();
				}

				for ( int i = 0; i < targets.Count; ++i )
				{
					Mobile m = targets[i];

					BaseCreature bc = m as BaseCreature;
                    if (TransformationSpellHelper.UnderTransformation(m))
                    {
                        double dispelChance = Math.Pow((((Caster.Hunger + Caster.Thirst) / 2 + Caster.Skills.Magery.Value) /
                            ((m.Hunger - m.Thirst) / 2 + m.Skills.Necromancy.Value + m.Skills.MagicResist.Value + 1)),4) / 2;
                        if (dispelChance < 0)
                            dispelChance = 0.01;
                        if (dispelChance > 1)
                            dispelChance = 1;
                        if (dispelChance > Utility.RandomDouble())
                        {
                            Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                            Effects.PlaySound(m, m.Map, 0x201);
                            Caster.DoHarmful(m);
                            TransformationSpellHelper.RemoveContext(m, true);
                        }
                        else
                        {
                            m.FixedEffect(0x3779, 10, 20);
                            Caster.SendLocalizedMessage(1010084); // The creature resisted the attempt to dispel it!
                            Caster.DoHarmful(m);
                        }
                    }
                    else if (!m.CanBeginAction(typeof(PolymorphSpell)))
                    {
                        double dispelChance = Math.Pow((((Caster.Hunger + Caster.Thirst) / 2 + Caster.Skills.Magery.Value) /
                            ((m.Hunger - m.Thirst) / 2 + m.Skills.Magery.Value + m.Skills.MagicResist.Value + 1)), 4) / 2;
                        if (dispelChance < 0)
                            dispelChance = 0.01;
                        if (dispelChance > 1)
                            dispelChance = 1;
                        if (dispelChance > Utility.RandomDouble())
                        {
                            Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                            Effects.PlaySound(m, m.Map, 0x201);
                            PolymorphSpell.EndPolymorph(m);
                            //TransformationSpellHelper.RemoveContext(m, true);
                            Caster.DoHarmful(m);
                        }
                        else
                        {
                            Caster.DoHarmful(m);
                            m.FixedEffect(0x3779, 10, 20);
                            Caster.SendLocalizedMessage(1010084); // The creature resisted the attempt to dispel it!
                        }
                    }
                    else if (AnimalForm.UnderTransformation(Caster))
                    {
                        double dispelChance = Math.Pow((((Caster.Hunger + Caster.Thirst) / 2 + Caster.Skills.Magery.Value) /
                            ((m.Hunger - m.Thirst) / 2 + m.Skills.Ninjitsu.Value + m.Skills.MagicResist.Value + 1)),4) / 2;
                        if (dispelChance < 0)
                            dispelChance = 0.01;
                        if (dispelChance > 1)
                            dispelChance = 1;
                        if (dispelChance > Utility.RandomDouble())
                        {
                            Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                            Effects.PlaySound(m, m.Map, 0x201);
                            AnimalForm.RemoveContext(m, true);
                            Caster.DoHarmful(m);
                        }
                        else
                        {
                            Caster.DoHarmful(m);
                            m.FixedEffect(0x3779, 10, 20);
                            Caster.SendLocalizedMessage(1010084); // The creature resisted the attempt to dispel it!
                        }
                    }
                    else if (TransformationSpellHelper.GetContext(m) != null)
                    {
                        double dispelChance = Math.Pow((((Caster.Hunger + Caster.Thirst) / 2 + Caster.Skills.Magery.Value) /
                            ((m.Hunger - m.Thirst) / 2 + m.Skills.Spellweaving.Value + m.Skills.MagicResist.Value + 1)) , 4) / 2;
                        if (dispelChance < 0)
                            dispelChance = 0.01;
                        if (dispelChance > 1)
                            dispelChance = 1;
                        if (dispelChance > Utility.RandomDouble())
                        {
                            Caster.DoHarmful(m);
                            Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                            Effects.PlaySound(m, m.Map, 0x201);
                            TransformationSpellHelper.RemoveContext(m, true);
                        }
                        else
                        {
                            Caster.DoHarmful(m);
                            m.FixedEffect(0x3779, 10, 20);
                            Caster.SendLocalizedMessage(1010084); // The creature resisted the attempt to dispel it!
                        }
                    }
                    else
                    {
                        if ( bc == null )
						    continue;

					    double dispelChance = (50.0 + ((100 * (Caster.Skills.Magery.Value - bc.DispelDifficulty)) / (bc.DispelFocus*2))) / 100;

					    if ( dispelChance > Utility.RandomDouble() )
					    {
						    Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
						    Effects.PlaySound( m, m.Map, 0x201 );

						    m.Delete();
					    }
					    else
					    {
						    Caster.DoHarmful( m );

						    m.FixedEffect( 0x3779, 10, 20 );
					    }
                    }
				}
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private MassDispelSpell m_Owner;

			public InternalTarget( MassDispelSpell owner ) : base( Core.ML ? 10 : 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				IPoint3D p = o as IPoint3D;

				if ( p != null )
					m_Owner.Target( p );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}