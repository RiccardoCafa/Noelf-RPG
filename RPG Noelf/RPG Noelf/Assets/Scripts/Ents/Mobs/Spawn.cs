using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    class Spawn
    {
        public static Mob CreateMob()
        {
            Random random = new Random();
            Mob mob = new Face();
            for (int i = 1; i <= 4; i++)
            {
                int code = random.Next(0, 5);
                switch (i)
                {
                    case 1:
                        mob = new Body(mob, code);
                        break;
                    case 2:
                        mob = new Arms(mob, code);
                        break;
                    case 3:
                        mob = new Legs(mob, code);
                        break;
                    case 4:
                        mob = new Skin(mob, code);
                        break;
                }
            }
            return mob;
        }
    }
}
