using GameProject.Components;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public class Tile
    {
        Texture2D model;
        public PositionComponent position;
        CollisionComponent collision;
    }
}
