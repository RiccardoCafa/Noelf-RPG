using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Inventory_Scripts
{
    public enum TypeArmor
    {
        Heavy, Medium, Light
    }
    public enum PositionArmor
    {
        Elm, Armor, Legs, Boots
    }
    class Armor : Item
    {
        public PositionArmor PositArmor;
        public TypeArmor tpArmor;
        public float defense;
        public Armor(string name) : base(name)
        {
            IsStackable = false;

        }

        public string GetTypeArmor()
        {
            switch (tpArmor)
            {
                case TypeArmor.Heavy:
                    return "Heavy Armor";
                case TypeArmor.Light:
                    return "Light Armor";
                case TypeArmor.Medium:
                    return "Medium Armor";

            }

            return "";
        }



    }

}
