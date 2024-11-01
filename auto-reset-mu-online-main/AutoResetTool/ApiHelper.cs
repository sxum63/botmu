using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoResetTool
{
  public class ApiHelper
  {
#if DEBUG
    private const string API_ENDPOINT = "http://localhost:8080";

#else
    private const string API_ENDPOINT = "https://license-manager-mwt53fqqcq-as.a.run.app/";
#endif

    internal static LicenseKeyModel GetLicenseKeyModel(string licenseKey)
    {
      try
      {
        var client = new RestClient(API_ENDPOINT);
        var request = new RestRequest();
        request.AddQueryParameter("key", licenseKey);
        var response = client.ExecuteGet(request);
        if (response.IsSuccessful)
        {
          var resp = JsonConvert.DeserializeObject<FireStoreResponse>(response.Content);
          return resp.Data;
        }
        return null;
      }
      catch (Exception)
      {
        return null;
      }
    }

    internal static bool UpdateLicenseKey(LicenseKeyModel model)
    {
      try
      {
        model.ComputerName = System.Environment.MachineName;
        var client = new RestClient(API_ENDPOINT);
        var request = new RestRequest();
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(model);
        var response = client.ExecutePost(request);
        if (response.IsSuccessful)
        {
          var resp = JsonConvert.DeserializeObject<FireStoreResponse>(response.Content);
          return resp.Success;
        }
        return false;
      }
      catch (Exception)
      {
        return false;
      }
    }
  }

  internal class FireStoreResponse
  {
    public bool Success { get; set; }
    public string Message { get; set; }
    public LicenseKeyModel Data { get; set; }
  }
}
