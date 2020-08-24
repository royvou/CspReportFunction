# CspReportFunction

This service implements an endpoint to store CspReports ([Mozilla Documentation](https://developer.mozilla.org/en-US/docs/Web/HTTP/CSP)) in Azure Application Insights on an self managed Environment.

## Build Status

![Build .NET core App](https://github.com/royvou/CspReportFunction/workflows/Build%20.NET%20core%20App/badge.svg)

## Technologies
This is build using the following technologies/Stack

- Application Insights
- Azure Functions
- Azure
- .NET Core 3.1

# Deployment

Deployment on your own environment is easy.

Execute the following script to provision your environment

```pwsh
$prefix = ""
& .\arm\.\deploy.ps1 -prefix $prefix
```

Deploy the application zip with the Azure CLI (Or with CI/CD)

```pwsh
$prefix = ""
az webapp deployment source config-zip --resource-group  $prefix-cspreport --name $prefix-cspreport  --src ./release/cspreportfunction.zip
```
