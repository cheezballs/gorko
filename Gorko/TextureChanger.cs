﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Gorko.CustomTextures
{
   abstract class TextureChanger : MonoBehaviour, Hoverable, Interactable, TextReceiver
   {
      private const int URL_CHAR_LIMIT = 500;
      private const string PROP_KEY_URL = "GORKO_Lives";
      private const int HTTP_COOLDOWN_SEC = 5;

      private ZNetView networkView;
      private string currentDisplayedURL;

      protected Renderer renderer;

      public string URL { get; private set; }

      protected abstract Renderer GetRenderer();      
      protected abstract void UpdateRenderedTexture(Texture2D newTexture);

      public void Awake()
      {
         networkView = GetComponent<ZNetView>();         
         if (networkView.GetZDO() == null)
         {
            // aint nobody got time for that
            return;
         }         
         renderer = GetRenderer();
         
         // init from the network
         URL = networkView.GetZDO().GetString(PROP_KEY_URL);         
      }

      public void OnBecameVisible()
      {
         UpdateTexture();
      }

      private void UpdateTexture()
      {
         if(currentDisplayedURL != URL)
         {
            FetchAndReplaceTexture();
         }
      }

      private void FetchAndReplaceTexture()
      {
         StartCoroutine(Download((string url, Texture2D texture) => {
            networkView.ClaimOwnership();
            networkView.GetZDO().Set(PROP_KEY_URL, url);      
            UpdateRenderedTexture(texture);
            currentDisplayedURL = url;
         }));
      }

      private IEnumerator Download(Action<string, Texture2D> callback)
      {
         using (UnityWebRequest uwr = UnityWebRequest.Get(URL))
         {
            yield return uwr.SendWebRequest();
            if (!uwr.isHttpError && !uwr.isNetworkError)
            {
               Texture2D texture = new Texture2D(2, 2);
               texture.LoadImage(uwr.downloadHandler.data);               
               callback.Invoke(URL, texture);
            }
         }
      }

      public string GetHoverText()
      {
         return Plugin.INSTANCE.AllowControls() ? Plugin.INSTANCE.PromptText.Value : null;
      }

      public string GetHoverName()
      {
         return Plugin.INSTANCE.AllowControls() ? Plugin.INSTANCE.HoverText.Value : null;
      }

      public bool Interact(Humanoid character, bool hold, bool alt)
      {
         if (!Plugin.INSTANCE.AllowControls()) return false;
         if (hold)
         {
            return false;
         }
         if (!PrivateArea.CheckAccess(transform.position, 0f, true))
         {
            return false;
         }
         TextInput.instance.RequestText(this, "$piece_sign_input", URL_CHAR_LIMIT);
         return true;
      }

      public string GetText()
      {
         return networkView.GetZDO().GetString(PROP_KEY_URL, null);
      }

      public bool UseItem(Humanoid user, ItemDrop.ItemData item)
      {
         return false;
      }

      public void SetText(string text)
      {
         URL = text;
         UpdateTexture();
      }
   }
}