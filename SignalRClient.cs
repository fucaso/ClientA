// Decompiled with JetBrains decompiler
// Type: ClientA.SignalRClient
// Assembly: ClientA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B451F4F-9880-4E2E-93A0-D0E06CC88CAA
// Assembly location: D:\Demo\Em\ClientA.exe

using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace ClientA
{
  public class SignalRClient
  {
    private HubConnection Connection;
    private IHubProxy ChatHubProxy;

    public event SignalRClient.MessageReceived OnMessageReceived;

    public SignalRClient(string url)
    {
      this.Connection = new HubConnection(url);
      this.Connection.StateChanged += (Action<StateChange>) (obj => { });
      this.ChatHubProxy = this.Connection.CreateHubProxy("Chat");
      this.ChatHubProxy.On<string, string>("MessageReceived", (Action<string, string>) ((username, text) =>
      {
        SignalRClient.MessageReceived onMessageReceived = this.OnMessageReceived;
        if (onMessageReceived == null)
          return;
        onMessageReceived(username, text);
      }));
    }

    public void SendMessage(string username, string text) => this.ChatHubProxy.Invoke(nameof (SendMessage), (object) username, (object) text);

    public Task Start() => this.Connection.Start();

    public bool IsConnectedOrConnecting => this.Connection.State != ConnectionState.Disconnected;

    public ConnectionState ConnectionState => this.Connection.State;

    public static async Task<SignalRClient> CreateAndStart(string url)
    {
      SignalRClient client = new SignalRClient(url);
      await client.Start();
      SignalRClient signalRclient = client;
      client = (SignalRClient) null;
      return signalRclient;
    }

    public delegate void MessageReceived(string username, string message);
  }
}
