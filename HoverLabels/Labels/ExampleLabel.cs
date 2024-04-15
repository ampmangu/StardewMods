﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.TerrainFeatures;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoverLabels.Drawing;

namespace HoverLabels.Labels;
internal class ExampleLabel : IHoverLabel
{
    public int Priority { get; set; } = -1;

    public void DrawOnOverlay(SpriteBatch spriteBatch)
    {
    }

    public void UpdateCursorTile(Vector2 cursorTile)
    {
    }
    public bool ShouldGenerateLabel(Vector2 cursorTile)
    {
        return Game1.currentLocation.terrainFeatures.ContainsKey(cursorTile)
            && Game1.currentLocation.terrainFeatures[cursorTile] is HoeDirt;
    }

    public IEnumerable<Border> GetContents()
    {
        List<Border> borders = new();
        Border title_border = new(new TitleLabelText("Tilled Dirt"));
        yield return title_border;

        Border desc = new();
        desc.AddLabelText(new LabelText("This tile has been hoe'd"));
        desc.AddLabelText(new LabelText("Isn't that cool?"));
        yield return desc;
    }

}
