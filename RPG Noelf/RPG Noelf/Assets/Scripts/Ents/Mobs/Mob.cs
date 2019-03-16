using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    interface IMob
    {
        void Make();
    }

    class Mob : Ent, IMob
    {

        public Mob(string code)// code = 0123 ou 4321 ou 0322...
        {
            /* fbtp = face braço tronco perna
             * 0 -> dragão
             * 1 -> gorila
             * 2 -> lagarto
             * 3 -> bisão
             * 4 -> gato
             */
        }

        public void Make() { }
    }
}
