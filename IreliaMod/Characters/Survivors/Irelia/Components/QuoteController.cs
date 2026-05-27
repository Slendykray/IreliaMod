using HG;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace IreliaMod.Survivors.Irelia.Components
{
    public class QuoteController : MonoBehaviour
    {
        private bool quotePlayed;
        private uint activeKillQuotePlayID;
        private uint activePlayID;
        private uint activeTeleportQuotePlayID;
        private uint activeWalkQuotePlayID;
        private bool killQuotePlayed;
        private bool teleportQuotePlayed;

        private bool startPlayed;

        private CharacterBody characterBody;

        private void Awake()
        {
            this.characterBody = base.GetComponent<CharacterBody>();

            TeleporterInteraction.onTeleporterFinishGlobal += TeleporterInteraction_onTeleporterFinishGlobal;

            On.RoR2.Inventory.UpdateEffectiveItemStacks += Inventory_UpdateEffectiveItemStacks;

            On.RoR2.GlobalEventManager.OnCharacterDeath += GlobalEventManager_OnCharacterDeath;

            On.EntityStates.SurvivorPod.ReleaseFinished.OnEnter += ReleaseFinished_OnEnter;

           
        }

        private void ReleaseFinished_OnEnter(On.EntityStates.SurvivorPod.ReleaseFinished.orig_OnEnter orig, EntityStates.SurvivorPod.ReleaseFinished self)
        {
            orig(self);

            if (!startPlayed)
            {
                startPlayed = true;
                Util.PlaySound("Play_FirstLevelStart", base.gameObject);
            }
        }

        private void GlobalEventManager_OnCharacterDeath(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, GlobalEventManager self, DamageReport damageReport)
        {
            orig(self, damageReport);

            if (damageReport.victim.body == characterBody)
            {
                Util.PlaySound("Play_Death", base.gameObject);
            }      

            if (damageReport.victim.body.bodyIndex == RoR2.BodyCatalog.FindBodyIndex("BrotherHurtBody"))
            {
                Util.PlaySound("Play_MithrixKill", base.gameObject);
            }
        }

        private void Inventory_UpdateEffectiveItemStacks(On.RoR2.Inventory.orig_UpdateEffectiveItemStacks orig, Inventory self, ItemIndex itemIndex)
        {       
            orig(self, itemIndex);

            CharacterMaster master = self.GetComponent<CharacterMaster>();

            if (!master)
                return;

            CharacterBody body = master.GetBody();

            if (!body || body != characterBody)
                return;

     
            if (itemIndex == ItemCatalog.FindItemIndex("BleedOnHit"))
            {
                Util.PlaySound("Play_TakingTriTippedDagger", base.gameObject);
            }

            if (itemIndex == ItemCatalog.FindItemIndex("Dagger"))
            {
                Util.PlaySound("Play_TakingCeremonialDagger", base.gameObject);
            }

            if (itemIndex == ItemCatalog.FindItemIndex("Icicle"))
            {
                Util.PlaySound("Play_TakeFrostRelic", base.gameObject);
            }
        }

        private void TeleporterInteraction_onTeleporterFinishGlobal(TeleporterInteraction obj)
        {
            this.activeTeleportQuotePlayID = Util.PlaySound("Play_Random_Clear", base.gameObject);
        }

        private void OnDestroy()
        {
            //if (this.activePlayID != 0) AkSoundEngine.StopPlayingID(this.activePlayID);
            //if (this.activeKillQuotePlayID != 0) AkSoundEngine.StopPlayingID(this.activeKillQuotePlayID);
            //if (this.activeTeleportQuotePlayID != 0) AkSoundEngine.StopPlayingID(this.activeTeleportQuotePlayID);
            //if (this.activeWalkQuotePlayID != 0) AkSoundEngine.StopPlayingID(this.activeWalkQuotePlayID);
            TeleporterInteraction.onTeleporterFinishGlobal -= TeleporterInteraction_onTeleporterFinishGlobal;

            On.RoR2.Inventory.UpdateEffectiveItemStacks -= Inventory_UpdateEffectiveItemStacks;

            On.RoR2.GlobalEventManager.OnCharacterDeath -= GlobalEventManager_OnCharacterDeath;

            On.EntityStates.SurvivorPod.ReleaseFinished.OnEnter -= ReleaseFinished_OnEnter;
            //On.RoR2.Inventory.GiveItem_ItemIndex_int -= Inventory_GiveItem_ItemIndex_int;
            //BossGroup.onBossGroupStartServer -= BossGroup_onBossGroupStartServer;
            //BossGroup.onBossGroupDefeatedServer -= BossGroup_onBossGroupDefeatedServer;
            //Run.onRunAmbientLevelUp -= Run_onRunAmbientLevelUp;
        }
    }
}

