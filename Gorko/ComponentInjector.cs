using HarmonyLib;
using UnityEngine;

namespace Gorko.CustomTextures
{
   class ComponentInjector
   {
      [HarmonyPatch(typeof(Ship), "Awake")]
      [HarmonyPostfix]
      private static void Ship_Awake(Ship __instance)
      {
         if (!__instance.gameObject.GetComponent<ShipTextureChanger>())
         {
            __instance.gameObject.AddComponent<ShipTextureChanger>();
         }
      }

      [HarmonyPatch(typeof(Piece), "Awake")]
      [HarmonyPostfix]
      private static void AddComponentToBanner(Piece __instance)
      {
         if (Piece.ComfortGroup.Banner == __instance.m_comfortGroup && !__instance.gameObject.GetComponent<ShipTextureChanger>())
         {
            __instance.gameObject.AddComponent<BannerTextureChanger>();            
         }
      }

      [HarmonyPatch(typeof(ShipControlls), "Interact")]
      [HarmonyPostfix]
      private static void ShipControls_Interact(Humanoid character, bool repeat, bool alt, ShipControlls __instance)
      {
         string favoriteUrl = Plugin.INSTANCE.FavoriteImageURL.Value;
         if (!string.IsNullOrEmpty(favoriteUrl))
         {
            ShipTextureChanger textureChanger = __instance.GetComponentInParent<ShipTextureChanger>();
            textureChanger.SetText(Plugin.INSTANCE.FavoriteImageURL.Value);
         }         
      }
   }
}
