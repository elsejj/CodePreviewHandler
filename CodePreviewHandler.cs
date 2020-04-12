using System;
using System.Runtime.InteropServices;
using Common;

namespace CodePreviewHandler
{
    /// <summary>
    /// 
    /// </summary>
    [Guid("74fd8caa-8d3d-43f6-a601-7dc8790ba12a")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class CodePreviewHandler : FileBasedPreviewHandler
    {

        private CodePreviewHandlerControl codePreviewHandlerControl;

        /// <summary>
        /// 
        /// </summary>
        public override void DoPreview()
        {
            codePreviewHandlerControl.DoPreview(this.FilePath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IPreviewHandlerControl CreatePreviewHandlerControl()
        {
            //CodeTelemetry.Info("CreatePreviewHandlerControl");
            codePreviewHandlerControl = new CodePreviewHandlerControl();
            return codePreviewHandlerControl;
        }
    }
}
