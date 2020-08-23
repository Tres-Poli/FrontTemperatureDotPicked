// Decompiled with JetBrains decompiler
// Type: BeOpen.iiko.Front.Temperature.Properties.Settings
// Assembly: BeOpen.iiko.Front.Temperature, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FCF00278-D055-4984-B166-1C8E705E33B1
// Assembly location: C:\Users\catav\Desktop\Temp\BeOpen.iiko.Front.TemperatureNew\BeOpen.iiko.Front.Temperature.dll

using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BeOpen.iiko.Front.Temperature.Properties
{
  [CompilerGenerated]
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.5.0.0")]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default => Settings.defaultInstance;

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("c0bcedb6-99d0-425f-a37f-34013d0c8cfd")]
    public Guid ApiOrganizationId => (Guid) this[nameof (ApiOrganizationId)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("600e3215-7e21-4df3-8434-0e0b62b6bf09")]
    public Guid ApiTemplateId => (Guid) this[nameof (ApiTemplateId)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("pTw0gyoVT_xtTV3LVZnaGBCKskNtadEI6Pie-IA1Bj4")]
    public string ApiToken => (string) this[nameof (ApiToken)];

    [ApplicationScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("2a76dbe5-dbb0-4494-aa99-3e527101130f")]
    public Guid StoreExternalId => (Guid) this[nameof (StoreExternalId)];
  }
}
