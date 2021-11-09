using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using UnityEngine;

namespace Gorko.CustomTextures
{
   [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
   public class Plugin : BaseUnityPlugin
   {
      public static Plugin INSTANCE { get; private set; }
      public ConfigEntry<bool> PluginEnabled { get; private set; }
      public ConfigEntry<string> PromptText { get; private set; }
      public ConfigEntry<string> ModifierKey { get; private set; }
      public ConfigEntry<string> FavoriteImageURL { get; private set; }
      public ConfigEntry<bool> ModifierKeyRequired { get; private set; }
      public ConfigEntry<float> ServerPollTime { get; private set; }
      public ConfigEntry<int> ComfortBonus { get; private set; }

      public void Awake()
      {
         INSTANCE = this;
         PluginEnabled = Config.Bind("General", "Enabled", true, "Enables The Plugin");
         PromptText = Config.Bind("General", "PromptText", "Set Image URL", "The prompt text that displays when you set an image URL.");
         ServerPollTime = Config.Bind("General", "ServerPollTime", 10.0f, "The number of seconds to wait before syncing texture changes from other clients.");
         //ComfortBonus = Config.Bind("General", "ComfortBonus", 0, "How much extra comfort a custom-textured piece of furniture provides.");         
         ModifierKeyRequired = Config.Bind("Keys", "ModifierKeyRequired", true, "Do you need to hold an additional key to do the magic?");
         ModifierKey = Config.Bind("Keys", "ModifierKey", "LeftControl", "The key to be held to trigger the magic.");
         FavoriteImageURL = Config.Bind("Favorites", "FavoriteImageURL", "", "If set, this image will be auto-loaded when you take-over driving a boat.");

         if (PluginEnabled.Value)
         {
            Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll(typeof(ComponentInjector));
         }
      }

      public bool AllowControls()
      {
         return !ModifierKeyRequired.Value || ModifierKeyPressed();
      }

      private bool ModifierKeyPressed()
      {
         if (Enum.TryParse(ModifierKey.Value, out KeyCode key))
         {
            if (Input.GetKey(key))
            {
               return true;
            }
         }
         return false;
      }
   }
}
