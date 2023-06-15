﻿using StardewValley;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchtuurCore.Events
{
    public class WateringFinishedArgs : EventArgs
    {
        public Farmer farmer;
        public HoeDirt target;
        public WateringFinishedArgs(Farmer _farmer, HoeDirt _target)
        {
            this.farmer = _farmer;
            this.target = _target;
        }
    }
}
