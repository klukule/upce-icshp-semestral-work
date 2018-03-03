using Engine.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Rendering
{
    public sealed class RenderingService : Service
    {
        private ResourceFactory Factory;
        private GraphicsDevice Device;

        private CommandList master;
        public RenderingService(MyCore core) : base(core)
        {
            Device = core.Device;
            Factory = Device.ResourceFactory;
            master = Factory.CreateCommandList();
            master.Begin();
            master.SetFramebuffer(Device.SwapchainFramebuffer);
            master.SetFullViewports();
            master.ClearColorTarget(0, RgbaFloat.Red);
            master.End();
        }

        public void Render()
        {
            Device.SubmitCommands(master);
            Device.SwapBuffers();
        }

        public override void Dispose()
        {
            master.Dispose();
        }

        internal bool Init()
        {
            return false;
        }
    }
}
