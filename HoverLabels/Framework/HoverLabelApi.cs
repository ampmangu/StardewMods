﻿using StardewModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HoverLabels.Framework;
public class HoverLabelApi : IHoverLabelApi
{
    IManifest manifest;
    public HoverLabelApi(IManifest manifest)
    {
        this.manifest = manifest;
    }

    public bool IsAlternativeSortButtonPressed()
    {
        return ModEntry.IsAlternativeSortButtonPressed();
    }

    public bool IsShowDetailsButtonPressed()
    {
        return ModEntry.IsShowDetailButtonPressed();
    }

    public void RegisterLabel(IManifest mod, string name, IHoverLabel label)
    {
        try
        {
            ModEntry.Instance.LabelManager.AddLabel(mod, name, label);
        }
        catch(Exception ex)
        {
            AchtuurCore.Logger.ErrorLog(ModEntry.Instance.Monitor, $"Label couldn't be registered: {ex}");
        }
    }
}
