using EntityStates;
using IreliaMod.Survivors.Irelia;
using RoR2;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using EntityStates.Huntress;
using RoR2.Projectile;

namespace IreliaMod.Survivors.Irelia.SkillStates
{
    public class VanguardsEdge : BaseSkillState
    {
        private GameObject blades;

        private float maxDuration = 1.5f;

        private GameObject areaIndicatorInstance;
        private EffectManagerHelper _emh_areaIndicatorInstance;
        private Vector3 _cachedAreaIndicatorScale = Vector3.one;

        public override void OnEnter()
        {     
      
            base.OnEnter();

            blades = GetModelChildLocator().FindChild("Blades").gameObject;

            blades.SetActive(false);

            if (ArrowRain.areaIndicatorPrefab)
            {
                if (!EffectManager.ShouldUsePooledEffect(ArrowRain.areaIndicatorPrefab))
                {
                    this.areaIndicatorInstance = UnityEngine.Object.Instantiate<GameObject>(ArrowRain.areaIndicatorPrefab);
                }
                else
                {
                    this._emh_areaIndicatorInstance = EffectManager.GetAndActivatePooledEffect(ArrowRain.areaIndicatorPrefab, Vector3.zero, Quaternion.identity);
                    this.areaIndicatorInstance = this._emh_areaIndicatorInstance.gameObject;
                }
                if (this.areaIndicatorInstance != null)
                {
                    this._cachedAreaIndicatorScale.x = ArrowRain.arrowRainRadius;
                    this._cachedAreaIndicatorScale.y = ArrowRain.arrowRainRadius;
                    this._cachedAreaIndicatorScale.z = ArrowRain.arrowRainRadius;
                    this.areaIndicatorInstance.transform.localScale = this._cachedAreaIndicatorScale;
                }
            }

        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.characterDirection)
            {
                base.characterDirection.moveVector = base.GetAimRay().direction;
            }

            if (base.characterMotor)
            {
                base.characterMotor.velocity = Vector3.zero;
            }        

            if (base.isAuthority && base.inputBank)
            {
                if (base.fixedAge >= this.maxDuration || base.inputBank.skill1.justPressed || base.inputBank.skill3.justPressed)
                {
                    //this.HandlePrimaryAttack();

                    base.outer.SetNextStateToMain();
                }
            }
        }

        public override void Update()
        {
            base.Update();
            this.UpdateAreaIndicator();
        }


        private void UpdateAreaIndicator()
        {
            //LayerIndex.world.mask
            if (this.areaIndicatorInstance)
            {
                float maxDistance = 1000f;
                RaycastHit raycastHit;
                if (Physics.Raycast(base.GetAimRay(), out raycastHit, maxDistance, LayerIndex.CommonMasks.bullet))
                {
                    this.areaIndicatorInstance.transform.position = raycastHit.point;
                    this.areaIndicatorInstance.transform.up = raycastHit.normal;
                }
            }
        }

        private void HandlePrimaryAttack()
        {
            if (isAuthority)
            {
                Ray aimRay = base.GetAimRay();

                ProjectileManager.instance.FireProjectile(
                    IreliaAssets.edgeProjectilePrefab,
                    aimRay.origin,
                    Util.QuaternionSafeLookRotation(aimRay.direction),
                    gameObject,
                    characterBody.damage * IreliaStaticValues.edgeDamageCoefficient,
                    0f,
                    Util.CheckRoll(characterBody.crit, characterBody.master),
                    damageType: DamageSource.Special
                );
            }
             

 
        }


        public override void OnExit()
        {
            HandlePrimaryAttack();

            PlayCrossfade("FullBody, Override", "UtilityRecover", "Special.playbackRate", 1f, 0.05f);

            Util.PlaySound("Play_UtilityImpact", base.gameObject);

            blades.SetActive(true);

            if (this.areaIndicatorInstance)
            {
                if (this._emh_areaIndicatorInstance != null && this._emh_areaIndicatorInstance.OwningPool != null)
                {
                    this._emh_areaIndicatorInstance.OwningPool.ReturnObject(this._emh_areaIndicatorInstance);
                }
                else
                {
                    EntityState.Destroy(this.areaIndicatorInstance.gameObject);
                }
                this.areaIndicatorInstance = null;
                this._emh_areaIndicatorInstance = null;
            }

            base.OnExit();
        }

    }
}