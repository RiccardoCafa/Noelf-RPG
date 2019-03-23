using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using RPG_Noelf.Assets.Scripts.PlayerFolder;


namespace RPG_Noelf.Assets.Scripts.Equipment
   
    
{
    class Equipment
    {
        public Equipment(Player player)
        {
            this.player = player;
        }

        public uint[] armor = new uint[3];//elmo 0, colete 1, pernas 2
        public uint weapon;
        private Player player;

        public void UseEquip(uint ID)
        {
            Item item = Encyclopedia.encyclopedia[ID];
            if (item is Armor)
            {
                Slot arm = player._Inventory.GetSlot(ID);
                if (arm != null)
                {
                    switch((item as Armor).PositArmor)
                    {
                        case PositionArmor.Elm:
                            armor[0] = ID;
                            player._Inventory.RemoveFromBag(ID, 1);
                            break;
                        case PositionArmor.Armor:
                            armor[1] = ID;
                            player._Inventory.RemoveFromBag(ID, 1);
                            break;
                        case PositionArmor.Legs:
                            armor[2] = ID;
                            player._Inventory.RemoveFromBag(ID, 1);
                            break;
                    }
                }
            }
            else if (item is Weapon)
            {
                Slot weap = player._Inventory.GetSlot(ID);
                if (weap != null)
                {
                    weapon = ID;
                    player._Inventory.RemoveFromBag(ID, 1);
                }
            }
        
        }
        public void DesEquip(uint ID)
        {
            Item item = Encyclopedia.encyclopedia[ID];
            if (item is Armor) {

                switch ((item as Armor).PositArmor)
                {
                case PositionArmor.Elm:
                        
                    if (player._Inventory.AddToBag(ID, 1))
                    {
                        armor[0] = 0;
                    }
                    break;
                case PositionArmor.Armor:
                        
                    if (player._Inventory.AddToBag(ID, 1))
                    {
                        armor[1] = 0;
                    }
                    break;
                case PositionArmor.Legs:
                        
                    if (player._Inventory.AddToBag(ID, 1))
                    {
                        armor[2] = 0;
                    }
                    break;
                }
            }
            else if (item is Weapon)
            {
                Slot weap = player._Inventory.GetSlot(ID);
                if (weap != null)
                {
                    
                    if(player._Inventory.AddToBag(ID, 1)) weapon = 0;
                }
            }
        }
    }
}

