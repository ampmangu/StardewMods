﻿using AchtuurCore.Patches;
using HarmonyLib;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AchtuurCore.Events
{
    internal class WateringPatcher : GenericPatcher
    {
        public override void Patch(Harmony harmony, IMonitor monitor)
        {
            // Prefix patch
            harmony.Patch(
                original: this.getOriginalMethod<HoeDirt>(nameof(HoeDirt.performToolAction)),
                prefix: this.getHarmonyMethod(nameof(this.prefix_performToolAction))
            );

            // Postfix patch
            harmony.Patch(
                original: this.getOriginalMethod<HoeDirt>(nameof(HoeDirt.performToolAction)),
                postfix: this.getHarmonyMethod(nameof(this.postfix_performToolAction))
            );
        }
        private static void prefix_performToolAction(Tool t, HoeDirt __instance, out WateringInfo __state)
        {
            __state = new WateringInfo();
            try
            {
                __state.soilStateBefore = __instance.state.Value;
                __state.toolUsed = t;
            }
            catch (Exception e)
            {
                ModEntry.Instance.Monitor.Log($"Something went wrong when prefix patching performToolAction (WateringPatcher):\n{e}", LogLevel.Error);
            }
        }

        private static void postfix_performToolAction(ref HoeDirt __instance, WateringInfo __state)
        {
            try
            {
                // If tool was not a watering can, return
                if (__state.toolUsed is null || !__state.toolUsed.Name.ToLower().Contains("watering can"))
                    return;

                // Tile has been watered -> call watering soil event
                if (__state.soilStateBefore != 1 && __instance.state.Value == 1)
                {
                    Farmer lastUser = __state.toolUsed.getLastFarmerToUse();
                    WateringFinishedArgs args = new WateringFinishedArgs(lastUser, __instance);
                    EventPublisher.InvokeFinishedWateringSoil(null, args);
                }
            }
            catch(Exception e)
            {
                ModEntry.Instance.Monitor.Log($"Something went wrong when postfix patching performToolAction (WateringPatcher):\n{e}", LogLevel.Error);
            }
        }
    }

    struct WateringInfo
    {
        /// <summary>
        /// Tracks whether soil state changes to 1 when calling <see cref="HoeDirt.performToolAction"/> 
        /// </summary>
        internal int soilStateBefore;

        internal Tool toolUsed;
        //internal Farmer toolUser;
    }
}
