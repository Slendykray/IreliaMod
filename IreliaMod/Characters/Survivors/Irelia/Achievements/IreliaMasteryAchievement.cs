using RoR2;
using IreliaMod.Modules.Achievements;

namespace IreliaMod.Survivors.Irelia.Achievements
{
    //automatically creates language tokens "ACHIEVMENT_{identifier.ToUpper()}_NAME" and "ACHIEVMENT_{identifier.ToUpper()}_DESCRIPTION" 
    [RegisterAchievement(identifier, unlockableIdentifier, null, 10, null)]
    public class IreliaMasteryAchievement : BaseMasteryAchievement
    {
        public const string identifier = IreliaSurvivor.HENRY_PREFIX + "masteryAchievement";
        public const string unlockableIdentifier = IreliaSurvivor.HENRY_PREFIX + "masteryUnlockable";

        public override string RequiredCharacterBody => IreliaSurvivor.instance.bodyName;

        //difficulty coeff 3 is monsoon. 3.5 is typhoon for grandmastery skins
        public override float RequiredDifficultyCoefficient => 3;
    }
}