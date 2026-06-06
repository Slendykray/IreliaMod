using System;
using IreliaMod.Modules;
using IreliaMod.Survivors.Irelia.Achievements;

namespace IreliaMod.Survivors.Irelia
{
    public static class IreliaTokens
    {
        public static void Init()
        {
            AddHenryTokens();

            ////uncomment this to spit out a lanuage file with all the above tokens that people can translate
            ////make sure you set Language.usingLanguageFolder and printingEnabled to true
            //Language.PrintOutput("Henry.txt");
            ////refer to guide on how to build and distribute your mod with the proper folders
        }

        public static void AddHenryTokens()
        {
            string prefix = IreliaSurvivor.HENRY_PREFIX;

            //string desc = "Henry is a skilled fighter who makes use of a wide arsenal of weaponry to take down his foes.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine
            // + "< ! > Sword is a good all-rounder while Boxing Gloves are better for laying a beatdown on more powerful foes." + Environment.NewLine + Environment.NewLine
            // + "< ! > Pistol is a powerful anti air, with its low cooldown and high damage." + Environment.NewLine + Environment.NewLine
            // + "< ! > Roll has a lingering armor buff that helps to use it aggressively." + Environment.NewLine + Environment.NewLine
            // + "< ! > Bomb can be used to wipe crowds with ease." + Environment.NewLine + Environment.NewLine;

            string desc = "";

            //string outro = "..and so she left, searching for a new identity.";
            //string outroFailure = "..and so he vanished, forever a blank slate.";

            string outro = "..femboys.";
            string outroFailure = "..tomboys.";

            string lore = "";

            Language.Add(prefix + "NAME", "Bladedancer");
            Language.Add(prefix + "DESCRIPTION", desc);
            Language.Add(prefix + "SUBTITLE", "the Blade Dancer");
            Language.Add(prefix + "LORE", lore);
            Language.Add(prefix + "OUTRO_FLAVOR", outro);
            Language.Add(prefix + "OUTRO_FAILURE", outroFailure);

            #region Skins
            Language.Add(prefix + "MASTERY_SKIN_NAME", "Alternate");
            #endregion

            #region Passive
            Language.Add(prefix + "PASSIVE_NAME", "Bladedancer passive");
            Language.Add(prefix + "PASSIVE_DESCRIPTION", "Every fourth attack becomes a quick double attack.");
            #endregion

            #region Primary
            Language.Add(prefix + "PRIMARY_SLASH_NAME", "Slash");
            Language.Add(prefix + "PRIMARY_SLASH_DESCRIPTION", Tokens.agilePrefix + $"Attack with 6 blades, for {Tokens.DamageValueText(IreliaStaticValues.swordDamageCoefficient)}. Attacking repeatedly {Tokens.UtilityText("increases attack speed")} by {Tokens.UtilityText(SkillStates.SlashCombo.buffCoefficient * 100f + "%")} for {Tokens.UtilityText(SkillStates.SlashCombo.buffDur + " seconds")}.");
            #endregion

            #region Secondary
            Language.Add(prefix + "SECONDARY_DASH_NAME", "Bladesurge");
            Language.Add(prefix + "SECONDARY_DASH_DESCRIPTION", Tokens.agilePrefix + $"Dash forward, for {Tokens.DamageValueText(IreliaStaticValues.dashDamageCoefficient)}. Hitting enemy {Tokens.UtilityText("lowers Special cooldown by 1s, up to 3s")}. Eliminations {Tokens.UtilityText("reset the dash")}. Enemies are {Tokens.UtilityText("marked")} when within execution threshold.");
            #endregion

            #region Utility
            Language.Add(prefix + "UTILITY_EDGE_NAME", "Vanguard's Edge");
            Language.Add(prefix + "UTILITY_EDGE_DESCRIPTION", Tokens.StunningPrefix() + $"Fly backwards, then release a diamond-shaped volley of blades for {Tokens.DamageValueText(IreliaStaticValues.edgeDamageCoefficient)}.");
            #endregion

            #region Special
            Language.Add(prefix + "SPECIAL_SHIELD_NAME", "Defiant Dance");
            Language.Add(prefix + "SPECIAL_SHIELD_DESCRIPTION", Tokens.StunningPrefix() + $"Form a defensive barrier of blades that makes you {Tokens.UtilityText("invulnerable")}. After {Tokens.UtilityText("3 seconds")}, the blades explode for {Tokens.DamageValueText(IreliaStaticValues.shieldDamageCoefficient)}.");
            #endregion

            #region Achievements
            Language.Add(Tokens.GetAchievementNameToken(IreliaMasteryAchievement.identifier), "Henry: Mastery");
            Language.Add(Tokens.GetAchievementDescriptionToken(IreliaMasteryAchievement.identifier), "As Henry, beat the game or obliterate on Monsoon.");
            #endregion
        }
    }
}
