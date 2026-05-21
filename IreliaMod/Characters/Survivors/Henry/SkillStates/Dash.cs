using EntityStates;
using HenryMod.Survivors.Henry;
using RoR2;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace HenryMod.Survivors.Henry.SkillStates
{
    public class Dash : BaseSkillState
    {
        public static float duration = 0.25f;

        private Vector3 dashVector;

        private int originalLayer;

        private float dashSpeed = 10f;

        private OverlapAttack overlapAttack;

      
        private bool kill;
        private int hitCount;
        public static int maxHit = 3;

        public override void OnEnter()
        {
            base.OnEnter();

            dashVector = inputBank.aimDirection;
            this.originalLayer = base.gameObject.layer;


            this.overlapAttack = base.InitMeleeOverlap(HenryStaticValues.dashDamageCoefficient, HenryAssets.swordHitImpactEffect, base.GetModelTransform(), "SwordGroup");

            this.overlapAttack.damageType.damageSource = DamageSource.Secondary;

            characterDirection.forward = dashVector;
            //GameObject dashEf = UnityEngine.Object.Instantiate(HenryAssets.dashEffect, characterBody.corePosition, Util.QuaternionSafeLookRotation(characterDirection.forward), transform);


            //delete vfx solve!!!!!!
            GameObject dashEf = UnityEngine.Object.Instantiate(HenryAssets.dashEffect, characterBody.corePosition, Util.QuaternionSafeLookRotation(dashVector), transform);

           
        }



        public override void FixedUpdate()
        {
            base.FixedUpdate();

            List<HurtBox> hitResults = new List<HurtBox>();
            if (overlapAttack.Fire(hitResults))
            {
                for (int i = 0; i < hitResults.Count; i++)
                {
                   
                    if (hitCount < maxHit)
                    {
                        hitCount++;

                        skillLocator.special.RunRecharge(1f);
                    }
                    CharacterBody body = hitResults[i].healthComponent.body;
           
                    if (!body.healthComponent.alive)
                    {
                        if (!kill)
                        {
                            kill = true;

                            skillLocator.secondary.RunRecharge(skillLocator.secondary.cooldownRemaining);
                        }
                   
                    }
                }
            }

            characterMotor.velocity = Vector3.zero;

            characterDirection.forward = dashVector;

            base.characterMotor.rootMotion += dashVector * (this.moveSpeedStat * dashSpeed * base.GetDeltaTime());

            base.gameObject.layer = LayerIndex.GetAppropriateFakeLayerForTeam(base.teamComponent.teamIndex).intVal;
            base.characterMotor.Motor.RebuildCollidableLayers();

            if (isAuthority && fixedAge >= duration)
            {
                outer.SetNextStateToMain();
                return;
            }
        }

        public override void OnExit()
        {
           
            base.OnExit();

            base.characterMotor.velocity *= 0.1f;
            SmallHop(characterMotor, 2f);

            base.gameObject.layer = this.originalLayer;
            base.characterMotor.Motor.RebuildCollidableLayers();

            //if (kill)
            //{
            //    skillLocator.secondary.RunRecharge(skillLocator.secondary.cooldownRemaining);
            //}

            //skillLocator.special.RunRecharge(1f * Mathf.Min(hitCount, 3));

        }

   
    }
}