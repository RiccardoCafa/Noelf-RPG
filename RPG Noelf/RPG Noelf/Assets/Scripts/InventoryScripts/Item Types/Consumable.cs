using RPG_Noelf.Assets.Scripts.Ents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Inventory_Scripts
{
    public interface Consumable
    {
        void Effect(Ent ent);
    }

    class LifePotion : Item, Consumable
    {
        public float hp;
        public LifePotion(string name, float hp) : base(name)
        {
            this.hp = hp;
        }

        public void Effect(Ent ent)
        {
            ent.Hp += hp;
        }
    }
    class ManaPotion : Item, Consumable
    {
        public float mana;
        public ManaPotion(string name, float mana) : base(name)
        {
            this.mana = mana;
        }

        public void Effect(Ent ent)
        {
            ent.Mp += mana;
        }
    }
}
