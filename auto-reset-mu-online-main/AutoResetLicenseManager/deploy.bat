gcloud functions deploy license-manager --gen2 --entry-point AutoResetLicenseManager.GetLicense --runtime dotnet6 --trigger-http --allow-unauthenticated --region=asia-southeast1