using System.Linq;
using UnityEngine;

namespace Gorko.CustomTextures
{
   class BannerTextureChanger : TextureChanger
   {
      private const string MAIN_TEXT = "_MainTex";
      private const string RENDERER_NAME = "default";

      protected override Renderer GetRenderer()
      {
         return GetComponentsInChildren<MeshRenderer>().First(x => x.name == RENDERER_NAME);
      }

      protected override void UpdateRenderedTexture(Texture2D newTexture)
      {         
         renderer.material.SetTexture(MAIN_TEXT, newTexture);
      }
   }
}
