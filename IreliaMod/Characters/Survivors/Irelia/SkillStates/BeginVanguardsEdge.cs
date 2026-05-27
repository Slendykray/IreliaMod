using EntityStates;
using IreliaMod.Survivors.Irelia;
using RoR2;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using EntityStates.Huntress;

namespace IreliaMod.Survivors.Irelia.SkillStates
{
    public class BeginVanguardsEdge : BaseSkillState
    {
        private float blinkDuration = 0.3f;

        public Vector3 blinkVector = new Vector3(0, 1, 0);

        private Vector3 worldBlinkVector;

        private float prepDuration;

        public float basePrepDuration = 0.2f;

        private bool beginBlink;

        public float jumpCoefficient = 3f;
        public override void OnEnter()
        {     
            //basePrepDuration = 0.25f;
            //blinkDuration = 0.3f;
            //jumpCoefficient = 3f;
            //blinkVector = new Vector3(0, 1, 0);
            base.OnEnter();
            //base.characterDirection.moveVector = base.GetAimRay().direction;

            this.prepDuration = this.basePrepDuration / this.attackSpeedStat;

            Vector3 direction = base.GetAimRay().direction;
            direction.y = 0f;
            direction.Normalize();
            Vector3 up = Vector3.up;
            this.worldBlinkVector = Matrix4x4.TRS(base.transform.position, Util.QuaternionSafeLookRotation(direction, up), new Vector3(1f, 1f, 1f)).MultiplyPoint3x4(this.blinkVector) - base.transform.position;
            this.worldBlinkVector.Normalize();

            PlayCrossfade("FullBody, Override", "Utility", "Special.playbackRate", 1f, 0.05f);

            Util.PlaySound("Play_IntoUtility", base.gameObject);

        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.fixedAge >= this.prepDuration && !this.beginBlink)
            {
                this.beginBlink = true;         
            }
            if (this.beginBlink && base.characterMotor)
            {
                base.characterMotor.velocity = Vector3.zero;
                base.characterMotor.rootMotion += this.worldBlinkVector * (base.characterBody.jumpPower * this.jumpCoefficient * base.GetDeltaTime());
            }
            if (base.fixedAge >= this.blinkDuration + this.prepDuration && base.isAuthority)
            {
                this.outer.SetNextState(new VanguardsEdge());
            }
        }

    }
}