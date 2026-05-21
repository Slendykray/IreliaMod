using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace HenryMod.Survivors.Henry.Components
{
    internal class BladeHoming : MonoBehaviour
    {
        public float speed = 60f;

        private Rigidbody rb;

        public Vector3 pos;

        Vector3 dir;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();

            dir = (pos - transform.position).normalized;

            transform.rotation = Quaternion.LookRotation(dir);
        }

        private void FixedUpdate()
        {
            //Vector3 dir = (pos - transform.position).normalized;

          

            //float dist = Vector3.Distance(pos, transform.position);

            rb.velocity = transform.forward * speed;

            //if (dist < 1f)
            //{
            //    rb.velocity = Vector3.zero;
            //}
            //else
            //{
            //    rb.velocity = dir * speed;
            //}
        }




    }
}