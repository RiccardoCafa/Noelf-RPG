using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Ents.Mobs
{
    interface IParts
    {
        void UpdateMob(Mob mob);

        IParts Choose(int code);
    }
}
