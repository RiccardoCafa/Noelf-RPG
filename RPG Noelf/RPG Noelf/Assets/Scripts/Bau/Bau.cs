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
        public int SizeCrate;
        private Bag intens;
        public int GeneratorSizeCrate(int Tamanho)//Quantos intens quer q o bau tenha
        {
            Random sizecrate = new Random();
            SizeCrate = sizecrate.Next(Tamanho);
            return SizeCrate;
        }
        public int CrateType()
        {
            Random type = new Random();
            switch (type.Next(3)) {
                case 1:

                    break;
    


            }

            return 1;
        }
    }
}
