﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Noelf.Assets.Scripts.Scenes
{
    enum TypeTile
    {
        grass, ground
    }

    class Tile
    {
        public static Dictionary<char, TypeTile> TileCode = new Dictionary<char, TypeTile>()
        {
            {'G', TypeTile.grass },
            {'g', TypeTile.ground }
        };

        public TypeTile tpTile;
        public double[] vetor = new double[2];
        public string Path { get; private set; }
        public readonly double[] Size;
        public readonly double[] VirtualSize;
        public double[] VirtualPosition { get; private set; } = new double[2];

        public Tile(TypeTile tpTile, double x, double y)
        {
            Size = new double[] { 60, 60 };
            VirtualSize = new double[] { 60, 115 };
            this.tpTile = tpTile;
            vetor[0] = x * Size[0];
            vetor[1] = y * Size[1];
            Path = "/Assets/Images/" + tpTile + ".png";
            VirtualPosition[0] = vetor[0];
            VirtualPosition[1] = vetor[1] - 40;
        }
    }
}