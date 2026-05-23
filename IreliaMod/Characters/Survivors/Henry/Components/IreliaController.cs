using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HenryMod.Survivors.Henry.Components
{
    internal class IreliaController : MonoBehaviour
    {
        public int attackNum;

        private float trackerUpdateStopwatch;
        public float trackerUpdateFrequency = 10f;

        private CharacterBody characterBody;

        //List<HurtBox> targetList = new List<HurtBox>();

        public float maxTrackingDistance = 100f;

        private readonly BullseyeSearch search = new BullseyeSearch();

        private TeamComponent teamComponent;

        private SkinnedMeshRenderer sk;
        private void Awake()
        {
            this.characterBody = base.GetComponent<CharacterBody>();
            this.teamComponent = base.GetComponent<TeamComponent>();

            sk = GetChildLocator().FindChild("Blades").GetComponent<SkinnedMeshRenderer>();
        }

        ChildLocator GetChildLocator()
        {
            var body = GetComponent<CharacterBody>();

            var modelTransform = body.modelLocator.modelTransform;

            var childLocator = modelTransform.GetComponent<ChildLocator>();

            return childLocator;
        }

        private void FixedUpdate()
        {
            this.MyFixedUpdate(Time.fixedDeltaTime);
        }

        private void MyFixedUpdate(float deltaTime)
        {
            this.trackerUpdateStopwatch += deltaTime;
            if (this.trackerUpdateStopwatch >= 1f / this.trackerUpdateFrequency)
            {
                this.trackerUpdateStopwatch -= 1f / this.trackerUpdateFrequency;

                SearchTargets();
            }

            if (attackNum == 3)
            {
                sk.material = HenryAssets.bladesGlowMat;
            }
            else
            {
                sk.material = HenryAssets.bladesDefMat;
            }
        }

        private  void SearchTargets()
        {
            //targetList.Clear();

            this.search.teamMaskFilter = TeamMask.all;
            this.search.teamMaskFilter.RemoveTeam(this.teamComponent.teamIndex);
            this.search.filterByLoS = true;
            //this.search.searchOrigin = aimRay.origin;
            this.search.searchOrigin = characterBody.corePosition;
            //this.search.searchDirection = aimRay.direction;
            this.search.sortMode = BullseyeSearch.SortMode.None;
            this.search.maxDistanceFilter = this.maxTrackingDistance;
            //this.search.maxAngleFilter = this.maxTrackingAngle;
            this.search.RefreshCandidates();
            this.search.FilterOutGameObject(base.gameObject);

          
            foreach (HurtBox hurtbox in search.GetResults())
            {
                

                CharacterBody body = hurtbox.healthComponent.body;
                if (body)
                {

                    if (body.healthComponent.combinedHealth < characterBody.damage * HenryStaticValues.dashDamageCoefficient)
                    {
                        //targetList.Add(hurtbox);
                        if (!body.HasBuff(HenryBuffs.executionBuff))
                        {
                            body.AddBuff(HenryBuffs.executionBuff);
                        }
                    }
                    else
                    {
                        if (body.HasBuff(HenryBuffs.executionBuff))
                        {
                            body.RemoveBuff(HenryBuffs.executionBuff);
                        }
                    }
                }
            }    

        }


    }
}