using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace IreliaMod.Survivors.Irelia.Components
{
    public class SpawnDiamond : MonoBehaviour
    {
        private Vector3 rot;
        void Start()
        {
            rot = transform.eulerAngles;
            rot.x = IreliaAssets.edgeEffect.transform.eulerAngles.x;
        }
        private void OnDestroy()
        {
            if (!NetworkServer.active)
                return;


            ProjectileController pc = this.GetComponent<ProjectileController>();
            if (!pc || !pc.owner)
                return;
            GameObject attacker = pc.owner;

            CharacterBody body = attacker.GetComponent<CharacterBody>();
            if (!body)
                return;

            float num3 = IreliaStaticValues.edgeDamageCoefficient * 0.3f;
            float baseDamage = body.damage * num3;

            RoR2.Projectile.ProjectileManager.instance.FireProjectile(
                IreliaAssets.edgeEffect,
                transform.position,
                Quaternion.Euler(rot),
                attacker,
                baseDamage,
                0f,
                Util.CheckRoll(body.crit, body.master),
                damageType: DamageSource.Utility
            );

        }


    }
}