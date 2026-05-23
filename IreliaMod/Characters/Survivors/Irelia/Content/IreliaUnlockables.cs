using IreliaMod.Survivors.Irelia.Achievements;
using RoR2;
using UnityEngine;

namespace IreliaMod.Survivors.Irelia
{
    public static class IreliaUnlockables
    {
        public static UnlockableDef characterUnlockableDef = null;
        public static UnlockableDef masterySkinUnlockableDef = null;

        public static void Init()
        {
            masterySkinUnlockableDef = Modules.Content.CreateAndAddUnlockbleDef(
                IreliaMasteryAchievement.unlockableIdentifier,
                Modules.Tokens.GetAchievementNameToken(IreliaMasteryAchievement.identifier),
                IreliaSurvivor.instance.assetBundle.LoadAsset<Sprite>("texMasteryAchievement"));
        }
    }
}
