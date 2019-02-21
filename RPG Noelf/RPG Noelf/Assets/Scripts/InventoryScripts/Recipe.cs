
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.InventoryScripts
{
    class Recipe
    {

        // a principio farei apenas combinações de dois itens do tipo Material
        public int nescessaryID_1 { get; set; }
        public int nescessaryID_2 { get; set; }
        public int amount1 { get; set; }
        public int amount2 { get; set; }
        public Recipe(int id1,int id2)
        {
            nescessaryID_1 = id1;
            nescessaryID_2 = id2;

        }

       public bool isIngredient(int ID)
        {
            if (ID == nescessaryID_1 || ID == nescessaryID_2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int needingAmount(int amount, int ID)
        {
            if(isIngredient(ID) == true)
            {
                if(ID == nescessaryID_1)
                {
                    return amount1 - amount;
                }
                else
                {
                    return amount2 - amount;
                }

            }
            else
            {
                return -1;
            }
            
        }

        public bool hasTheAmount(int ID, int amount)
        {
            if(isIngredient(ID) == true)
            {
                if(amount == amount1 || amount == amount2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        public bool canCraft(int ID1,int amountID1, int ID2, int amountID2)
        {
            if(isIngredient(ID1) == true  && isIngredient(ID2) ==  true && hasTheAmount(ID1,amountID1) ==  true && hasTheAmount(ID2,amountID2) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
    
    
}


