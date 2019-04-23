using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Composition;
using Windows.UI.Xaml.Controls;

namespace RPG_Noelf.Assets.Scripts.Ents
{
    public static class Collision
    {
        public static List<Solid> solids;

        public static void OnMoved(DynamicSolid sender, EventArgs e)//sempre q um DynamicSolid se mover, verifica-se sua colisao com os outros Solid
        {
            bool down = false, up = false, right = false, left = false;
            sender.lockedKeys.Clear();
            foreach (Solid solid in solids)
            {
                if (sender.Equals(solid)) continue;//se for comparar o DynamicSolid com ele msm, pule
                if (sender.Xi <= solid.Xf && sender.Xf >= solid.Xi)//se o DynamicSolid movendo esta numa regiao q pode colidir em cima ou embaixo
                {
                    //if (sender.Yi <= solid.Yf)//se o DynamicSolid movendo esta colidindo em cima
                    //{
                    //    sender.lockedKeys.Add(VirtualKey.Up);
                    //    sender.lockedKeys.Add(VirtualKey.W);
                    //    up = true;
                    //}
                    if (sender.Yf >= solid.Yi)//se o DynamicSolid movendo esta colidindo embaixo
                    {
                        sender.lockedKeys.Add(VirtualKey.Down);
                        sender.lockedKeys.Add(VirtualKey.S);
                        down = true;
                    }
                }
                if (sender.Yi <= solid.Yf && sender.Yf >= solid.Yi)//se o DynamicSolid movendo esta numa regiao q pode colidir na esquerda ou na direita
                {
                    if (sender.Xf >= solid.Xi)//se o DynamicSolid movendo esta colidindo na direita
                    {
                        sender.lockedKeys.Add(VirtualKey.Right);
                        sender.lockedKeys.Add(VirtualKey.D);
                        right = true;
                    }
                    if (sender.Xi <= solid.Xf)//se o DynamicSolid movendo esta colidindo na esquerda
                    {
                        sender.lockedKeys.Add(VirtualKey.Left);
                        sender.lockedKeys.Add(VirtualKey.A);
                        left = true;
                    }
                }
                if (down && up && right && left) break;
            }
        }
    }
}
