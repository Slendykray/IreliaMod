using EntityStates;
using HenryMod.Survivors.Henry;
using RoR2;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using EntityStates.Huntress;

namespace HenryMod.Survivors.Henry.SkillStates
{
    public class Edge : BaseSkillState
    {
        public static float duration = 5f;

        private Vector3 dashVector;

        private int originalLayer;

        private float dashSpeed = 12f;

        private OverlapAttack overlapAttack;

        private Vector3 forwardDirection;

        private float stopwatch;


        public override void OnEnter()
        {
            base.OnEnter();
            base.characterMotor.velocity.y = Mathf.Max(base.characterMotor.velocity.y, 0f);

            if (base.characterMotor && BackflipState.smallHopStrength != 0f)
            {
                base.characterMotor.velocity.y = BackflipState.smallHopStrength;
            }
            if (base.isAuthority && base.inputBank)
            {
                this.forwardDirection = -Vector3.ProjectOnPlane(base.inputBank.aimDirection, Vector3.up);
            }
            base.characterDirection.moveVector = -this.forwardDirection;

            //dashVector = inputBank.aimDirection;
            //this.originalLayer = base.gameObject.layer;


            //this.overlapAttack = base.InitMeleeOverlap(HenryStaticValues.dashDamageCoefficient, HenryAssets.swordHitImpactEffect, base.GetModelTransform(), "SwordGroup");

            //this.overlapAttack.damageType.damageSource = DamageSource.Secondary;

        }

       

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            float deltaTime = base.GetDeltaTime();
            this.stopwatch += deltaTime;

            if (base.characterMotor && base.characterDirection)
            {
                Vector3 velocity = base.characterMotor.velocity;
                Vector3 velocity2 = this.forwardDirection * (this.moveSpeedStat * Mathf.Lerp(BackflipState.initialSpeedCoefficient, BackflipState.finalSpeedCoefficient, this.stopwatch / BackflipState.duration));
                base.characterMotor.velocity = velocity2;
                base.characterMotor.velocity.y = velocity.y;
                base.characterMotor.moveDirection = this.forwardDirection;
            }
            //List<HurtBox> hitResults = new List<HurtBox>();

            //if (overlapAttack.Fire(hitResults))
            //{

            //    for (int i = 0; i < hitResults.Count; i++)
            //    {
            //        CharacterBody body = hitResults[i].healthComponent.body;

            //        if (body.HasBuff(HenryBuffs.executionBuff))
            //        {
            //            skillLocator.secondary.RunRecharge(skillLocator.secondary.cooldownRemaining);
            //            Log.Info("buff");
            //        }
            //        else
            //        {
            //            skillLocator.secondary.RunRecharge(skillLocator.secondary.cooldownRemaining);

            //            skillLocator.special.RunRecharge(1f);
            //            //skillLocator.special.cooldownOverride = skillLocator.special.cooldownRemaining - 1f;
            //            Log.Info("no buff");
            //        }              
            //    }
            //}

            //characterMotor.velocity = Vector3.zero;

            //characterDirection.forward = dashVector;

            //base.characterMotor.rootMotion += dashVector * (this.moveSpeedStat * dashSpeed * base.GetDeltaTime());

            //base.gameObject.layer = LayerIndex.GetAppropriateFakeLayerForTeam(base.teamComponent.teamIndex).intVal;
            //base.characterMotor.Motor.RebuildCollidableLayers();

            if (isAuthority && fixedAge >= duration)
            {
                outer.SetNextStateToMain();
                return;
            }
        }

        public override void OnExit()
        {
           
            base.OnExit();

            //base.characterMotor.velocity *= 0.1f;
            //SmallHop(characterMotor, 2f);

            //base.gameObject.layer = this.originalLayer;
            //base.characterMotor.Motor.RebuildCollidableLayers();
        }

   
    }
}