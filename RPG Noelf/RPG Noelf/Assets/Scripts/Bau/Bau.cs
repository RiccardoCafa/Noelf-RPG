using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Bau
{
    class Bau
    {
        private Category Type;
        private Bag intens;

        public Bau(Category Type,int Qnt_itensMax)
        {
            this.Type = Type;
            GeneratorSizeCrate(Qnt_itensMax);
            CrateCreation();
        }

        public void GeneratorSizeCrate(int Tamanho)//Quantos itens quer q o bau tenha
        {
            Random sizecrate = new Random();
            intens.FreeSlots = sizecrate.Next(Tamanho);
        }

        public void CrateCreation()
        {
            Random GetRand = new Random();
            int typeID = GetTypeId(Type);
            for (int i = 0; i < intens.FreeSlots; i++)
            {
                uint itemProcurado = (uint) GetRand.Next(Encyclopedia.encyclopedia.Count);
                if (typeID >= GetTypeId(Encyclopedia.SearchFor(itemProcurado).ItemCategory))
                {
                    intens.AddToBag(new Slot(itemProcurado, (uint)GetRand.Next(10)));
                }
            }
        }

        public int GetTypeId(Category Type)
        {
            switch(Type)
            {
                case Category.Legendary:
                    return 3;
                case Category.Epic:
                    return 2;
                case Category.Uncommon:
                    return 1;
                default: return 0;
            }

        }
    }
}
