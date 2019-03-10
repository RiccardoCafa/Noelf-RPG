using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Crafting_Scripts
{
    class Recipe//a receita na mochila mostra os possiveis craftings e materiais do player
    {
       
        public List<string> IDsMateriais { get; set; }//materiais nescessarios
        public int qtdItens { get; set; } //quantidade de itens



        public Recipe()
        {
            IDsMateriais = new List<string>();
            qtdItens = IDsMateriais.Count;
        }

        public void addInRecipeList(string IDfound)//adiciona na lista o ID do item achado
        {
            if(IDsMateriais.Contains(IDfound) == false)
            {
                IDsMateriais.Add(IDfound);
            }
            else
            {

            }

        }


        public bool readyToCraft(Bag playerBag)
        {

            int count = 0;//contador de materiais
           foreach(Item item in playerBag.slots)//buscando os IDs na mochila do player
            {
                // TODO COMUNISTA
                /*if(IDsMateriais.Contains(item.itemID) == true)
                {
                    count++;//incrementa se achar
                }*/
            }


           if(count == qtdItens)//se a qtd de itens estiver certa, retorna true
            {
                return true;
            }
            else//senão retorna falso
            {
                return false;
            }

        }

        public int nMaterials(Bag playerBag)
        {
            int count = 0;

            foreach(Item item in playerBag.slots)
            {
                // TODO COMUNISTA
                /*if(item.itemID.StartsWith("BM") == true || item.itemID.StartsWith("AM") == true)
                {
                    count++;
                }*/
            }

            return count;
        }




    }
}
