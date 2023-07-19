﻿using StardewValley.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using AchtuurCore.Utility;

namespace AchtuurCore.Extensions;
public static class BuildingExtensions
{
    public static Rectangle GetRect(this Building building)
    {
        return new Rectangle(
            x: building.tileX.Value,
            y: building.tileY.Value,
            width: building.tilesWide.Value,
            height: building.tilesHigh.Value
        );
    }
    public static IEnumerable<Vector2> GetTiles(this Building building)
    {
        return building.GetRect().GetTiles();
    }
}
