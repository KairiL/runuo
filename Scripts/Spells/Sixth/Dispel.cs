using System;
using Server.Misc;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Ninjitsu;

namespace Server.Spells.Sixth
{
	public class DispelSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Dispel", "An Ort",
				218,
				9002,
				Reagent.Garlic,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.Sixth; } }

		public DispelSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public class InternalTarget : Target
		{
			private DispelSpell m_Owner;

			public InternalTarget( DispelSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					Mobile m = (Mobile)o;
					BaseCreature bc = m as BaseCreature;

					if ( !from.CanSee( m ) )
					{
						from.SendLocalizedMessage( 500237 ); // Target can not be seen.
					}
                    else if (TransformationSpellHelper.UnderTransformation(m))
                    {
                        double dispelChance = ((from.Hunger + from.Thirst)/2 + from.Skills.Magery.Value - (m.Hunger - m.Thirst)/2 - m.Skills.Necromancy.Value + m.Skills.MagicResist.Value + 1)/100;
                        if ( dispelChance < 0 )
                            dispelChance = 0.01;
                        if ( dispelChance > 1 )
                            dispelChance = 1;
                        if (dispelChance > Utility.RandomDouble())
                        {
                            Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                            Effects.PlaySound(m, m.Map, 0x201);
                            from.DoHarmful(m);
                            TransformationSpellHelper.RemoveContext(m, true);
                        }
                        else
                        {
                            m.FixedEffect(0x3779, 10, 20);
                            from.SendLocalizedMessage(1010084); // The creature resisted the attempt to dispel it!
                            from.DoHarmful(m);
                        }
                    }
                    else if (!m.CanBeginAction(typeof(PolymorphSpell)))
                    {
                        double dispelChance = ((from.Hunger + from.Thirst) / 2 + from.Skills.Magery.Value - (m.Hunger - m.Thirst) / 2 - m.Skills.Magery.Value + m.Skills.MagicResist.Value + 1) / 100;
                        if (dispelChance < 0)
                            dispelChance = 0.01;
                        if (dispelChance > 1)
                            dispelChance = 1;
                        if (dispelChance > Utility.RandomDouble())
                        {
                            Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                            Effects.PlaySound(m, m.Map, 0x201);
                            //TransformationSpellHelper.RemoveContext(m, true);
                            PolymorphSpell.EndPolymorph(m);
                            from.DoHarmful(m);
                        }
                        else
                        {
                            m.FixedEffect(0x3779, 10, 20);
                            from.SendLocalizedMessage(1010084); // The creature resisted the attempt to dispel it!
                        }
                    }
                    else if (AnimalForm.UnderTransformation(m))
                    {
                        double dispelChance = ((from.Hunger + from.Thirst) / 2 + from.Skills.Magery.Value - (m.Hunger - m.Thirst) / 2 - m.Skills.Ninjitsu.Value + m.Skills.MagicResist.Value + 1) / 100;
                        if (dispelChance < 0)
                            dispelChance = 0.01;
                        if (dispelChance > 1)
                            dispelChance = 1;
                        if (dispelChance > Utility.RandomDouble())
                        {
                            Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                            Effects.PlaySound(m, m.Map, 0x201);
                            AnimalForm.RemoveContext(m, true);
                            from.DoHarmful(m);
                        }
                        else
                        {
                            from.DoHarmful(m);
                            m.FixedEffect(0x3779, 10, 20);
                            from.SendLocalizedMessage(1010084); // The creature resisted the attempt to dispel it!
                        }
                    }
                    else if (TransformationSpellHelper.GetContext(m) != null)
                    {
                        double dispelChance = ((from.Hunger + from.Thirst) / 2 + from.Skills.Magery.Value - (m.Hunger - m.Thirst) / 2 - m.Skills.Spellweaving.Value + m.Skills.MagicResist.Value + 1) / 100;
                        if (dispelChance < 0)
                            dispelChance = 0.01;
                        if (dispelChance > 1)
                            dispelChance = 1;
                        if (dispelChance > Utility.RandomDouble())
                        {
                            from.DoHarmful(m);
                            Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                            Effects.PlaySound(m, m.Map, 0x201);
                            TransformationSpellHelper.RemoveContext(m, true);
                        }
                        else
                        {
                            from.DoHarmful(m);
                            m.FixedEffect(0x3779, 10, 20);
                            from.SendLocalizedMessage(1010084); // The creature resisted the attempt to dispel it!
                        }
                    }
                    else if ( bc == null || !bc.IsDispellable )
					{
						from.SendLocalizedMessage( 1005049 ); // That cannot be dispelled.
					}
					else if ( m_Owner.CheckHSequence( m ) )
					{
						SpellHelper.Turn( from, m );

						double dispelChance = (50.0 + ((100 * (from.Skills.Magery.Value - bc.DispelDifficulty)) / (bc.DispelFocus*2))) / 100;

						if ( dispelChance > Utility.RandomDouble() )
						{
							Effects.SendLocationParticles( EffectItem.Create( m.Location, m.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
							Effects.PlaySound( m, m.Map, 0x201 );

							m.Delete();
						}
						else
						{
							m.FixedEffect( 0x3779, 10, 20 );
							from.SendLocalizedMessage( 1010084 ); // The creature resisted the attempt to dispel it!
						}
					}
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}