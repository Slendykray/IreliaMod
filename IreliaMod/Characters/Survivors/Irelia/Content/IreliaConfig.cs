using BepInEx.Configuration;
using IreliaMod.Modules;

namespace IreliaMod.Survivors.Irelia
{
    public static class IreliaConfig
    {
        public static ConfigEntry<float> voiceVolume;

        public static ConfigEntry<bool> altSfx;

        public static void Init()
        {
            voiceVolume = Config.BindAndOptions("Voice Lines", "Volume", 100f, min:0f, max: 100f);

            altSfx = Config.BindAndOptions("Voice Lines", "Alt SFX", true, description: "Enable alt sfx for different skins");
        }
    }
}
