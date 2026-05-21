using EntityStates;
using HenryMod.Survivors.Henry;
using RoR2;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using EntityStates.Huntress;

namespace HenryMod.Survivors.Henry.SkillStates
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