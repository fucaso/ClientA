// Decompiled with JetBrains decompiler
// Type: ClientA.Properties.Settings
// Assembly: ClientA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B451F4F-9880-4E2E-93A0-D0E06CC88CAA
// Assembly location: D:\Demo\Em\ClientA.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace ClientA.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default
    {
      get
      {
        Settings defaultInstance = Settings.defaultInstance;
        return defaultInstance;
      }
    }
  }
}
