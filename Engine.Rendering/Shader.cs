﻿using System;

namespace Engine.Rendering
{
    /// <summary>
    /// A device resource encapsulating a single shader module.
    /// See <see cref="ShaderDescription"/>.
    /// </summary>
    public abstract class Shader : DeviceResource, IDisposable
    {
        internal Shader(ShaderStages stage)
        {
            Stage = stage;
        }

        /// <summary>
        /// The shader stage this instance can be used in.
        /// </summary>
        public ShaderStages Stage { get; }

        /// <summary>
        /// The name of the entry point function.
        /// </summary>
        public string EntryPoint { get; }

        /// <summary>
        /// A string identifying this instance. Can be used to differentiate between objects in graphics debuggers and other
        /// tools.
        /// </summary>
        public abstract string Name { get; set; }

        /// <summary>
        /// Frees unmanaged device resources controlled by this instance.
        /// </summary>
        public abstract void Dispose();
    }
}