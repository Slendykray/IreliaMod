using RoR2;
using UnityEngine;

namespace HenryMod.Survivors.Henry
{
    public static class HenryBuffs
    {
        // armor buff gained during roll
        public static BuffDef armorBuff;

        public static BuffDef executionBuff;

        public static BuffDef atkSpeedBuff;

        public static void Init(AssetBundle assetBundle)
        {
            armorBuff = Modules.Content.CreateAndAddBuff("HenryArmorBuff",
                LegacyResourcesAPI.Load<BuffDef>("BuffDefs/HiddenInvincibility").iconSprite,
                Color.white,
                false,
                false);


            executionBuff = Modules.Content.CreateAndAddBuff("IreliaExecutionBuff",
             null,
             Color.white,
             false,
             false);

            atkSpeedBuff = Modules.Content.CreateAndAddBuff("IreliaAtkSpeedBuff",
             LegacyResourcesAPI.Load<BuffDef>("BuffDefs/AttackSpeedOnCrit").iconSprite,
             Color.white,
             true,
             false);

        }
    }
}
