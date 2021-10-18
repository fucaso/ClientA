// Decompiled with JetBrains decompiler
// Type: ClientA.Net
// Assembly: ClientA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B451F4F-9880-4E2E-93A0-D0E06CC88CAA
// Assembly location: D:\Demo\Em\ClientA.exe

using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ClientA
{
  public class Net
  {
    public static string PostURI(Uri u, object o)
    {
      string str = string.Empty;
      StringContent stringContent = new StringContent(JsonConvert.SerializeObject(o, Formatting.Indented), Encoding.UTF8, "application/json");
      using (HttpClient httpClient = new HttpClient())
      {
        try
        {
          HttpResponseMessage result = httpClient.PostAsync(u, (HttpContent) stringContent).Result;
          if (result.IsSuccessStatusCode)
            str = result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
          string message = ex.Message;
        }
      }
      return str;
    }

    public static string GetURI(string url)
    {
      string empty = string.Empty;
      return new WebClient().DownloadString(url);
    }
  }
}
