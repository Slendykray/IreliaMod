using RoR2;
using UnityEngine;
using IreliaMod.Modules;
using System;
using RoR2.Projectile;
using UnityEngine.AddressableAssets;
using IreliaMod.Survivors.Irelia.Components;
using System.Collections.Generic;
using R2API;

namespace IreliaMod.Survivors.Irelia
{
    public static class IreliaAssets
    {


        private static AssetBundle _assetBundle;

        public static NetworkSoundEventDef swordHitSoundEvent;

        //public static Material bladesGlowMat;
        //public static Material bladesDefMat;


        //public static GameObject swordSwingEffect;
        //public static GameObject swordHitImpactEffect;

        //public static GameObject shieldEffect;
        //public static GameObject bladeVFX;
        
        //public static GameObject dashEffect;

        //public static GameObject edgeProjectilePrefab;

        //public static GameObject edgeEffect;

        //public static GameObject shurikenProjectilePrefab;



        public static void Init(AssetBundle assetBundle)
        {

            _assetBundle = assetBundle;

            RiskOfOptions.ModSettingsManager.SetModIcon(_assetBundle.LoadAsset<Sprite>("texIreliaIcon"));

            swordHitSoundEvent = Content.CreateAndAddNetworkSoundEventDef("HenrySwordHit");

            //GameObject prefab = _assetBundle.LoadAsset<GameObject>("ExecutionMark");

            //R2API.TempVisualEffectAPI.EffectCondition condition = body => body.HasBuff(IreliaBuffs.executionBuff);
            //R2API.TempVisualEffectAPI.AddTemporaryVisualEffect(prefab, condition, useBestFitRadius: true);

            CreateEffects();

            //CreateEffects();

            //CreateProjectiles();

        }

        //#region effects
        //public static void CreateEffects()
        //{



        //    //bladesDefMat = _assetBundle.LoadAsset<Material>("matIreliaRor");
        //    //bladesGlowMat = _assetBundle.LoadAsset<Material>("BladeGlowfMat");



        //    GameObject prefab = _assetBundle.LoadAsset<GameObject>("ExecutionMark");

        //    R2API.TempVisualEffectAPI.EffectCondition condition = body => body.HasBuff(IreliaBuffs.executionBuff);
        //    R2API.TempVisualEffectAPI.AddTemporaryVisualEffect(prefab, condition, useBestFitRadius: true);



        //    swordSwingEffect = _assetBundle.LoadEffect("IreliaSlashAttack", true);
        //    swordHitImpactEffect = _assetBundle.LoadEffect("IreliaHit");

        //    shieldEffect = _assetBundle.LoadAsset<GameObject>("Shield2");
        //    bladeVFX = _assetBundle.LoadAsset<GameObject>("BladeVFX");

        //    dashEffect = _assetBundle.LoadAsset<GameObject>("Dash2");


        //    shurikenProjectilePrefab = _assetBundle.LoadAsset<GameObject>("Shuriken");

        //    PrefabAPI.RegisterNetworkPrefab(shurikenProjectilePrefab);
        //    Content.AddProjectilePrefab(shurikenProjectilePrefab);


        //    edgeProjectilePrefab = _assetBundle.LoadAsset<GameObject>("EdgeKnife");
        //    edgeProjectilePrefab.AddComponent<SpawnDiamond>();

        //    PrefabAPI.RegisterNetworkPrefab(edgeProjectilePrefab);
        //    Content.AddProjectilePrefab(edgeProjectilePrefab);


        //    edgeEffect = _assetBundle.LoadAsset<GameObject>("Diamond");
        //    var knifeSpawn = edgeEffect.transform.Find("EdgeEffect/Irelia-Vanguard's Edge/KnifeSpawn").gameObject.AddComponent<IreliaKnifeSpawn>();
        //    knifeSpawn.knifeObj = _assetBundle.LoadAsset<GameObject>("Irelia-Knife");
        //    knifeSpawn.transformParent = knifeSpawn.transform.Find("Parent");

        //    List<Transform> start = new List<Transform>();
        //    List<Transform> end = new List<Transform>();
        //    Transform[] AllChildren = knifeSpawn.transform.GetComponentsInChildren<Transform>();

        //    foreach (Transform child in AllChildren)
        //    {
        //        if (child.name.Contains("Point"))
        //        {
        //            if (child.parent.parent.name.Contains("Knife-Start") || child.parent.name.Contains("Knife-Start"))
        //                start.Add(child);

        //            if (child.parent.parent.name.Contains("Knife-End") || child.parent.name.Contains("Knife-End"))
        //                end.Add(child);
        //        }    
        //    }
        //    knifeSpawn.startPoint = start.ToArray();
        //    knifeSpawn.endPoint = end.ToArray();

        //    PrefabAPI.RegisterNetworkPrefab(edgeEffect);
        //    Content.AddProjectilePrefab(edgeEffect);
        //}


        //#endregion effects

        public enum Skins
        {
            Default = 0,
            Guardian = 1,
        }

        private static Dictionary<Skins, GameObject> _swordSwingEffects = new Dictionary<Skins, GameObject>();
        private static Dictionary<Skins, GameObject> _swordHitImpactEffects = new Dictionary<Skins, GameObject>();
        private static Dictionary<Skins, GameObject> _shieldEffects = new Dictionary<Skins, GameObject>();
        private static Dictionary<Skins, GameObject> _bladeVFXs = new Dictionary<Skins, GameObject>();
        private static Dictionary<Skins, GameObject> _dashEffects = new Dictionary<Skins, GameObject>();
        private static Dictionary<Skins, GameObject> _shurikenProjectilePrefabs = new Dictionary<Skins, GameObject>();
        private static Dictionary<Skins, GameObject> _edgeProjectilePrefabs = new Dictionary<Skins, GameObject>();
        private static Dictionary<Skins, GameObject> _edgeEffects = new Dictionary<Skins, GameObject>();
        private static Dictionary<Skins, GameObject> _executionMarks = new Dictionary<Skins, GameObject>();

        public static GameObject swordSwingEffect { get; private set; }
        public static GameObject swordHitImpactEffect { get; private set; }
        public static GameObject shieldEffect { get; private set; }
        public static GameObject bladeVFX { get; private set; }
        public static GameObject dashEffect { get; private set; }
        public static GameObject shurikenProjectilePrefab { get; private set; }
        public static GameObject edgeProjectilePrefab { get; private set; }
        public static GameObject edgeEffect { get; private set; }
        public static GameObject executionMark { get; private set; }

        private static readonly Dictionary<Skins, string> SkinAssetSuffix = new Dictionary<Skins, string>
        {
            { Skins.Default,  "" },
            { Skins.Guardian, "_Guardian" },
        };

        private static GameObject LoadSkinAsset(string baseName, Skins skin)
        {
            string suffix = SkinAssetSuffix.TryGetValue(skin, out var s) ? s : "";
            var asset = _assetBundle.LoadAsset<GameObject>(baseName + suffix);
            return asset != null ? asset : _assetBundle.LoadAsset<GameObject>(baseName);
        }

        private static GameObject LoadSkinEffect(string baseName, Skins skin, bool someFlag = false)
        {
            string suffix = SkinAssetSuffix.TryGetValue(skin, out var s) ? s : "";
            var effect = _assetBundle.LoadEffect(baseName + suffix, someFlag);
            return effect != null ? effect : _assetBundle.LoadEffect(baseName, someFlag);
        }

        private static void CreateEffects()
        {
            foreach (Skins skin in Enum.GetValues(typeof(Skins)))
            {

                //Skins currentSkin = skin;
                //var executionMark = LoadSkinAsset("ExecutionMark", skin);

                //R2API.TempVisualEffectAPI.EffectCondition condition = body =>
                //{
                //    var tracker = body.gameObject.GetComponent<ExecutionMarkTracker>();
                //    return tracker && tracker.skin == currentSkin;
                //};

                ////R2API.TempVisualEffectAPI.EffectCondition condition = body => body.HasBuff(IreliaBuffs.executionBuff);
                //R2API.TempVisualEffectAPI.AddTemporaryVisualEffect(executionMark, condition, useBestFitRadius: true);
                _executionMarks[skin] = LoadSkinAsset("ExecutionMark2", skin);

                _swordSwingEffects[skin] = LoadSkinEffect("IreliaSlashAttack", skin, true);
                _swordHitImpactEffects[skin] = LoadSkinEffect("IreliaHit", skin);

                _shieldEffects[skin] = LoadSkinAsset("Shield2", skin);
                _bladeVFXs[skin] = LoadSkinAsset("BladeVFX", skin);
                _dashEffects[skin] = LoadSkinAsset("Dash2", skin);

                var shuriken = LoadSkinAsset("Shuriken", skin);
                PrefabAPI.RegisterNetworkPrefab(shuriken);
                Content.AddProjectilePrefab(shuriken);
                _shurikenProjectilePrefabs[skin] = shuriken;

                var edgeProjectile = LoadSkinAsset("EdgeKnife", skin);
                edgeProjectile.AddComponent<SpawnDiamond>();
                PrefabAPI.RegisterNetworkPrefab(edgeProjectile);
                Content.AddProjectilePrefab(edgeProjectile);
                _edgeProjectilePrefabs[skin] = edgeProjectile;

                var edgeEffectObj = LoadSkinAsset("Diamond", skin);
                var knifeSpawn = edgeEffectObj.transform
                    .Find("EdgeEffect/Irelia-Vanguard's Edge/KnifeSpawn")
                    .gameObject.AddComponent<IreliaKnifeSpawn>();

                knifeSpawn.knifeObj = LoadSkinAsset("Irelia-Knife", skin);
                knifeSpawn.transformParent = knifeSpawn.transform.Find("Parent");

                var start = new List<Transform>();
                var end = new List<Transform>();
                foreach (Transform child in knifeSpawn.GetComponentsInChildren<Transform>())
                {
                    if (!child.name.Contains("Point")) continue;
                    if (child.parent.parent.name.Contains("Knife-Start") || child.parent.name.Contains("Knife-Start"))
                        start.Add(child);
                    if (child.parent.parent.name.Contains("Knife-End") || child.parent.name.Contains("Knife-End"))
                        end.Add(child);
                }
                knifeSpawn.startPoint = start.ToArray();
                knifeSpawn.endPoint = end.ToArray();

                PrefabAPI.RegisterNetworkPrefab(edgeEffectObj);
                Content.AddProjectilePrefab(edgeEffectObj);
                _edgeEffects[skin] = edgeEffectObj;
            }

            SetEffects(Skins.Default, null);
        }

        public static void SetEffects(Skins skin, GameObject source)
        {
            swordSwingEffect = _swordSwingEffects[skin];
            swordHitImpactEffect = _swordHitImpactEffects[skin];
            shieldEffect = _shieldEffects[skin];
            bladeVFX = _bladeVFXs[skin];
            dashEffect = _dashEffects[skin];
            shurikenProjectilePrefab = _shurikenProjectilePrefabs[skin];
            edgeProjectilePrefab = _edgeProjectilePrefabs[skin];
            edgeEffect = _edgeEffects[skin];
            executionMark = _executionMarks[skin];

            if (IreliaConfig.altSfx.Value)
            {
                AkSoundEngine.SetSwitch("IreliaSkin", skin.ToString(), source);
            }
              
   
        }


    }
}
