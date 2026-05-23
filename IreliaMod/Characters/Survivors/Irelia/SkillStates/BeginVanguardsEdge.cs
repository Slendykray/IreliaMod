using EntityStates;
using IreliaMod.Survivors.Irelia;
using RoR2;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using EntityStates.Huntress;

namespace IreliaMod.Survivors.Irelia.SkillStates
{
    public class BeginVanguardsEdge : BaseBeginArrowBarrage
    {
        public override void OnEnter()
        {     
            basePrepDuration = 0.25f;
            blinkDuration = 0.3f;
            jumpCoefficient = 3f;
            blinkVector = new Vector3(0, 1, 0);
            base.OnEnter();
          
        }
        public override EntityState InstantiateNextState()
        {
            return new ArrowRain();
        }

    }
}