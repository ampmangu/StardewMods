﻿using StardewModdingAPI;
using System;

namespace LGreenhouseBTQPlus
{
    internal class ModEntry : Mod
    {
        internal static ModEntry Instance;
        public ModConfig Config;
        public override void Entry(IModHelper helper)
        {
            I18n.Init(helper.Translation);
            ModEntry.Instance = this;

            
        }
    }
}
