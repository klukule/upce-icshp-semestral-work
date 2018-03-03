using Engine.Core;
using Engine.Rendering;
using Engine.Rendering.D3D11;
using Engine.Windowing;
using System;
using System.Xml;

namespace Renderer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Game g = new Game(args);
            g.Run();
        }
    }
}