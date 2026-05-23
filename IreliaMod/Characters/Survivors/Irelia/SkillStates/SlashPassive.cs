using IreliaMod.Modules.BaseStates;
using IreliaMod.Survivors.Irelia.Components;
using RoR2;
using UnityEngine;

namespace IreliaMod.Survivors.Irelia.SkillStates
{
    public class SlashPassive : BaseMeleeAttack
    {
        public override void OnEnter()
        {
            hitboxGroupName = "SwordGroup";

            damageType = DamageTypeCombo.GenericPrimary;
            damageCoefficient = IreliaStaticValues.swordDamageCoefficient;
            procCoefficient = 1f;
            pushForce = 300f;
            bonusForce = Vector3.zero;
            baseDuration = 0.6f;

            //0-1 multiplier of baseduration, used to time when the hitbox is out (usually based on the run time of the animation)
            //for example, if attackStartPercentTime is 0.5, the attack will start hitting halfway through the ability. if baseduration is 3 seconds, the attack will start happening at 1.5 seconds
            attackStartPercentTime = 0f;
            attackEndPercentTime = 0.4f;

            //this is the point at which the attack can be interrupted by itself, continuing a combo
            earlyExitPercentTime = 1f;

            hitStopDuration = 0.012f;
            attackRecoil = 0.5f;
            hitHopVelocity = 4f;

            //swingSoundString = "HenrySwordSwing";
            swingSoundString = "";
            hitSoundString = "";
            muzzleString = "SwingLeft";
            playbackRateParam = "Slash.playbackRate";
            swingEffectPrefab = IreliaAssets.swordSwingEffect;
            hitEffectPrefab = IreliaAssets.swordHitImpactEffect;


            impactSound = IreliaAssets.swordHitSoundEvent.index;


            base.OnEnter();
        }

        protected override void PlayAttackAnimation()
        {
            PlayCrossfade("FullBody, Override", "AttackPassiveHit", playbackRateParam, duration, 0.1f * duration);
        }

        protected override void PlaySwingEffect()
        {
            base.PlaySwingEffect();
        }

        protected override void OnHitEnemyAuthority()
        {

            characterBody.AddTimedBuff(IreliaBuffs.atkSpeedBuff, SlashCombo.buffDur, SlashCombo.maxStacks);

            base.OnHitEnemyAuthority();    
        }

        public override void OnExit()
        {
        
            base.OnExit();
        }

      
    }
}