using System;
using System.Collections.Generic;
using System.Linq;
using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using RPG_Noelf.Assets.Scripts.PlayerFolder;


namespace RPG_Noelf.Assets.Scripts.Ents
{
    public class Equips
    {
        public Armor[] armor = new Armor[4];//elm 0, armor 1, Legs 2.
        public Weapon weapon;
        private Player player;

        public Equips(Player player)
        {
            this.player = player;
        }
        
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
                            armor[0] = Encyclopedia.SearchFor(ID) as Armor;
                            player._Inventory.RemoveFromBag(ID, 1);//equip elm
                            break;
                        case PositionArmor.Armor:
                            armor[1] = Encyclopedia.SearchFor(ID) as Armor;
                            player._Inventory.RemoveFromBag(ID, 1);//equip armor
                            break;
                        case PositionArmor.Legs:
                            armor[2] = Encyclopedia.SearchFor(ID) as Armor;
                            player._Inventory.RemoveFromBag(ID, 1);//equip legs
                            break;
                        case PositionArmor.Boots:
                            armor[3] = Encyclopedia.SearchFor(ID) as Armor;
                            player._Inventory.RemoveFromBag(ID, 1);//equip boots
                            break;
                        default:

                            break;
                    }
                    UpdateEquip();
                }
            }
            else if (item is Weapon)
            {
                Slot weap = player._Inventory.GetSlot(ID);
                if (weap != null)
                {
                    weapon = Encyclopedia.SearchFor(weap.ItemID) as Weapon;
                    player._Inventory.RemoveFromBag(ID, 1);// equip weapon
                    UpdateEquip();
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
                            armor[0] = null;
                        }
                        break;
                    case PositionArmor.Armor:

                        if (player._Inventory.AddToBag(ID, 1))//unequip armor
                        {
                            armor[1] = null;
                        }
                        break;
                    case PositionArmor.Legs:

                        if (player._Inventory.AddToBag(ID, 1))//unequip legs
                        {
                            armor[2] = null;
                        }
                        break;
                    case PositionArmor.Boots:

                        if (player._Inventory.AddToBag(ID, 1))//unequip legs
                        {
                            armor[3] = null;
                        }
                        break;
                }
                UpdateEquip();
            }
            else if (item is Weapon)
            {
                Slot weap = player._Inventory.GetSlot(ID);
                if (weap != null)
                {

                    if (player._Inventory.AddToBag(ID, 1)) weapon = null;//unequip weapon
                }
            }
        }
        public void UpdateEquip()
        {
            double defesaTotal = 0;
            foreach(Armor arm in armor)
            {
                if(arm != null)
                    defesaTotal += arm.defense;
            }
            player.ArmorEquip = defesaTotal;
        }

    }
}