﻿using System;

namespace Engine.Rendering
{
    /// <summary>
    /// A bitmask describing the permitted uses of a <see cref="DeviceBuffer"/> object.
    /// </summary>
    [Flags]
    public enum BufferUsage : byte
    {
        /// <summary>
        /// Indicates that a <see cref="DeviceBuffer"/> can be used as the source of vertex data for drawing commands.
        /// This flag enables the use of a Buffer in the <see cref="CommandList.SetVertexBuffer(uint, DeviceBuffer)"/> method.
        /// </summary>
        VertexBuffer = 1 << 0,

        /// <summary>
        /// Indicates that a <see cref="DeviceBuffer"/> can be used as the source of index data for drawing commands.
        /// This flag enables the use of a Buffer in the <see cref="CommandList.SetIndexBuffer(DeviceBuffer, IndexFormat)" /> method.
        /// </summary>
        IndexBuffer = 1 << 1,

        /// <summary>
        /// Indicates that a <see cref="DeviceBuffer"/> can be used as a uniform Buffer.
        /// This flag enables the use of a Buffer in a <see cref="ResourceSet"/> as a uniform Buffer.
        /// </summary>
        UniformBuffer = 1 << 2,

        /// <summary>
        /// Indicates that a <see cref="DeviceBuffer"/> can be used as a read-only structured Buffer.
        /// This flag enables the use of a Buffer in a <see cref="ResourceSet"/> as a read-only structured Buffer.
        /// </summary>
        StructuredBufferReadOnly = 1 << 3,

        /// <summary>
        /// Indicates that a <see cref="DeviceBuffer"/> can be used as a read-write structured Buffer.
        /// This flag enables the use of a Buffer in a <see cref="ResourceSet"/> as a read-write structured Buffer.
        /// </summary>
        StructuredBufferReadWrite = 1 << 4,

        /// <summary>
        /// Indicates that a <see cref="DeviceBuffer"/> can be used as the source of indirect drawing information.
        /// This flag enables the use of a Buffer in the *Indirect methods of <see cref="CommandList"/>.
        /// </summary>
        IndirectBuffer = 1 << 5,

        /// <summary>
        /// Indicates that a <see cref="DeviceBuffer"/> will be updated with new data very frequently. Dynamic Buffers are able to be
        /// mapped with <see cref="MapMode.Write"/>.
        /// </summary>
        Dynamic = 1 << 6,

        /// <summary>
        /// Indicates that a <see cref="DeviceBuffer"/> will be used as a staging Buffer. Staging Buffers can be used to transfer data
        /// to-and-from the CPU using <see cref="GraphicsDevice.Map(MappableResource, MapMode)"/>. Staging Buffers can use all
        /// <see cref="MapMode"/> values.
        /// If this flag is present, no other flags are permitted.
        /// </summary>
        Staging = 1 << 7,
    }
}