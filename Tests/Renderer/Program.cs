using Engine.Rendering;
using Engine.Rendering.D3D11;
using Engine.Windowing;
using System;
using System.Runtime.CompilerServices;

namespace Renderer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SDL_WindowFlags flags = SDL_WindowFlags.OpenGL | SDL_WindowFlags.Resizable | SDL_WindowFlags.Shown;
            Sdl2Window window = new Sdl2Window("Testing window", 300, 300, 1280, 720, flags, false);

            GraphicsDevice device = CreateD3D11(new GraphicsDeviceOptions(true), window.Handle, 1280, 720);
            ResourceFactory factory = device.ResourceFactory;

            CommandList master = factory.CreateCommandList();
            master.Begin();
            master.SetFramebuffer(device.SwapchainFramebuffer);
            master.SetFullViewports();
            master.ClearColorTarget(0, RgbaFloat.Red);
            master.End();

            while (window.Exists)
            {
                var events = window.PumpEvents();
                foreach (var item in events.KeyEvents)
                {
                    if (item.Key == Key.Escape)
                    {
                        window.Close();
                    }
                }

                device.SubmitCommands(master);
                device.SwapBuffers();
            }

            master.Dispose();
        }

        public static GraphicsDevice CreateD3D11(GraphicsDeviceOptions options, IntPtr hwnd, uint width, uint height)
        {
            SwapchainDescription swapchainDescription = new SwapchainDescription(
                SwapchainSource.CreateWin32(hwnd, IntPtr.Zero),
                width, height,
                options.SwapchainDepthFormat,
                options.SyncToVerticalBlank);

            return new D3D11GraphicsDevice(options, swapchainDescription);
        }
    }
}