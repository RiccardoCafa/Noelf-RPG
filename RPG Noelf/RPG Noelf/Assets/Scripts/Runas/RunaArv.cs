using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Runas
{
    class RunaArv
    {
        public static string description;
        public bool isAtive; //booleana que ativa o efeito da runa
        public float bonusEffect;//efeito que a runa retorna
        public int key;
        public int[] nodeKey = new int[5];//caracter que representa a chave do nó da runa
        public RunaArv atq;//runa de ataque
        public RunaArv defense;//runa de defesa
        public RunaArv skill;//runa de habilidades
        public RunaArv LifeRegen;//runa da vida
        public RunaArv ManaRegen;//runa da mana

        public RunaArv()
        {
            for(int i = 0;i < 5; i++)
            {
                nodeKey[i] = i;
            }//colocando as chaves em ordem

            //atribuindo as chaves as folhas
            atq = new RunaArv();
            atq.key = this.nodeKey[0];
            defense = new RunaArv();
            defense.key = this.nodeKey[1];
            skill = new RunaArv();
            skill.key = this.nodeKey[2];
            LifeRegen = new RunaArv();
            LifeRegen.key = this.nodeKey[3];
            ManaRegen = new RunaArv();
            ManaRegen.key = this.nodeKey[4];
        }

       public void selectRune(int key, RunaArv runa)
        {
            if(nodeKey.Contains(key) == true)
            {
               if(runa.key = key)
                {
                    runa.isAtive = true;
                   
                }

            }
            else
            {


            }
        }

        public void blockPath(RunaArv active)
        {
           

        }
       




    }
}
