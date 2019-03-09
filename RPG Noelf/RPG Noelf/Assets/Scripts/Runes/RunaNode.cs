using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Runes
{
    class RunaNode
    {
        public bool status;
        public int key { get; set; }
        public int[] keylist;
        public float bonus;
        public RunaNode[] nodes;
        /*sequencia de atributos dos nós:
         * 
         Ataque,Defesa, Regeneração, Skills, HS/SP*/


        public RunaNode(int nkey)//construindo diversos nós cada vez que uma runa nova é instanciada
        {
            key = nkey;
            keylist = new int[5];
            nodes = new RunaNode[5];
            for(int i = 0; i<5 ; i++)
            {
                nodes[i] = new RunaNode(keylist[i]);
            }
        }
        /*selecionar a runa, e deixar bloqueada as outras */
        public void selectRuna(RunaNode selectedBonus)//metodo de seleção das runas
        { 
            int i = 0;
            if(nodes.Contains(selectedBonus) == true)
            {
                selectedBonus.status = true;
                if (keylist.Contains(selectedBonus.key) == true)
                {
                    selectedBonus.status = true;
                    while (i < 5)
                    {
                        if (nodes[i].key != selectedBonus.key)
                        {
                            nodes[i] = null;
                        }
                        i--;
                    }

                }

            }
           
        }

        /* bonus que a runa traz ao jogador*/
        public float giveBonus(int key)
        {
            int i = 0;
            if (keylist.Contains(key) == true)
            {
                while (i < 5)
                {
                    if(nodes[i].key == key)
                    {
                        return nodes[i].bonus;
                       
                    }
                    i++;
                }
                
            }
            return 0;   
        }


        /* metodo para percorrer o caminho feito pela runa */
        public void percorreRunas(RunaNode raiz, int key)
        {
            if(raiz != null && raiz.key == key){
                    


            }


        }

    }



    
}
