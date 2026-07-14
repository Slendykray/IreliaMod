using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

//using ShaderSwapper;

namespace IreliaMod.Survivors.Irelia.Components
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

        public IreliaAssets.Skins skinType = IreliaAssets.Skins.Default;

        private void Start()
        {
            //IreliaAssets._assetBundle.UpgradeStubbedShadersAsync();


            ModelLocator modelLocator = characterBody.GetComponent<ModelLocator>();
            if (modelLocator && modelLocator.modelTransform)
            {
                ModelSkinController skinController = modelLocator.modelTransform.GetComponent<ModelSkinController>();
                if (skinController)
                {
                    uint currentSkinIndex = characterBody.skinIndex;

                    SkinDef currentSkinDef = skinController.skins[currentSkinIndex];

                    if (currentSkinDef.nameToken == IreliaSurvivor.HENRY_PREFIX + "GUARDIAN_SKIN_NAME")
                    {
                        skinType = IreliaAssets.Skins.Guardian;
                    }
                }
            }

            IreliaAssets.SetEffects(skinType, gameObject);
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

            //if (attackNum == 3)
            //{
            //    sk.material = IreliaAssets.bladesGlowMat;
            //}
            //else
            //{
            //    sk.material = IreliaAssets.bladesDefMat;
            //}
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

                    if (body.healthComponent.combinedHealth <= characterBody.damage * IreliaStaticValues.dashDamageCoefficient)
                    {
                        //body.AddTimedBuff(IreliaBuffs.executionBuff, 5f);

                        //body.AddBuff(IreliaBuffs.executionBuff);

                        if (NetworkServer.active)
                        {




                            if (!body.gameObject.GetComponent<ExecutionMarkTracker>())
                            {
                                var tracker = body.gameObject.AddComponent<ExecutionMarkTracker>();
                                //tracker.skin = skinType;
                            }
                            else
                            {
                                var tracker = body.gameObject.GetComponent<ExecutionMarkTracker>();
                                tracker.stopwatch = 0f;
                            }

                        }


                    }
                    else
                    {
                        //if (NetworkServer.active)
                        //{
                         
                        //}

                        //if (body.HasBuff(IreliaBuffs.executionBuff))
                        //{
                        //    body.RemoveBuff(IreliaBuffs.executionBuff);
                        //}

                        if (body.gameObject.GetComponent<ExecutionMarkTracker>())
                        {
                            var tracker = body.gameObject.GetComponent<ExecutionMarkTracker>();
                            Destroy(tracker);
                        }

                    }
                }
            }    

        }


    }
}