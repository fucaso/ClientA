// Decompiled with JetBrains decompiler
// Type: ClientA.Properties.Resources
// Assembly: ClientA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B451F4F-9880-4E2E-93A0-D0E06CC88CAA
// Assembly location: D:\Demo\Em\ClientA.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ClientA.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (ClientA.Properties.Resources.resourceMan == null)
          ClientA.Properties.Resources.resourceMan = new ResourceManager("ClientA.Properties.Resources", typeof (ClientA.Properties.Resources).Assembly);
        return ClientA.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => ClientA.Properties.Resources.resourceCulture;
      set => ClientA.Properties.Resources.resourceCulture = value;
    }
  }
}
