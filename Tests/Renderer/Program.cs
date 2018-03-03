using Engine.Windowing;

namespace Renderer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SDL_WindowFlags flags = SDL_WindowFlags.OpenGL | SDL_WindowFlags.Resizable | SDL_WindowFlags.Shown;
            Sdl2Window window = new Sdl2Window("Testing window", 300, 300, 1280, 720, flags, false);
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
            }
        }
    }
}