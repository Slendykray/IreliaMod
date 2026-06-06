using RoR2;
using UnityEngine;
using IreliaMod.Modules;
using System;
using RoR2.Projectile;
using UnityEngine.AddressableAssets;
using IreliaMod.Survivors.Irelia.Components;
using System.Collections.Generic;

namespace IreliaMod.Survivors.Irelia
{
    public static class IreliaAssets
    {
        // particle effects
        public static GameObject swordSwingEffect;
        public static GameObject swordHitImpactEffect;

        public static GameObject bombExplosionEffect;

        // networked hit sounds
        public static NetworkSoundEventDef swordHitSoundEvent;

        //projectiles
        public static GameObject bombProjectilePrefab;

        private static AssetBundle _assetBundle;


        public static GameObject shieldEffect;
        public static GameObject bladeVFX;
        
        public static GameObject dashEffect;

        public static Material bladesGlowMat;
        public static Material bladesDefMat;

        public static GameObject edgeProjectilePrefab;

        public static GameObject edgeEffect;
        public static void Init(AssetBundle assetBundle)
        {

            _assetBundle = assetBundle;

            RiskOfOptions.ModSettingsManager.SetModIcon(_assetBundle.LoadAsset<Sprite>("texIreliaIcon"));



            swordHitSoundEvent = Content.CreateAndAddNetworkSoundEventDef("HenrySwordHit");
        

            //GameObject prefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ElitePoison/HealingDisabledEffect.prefab").WaitForCompletion();
            GameObject prefab = _assetBundle.LoadAsset<GameObject>("ExecutionMark");

            R2API.TempVisualEffectAPI.EffectCondition condition = body => body.HasBuff(IreliaBuffs.executionBuff);
            R2API.TempVisualEffectAPI.AddTemporaryVisualEffect(prefab, condition, useBestFitRadius: true);

            CreateEffects();

            //CreateProjectiles();
        }

        #region effects
        private static void CreateEffects()
        {
            CreateBombExplosionEffect();

            //swordSwingEffect = _assetBundle.LoadEffect("HenrySwordSwingEffect", true);
            //swordHitImpactEffect = _assetBundle.LoadEffect("ImpactHenrySlash");

            //swordSwingEffect = _assetBundle.LoadEffect("IreliaSlashAttack", parentToTransform: true, soundName: "HenrySwordHit");

            swordSwingEffect = _assetBundle.LoadEffect("IreliaSlashAttack", true);
            swordHitImpactEffect = _assetBundle.LoadEffect("IreliaHit");

            //bladeSwingEffect = _assetBundle.LoadEffect("IreliaSlashAttack", true);
            //bladeHitImpactEffect = _assetBundle.LoadEffect("IreliaHit");

            shieldEffect = _assetBundle.LoadAsset<GameObject>("Shield2");
            bladeVFX = _assetBundle.LoadAsset<GameObject>("BladeVFX");

            dashEffect = _assetBundle.LoadAsset<GameObject>("Dash2");

            bladesDefMat = _assetBundle.LoadAsset<Material>("matIreliaRor");
            bladesGlowMat = _assetBundle.LoadAsset<Material>("BladeGlowfMat");




            edgeProjectilePrefab = _assetBundle.LoadAsset<GameObject>("EdgeKnife");
            edgeProjectilePrefab.AddComponent<SpawnDiamond>();

            edgeEffect = _assetBundle.LoadAsset<GameObject>("Diamond");
            var knifeSpawn = edgeEffect.transform.Find("EdgeEffect/Irelia-Vanguard's Edge/KnifeSpawn").gameObject.AddComponent<IreliaKnifeSpawn>();
            knifeSpawn.knifeObj = _assetBundle.LoadAsset<GameObject>("Irelia-Knife");
            knifeSpawn.transformParent = knifeSpawn.transform.Find("Parent");

            List<Transform> start = new List<Transform>();
            List<Transform> end = new List<Transform>();
            Transform[] AllChildren = knifeSpawn.transform.GetComponentsInChildren<Transform>();

            foreach (Transform child in AllChildren)
            {
                if (child.name.Contains("Point"))
                {
                    if (child.parent.parent.name.Contains("Knife-Start") || child.parent.name.Contains("Knife-Start"))
                        start.Add(child);

                    if (child.parent.parent.name.Contains("Knife-End") || child.parent.name.Contains("Knife-End"))
                        end.Add(child);
                }    
            }
            knifeSpawn.startPoint = start.ToArray();
            knifeSpawn.endPoint = end.ToArray();
        }


        private static void CreateBombExplosionEffect()
        {
            bombExplosionEffect = _assetBundle.LoadEffect("BombExplosionEffect", "HenryBombExplosion");

            if (!bombExplosionEffect)
                return;

            ShakeEmitter shakeEmitter = bombExplosionEffect.AddComponent<ShakeEmitter>();
            shakeEmitter.amplitudeTimeDecay = true;
            shakeEmitter.duration = 0.5f;
            shakeEmitter.radius = 200f;
            shakeEmitter.scaleShakeRadiusWithLocalScale = false;

            shakeEmitter.wave = new Wave
            {
                amplitude = 1f,
                frequency = 40f,
                cycleOffset = 0f
            };

        }
        #endregion effects

        #region projectiles
        private static void CreateProjectiles()
        {
            CreateBombProjectile();
            Content.AddProjectilePrefab(bombProjectilePrefab);
        }

        private static void CreateBombProjectile()
        {
            //highly recommend setting up projectiles in editor, but this is a quick and dirty way to prototype if you want
            bombProjectilePrefab = Asset.CloneProjectilePrefab("CommandoGrenadeProjectile", "HenryBombProjectile");

            //remove their ProjectileImpactExplosion component and start from default values
            UnityEngine.Object.Destroy(bombProjectilePrefab.GetComponent<ProjectileImpactExplosion>());
            ProjectileImpactExplosion bombImpactExplosion = bombProjectilePrefab.AddComponent<ProjectileImpactExplosion>();
            
            bombImpactExplosion.blastRadius = 16f;
            bombImpactExplosion.blastDamageCoefficient = 1f;
            bombImpactExplosion.falloffModel = BlastAttack.FalloffModel.None;
            bombImpactExplosion.destroyOnEnemy = true;
            bombImpactExplosion.lifetime = 12f;
            bombImpactExplosion.impactEffect = bombExplosionEffect;
            bombImpactExplosion.lifetimeExpiredSound = Content.CreateAndAddNetworkSoundEventDef("HenryBombExplosion");
            bombImpactExplosion.timerAfterImpact = true;
            bombImpactExplosion.lifetimeAfterImpact = 0.1f;

            ProjectileController bombController = bombProjectilePrefab.GetComponent<ProjectileController>();

            if (_assetBundle.LoadAsset<GameObject>("HenryBombGhost") != null)
                bombController.ghostPrefab = _assetBundle.CreateProjectileGhostPrefab("HenryBombGhost");
            
            bombController.startSound = "";
        }
        #endregion projectiles
    }
}
