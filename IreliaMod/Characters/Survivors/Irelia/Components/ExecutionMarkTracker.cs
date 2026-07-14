using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace IreliaMod.Survivors.Irelia.Components
{
    public class ExecutionMarkTracker : MonoBehaviour
    {
        public float stopwatch;
        private float duration = 5f;

        private CharacterBody characterBody;

        private GameObject activeMarkInstance;

        private void FixedUpdate()
        {
            stopwatch += Time.fixedDeltaTime;

            if (stopwatch >= duration)
            {
                Destroy(this);
            }

            if (!characterBody.healthComponent.alive)
            {
                Destroy(this);
            }

            activeMarkInstance.transform.position = characterBody.corePosition;
        }

      

        private void Awake()
        {
            characterBody = transform.GetComponent<CharacterBody>();

            SpawnMark();    
        }

        private void SpawnMark()
        {
            if (activeMarkInstance != null) return;

            GameObject prefab = IreliaAssets.executionMark;
            if (prefab == null) return;

            activeMarkInstance = UnityEngine.Object.Instantiate(prefab, characterBody.corePosition, transform.rotation, transform);
        }

        private void DespawnMark()
        {
            if (activeMarkInstance != null)
            {
                Destroy(activeMarkInstance);
                activeMarkInstance = null;
            }
        }

        private void OnDestroy()
        {
            DespawnMark();
        }
    }
}