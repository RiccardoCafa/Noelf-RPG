using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Enviroment
{
    public class Bau
    {
        private Category Type;
        public Bag itens { get; set; }

        private const int maxSlots = 15;

        public Bau(Category Type,int Qnt_itensMax)
        {
            this.Type = Type;
            itens = new Bag();
            GeneratorSizeCrate(Math.Clamp(Qnt_itensMax, 1, maxSlots));
            CrateCreation();
        }

        public void GeneratorSizeCrate(int Tamanho)//Quantos itens quer q o bau tenha
        {
            Random sizecrate = new Random();
            itens.FreeSlots = sizecrate.Next(Tamanho);
        }

        public void CrateCreation()
        {
            Random GetRand = new Random();
            int typeID = GetTypeId(Type);
            for (int i = 0; i < itens.FreeSlots; i++)
            {
                uint itemProcurado;
                do
                {
                    itemProcurado = (uint)GetRand.Next(Encyclopedia.encyclopedia.Count - 1);
                } while (Encyclopedia.SearchFor(itemProcurado) == null);

                if (typeID >= GetTypeId(Encyclopedia.SearchFor(itemProcurado).ItemCategory))
                {
                    if(Encyclopedia.SearchFor(itemProcurado).IsStackable)
                    {
                        itens.AddToBag(new Slot(itemProcurado, (uint)GetRand.Next(9) + 1));
                    } else
                    {
                        itens.AddToBag(new Slot(itemProcurado, 1));
                    }
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
