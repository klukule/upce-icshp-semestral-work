using Engine.Core;
using Engine.Windowing;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Renderer
{
    public sealed class Game : MyGame
    {
        private Assembly EngineAssembly;

        public Game(string[] args) : base(60, args)
        {
            EngineAssembly = GetType().Assembly;
            InitializeCore();
            try
            {
                CreateDeviceAndWindow();
            }
            catch (Exception)
            {
                Environment.Exit(-1);
            }
        }

        private void InitializeCore()
        {
            Core = new MyCore(this, OnFinalPrepare);
        }

        private void OnFinalPrepare()
        {

        }

        public override void Initialize()
        {
            Core.Initialization();
            Core.FinalPrepare();
        }

        public override void Update()
        {
        }
    }
}
