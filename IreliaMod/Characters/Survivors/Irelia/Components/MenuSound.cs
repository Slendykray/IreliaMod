using RoR2;
using UnityEngine;

namespace IreliaMod.Survivors.Irelia.Components
{
    public class MenuSound : MonoBehaviour
    {
        private uint playID;

        private void Awake()
        {
            On.RoR2.ModelSkinController.ApplySkinAsync += ModelSkinController_ApplySkinAsync;
        }

        public IreliaAssets.Skins skinType = IreliaAssets.Skins.Default;

        private System.Collections.IEnumerator ModelSkinController_ApplySkinAsync(On.RoR2.ModelSkinController.orig_ApplySkinAsync orig, ModelSkinController self, int skinIndex, RoR2.ContentManagement.AsyncReferenceHandleUnloadType unloadType)
        {
            if (this && self == this.GetComponent<ModelSkinController>())
            {
                SkinDef currentSkinDef = self.skins[skinIndex];


                if (currentSkinDef.nameToken == "DEFAULT_SKIN")
                {
                    skinType = IreliaAssets.Skins.Default;
                }

                if (currentSkinDef.nameToken == IreliaSurvivor.HENRY_PREFIX + "GUARDIAN_SKIN_NAME")
                {
                    skinType = IreliaAssets.Skins.Guardian;
                }

          

                IreliaAssets.SetEffects(skinType, gameObject);

                //if (this.playID != 0) AkSoundEngine.StopPlayingID(this.playID);

                //PlayEffect();
            }

            return orig(self, skinIndex, unloadType);

            
        }

    

        private void OnDestroy()
        {
            if (this.playID != 0) AkSoundEngine.StopPlayingID(this.playID);

            On.RoR2.ModelSkinController.ApplySkinAsync -= ModelSkinController_ApplySkinAsync;
        }

        private void OnEnable()
        {
            this.Invoke("PlayEffect", 0.05f);
        }

        private void PlayEffect()
        {
            this.playID = Util.PlaySound("Play_MenuSelect", base.gameObject);
        }
    }
}