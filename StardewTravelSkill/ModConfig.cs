﻿using Pathoschild.Stardew.Common.Integrations.GenericModConfigMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewTravelSkill
{
    internal class ModConfig
    {
        /// <summary>
        /// Exp gained for watering a single tile
        /// </summary>
        public static float LevelMovespeedBonus { get; set; }

        /// <summary>
        /// Movespeed bonus granted by <see cref="ProfessionMovespeed"/>. Defaults to 0.05.
        /// </summary>
        public static float MovespeedProfessionBonus { get; set; }

        /// <summary>
        /// Percentage of stamina that recovers every 10 minutes by <see cref="ProfessionRestoreStamina"/>. Defaults to 1%
        /// </summary>
        public static float RestoreStaminaPercentage { get; set; }


        /// <summary>
        /// Number of steps to walk before getting sprint bonus
        /// </summary>
        public static int SprintSteps { get; set; }

        /// <summary>
        /// Bonus multiplier to movespeed that is applied by sprinting
        /// </summary>
        public static float SprintMovespeedBonus { get; set; }

        /// <summary>
        /// Use chance for a totem when profession is unlocked
        /// </summary>
        public static float TotemUseChance { get; set; }

        /// <summary>
        /// Number of steps to walk before getting 1 Exp
        /// </summary>
        public static int StepsPerExp { get; set; }

        

        public ModConfig()
        {
            ModConfig.LevelMovespeedBonus = 0.01f;
            ModConfig.MovespeedProfessionBonus = 0.05f;
            ModConfig.RestoreStaminaPercentage = 0.01f;
            ModConfig.SprintMovespeedBonus = 0.15f;
            ModConfig.SprintSteps = 5;
            ModConfig.TotemUseChance = 0.5f;
            ModConfig.StepsPerExp = 25;
        }

        /// <summary>
        /// Constructs config menu for GenericConfigMenu mod
        /// </summary>
        /// <param name="instance"></param>
        public void createMenu(ModEntry instance)
        {
            // get Generic Mod Config Menu's API (if it's installed)
            var configMenu = instance.Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
            if (configMenu is null)
                return;

            // register mod
            configMenu.Register(
                mod: instance.ModManifest,
                reset: () => instance.Config = new ModConfig(),
                save: () => instance.Helper.WriteConfig(instance.Config)
            );

            /// General travel skill settings header
            configMenu.AddSectionTitle(
                mod: instance.ModManifest,
                text: I18n.CfgSection_Travelskill,
                tooltip: null
            );

            // Steps per Exp
            configMenu.AddTextOption(
                mod: instance.ModManifest,
                name: I18n.CfgExpgain_Name,
                tooltip: I18n.CfgExpgain_Desc,
                getValue: () => StepsPerExp.ToString(),
                setValue: value => StepsPerExp = int.Parse(value),
                allowedValues: new string[] {"10", "25", "50", "100"},
                formatAllowedValue: displayExpGainValues
             );

            // Level movespeed bonus
            configMenu.AddNumberOption(
                mod: instance.ModManifest,
                name: I18n.CfgLevelmovespeed_Name,
                tooltip: I18n.CfgLevelmovespeed_Desc,
                getValue: () => LevelMovespeedBonus,
                setValue: value => LevelMovespeedBonus = value,
                min: 0f / 100f,
                max: 2f / 100f,
                interval: 0.05f / 100f,
                formatValue: displayAsPercentage
             );

            /// profession settings header
            configMenu.AddSectionTitle(
                mod: instance.ModManifest,
                text: I18n.CfgSection_Professions,
                tooltip: null
            );

            // Movespeed profession bonus
            configMenu.AddNumberOption(
                mod: instance.ModManifest,
                name: I18n.CfgMovespeedbonus_Name,
                tooltip: I18n.CfgMovespeedbonus_Desc,
                getValue: () => MovespeedProfessionBonus,
                setValue: value => MovespeedProfessionBonus = value,
                min: 0f / 100f,
                max: 10f / 100f,
                interval: 0.5f / 100f,
                formatValue: displayAsPercentage
             );

            // Sprint profession bonus
            configMenu.AddNumberOption(
                mod: instance.ModManifest,
                name: I18n.CfgSprintbonus_Name,
                tooltip: I18n.CfgSprintbonus_Desc,
                getValue: () => SprintMovespeedBonus,
                setValue: value => SprintMovespeedBonus = value,
                min: 0f / 100f,
                max: 30f / 100f,
                interval: 0.5f / 100f,
                formatValue: displayAsPercentage
             );

            // Restore stamina percentage
            configMenu.AddNumberOption(
                mod: instance.ModManifest,
                name: I18n.CfgRestorestamina_Name,
                tooltip: I18n.CfgRestorestamina_Desc,
                getValue: () => RestoreStaminaPercentage,
                setValue: value => RestoreStaminaPercentage = value,
                min: 0f / 100f,
                max: 2f / 100f,
                interval: 0.05f / 100f,
                formatValue: displayAsPercentage
             );

            // Totem reuse
            configMenu.AddNumberOption(
                mod: instance.ModManifest,
                name: I18n.CfgTotemreuse_Name,
                tooltip: I18n.CfgTotemreuse_Desc,
                getValue: () => TotemUseChance,
                setValue: value => TotemUseChance = value,
                min: 25f / 100f,
                max: 75f / 100f,
                interval: 5f / 100f,
                formatValue: displayAsPercentage
             );

            // TODO add options for cheap recipes/obelisks
        }

        private static string displayExpGainValues(string expgain_option)
        {
            switch (expgain_option)
            {
                case "10": return "10 (Fast)";
                case "25": return "25 (Normal)";
                case "50": return "50 (Slow)";
                case "100": return "100 (Very Slow)";
            }
            return "Something went wrong... :(";
        }

        public static string displayAsPercentage(float value)
        {
            return Math.Round(100f * value, 2).ToString() + "%";
        }

        

    }
}
