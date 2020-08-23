// Decompiled with JetBrains decompiler
// Type: BeOpen.iiko.Front.Temperature.TemperaturePlugin
// Assembly: BeOpen.iiko.Front.Temperature, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FCF00278-D055-4984-B166-1C8E705E33B1
// Assembly location: C:\Users\catav\Desktop\Temp\BeOpen.iiko.Front.TemperatureNew\BeOpen.iiko.Front.Temperature.dll

using BeOpen.iiko.Front.Temperature.Properties;
using Resto.Front.Api;
using Resto.Front.Api.Attributes;
using Resto.Front.Api.Data.Cheques;
using Resto.Front.Api.Data.Print;
using Resto.Front.Api.RemotingHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TemperatureLib;

namespace BeOpen.iiko.Front.Temperature
{
    [PluginLicenseModuleId(21016318)]
    public class TemperaturePlugin : IFrontPlugin, IDisposable
    {
        private readonly TemperatureApiClient _apiClient;
        private readonly Guid _storeExternalId;
        
        public TemperaturePlugin()
        {
            _apiClient = new TemperatureApiClient(Settings.Default.ApiOrganizationId, Settings.Default.ApiTemplateId, Settings.Default.ApiToken);
            _storeExternalId = Settings.Default.StoreExternalId;
            PluginContext.Log.Info("Api client initialized");
            RemotableFunc<Guid, BillCheque> remotableFunc = new RemotableFunc<Guid, BillCheque>(new Func<Guid, BillCheque>(OnBillChequePrintingCallback));
            PluginContext.Notifications.SubscribeOnBillChequePrinting(remotableFunc);
        }
        
        private BillCheque OnBillChequePrintingCallback(Guid orderId)
        {
            BillCheque billCheque = new BillCheque();
            try
            {
                IPrinterRef receiptChequePrinter = PluginContext.Operations.TryGetReceiptChequePrinter(false);
                if (receiptChequePrinter == null)
                {
                    PluginContext.Operations.AddErrorMessage("Cannot print temperature info: printer is missing", nameof (TemperaturePlugin));
                    return billCheque;
                }
                IEnumerable<TemperatureResponse> temperature = this._apiClient.GetTemperature();
                if (temperature == null)
                {
                    PluginContext.Operations.AddErrorMessage("Cannot print temperature info: could not get temperature info", nameof (TemperaturePlugin));
                    return billCheque;
                }
                TemperatureResponse temperatureResponse = temperature.FirstOrDefault(i => Guid.Parse(i.StoreExternalId) == _storeExternalId);
                if (temperatureResponse == null)
                {
                    PluginContext.Operations.AddErrorMessage("Cannot print temperature info: could not find current store id", nameof (TemperaturePlugin));
                    return billCheque;
                }
                
                XElement xelement1 = new XElement("doc");
                
                var checkDate = temperatureResponse.CheckDate;
                
                var day = checkDate.Day;
                checkDate = temperatureResponse.CheckDate;
                var month = checkDate.Month;
                checkDate = temperatureResponse.CheckDate;
                var year = checkDate.Year;
                string dateStr = string.Format("{0}.{1}.{2}  ", day, month, year);
                checkDate = temperatureResponse.CheckDate;
                var hour = checkDate.Hour;
                checkDate = temperatureResponse.CheckDate;
                var minute = checkDate.Minute;
                checkDate = temperatureResponse.CheckDate;
                var second = checkDate.Second;
                
                string timeStr = string.Format("{0}:{1}:{2}", hour, minute, second);
                string dateTimeStr = $"<b>Temperature information on {dateStr} {timeStr}</b>";
                var xelement2 = new XElement("fill", dateTimeStr);
                var xelement3 = new XElement("fill", string.Format("Avarage temperature: {0}", temperatureResponse.TemperatureInfo.AverageTemp));
                var xelement4 = new XElement("fill", ("Min temperature " + temperatureResponse.TemperatureInfo.EmployeeIdMinTemp));
                var xelement5 = new XElement("fill", ("Max temperature " + temperatureResponse.TemperatureInfo.EmployeeIdMaxTemp));
                xelement1.Add(xelement2);
                xelement1.Add(xelement3);
                xelement1.Add(xelement4);
                xelement1.Add(xelement5);
                PluginContext.Operations.Print(receiptChequePrinter, xelement1);
            }
            catch (Exception ex)
            {
                PluginContext.Log.Error("Cannot print temperature: " + ex.Message);
                PluginContext.Operations.AddErrorMessage("Cannot print temperature info: could not get temperature info", nameof (TemperaturePlugin));
            }
            return billCheque;
        }
        
        public void Dispose()
        {
        }
    }
}
