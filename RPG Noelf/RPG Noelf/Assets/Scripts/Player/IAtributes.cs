using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.PlayerFolder
{
    interface IAtributes
    {
        int Str { get; set; }//força
        int Spd { get; set; }//velocidade
        int Dex { get; set; }//destreza
        int Con { get; set; }//constituiçao
        int Mnd { get; set; }//mente
    }
}