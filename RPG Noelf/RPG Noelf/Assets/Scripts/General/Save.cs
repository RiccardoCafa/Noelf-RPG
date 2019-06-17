using RPG_Noelf.Assets.Scripts.Inventory_Scripts;
using RPG_Noelf.Assets.Scripts.PlayerFolder;
using RPG_Noelf.Assets.Scripts.Skills;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.General
{
    class Save
    {
        int selectedSlot;
        public void SavePlayerData(Player CustomPlayer)
        {
            string path = Path.GetTempPath() + @"Noelf";
            
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = "slot_" + selectedSlot;
            int slotcount = 0;
            int skillcount = 0;
            path = Path.Combine(path, fileName);

            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("ID " + CustomPlayer.Id+ "\n");
                sw.WriteLine("STATUS:"+ "\n");
                sw.WriteLine("LEVEL " + CustomPlayer.level.actuallevel + "\n");
                sw.WriteLine("CON " + CustomPlayer.Con + "\n");
                sw.WriteLine("DEX " + CustomPlayer.Dex + "\n");
                sw.WriteLine("SPD " + CustomPlayer.Spd + "\n");
                sw.WriteLine("STR " + CustomPlayer.Str + "\n");
                sw.WriteLine("MND " + CustomPlayer.Mnd + "\n");
                sw.WriteLine("XP " + CustomPlayer.level.actualEXP + "\n");
                sw.WriteLine("CLASSE " + CustomPlayer._Class.ClassName + "\n");
                sw.WriteLine("WEAPON " + Encyclopedia.SearchFor(CustomPlayer.Equipamento.weapon) + "\n");
                for (int j = 0; j<3; j++)
                {
                    sw.WriteLine("ARMADURA" + j + " " + Encyclopedia.SearchFor(CustomPlayer.Equipamento.armor[j]));
                }
                sw.WriteLine("SKILLPOINTS " + CustomPlayer._SkillManager.SkillPoints + "\n");
                for(int j = 0; j < 4; j++)
                {
                sw.WriteLine("XP " + CustomPlayer._SkillManager.SkillBar[j] + "\n");
                }
                foreach (SkillGenerics skill in CustomPlayer._SkillManager.SkillList)
                {
                    sw.WriteLine("SKILL"+ skillcount+ " " + CustomPlayer._SkillManager.SkillList + "\n");
                    skillcount++;
                }
                sw.WriteLine("ATRIBUTOPOINTS " + CustomPlayer._Class.StatsPoints + "\n");
                sw.WriteLine("XI " + CustomPlayer.box.Xi + "\n");
                sw.WriteLine("YI " + CustomPlayer.box.Yi + "\n");
                sw.WriteLine("Invetario \n");
                foreach (Slot a in CustomPlayer._Inventory.Slots)
                {
                    sw.WriteLine("slot" + slotcount + a.ItemID + " Amount:" + a.ItemAmount + "\n");
                    slotcount++;
                }
            }
        }
        public void LoadGameData(ref Player loadPlayer)
        {

        }
    }
}
