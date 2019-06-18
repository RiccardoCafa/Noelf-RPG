using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Scenes
{
    enum TypeTile
    {
        grass, ground,
        neve, groundNeve
    }

    class Tile
    {
        public static Dictionary<char, TypeTile> TileCode = new Dictionary<char, TypeTile>()
        {
            {'G', TypeTile.grass },
            {'g', TypeTile.ground },
            {'O', TypeTile.neve },
            {'o', TypeTile.groundNeve }
        };

        public TypeTile tpTile;
        public double[] vetor = new double[2];
        public string Path { get; private set; }
        public static readonly double[] Size = new double[] { Matriz.scale, Matriz.scale };
        public static readonly double[] VirtualSize = new double[] { Matriz.scale, Matriz.scale * 2 };
        public double[] VirtualPosition { get; private set; } = new double[2];

        public Tile(TypeTile tpTile, double x, double y)
        {
            this.tpTile = tpTile;
            vetor[0] = x * Size[0];
            vetor[1] = y * Size[1];
            Path = "/Assets/Images/tiles/" + tpTile + ".png";
            VirtualPosition[0] = vetor[0];
            VirtualPosition[1] = vetor[1] - Matriz.scale * 0.6;
        }
    }
}
