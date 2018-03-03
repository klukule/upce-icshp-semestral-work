using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Core
{
    public abstract class Service : IDisposable
    {
        public MyCore Core;

        protected Service(MyCore core)
        {
            Core = core;
        }

        public abstract void Dispose();
    }
}
