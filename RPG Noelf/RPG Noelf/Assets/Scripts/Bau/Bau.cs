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
        private int Type;
        private Bag intens;
        public Bau(int Type,int Qnt_itensMax)
        {
            this.Type = Type;
            GeneratorSizeCrate(Qnt_itensMax);
        }
        public void GeneratorSizeCrate(int Tamanho)//Quantos intens quer q o bau tenha
        {
            Random sizecrate = new Random();
            intens.FreeSlots = sizecrate.Next(Tamanho);
        }
        public int CrateCreation(Bau Novobau)
        {
            switch (Novobau.Type) {
                case 1:
                    for (int i = 0; i < Novobau.intens.FreeSlots; i++)
                    {
                        Random additem = new Random();
                    }
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }
            return 1;
        }
    }
}
