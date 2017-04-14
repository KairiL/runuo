//Created by Milkman Dan 07/24/2006
using System;
using Server;
using Server.Items;


namespace Server.Items
{
              public class ScrappersCompendium: Spellbook
{
              
              [Constructable]
              public ScrappersCompendium()
{

                          Weight = 1;
                          Name = "Scrapper's Compendium";
                          Hue = 1172;
              
              Attributes.CastRecovery = 1;
              Attributes.SpellDamage = 25;
              Attributes.CastSpeed = 1;
              Attributes.LowerManaCost = 10;
                  }
              public ScrappersCompendium( Serial serial ) : base( serial )
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
