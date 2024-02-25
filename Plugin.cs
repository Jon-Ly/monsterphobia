using BepInEx;
using BepInEx.Configuration;
using GameNetcodeStuff;
using System;
using System.Linq;
using System.IO;
using System.Reflection;
using UnityEngine;
using HarmonyLib;

namespace Monsterphobia
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static Config config { get; internal set; }

        public static Plugin Instance = null;

        private void Awake()
        {
            config = new Config(base.Config);

            Instance = this;

            try
            {
                Assets.PopulateAssets();
            } 
            catch(Exception ex)
            {
                Console.WriteLine($"{PluginInfo.PLUGIN_GUID}: {ex.Message}");
                Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is NOT loaded!");
                return;
            }

            Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll();

            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }

        [HarmonyPatch(typeof(EnemyAI))]
        public class EnemyAIPatch
        {
            [HarmonyPatch("Start")]
            [HarmonyPostfix]
            private static void InitReplacement(ref EnemyAI __instance)
            {
                try
                {
                    Instance.Logger.LogInfo($"{PluginInfo.PLUGIN_GUID} Attached to {__instance.GetType().Name}");
                    __instance.gameObject.AddComponent(typeof(MonsterReplacement));
                    __instance.gameObject.AddComponent(typeof(RemoveAudio));
                }
                catch (Exception e)
                {
                    Instance.Logger.LogError($"Monsterphobia: Error in EnemyAI InitReplacement: {e}");
                }
            }
        }

        // MaskedPlayerEnemy doesn't call base.Start(), so have to patch it's Start separately
        [HarmonyPatch(typeof(MaskedPlayerEnemy))]
        public class MaskedPlayerEnemyPatch
        {
            [HarmonyPatch("Start")]
            [HarmonyPostfix]
            private static void InitReplacement(ref EnemyAI __instance)
            {
                try
                {
                    Instance.Logger.LogInfo($"{PluginInfo.PLUGIN_GUID} Attached to {__instance.GetType().Name}");
                    __instance.gameObject.AddComponent(typeof(MonsterReplacement));
                    __instance.gameObject.AddComponent(typeof(RemoveAudio));
                }
                catch (Exception e)
                {
                    Instance.Logger.LogError($"Monsterphobia: Error in MaskedPlayerEnemy InitReplacement: {e}");
                }
            }
        }
    }

    public class Config
    {
        // Meshes
        public static ConfigEntry<bool> enableMonsterphbia { get; private set; }
        public static ConfigEntry<bool> enableBaboonText { get; private set; }
        public static ConfigEntry<bool> enableSlimeText { get; private set; }
        public static ConfigEntry<bool> enableThumperText { get; private set; }
        public static ConfigEntry<bool> enableSnareFleaText { get; private set; }
        public static ConfigEntry<bool> enableGhostGirlText { get; private set; }
        public static ConfigEntry<bool> enableBrackenText { get; private set; }
        public static ConfigEntry<bool> enableForestGiantText { get; private set; }
        public static ConfigEntry<bool> enableHoardingBugText { get; private set; }
        public static ConfigEntry<bool> enableJesterText { get; private set; }
        public static ConfigEntry<bool> enableEyelessDogText { get; private set; }
        public static ConfigEntry<bool> enableNutcrackerText { get; private set; }
        public static ConfigEntry<bool> enableSporeLizardText { get; private set; }
        public static ConfigEntry<bool> enableEarthLeviathanText { get; private set; }
        public static ConfigEntry<bool> enableCoilHeadText { get; private set; }
        public static ConfigEntry<bool> enableMaskedText { get; private set; }

        // Audio
        public static ConfigEntry<bool> disableMonsterSounds { get; private set; }
        public static ConfigEntry<bool> disableBaboonSounds { get; private set; }
        public static ConfigEntry<bool> disableSlimeSounds { get; private set; }
        public static ConfigEntry<bool> disableThumperSounds { get; private set; }
        public static ConfigEntry<bool> disableSnareFleaSounds { get; private set; }
        public static ConfigEntry<bool> disableGhostGirlSounds { get; private set; }
        public static ConfigEntry<bool> disableBrackenSounds { get; private set; }
        public static ConfigEntry<bool> disableForestGiantSounds { get; private set; }
        public static ConfigEntry<bool> disableHoardingBugSounds { get; private set; }
        public static ConfigEntry<bool> disableJesterSounds { get; private set; }
        public static ConfigEntry<bool> disableEyelessDogSounds { get; private set; }
        public static ConfigEntry<bool> disableNutcrackerSounds { get; private set; }
        public static ConfigEntry<bool> disableSpiderSounds { get; private set; }
        public static ConfigEntry<bool> disableSporeLizardSounds { get; private set; }
        public static ConfigEntry<bool> disableEarthLeviathanSounds { get; private set; }
        public static ConfigEntry<bool> disableCoilHeadSounds { get; private set; }
        public static ConfigEntry<bool> disableMaskedSounds { get; private set; }

        public Config(ConfigFile cfg)
        {
            // Meshes
            enableMonsterphbia = cfg.Bind<bool>("Turn all monsters into text!", "Enable monster to text technology", true, "Check this to replace all monsters with their text variant. Set to false to play it scary, again.");
            enableBaboonText = cfg.Bind<bool>("Turn all monsters into text!", "Enable Baboon Hawk text", true, "");
            enableSlimeText = cfg.Bind<bool>("Turn all monsters into text!", "Enable Slime text", true, "");
            enableThumperText = cfg.Bind<bool>("Turn all monsters into text!", "Enable Thumper text", true, "");
            enableSnareFleaText = cfg.Bind<bool>("Turn all monsters into text!", "Enable Snare Flea text", true, "");
            enableGhostGirlText = cfg.Bind<bool>("Turn all monsters into text!", "Enable Ghost Girl text", true, "");
            enableBrackenText = cfg.Bind<bool>("Turn all monsters into text!", "Enable Bracken text", true, "");
            enableForestGiantText = cfg.Bind<bool>("Turn all monsters into text!", "Enable Forest Giant text", true, "");
            enableHoardingBugText = cfg.Bind<bool>("Turn all monsters into text!", "Enable Hoarding Bug text", true, "");
            enableJesterText = cfg.Bind<bool>("Turn all monsters into text!", "Enable Jester text", true, "");
            enableEyelessDogText = cfg.Bind<bool>("Turn all monsters into text!", "Enable Eyeless Dog text", true, "");
            enableNutcrackerText = cfg.Bind<bool>("Turn all monsters into text!", "Enable Nutcracker text", true, "");
            enableSporeLizardText = cfg.Bind<bool>("Turn all monsters into text!", "Enable Spore Lizard text", true, "");
            enableEarthLeviathanText = cfg.Bind<bool>("Turn all monsters into text!", "Enable Earth Leviathan text", true, "");
            enableCoilHeadText = cfg.Bind<bool>("Turn all monsters into text!", "Enable Coil Head text", true, "");
            enableMaskedText = cfg.Bind<bool>("Turn all monsters into text!", "Enable Masked text", true, "");

            // Audio
            disableMonsterSounds = cfg.Bind<bool>("Turn off monster sounds!", "Disable monster sounds", false, "Check this to turn off monster sounds. Choose the ones you want disabled below");
            disableBaboonSounds = cfg.Bind<bool>("Turn off monster sounds!", "Disable Baboon sounds", false, "");
            disableSlimeSounds = cfg.Bind<bool>("Turn off monster sounds!", "Disable Slime sounds", false, "");
            disableThumperSounds = cfg.Bind<bool>("Turn off monster sounds!", "Disable Thumper sounds", false, "");
            disableSnareFleaSounds = cfg.Bind<bool>("Turn off monster sounds!", "Disable Snare Flea sounds", false, "");
            disableGhostGirlSounds = cfg.Bind<bool>("Turn off monster sounds!", "Disable Ghost Girl sounds", false, "");
            disableBrackenSounds = cfg.Bind<bool>("Turn off monster sounds!", "Disable Bracken sounds", false, "");
            disableForestGiantSounds = cfg.Bind<bool>("Turn off monster sounds!", "Disable Forest Giant sounds", false, "");
            disableHoardingBugSounds = cfg.Bind<bool>("Turn off monster sounds!", "Disable Hoarding Bug sounds", false, "");
            disableJesterSounds = cfg.Bind<bool>("Turn off monster sounds!", "Disable Jester sounds", false, "");
            disableEyelessDogSounds = cfg.Bind<bool>("Turn off monster sounds!", "Disable Eyeless Dog sounds", false, "");
            disableNutcrackerSounds = cfg.Bind<bool>("Turn off monster sounds!", "Disable Nutcracker sounds", false, "");
            disableSpiderSounds = cfg.Bind<bool>("Turn off monster sounds!", "Disable Spider sounds", false, "");
            disableSporeLizardSounds = cfg.Bind<bool>("Turn off monster sounds!", "Disable Spore Lizard sounds", false, "");
            disableEarthLeviathanSounds = cfg.Bind<bool>("Turn off monster sounds!", "Disable Earth Leviathan sounds", false, "");
            disableCoilHeadSounds = cfg.Bind<bool>("Turn off monster sounds!", "Disable Coil Head sounds", false, "");
            disableMaskedSounds = cfg.Bind<bool>("Turn off monster sounds!", "Disable Masked sounds", false, "");
        }
    }

    public static class Assets
    {
        public static string AssetBundleName = "monsterphobia\\monsterphobia";
        public static AssetBundle Bundle = null;

        private static string GetAssemblyName() => Assembly.GetExecutingAssembly().FullName.Split(',')[0];
        public static void PopulateAssets()
        {
            string sAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            AssetBundle assets = AssetBundle.LoadFromFile(Path.Combine(sAssemblyLocation, AssetBundleName));

            if (assets == null)
            {
                throw new Exception($"Please include the asset bundle");
            } 
            else if (Bundle == null)
            {
                Bundle = assets;
            }
        }
    }
}
