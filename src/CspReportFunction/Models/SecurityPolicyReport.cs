using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CspReportFunction.Models
{
    public class SecurityPolicyReport
    {
        [JsonPropertyName("csp-report")]
        public SecurityPolicyViolation CspReport { get; set; } = new SecurityPolicyViolation();
    }

    /*
     
        interface SecurityPolicyViolation : Event
        {
             readonly    attribute USVString      documentURL;
             readonly    attribute USVString      documentURI; // historical alias of documentURL
             readonly    attribute USVString      referrer;
             readonly    attribute USVString      blockedURL;
             readonly    attribute USVString      blockedURI; // historical alias of blockedURL
             readonly    attribute DOMString      effectiveDirective;
             readonly    attribute DOMString      violatedDirective; // historical alias of effectiveDirective
             readonly    attribute DOMString      originalPolicy;
             readonly    attribute USVString      sourceFile;
             readonly    attribute DOMString      sample;
             readonly    attribute SecurityPolicyViolationEventDisposition      disposition;
             readonly    attribute unsigned short statusCode;
             readonly    attribute unsigned long  lineno;
             readonly    attribute unsigned long  lineNumber; // historical alias of lineno
             readonly    attribute unsigned long  colno;
             readonly    attribute unsigned long  columnNumber; // historical alias of colno
        };
    */
    public class SecurityPolicyViolation
    {
        private const string OBSOLETE_V1 = "This is a V1 CSP property";

        [JsonPropertyName("document-url")]
        public Uri DocumentUrl { get; set; }

        [JsonPropertyName("document-uri")]
        [Obsolete(OBSOLETE_V1)]
        // ReSharper disable once UnusedMember.Global
        public Uri DocumentUri
        {
            get => DocumentUrl;
            set => DocumentUrl = value;
        }

        [JsonPropertyName("referrer")]
        public string? Referrer { get; set; }

        [JsonPropertyName("blocked-url")]
        public Uri BlockedUrl { get; set; }

        [JsonPropertyName("blocked-uri")]
        [Obsolete(OBSOLETE_V1)]
        public Uri BlockedUri
        {
            get => BlockedUrl;
            set => BlockedUrl = value;
        }

        [JsonPropertyName("effective-directive")]
        public string EffectiveDirective { get; set; } = string.Empty;

        [JsonIgnore]
        [JsonPropertyName("violated-directive")]
        [Obsolete(OBSOLETE_V1)]
        // ReSharper disable once UnusedMember.Global
        public string ViolatedDirective
        {
            get => EffectiveDirective;
            set => EffectiveDirective = value;
        }

        [JsonPropertyName("original-policy")]
        public string OriginalPolicy { get; set; } = string.Empty;

        [JsonPropertyName("disposition")]
        public SecurityPolicyViolationEventDisposition Disposition { get; set; }

        [JsonPropertyName("status-code")]
        public short? StatusCode { get; set; }

        [MaxLength(40)]
        [JsonPropertyName("sample")]
        public string? Sample { get; set; }

        [JsonPropertyName("source-file")]
        public string? SourceFile { get; set; }

        [JsonIgnore]
        [JsonPropertyName("line-number")]
        [Obsolete(OBSOLETE_V1)]
        // ReSharper disable once UnusedMember.Global
        public long? LineNumber
        {
            get => LineNo;
            set => LineNo = value;
        }

        [JsonPropertyName("lineno")]
        public long? LineNo { get; set; }

        [JsonPropertyName("colno")]
        public long? ColNo { get; set; }

        [JsonIgnore]
        [JsonPropertyName("column-number")]
        [Obsolete(OBSOLETE_V1)]
        // ReSharper disable once UnusedMember.Global
        public long? ColumnNumber
        {
            get => ColNo;
            set => ColNo = value;
        }
    }

    public enum SecurityPolicyViolationEventDisposition
    {
        Enforce,
        Report
    }
}
