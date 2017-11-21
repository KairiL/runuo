using System;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Items
{
    public class TrapRepairKit : HolidayBell
    {
        [Constructable]
        public TrapRepairKit()
		{
            Name = "A Trap Repair Kit";
        }
        public TrapRepairKit(Serial serial) : base( serial )
		{
            Name = "A Trap Repair Kit";
        }
        public override void OnDoubleClick(Mobile from)
        {
            from.SendMessage("What would you like to repair?");
            from.Target = new InternalTarget();

        }
        private class InternalTarget : Target
        {
            public InternalTarget() : base(20, false, TargetFlags.None)
            {
            }
            protected override void OnTarget(Mobile from, object target)
            {
                int newUses;
                int newTrapPower;
                int newTimeSpan;
                int newDamageRange;
                int newTriggerRange;
                int newDamage=0;



                bool repaired = false;
                bool upgraded = false;
                

                if (target is CraftedTrap)
                {
                    newUses = (int)(from.Skills.Tailoring.Value + 
                        (from.Skills.Carpentry.Value + (from.Skills.ArmsLore.Value + from.Skills[((CraftedTrap)target).BonusSkill].Value / 2) / 4) / 2 + Utility.RandomMinMax(1, 3));
                    if ( ( (CraftedTrap)target).UsesRemaining > newUses )
                    {
                        ((CraftedTrap)target).UsesRemaining = newUses;
                        repaired = true;
                        from.SendMessage("You repair the trap");
                    }

                    if (!(((CraftedTrap)target).TrapPower != 0 ))
                    {
                        newTrapPower = (int)Math.Round(from.Skills.Tinkering.Value) + (int)Math.Round(from.Skills[((CraftedTrap)target).BonusSkill].Value) - 50;
                        if (newTrapPower > ((CraftedTrap)target).TrapPower)
                        {
                            ( (CraftedTrap)target).TrapPower = newTrapPower;
                            from.SendMessage("You upgrade the trap's power.");
                            upgraded = true;
                        }

                    }

                    if ( target is CraftedPoisonGasTrap || target is CraftedFireColumnTrap || 
                         target is CraftedExplosionTrap || target is CraftedElectricTrap )
                    {
                        newTimeSpan = 5 - (int)(from.Skills.Blacksmith.Value + from.Skills.Carpentry.Value) / 100;
                        if (from.Skills.Blacksmith.Value >= 120)
                            newTimeSpan -= 1;
                        if (TimeSpan.FromSeconds(newTimeSpan) < ((CraftedTrap)target).Delay)
                        {
                            ((CraftedTrap)target).Delay = TimeSpan.FromSeconds(newTimeSpan);
                            from.SendMessage("You upgrade the trap's delay.");
                            upgraded = true;
                        }
                    }

                    if (target is CraftedTeleporter || target is CraftedBoltTrap )
                    {
                        newTimeSpan = 3 - (int)(from.Skills.Blacksmith.Value + 
                                                 from.Skills.Carpentry.Value) / 100;
                        if (from.Skills.Blacksmith.Value >= 120)
                            newTimeSpan -= 1;
                        if (TimeSpan.FromSeconds(newTimeSpan) < ((CraftedTrap)target).Delay)
                        {
                            ((CraftedTrap)target).Delay = TimeSpan.FromSeconds(newTimeSpan);
                            from.SendMessage("You upgrade the trap's delay.");
                            upgraded = true;
                        }
                    }

                    newTriggerRange = 0;

                    if (target is CraftedPoisonGasTrap )
                        newTriggerRange = 2;
                    else if (target is CraftedExplosionTrap ||
                             target is CraftedBoltTrap)
                        newTriggerRange = 3;
                    else if (target is CraftedElectricTrap )
                        newTriggerRange = 1;

                    newTriggerRange += (int)(from.Skills.Fletching.Value * 2 + 
                                             from.Skills.ArmsLore.Value) / 100;

                    if ( newTriggerRange > ((CraftedTrap)target).TriggerRange )
                    {
                        ((CraftedTrap)target).TriggerRange = newTriggerRange;
                        from.SendMessage("You have upgraded the trap's Trigger Range");
                        upgraded = true;
                    }
                    
                    newDamageRange = 0;

                    if (target is CraftedExplosionTrap ||
                        target is CraftedBoltTrap)
                        newDamageRange = 3;
                    else if (target is CraftedPoisonGasTrap )
                        newDamageRange = 2;
                    else if (target is CraftedElectricTrap )
                        newDamage = 1;

                    if (target is CraftedPoisonGasTrap )
                        newDamage += 2 * (int)( from.Skills.Alchemy.Value + 
                                               from.Skills.Blacksmith.Value + 
                                               from.Skills.Tinkering.Value + 
                                               from.Skills.Poisoning.Value) / 100;
                    else if ( target is CraftedFireColumnTrap ||
                              target is CraftedElectricTrap ||
                              target is CraftedBoltTrap )
                        newDamage += (int)( from.Skills.Alchemy.Value + 
                                            from.Skills.Blacksmith.Value + 
                                            from.Skills.Tinkering.Value ) / 100;
                    else if ( target is CraftedExplosionTrap )
                        newDamage += 2 * (int)(from.Skills.Alchemy.Value + 
                                               from.Skills.Blacksmith.Value + 
                                               from.Skills.Tinkering.Value) / 100;


                    if (newDamageRange > ((CraftedTrap)target).DamageRange)
                    {
                        ((CraftedTrap)target).DamageRange = newDamageRange;
                        from.SendMessage("You have upgraded the trap's Damage Range");
                        upgraded = true;
                    }



                }
                else
                    from.SendMessage("You can't repair that with this kit.");
            }
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
    
}