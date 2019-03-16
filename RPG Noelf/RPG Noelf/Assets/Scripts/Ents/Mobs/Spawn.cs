using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    class Spawn
    {
        public void CreateMob()
        {
            Random random = new Random();
            Mob mob = new MobCore();
            switch (random.Next(0, 5))
            {
                case 0:
                    mob = new DragonFace(mob);
                    break;
                case 1:
                    mob = new KongFace(mob);
                    break;
                case 2:
                    mob = new LizardFace(mob);
                    break;
                case 3:
                    mob = new BisonFace(mob);
                    break;
                case 4:
                    mob = new CatFace(mob);
                    break;
            }
            switch (random.Next(0, 5))
            {
                case 0:
                    mob = new DragonBody(mob);
                    break;
                case 1:
                    mob = new KongBody(mob);
                    break;
                case 2:
                    mob = new LizardBody(mob);
                    break;
                case 3:
                    mob = new BisonBody(mob);
                    break;
                case 4:
                    mob = new CatBody(mob);
                    break;
            }
            switch (random.Next(0, 5))
            {
                case 0:
                    mob = new DragonArms(mob);
                    break;
                case 1:
                    mob = new KongArms(mob);
                    break;
                case 2:
                    mob = new LizardArms(mob);
                    break;
                case 3:
                    mob = new BisonArms(mob);
                    break;
                case 4:
                    mob = new CatArms(mob);
                    break;
            }
            switch (random.Next(0, 5))
            {
                case 0:
                    mob = new DragonLegs(mob);
                    break;
                case 1:
                    mob = new KongLegs(mob);
                    break;
                case 2:
                    mob = new LizardLegs(mob);
                    break;
                case 3:
                    mob = new BisonLegs(mob);
                    break;
                case 4:
                    mob = new CatLegs(mob);
                    break;
            }
        }
    }
}
