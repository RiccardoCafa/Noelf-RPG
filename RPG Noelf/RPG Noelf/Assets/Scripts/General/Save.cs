using RPG_Noelf.Assets.Scripts.Ents;
using RPG_Noelf.Assets.Scripts.Ents.NPCs;
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
            int Questcount = 0;
            path = Path.Combine(path, fileName);

            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("ID" + CustomPlayer.Id+ "\n");
                sw.WriteLine("LEVEL " + CustomPlayer.level.actuallevel + "\n");
                sw.WriteLine("CON " + CustomPlayer.Con + "\n");
                sw.WriteLine("DEX " + CustomPlayer.Dex + "\n");
                sw.WriteLine("SPD " + CustomPlayer.Spd + "\n");
                sw.WriteLine("STR " + CustomPlayer.Str + "\n");
                sw.WriteLine("MND " + CustomPlayer.Mnd + "\n");
                sw.WriteLine("GOLD " + CustomPlayer._Inventory.Gold + "\n");
                sw.WriteLine("XP " + CustomPlayer.level.actualEXP + "\n");
                sw.WriteLine("WEAPON " + Encyclopedia.SearchFor(CustomPlayer.Equipamento.weapon) + "\n");
                for (int j = 0; j<3; j++)
                {
                    sw.WriteLine("ARMADURA" + j + " " + Encyclopedia.SearchFor(CustomPlayer.Equipamento.armor[j])+"\n");
                }
                sw.WriteLine("SKILLPOINTS " + CustomPlayer._SkillManager.SkillPoints + "\n");
                for(int j = 0; j < 4; j++)
                {
                    sw.WriteLine("SkillBAR"+j+" " + CustomPlayer._SkillManager.SkillBar[j].ID + "\n");
                    sw.WriteLine("SkillBARLVL" + j + " " + CustomPlayer._SkillManager.SkillBar[j].Lvl + "\n");

                }
                foreach (SkillGenerics skill in CustomPlayer._SkillManager.SkillList)
                {
                    sw.WriteLine("SKILL"+ skillcount+ " " + CustomPlayer._SkillManager.SkillList[skillcount].Lvl + "\n");
                    skillcount++;
                }
                sw.WriteLine("ATRIBUTOPOINTS " + CustomPlayer._Class.StatsPoints + "\n");
                sw.WriteLine("XI " + CustomPlayer.box.Xi + "\n");
                sw.WriteLine("YI " + CustomPlayer.box.Yi + "\n");
                foreach (Slot a in CustomPlayer._Inventory.Slots)
                {
                    sw.WriteLine("SLOT" + slotcount+" " + a.ItemID+" "+ a.ItemAmount +"\n");
                    slotcount++;
                }
                foreach (Quest q in CustomPlayer._Questmanager.finishedQuests)
                {
                    sw.WriteLine("QUESTS" + Questcount + " " + CustomPlayer._Questmanager.finishedQuests[Questcount].QUEST_ID);
                }
            }
        }
        public void LoadGameData(ref Player loadPlayer)
        {
            int lvl,xp;
            string path = Path.Combine(Path.GetTempPath() + @"/Noelf/slot_");
            if (File.Exists(path))
            {
                using (StreamReader rw = File.OpenText(path))
                {
                    string r = "";
                    while ((r = rw.ReadLine()) != null)
                    {
                        var b = r.Split(" ");
                        if (b[0] == "ID") loadPlayer.Id = b[1];
                        if (b[0] == "LEVEL")
                        {
                            int.TryParse(b[1], out lvl);
                            loadPlayer.level.actuallevel = lvl;
                        }
                        if (b[0] == "CON") int.TryParse(b[1], out loadPlayer.Con);
                        if (b[0] == "DEX") int.TryParse(b[1], out loadPlayer.Dex);
                        if (b[0] == "SPD") int.TryParse(b[1], out loadPlayer.Spd);
                        if (b[0] == "STR") int.TryParse(b[1], out loadPlayer.Str);
                        if (b[0] == "MND") int.TryParse(b[1], out loadPlayer.Mnd);
                        if (b[0] == "XP")
                        {
                            int.TryParse(b[1], out xp);
                            loadPlayer.level.actualEXP = xp;
                        }
                        if (b[0] == "WEAPON")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer.Equipamento.weapon = (Weapon)Encyclopedia.SearchFor((uint)result);
                        }
                        if (b[0] == "ARMADURA0")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer.Equipamento.armor[0] = (Armor)Encyclopedia.SearchFor((uint)result);
                        }
                        if (b[0] == "ARMADURA1")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer.Equipamento.armor[1] = (Armor)Encyclopedia.SearchFor((uint)result);
                        }
                        if (b[0] == "ARMADURA2")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer.Equipamento.armor[2] = (Armor)Encyclopedia.SearchFor((uint)result);
                        }
                        if (b[0] == "SKILLPOINTS")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.SkillPoints = result;
                        }
                        if(b[0] == "SKILLBAR0")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.AddSkillToBar(loadPlayer._SkillManager.FindSkill(result), 0);
                        }
                        if (b[0] == "SKILLBAR1")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.AddSkillToBar(loadPlayer._SkillManager.FindSkill(result), 1);
                        }
                        if (b[0] == "SKILLBAR2")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.AddSkillToBar(loadPlayer._SkillManager.FindSkill(result), 2);
                        }
                        if (b[0] == "SKILLBAR3")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.AddSkillToBar(loadPlayer._SkillManager.FindSkill(result), 3);
                        }
                        if (b[0] == "SKILLBARLVL0")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.SkillBar[0].Lvl = result;
                        }
                        if (b[0] == "SKILLBARLVL1")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.SkillBar[1].Lvl = result;
                        }
                        if (b[0] == "SKILLBARLVL2")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.SkillBar[2].Lvl = result;
                        }
                        if (b[0] == "SKILLBARLVL3")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.SkillBar[3].Lvl = result;
                        }
                        if(b[0] == "SKILL0")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.SkillList[0].Lvl = result;
                        }
                        if (b[0] == "SKILL1")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.SkillList[1].Lvl = result;
                        }
                        if (b[0] == "SKILL2")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.SkillList[2].Lvl = result;
                        }
                        if (b[0] == "SKILL3")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.SkillList[3].Lvl = result;
                        }
                        if (b[0] == "SKILL4")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.SkillList[4].Lvl = result;
                        }
                        if (b[0] == "SKILL5")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.SkillList[5].Lvl = result;
                        }
                        if (b[0] == "SKILL6")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.SkillList[6].Lvl = result;
                        }
                        if (b[0] == "SKILL7")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.SkillList[7].Lvl = result;
                        }
                        if (b[0] == "SKILL8")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.SkillList[8].Lvl = result;
                        }
                        if (b[0] == "SKILL9")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.SkillList[9].Lvl = result;
                        }
                        if (b[0] == "SKILL10")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.SkillList[10].Lvl = result;
                        }
                        if (b[0] == "SKILL11")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.SkillList[11].Lvl = result;
                        }
                        if (b[0] == "SKILL12")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.SkillList[12].Lvl = result;
                        }
                        if (b[0] == "SKILL13")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.SkillList[13].Lvl = result;
                        }
                        if (b[0] == "SKILL14")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._SkillManager.SkillList[14].Lvl = result;
                        }
                        if(b[0] == "ATRIBUTOPOINTS")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer._Class.StatsPoints = result;
                        }
                        if (b[0] == "XI")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer.box.Xi = result;
                        }
                        if (b[0] == "YI")
                        {
                            int.TryParse(b[1], out int result);
                            loadPlayer.box.Yi = result;
                        }
                        for (int i = 0; i < 30 && b[0] == "SLOT"+i; i++)
                        {
                            int.TryParse(b[1], out int result);
                            int.TryParse(b[2], out int result2);
                            Slot slot = new Slot((uint)result,(uint)result2);
                            loadPlayer._Inventory.AddToBag(slot);
                        }
                        
                    }
                }

            }
        }
    }
}
