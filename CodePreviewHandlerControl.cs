using Common;
using Microsoft.Win32;
using PreviewHandlerCommon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodePreviewHandler
{
    class CodePreviewHandlerControl : FormHandlerControl
    {
        private readonly string configKey = "SOFTWARE\\CodePreviewHandler";
        private string highLighter = "highlight.exe";
        private string highLighterArgs = "-l -k \"JetBrains Mono\" --inline-css {file}";
        private long maxFileSize = 200 * 1024;

        /// <summary>
        /// Extended Browser Control to display source code html.
        /// </summary>
        private WebBrowserExt browser;
        /// <summary>
        /// Start the preview on the Control.
        /// </summary>
        /// <param name="dataSource">Path to the file.</param>
        public override void DoPreview<T>(T dataSource)
        {
            this.InvokeOnControlThread(() =>
            {
                try
                {
                    LoadConfig();


                    string filePath = dataSource as string;


                    FileInfo fileInfo = new FileInfo(filePath);


                    string html = "";
                    if (fileInfo.Length > maxFileSize)
                    {
                        html = GenerateBigFileHTML(filePath, fileInfo.Length);
                    }
                    else
                    {
                        html = GenerateHighlightHTML(filePath);
                    }


                    //string html = String.Format("<html><body>{0}, {1}, {2}</body></html>", highLighter, highLighterArgs, filePath);
                    //CodeTelemetry.Info(String.Format("html: %s", html));

                    this.browser = new WebBrowserExt
                    {
                        DocumentText = html,
                        Dock = DockStyle.Fill,
                        IsWebBrowserContextMenuEnabled = true,
                        ScriptErrorsSuppressed = true,
                        ScrollBarsEnabled = true,
                        AllowNavigation = false,
                    };
                    this.Controls.Add(this.browser);
                    base.DoPreview(dataSource);
                }
                catch (Exception e)
                {
                    CodeTelemetry.Info(e.Message);
                    base.DoPreview(dataSource);
                }
            });
        }

        private string GenerateBigFileHTML(string filePath, long fileSize)
        {


            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<html><body>");

            sb.AppendFormat("<div style=\"background-color:lightblue\">big file {0} {1}</div><pre>", fileSize, filePath);

            try
            {
                byte[] head = new byte[maxFileSize / 2];
                byte[] tail = new byte[maxFileSize / 2];


                FileStream fp = File.OpenRead(filePath);

                fp.Read(head, 0, head.Length);
                fp.Seek(fileSize - tail.Length, SeekOrigin.Begin);
                fp.Read(tail, 0, tail.Length);
                fp.Close();

                sb.Append(EncodeString(head));

                sb.AppendLine("</pre>");
                sb.AppendFormat("<div style=\"background-color:lightblue\"> omit {0} bytes</div>", fileSize - maxFileSize);
                sb.AppendLine("<pre>");

                sb.Append(EncodeString(tail));
            }
            catch(Exception e)
            {
                sb.AppendLine(e.ToString());
            }
            sb.AppendLine("</pre></body></html>");

            return sb.ToString();
        }

        private string GenerateHighlightHTML(string src)
        {
            StringBuilder output = new StringBuilder();

            Process process = new Process();

            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = highLighter;
            process.StartInfo.Arguments = highLighterArgs.Replace("{file}", src);
            process.OutputDataReceived += (sender, args) => output.AppendLine(args.Data);

            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();

            return output.ToString();
        }

        private string EncodeString(byte[] buff)
        {
            string text = "";
            try
            {
                text = Encoding.UTF8.GetString(buff);

            }
            catch (Exception)
            {

            }
            if (text.Length == 0)
            {
                try
                {
                    text = Encoding.GetEncoding("gbk").GetString(buff);
                }
                catch (Exception)
                {

                }

            }
            if (text.Length == 0)
            { 
                try
                {
                    text = Encoding.ASCII.GetString(buff);
                }
                catch (Exception)
                {

                }
            }
            return text;
        }


        private void LoadConfig()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(configKey);
            if (key != null)
            {
                if (key.GetValue("HighLighter") is string exe && exe.Length > 0)
                        highLighter = exe;

                if (key.GetValue("HighLighterArgs") is string arg && arg.Length > 0)
                        highLighterArgs = arg;
            }
        }
    }


}
