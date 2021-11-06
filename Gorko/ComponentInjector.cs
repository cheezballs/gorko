using HarmonyLib;

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
   }
}
