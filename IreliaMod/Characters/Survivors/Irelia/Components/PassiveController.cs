using HG;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace IreliaMod.Survivors.Irelia.Components
{
    public class PassiveController : MonoBehaviour
    {
        public GenericSkill passiveGenericSkill;

        public Passives passiveType = Passives.None;

        public enum Passives
        {
            None = 0,
            Strike = 1,
            Shuriken = 2,
        }

        private void Update()
        {
            CheckKeyStone();
        }

        private void CheckKeyStone()
        {
            if (this.passiveGenericSkill.skillNameToken == IreliaSurvivor.HENRY_PREFIX + "PASSIVE_STRIKE_NAME") this.passiveType = Passives.Strike;
            if (this.passiveGenericSkill.skillNameToken == IreliaSurvivor.HENRY_PREFIX + "PASSIVE_SHURIKEN_NAME") this.passiveType = Passives.Shuriken;
        }
    }
}

