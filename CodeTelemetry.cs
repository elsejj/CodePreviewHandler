using PreviewHandlerCommon.Telemetry;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePreviewHandler
{
    /// <summary>
    /// Telemetry helper class for markdown renderer.
    /// </summary>
    public class CodeTelemetry //: TelemetryBase
    {
        /// <summary>
        /// Name for ETW event.
        /// </summary>
        private const string EventSourceName = "Microsoft.PowerToys";

        /// <summary>
        /// ETW event name when markdown is previewed.
        /// </summary>
        private const string CodeFilePreviewedEventName = "PowerPreview_CodeRenderer_Previewed";

        /// <summary>
        /// ETW event name when error is thrown during markdown preview.
        /// </summary>
        private const string CodeFilePreviewErrorEventName = "PowerPreview_CodeRenderer_Error";

        /// <summary>
        /// construcor
        /// </summary>
        public CodeTelemetry() // : base(EventSourceName)
        {
        }

        /*

        /// <summary>
        /// Gets an instance of the <see cref="CodeTelemetry"/> class.
        /// </summary>
        public static CodeTelemetry Log { get; } = new CodeTelemetry();

        /// <summary>
        /// Publishes ETW event when markdown is previewed successfully.
        /// </summary>
        public void CodeFilePreviewed()
        {
            using (System.IO.StreamWriter file =
                     new System.IO.StreamWriter(@"d:\temp\codepreview.txt", true))
            {
                file.WriteLine("start");
            }
            this.Write(CodeFilePreviewedEventName, new EventSourceOptions()
            {
                Keywords = ProjectKeywordMeasure,
                Tags = ProjectTelemetryTagProductAndServicePerformance,
            });

        }

        /// <summary>
        /// Publishes ETW event when markdown could not be previewed.
        /// </summary>
        public void CodeFilePreviewError(string message)
        {
            using (System.IO.StreamWriter file =
                     new System.IO.StreamWriter(@"d:\temp\codepreview.txt", true))
            {
                file.WriteLine(message);
            }
            this.Write(
                CodeFilePreviewErrorEventName,
                new EventSourceOptions()
                {
                    Keywords = ProjectKeywordMeasure,
                    Tags = ProjectTelemetryTagProductAndServicePerformance,
                },
                new { Message = message, });
        }
        */


        public static void Info(string message)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry("CodePreview " + message, EventLogEntryType.Information, 101, 1);
            }
        }
    }
}
