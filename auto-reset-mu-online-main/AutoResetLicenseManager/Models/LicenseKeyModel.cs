using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoResetLicenseManager.Models
{
  [FirestoreData]
  public class LicenseKeyModel
  {
    public string Id { get; set; }
    [FirestoreProperty("key")]
    [JsonProperty("key")]
    public string Key { get; set; }
    [FirestoreProperty("isActive")]
    [JsonProperty("isActive")]
    public bool IsActive { get; set; }
    [FirestoreProperty("ip")]
    [JsonProperty("ip")]
    public string Ip { get; set; }
    [FirestoreProperty("computerName")]
    [JsonProperty("computerName")]
    public string ComputerName { get; set; }
    [FirestoreProperty("activeDate")]
    [JsonProperty("activeDate")]
    public double ActiveDate { get; set; }
    [FirestoreProperty("expiredDate")]
    [JsonProperty("expiredDate")]
    public double ExpiredDate { get; set; }
  }
}
