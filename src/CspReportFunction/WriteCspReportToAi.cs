using System;
using System.Text.Json;
using System.Threading.Tasks;
using CspReportFunction.Models;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace CspReportFunction
{
    // ReSharper disable once UnusedMember.Global
    public class WriteCspReportToAi
    {
        private readonly TelemetryClient _telemetryClient;

        private const string EVENTTYPE_CSPREPORT = "CspReport";


        public WriteCspReportToAi(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        [FunctionName("WriteCspReportToAi")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
        {
            req = req ?? throw new ArgumentNullException(nameof(req));

            //Load
            var parsedResponse = await ParseResponse(req).ConfigureAwait(false);
            //Validate
            if (ValidResponse(parsedResponse))
            {
                //Save
                SaveResponse(_telemetryClient, parsedResponse);

                return new AcceptedResult();
            }

            return new BadRequestResult();
        }

        private static void SaveResponse(TelemetryClient telemetryClient, SecurityPolicyReport parsedResponse)
        {
            var eventTelemetry = new EventTelemetry(EVENTTYPE_CSPREPORT)
            {
                Properties =
                {
                    [nameof(parsedResponse.CspReport.DocumentUrl)] = parsedResponse.CspReport.DocumentUrl.ToString(),
                    [nameof(parsedResponse.CspReport.Referrer)] = parsedResponse.CspReport.Referrer,
                    [nameof(parsedResponse.CspReport.BlockedUrl)] = parsedResponse.CspReport.BlockedUrl.ToString(),
                    [nameof(parsedResponse.CspReport.EffectiveDirective)] = parsedResponse.CspReport.EffectiveDirective,
                    [nameof(parsedResponse.CspReport.OriginalPolicy)] = parsedResponse.CspReport.OriginalPolicy,
                    [nameof(parsedResponse.CspReport.SourceFile)] = parsedResponse.CspReport.SourceFile,
                    [nameof(parsedResponse.CspReport.Sample)] = parsedResponse.CspReport.Sample,
                    [nameof(parsedResponse.CspReport.Disposition)] = parsedResponse.CspReport.Disposition.ToString(),
                    [nameof(parsedResponse.CspReport.StatusCode)] = parsedResponse.CspReport.StatusCode.ToString(),
                    [nameof(parsedResponse.CspReport.ColNo)] = parsedResponse.CspReport.ColNo.ToString(),
                    [nameof(parsedResponse.CspReport.LineNo)] = parsedResponse.CspReport.LineNo.ToString()
                },
                Timestamp = DateTimeOffset.UtcNow,
                //Try Force Sampling in :)
                ProactiveSamplingDecision = SamplingDecision.SampledIn
            };

            telemetryClient.TrackEvent(eventTelemetry);

        }

        private static bool ValidResponse(SecurityPolicyReport parsedResponse)
        {
            return parsedResponse.CspReport != default;
        }

        private static ValueTask<SecurityPolicyReport> ParseResponse(HttpRequest req)
            => JsonSerializer.DeserializeAsync<SecurityPolicyReport>(req.Body);
    }
}
