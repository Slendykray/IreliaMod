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

            string desc = "Trained in the ancient dances of her province, Irelia has adapted her art for war, using the graceful and carefully practiced movements to levitate a host of deadly blades.<color=#CCD3E0>" + Environment.NewLine + Environment.NewLine
             + "< ! > Bladesurge may help give you that extra reach for floating enemies." + Environment.NewLine + Environment.NewLine
             + "< ! > Vanguard's Edge can stun entire areas, but is also a great tool for digging yourself out of a pit of enemies." + Environment.NewLine + Environment.NewLine
             + "< ! > You may be invulnerable during Defiant Dance, but you are also stationary. Think carefully about your positioning." + Environment.NewLine + Environment.NewLine
             + "< ! > Never stop learning. There's always a form you don't know." + Environment.NewLine + Environment.NewLine;

            //string outro = "..and so he left, searching for a new identity.";
            //string outroFailure = "..and so he vanished, forever a blank slate.";

            string outro = "I really should’ve stretched first… Oh well!";
            string outroFailure = "O-ma?…";

            string lore = $"<style=cMono>>> PROCESSING QUERY...\r\n>> RESULTS RETURNED: 0\r\n \r\n> </style>searchmod archivalscan\r\n \r\n<style=cMono>>> PROCESSING QUERY: ARCHIVAL SCAN...\r\n>> PLEASE WAIT.\r\n>> RESULTS RETURNED: 1 ENTRY, LAST ACCESSED: 12010D AGO\r\n \r\n></style> view\r\n \r\n<style=cMono>>> DIGITIZING ENTRY...\r\n>> OUTPUTTING RESULT:</style>\r\n \r\nElegy of the Blade Dancer\r\n \r\nA land where blood waters the trees remembers\r\nThe tale ancestral voices carry across hill and valley and sea\r\nWinds whispering of <sub>a helpless girl an insurgent a bulwark</sub> a hero\r\nThe graceful whirling twirling swirling Blade Dancer\r\n \r\nWhere graves outnumber the farms and mills and wells and hills\r\nA land sundered and burnt and bled\r\nWhere she stood <sub>and danced</sub> and fought<sub> and danced</sub> and triumphed\r\nWhere farmer and carpenter and rancher and beggar and child\r\nStood with her in defiance\r\n \r\nThe winds still at her partner's approach\r\nSteps heavy, arms rigid, mind closed\r\nThe motions lurch - \r\n<nobr>                   </nobr>and jerk - \r\n<nobr>         </nobr>and falter - \r\nand halt. A clever turn and twist and twirl of hip and shoulder and arm\r\n<nobr><sup>                                                                                                    blade and blade and blade</sup></nobr>\r\nEnds the dance all too soon\r\n \r\nAnd so stood the protector of the land\r\nWhere she had lost had found had bled had killed and stood firm\r\nUntil she stood there no more\r\nThe dancer cannot stand still\r\n<nobr><sup>                       cannot be complacent cannot be rigid cannot be stagnant</sup></nobr>\r\nThis one wishes to dance alone\r\n \r\n<style=cMono>>> END OF RESULT\r\n \r\n></style> composemsg to ''C''\r\n \r\n<style=cMono>>> ENTER MESSAGE\r\n></style>\r\nHey, this is all that I can find. Only related records to it I can find are about some old clan from the east, wiped out centuries ago. Still nothing to do with our girl here, so I got squat. Go through your other sources, see if you can dig anything up on her that the databases won't show me.\r\n \r\n<style=cMono>></style> send\r\n \r\n<style=cMono>>> MESSAGE SENT.</style>";

            Language.Add(prefix + "NAME", "Bladedancer");
            Language.Add(prefix + "DESCRIPTION", desc);
            Language.Add(prefix + "SUBTITLE", "the Blade Dancer");
            Language.Add(prefix + "LORE", lore);
            Language.Add(prefix + "OUTRO_FLAVOR", outro);
            Language.Add(prefix + "OUTRO_FAILURE", outroFailure);

            #region Skins
            Language.Add(prefix + "MASTERY_SKIN_NAME", "Alternate");
            Language.Add(prefix + "GUARDIAN_SKIN_NAME", "Star Guardian");
            #endregion

            #region Passive
            Language.Add(prefix + "PASSIVE_STRIKE_NAME", "Ionian Fervor");
            Language.Add(prefix + "PASSIVE_STRIKE_DESCRIPTION", "Every fourth attack becomes a quick double attack.");

            Language.Add(prefix + "PASSIVE_SHURIKEN_NAME", "Ionian Shuriken");
            Language.Add(prefix + "PASSIVE_SHURIKEN_DESCRIPTION", $"Every third attack fire shuriken for {Tokens.DamageValueText(IreliaStaticValues.shurikenDamageCoefficient)}.");
            #endregion

            #region Primary
            Language.Add(prefix + "PRIMARY_SLASH_NAME", "Adapted Cut");
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
            Language.Add(prefix + "SPECIAL_SHIELD_DESCRIPTION", Tokens.StunningPrefix() + $"Form a defensive barrier of blades that makes you {Tokens.UtilityText("invulnerable")}. After {Tokens.UtilityText("3 seconds")}, the blades explode for up to {Tokens.DamageValueText(IreliaStaticValues.shieldDamageCoefficient)}. {Tokens.UtilityText("Press again to explode early")}.");
            #endregion

            #region Achievements
            Language.Add(Tokens.GetAchievementNameToken(IreliaMasteryAchievement.identifier), "Henry: Mastery");
            Language.Add(Tokens.GetAchievementDescriptionToken(IreliaMasteryAchievement.identifier), "As Henry, beat the game or obliterate on Monsoon.");
            #endregion
        }
    }
}
