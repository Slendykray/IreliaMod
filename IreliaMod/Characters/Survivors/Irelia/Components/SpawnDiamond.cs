using RoR2;
using UnityEngine;

namespace IreliaMod.Survivors.Irelia.Components
{
    public class SpawnDiamond : MonoBehaviour
    {
        Vector3 rot;
        void Start()
        {
            rot = transform.eulerAngles;
        }
        private void OnDestroy()
        {
            GameObject ef = Instantiate<GameObject>(IreliaAssets.edgeEffect, transform.position, Quaternion.identity);

            rot.x = ef.transform.eulerAngles.x;

            ef.transform.rotation = Quaternion.Euler(rot);

        }


    }
}