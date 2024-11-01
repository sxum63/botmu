using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoResetTool
{
  internal class LicenseKeyModel
  {
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("key")]
    public string Key { get; set; }
    [JsonProperty("isActive")]
    public bool IsActive { get; set; }
    [JsonProperty("ip")]
    public string Ip { get; set; }
    [JsonProperty("computerName")]
    public string ComputerName { get; set; }
    [JsonProperty("activeDate")]
    public double ActiveDate { get; set; }
    [JsonProperty("expiredDate")]
    public double ExpiredDate { get; set; }
  }
}
