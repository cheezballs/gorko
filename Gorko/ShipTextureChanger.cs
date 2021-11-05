using System.Linq;
using UnityEngine;

namespace Gorko.CustomTextures
{
   class ShipTextureChanger : TextureChanger
   {
      private const string MAIN_TEXT = "_MainTex";
      private const string RENDERER_NAME = "sail_full";

      protected override Renderer GetRenderer()
      {
         return GetComponentsInChildren<SkinnedMeshRenderer>().First(x => x.name == RENDERER_NAME);
      }

      protected override void UpdateRenderedTexture(Texture2D newTexture)
      {
         renderer.material.SetTexture(MAIN_TEXT, newTexture);
      }
   }
}
