//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Windows.System;
//using Windows.UI.Composition;
//using Windows.UI.Xaml.Controls;
//using static RPG_Noelf.Assets.Scripts.Solid;

//namespace RPG_Noelf.Assets.Scripts.Ents
//{
//    //public static class Collision
//    //{
//    //private const double margin = 20;
//    //public static void OnMoved(Solid solidMoving)//sempre q um DynamicSolid se mover, verifica-se sua colisao com os outros Solid
//    //{
//    //    solidMoving.freeDirections[Direction.down] =
//    //    solidMoving.freeDirections[Direction.right] =
//    //    solidMoving.freeDirections[Direction.left] = true;
//    //    foreach (Solid solid in solids)
//    //    {
//    //        solidMoving.Interaciton(solid);
//    //if (solidMoving.Equals(solid)) continue;//se for comparar o solidMoving com ele msm, pule o teste
//    //if (solidMoving.Yf >= solid.Yi && solidMoving.Yf < solid.Yi + margin)//se o solidMoving esta no nivel de pisar em algum Solid
//    //{
//    //    if (solidMoving.Xi < solid.Xf && solidMoving.Xf > solid.Xi)//se o solidMoving esta colindindo embaixo
//    //    {
//    //        solidMoving.Yf = solid.Yi;
//    //        solidMoving.freeDirections[Direction.down] = false;
//    //    }
//    //}
//    //if (solidMoving.Yi < solid.Yf && solidMoving.Yf > solid.Yi)//se o solid eh candidato a colidir nos lados do solidMoving
//    //{
//    //    if (solidMoving.Xf >= solid.Xi && solidMoving.Xf < solid.Xi + margin)//se o solidMoving esta colindindo a direita
//    //    {
//    //        solidMoving.Xf = solid.Xi;
//    //        solidMoving.freeDirections[Direction.right] = false;
//    //    }
//    //    if (solidMoving.Xi <= solid.Xf && solidMoving.Xi > solid.Xf - margin)//se o solidMoving esta colindindo a esquerda
//    //    {
//    //        solidMoving.Xi = solid.Xf;
//    //        solidMoving.freeDirections[Direction.left] = false;
//    //    }
//    //}
//    //if (!solidMoving.freeDirections[Direction.down] &&
//    //    !solidMoving.freeDirections[Direction.right] &&
//    //    !solidMoving.freeDirections[Direction.left]) break;
//    //}
//    //}
//    //}
//}