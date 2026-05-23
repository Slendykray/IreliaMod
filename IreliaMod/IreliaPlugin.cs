using BepInEx;
using IreliaMod.Survivors.Irelia;
using R2API.Utils;
using RoR2;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AddressableAssets;

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

//rename this namespace
namespace IreliaMod
{
    //[BepInDependency("com.rune580.riskofoptions", BepInDependency.DependencyFlags.SoftDependency)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [BepInPlugin(MODUID, MODNAME, MODVERSION)]
    public class IreliaPlugin : BaseUnityPlugin
    {
        // if you do not change this, you are giving permission to deprecate the mod-
        //  please change the names to your own stuff, thanks
        //   this shouldn't even have to be said
        public const string MODUID = "com.slendykray.IreliaMod";
        public const string MODNAME = "IreliaMod";
        public const string MODVERSION = "1.0.0";

        // a prefix for name tokens to prevent conflicts- please capitalize all name tokens for convention
        public const string DEVELOPER_PREFIX = "SLENDYKRAY";

        public static IreliaPlugin instance;

        void Awake()
        {
            instance = this;

            //easy to use logger
            Log.Init(Logger);

            SetVFX();

            // used when you want to properly set up language folders
            Modules.Language.Init();

            // character initialization
            new IreliaSurvivor().Initialize();

            // make a content pack and add it. this has to be last
            new Modules.ContentPacks().Initialize();
        }

        void SetVFX()
        {
            GameObject prefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ElitePoison/HealingDisabledEffect.prefab").WaitForCompletion();

            //R2API.TempVisualEffectAPI.EffectCondition condition = body => body.healthComponent.combinedHealth < bodyInfo.damage * HenryStaticValues.dashDamageCoefficient;
            R2API.TempVisualEffectAPI.EffectCondition condition = body => body.HasBuff(IreliaBuffs.executionBuff);
            R2API.TempVisualEffectAPI.AddTemporaryVisualEffect(prefab, condition, useBestFitRadius: true);
        }
    }
}
