using AutoResetLicenseManager.Models;
using Google.Api;
using Google.Cloud.Firestore;
using Google.Cloud.Functions.Framework;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Rpc.Context.AttributeContext.Types;

namespace AutoResetLicenseManager
{
  public class License : IHttpFunction
  {
    public async Task HandleAsync(HttpContext context)
    {
      InitFirestoreEnvironment();
      try
      {
        string method = context.Request.Method.ToLower();
        switch (method)
        {
          case "post":
            if (await UpdateLicense(context))
            {
              await context.Response.WriteAsJsonAsync(new
              {
                Success = true,
                Message = "Save data succeeded."
              });
              return;
            }
            else
            {
              await context.Response.WriteAsJsonAsync(new
              {
                Success = false,
                Message = "Save data failed."
              });
              return;
            }
          case "get":
            await GetLicense(context);
            return;
        }
        await context.Response.WriteAsync("Access Denied!");
      }
      catch (Exception)
      {
        await ReturnError(context);
      }
    }

    private async Task GetLicense(HttpContext context)
    {
      string key = context.Request.Query["key"];
      if (!string.IsNullOrEmpty(key))
      {
        var data = await GetLicenseByKey(key);
        if (data != null)
        {
          await context.Response.WriteAsJsonAsync(new
          {
            Success = true,
            Message = "Get data succeeded.",
            Data = data
          });
          return;
        }
      }
      await context.Response.WriteAsJsonAsync(new
      {
        Success = false,
        Message = "Get data failed."
      });
    }

    private async Task<LicenseKeyModel> GetLicenseByKey(string key)
    {
      FirestoreDb db = await FirestoreDb.CreateAsync("auto-reset-muonline");
      CollectionReference collection = db.Collection("license");
      Query query = collection.WhereEqualTo("key", key);
      QuerySnapshot querySnapshot = await query.GetSnapshotAsync();
      var document = querySnapshot.Documents.FirstOrDefault();
      if (document != null)
      {
        var data = document.ConvertTo<LicenseKeyModel>();
        data.Id = document.Id;
        return data;
      }
      return null;
    }

    private async Task<bool> UpdateLicense(HttpContext context)
    {
      var body = context.Request.Body;
      string json = await new StreamReader(body).ReadToEndAsync();
      try
      {
        var model = JsonConvert.DeserializeObject<LicenseKeyModel>(json);
        var data = await GetLicenseByKey(model.Key);
        if (data != null) //update
        {
          FirestoreDb db = await FirestoreDb.CreateAsync("auto-reset-muonline");
          var document = db.Collection("license").Document(data.Id);
          await document.SetAsync(model, SetOptions.MergeAll);
          return true;
        }
        else //add
        {
          FirestoreDb db = await FirestoreDb.CreateAsync("auto-reset-muonline");
          var document = db.Collection("license").Document();
          await document.SetAsync(model);
          return true;
        }
      }
      catch (Exception)
      {

      }
      return false;
    }

    private async Task ReturnError(HttpContext context)
    {
      await context.Response.WriteAsync("Internal server error!");
    }
    private void InitFirestoreEnvironment()
    {
      const string var_name = "GOOGLE_APPLICATION_CREDENTIALS";
      const string path_value = "auto-reset-muonline-b64d7e6dc370.json";
      Environment.SetEnvironmentVariable(var_name, path_value);
    }
  }
}
