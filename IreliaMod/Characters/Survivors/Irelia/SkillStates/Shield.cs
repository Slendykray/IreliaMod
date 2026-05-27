using BepHookGen;
using EntityStates;
using IreliaMod.Modules.BaseStates;
using IreliaMod.Survivors.Irelia;
using IreliaMod.Survivors.Irelia.Components;
using RoR2;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace IreliaMod.Survivors.Irelia.SkillStates
{
    public class Shield : BaseTimedSkillState
    {

        public override float TimedBaseDuration => 3f;
        public override float TimedBaseCastStartPercentTime => 1f;



        //private float duration = 3f;

        private float slamRadius = 25f;

        private GameObject vfxInstance;  

        private Transform slamIndicatorInstance;

        private GameObject blades;

        public override void OnEnter()
        {
            base.OnEnter();
            vfxInstance = UnityEngine.Object.Instantiate(IreliaAssets.shieldEffect, transform);
            vfxInstance.transform.position = GetModelChildLocator().FindChild("ShieldPos").position;

            characterBody.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, duration);

            //PlayAnimation("FullBody, Override", "Special", "Special.playbackRate", duration);

            PlayCrossfade("FullBody, Override", "Special", "Special.playbackRate", duration, 0.05f);

            blades = GetModelChildLocator().FindChild("Blades").gameObject;

            blades.SetActive(false);

            Util.PlaySound("Play_SpecialCharge", gameObject);
        }

        protected override void InitDurationValues()
        {
            //duration = TimedBaseDuration / attackSpeedStat;
            duration = TimedBaseDuration;
            this.castStartTime = TimedBaseCastStartPercentTime * duration;
            this.castEndTime = TimedBaseCastEndPercentTime * duration;
        }

        protected override void OnCastEnter()
        {
            Fire();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();


            if (isAuthority && fixedAge >= duration)
            {
                outer.SetNextStateToMain();
                return;
            }
        }

        private void Fire()
        {
            Util.PlaySound("Play_SpecialProjectilePop", gameObject);

            UnityEngine.Object.Destroy(vfxInstance);
            vfxInstance = null;

            if (!this.slamIndicatorInstance) this.CreateIndicator();

            List<HurtBox> HurtBoxes = new List<HurtBox>();
            HurtBoxes = new SphereSearch
            {
                radius = slamRadius,
                mask = LayerIndex.entityPrecise.mask,
                origin = transform.position
            }.RefreshCandidates().FilterCandidatesByHurtBoxTeam(TeamMask.GetEnemyTeams(base.teamComponent.teamIndex)).FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes().ToList();

            foreach (HurtBox hurtbox in HurtBoxes)
            {
                GameObject blade = UnityEngine.Object.Instantiate<GameObject>(IreliaAssets.bladeVFX, characterBody.corePosition, Quaternion.identity);
                //network sync shiitttt!!!!!!!!
                //NetworkIdentity n = blade.AddComponent<NetworkIdentity>();
                //spawn network id
                BladeHoming homing = blade.AddComponent<BladeHoming>();
                homing.pos = hurtbox.healthComponent.body.corePosition;
                //homing.pos = hurtbox.transform.position;
                UnityEngine.Object.Destroy(blade, 0.5f);
                


                DamageInfo damageInfo = new DamageInfo();
                damageInfo.damage = this.damageStat * IreliaStaticValues.shieldDamageCoefficient;
                damageInfo.attacker = base.gameObject;
                damageInfo.inflictor = base.gameObject;
                damageInfo.force = Vector3.zero;
                damageInfo.crit = base.RollCrit();
                damageInfo.procCoefficient = 1f;
                damageInfo.position = hurtbox.gameObject.transform.position;
                damageInfo.damageType = DamageType.Stun1s;

                hurtbox.healthComponent.TakeDamage(damageInfo);
                GlobalEventManager.instance.OnHitEnemy(damageInfo, hurtbox.healthComponent.gameObject);
                GlobalEventManager.instance.OnHitAll(damageInfo, hurtbox.healthComponent.gameObject);

                GameObject hitEffectPrefab = IreliaAssets.swordHitImpactEffect;
                if (hitEffectPrefab)
                {
                    EffectManager.SpawnEffect(hitEffectPrefab, new EffectData
                    {
                        origin = hurtbox.healthComponent.gameObject.transform.position,
                        rotation = Quaternion.identity,
                        networkSoundEventIndex = IreliaAssets.swordHitSoundEvent.index
                    }, true);
                }

            }
        }

        private void CreateIndicator()
        {
            if (EntityStates.Huntress.ArrowRain.areaIndicatorPrefab)
            {
                this.slamIndicatorInstance = UnityEngine.Object.Instantiate<GameObject>(EntityStates.Huntress.ArrowRain.areaIndicatorPrefab).transform;
                this.slamIndicatorInstance.localScale = Vector3.one * slamRadius;
                this.slamIndicatorInstance.transform.position = transform.position;
            }
        }

        public override void OnExit()
        {
            PlayCrossfade("Gesture, Override", "SpecialRecover", "Special.playbackRate", duration, 0.05f);
            //PlayAnimation("Gesture, Override", "SpecialRecover", "Special.playbackRate", duration);

            blades.SetActive(true);

            if (this.slamIndicatorInstance) UnityEngine.Object.Destroy(this.slamIndicatorInstance.gameObject, 0.5f);

            base.OnExit();

        }

   
    }
}