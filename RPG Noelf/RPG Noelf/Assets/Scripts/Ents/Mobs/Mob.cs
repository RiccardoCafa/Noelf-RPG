using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    class Mob : Ent
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
            
            for (int i = 0; i < 5; i++)
            {

            }
        }
    }
}

/* Itens:
 * #Dragão
 *  > Face
 *   - Mnd +2
 *   - attack +Fireball()
 *  > Braços
 *   - Mnd +1
 *  > Tronco
 *   - Mnd +2
 *   - armor +Fire
 *  > Pernas
 *   - Mnd +1
 *   - armor -Ice
 * #Gorila
 *  > Face
 *   - For +2
 *  > Braços
 *   - For +1
 *   - attack +Poopoo()
 *  > Tronco
 *   - For +2
 *   - armor -Fire
 *  > Pernas
 *   - For +1
 *   - Spd +6
 * #Lagarto
 *  > Face
 *   - Spd +2
 *   - attack +Lick()
 *  > Braços
 *   - Spd +1
 *  > Tronco
 *   - Spd +2
 *   - armor +Poison
 *  > Pernas
 *   - Spd =0
 * #Bisão
 *  > Face
 *   - Con +2
 *   - attack +Headache()
 *  > Braços
 *   - Con +1
 *  > Tronco
 *   - Con +2
 *   - armor +Common
 *  > Pernas
 *   - Con +1
 *   - behavior +Meek
 * #Gato
 *  > Face
 *   - Dex +2
 *  > Braços
 *   - Dex +1
 *   - attack +Scratch()
 *  > Tronco
 *   - Dex +2
 *   - behavior +Camouflage
 *  > Pernas
 *   - Dex +1
 *   - armor -Common
 */
