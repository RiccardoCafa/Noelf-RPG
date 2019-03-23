using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using RPG_Noelf.Assets.Scripts.PlayerFolder;


namespace RPG_Noelf.Assets.Scripts.Ents
{
    class Equips
    {
        public Equips(Player player)
        {
            this.player = player;
        }

        public uint[] armor = new uint[3];//elm 0, armor 1, Legs 2.
        public uint weapon;
        private Player player;

        public void UseEquip(uint ID)// function that makes the equipment be equipped by the player.
        {
            Item item = Encyclopedia.encyclopedia[ID];
            if (item is Armor)
            {
                Slot arm = player._Inventory.GetSlot(ID);
                if (arm != null)
                {
                    switch ((item as Armor).PositArmor) //swtich to know where the armor is
                    {
                        case PositionArmor.Elm:
                            armor[0] = ID;
                            player._Inventory.RemoveFromBag(ID, 1);//equip elm
                            break;
                        case PositionArmor.Armor:
                            armor[1] = ID;
                            player._Inventory.RemoveFromBag(ID, 1);//equip armor
                            break;
                        case PositionArmor.Legs:
                            armor[2] = ID;
                            player._Inventory.RemoveFromBag(ID, 1);//equip legs
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
                    player._Inventory.RemoveFromBag(ID, 1);// equip weapon
                }
            }

        }
        public void DesEquip(uint ID)//function that causes the equipment to be unbalanced by the player.
        {
            Item item = Encyclopedia.encyclopedia[ID];
            if (item is Armor)
            {

                switch ((item as Armor).PositArmor)//swtich to know where the armor is
                {
                    case PositionArmor.Elm:

                        if (player._Inventory.AddToBag(ID, 1))//unequip elm
                        {
                            armor[0] = 0;
                        }
                        break;
                    case PositionArmor.Armor:

                        if (player._Inventory.AddToBag(ID, 1))//unequip armor
                        {
                            armor[1] = 0;
                        }
                        break;
                    case PositionArmor.Legs:

                        if (player._Inventory.AddToBag(ID, 1))//unequip legs
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

                    if (player._Inventory.AddToBag(ID, 1)) weapon = 0;//unequip weapon
                }
            }
        }
    }
}