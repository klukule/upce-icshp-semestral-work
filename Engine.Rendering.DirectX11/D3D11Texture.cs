﻿using SharpDX.Direct3D11;
using System.Diagnostics;

namespace Engine.Rendering.D3D11
{
    internal class D3D11Texture : Texture
    {
        private string _name;

        public override uint Width { get; }
        public override uint Height { get; }
        public override uint Depth { get; }
        public override uint MipLevels { get; }
        public override uint ArrayLayers { get; }
        public override PixelFormat Format { get; }
        public override TextureUsage Usage { get; }
        public override TextureType Type { get; }
        public override TextureSampleCount SampleCount { get; }

        public Resource DeviceTexture { get; }
        public SharpDX.DXGI.Format DxgiFormat { get; }

        public D3D11Texture(Device device, ref TextureDescription description)
        {
            Width = description.Width;
            Height = description.Height;
            Depth = description.Depth;
            MipLevels = description.MipLevels;
            ArrayLayers = description.ArrayLayers;
            Format = description.Format;
            Usage = description.Usage;
            Type = description.Type;
            SampleCount = description.SampleCount;

            DxgiFormat = D3D11Formats.ToDxgiFormat(
                description.Format,
                (description.Usage & TextureUsage.DepthStencil) == TextureUsage.DepthStencil);

            CpuAccessFlags cpuFlags = CpuAccessFlags.None;
            ResourceUsage resourceUsage = ResourceUsage.Default;
            BindFlags bindFlags = BindFlags.None;

            if ((description.Usage & TextureUsage.RenderTarget) == TextureUsage.RenderTarget)
            {
                bindFlags |= BindFlags.RenderTarget;
            }
            if ((description.Usage & TextureUsage.DepthStencil) == TextureUsage.DepthStencil)
            {
                bindFlags |= BindFlags.DepthStencil;
            }
            if ((description.Usage & TextureUsage.Sampled) == TextureUsage.Sampled)
            {
                bindFlags |= BindFlags.ShaderResource;
            }
            if ((description.Usage & TextureUsage.Storage) == TextureUsage.Storage)
            {
                bindFlags |= BindFlags.UnorderedAccess;
            }
            if ((description.Usage & TextureUsage.Staging) == TextureUsage.Staging)
            {
                cpuFlags = CpuAccessFlags.Read | CpuAccessFlags.Write;
                resourceUsage = ResourceUsage.Staging;
            }

            ResourceOptionFlags optionFlags = ResourceOptionFlags.None;
            int arraySize = (int)description.ArrayLayers;
            if ((description.Usage & TextureUsage.Cubemap) == TextureUsage.Cubemap)
            {
                optionFlags = ResourceOptionFlags.TextureCube;
                arraySize *= 6;
            }

            if (Type == TextureType.Texture1D)
            {
                Texture1DDescription desc1D = new Texture1DDescription()
                {
                    Width = (int)description.Width,
                    MipLevels = (int)description.MipLevels,
                    ArraySize = arraySize,
                    Format = DxgiFormat,
                    BindFlags = bindFlags,
                    CpuAccessFlags = cpuFlags,
                    Usage = resourceUsage,
                    OptionFlags = optionFlags,
                };

                DeviceTexture = new Texture1D(device, desc1D);
            }
            else if (Type == TextureType.Texture2D)
            {
                Texture2DDescription deviceDescription = new Texture2DDescription()
                {
                    Width = (int)description.Width,
                    Height = (int)description.Height,
                    MipLevels = (int)description.MipLevels,
                    ArraySize = arraySize,
                    Format = DxgiFormat,
                    BindFlags = bindFlags,
                    CpuAccessFlags = cpuFlags,
                    Usage = resourceUsage,
                    SampleDescription = new SharpDX.DXGI.SampleDescription((int)FormatHelpers.GetSampleCountUInt32(SampleCount), 0),
                    OptionFlags = optionFlags,
                };

                DeviceTexture = new Texture2D(device, deviceDescription);
            }
            else
            {
                Debug.Assert(Type == TextureType.Texture3D);
                Texture3DDescription desc3D = new Texture3DDescription()
                {
                    Width = (int)description.Width,
                    Height = (int)description.Height,
                    Depth = (int)description.Depth,
                    MipLevels = (int)description.MipLevels,
                    Format = DxgiFormat,
                    BindFlags = bindFlags,
                    CpuAccessFlags = cpuFlags,
                    Usage = resourceUsage,
                    OptionFlags = optionFlags,
                };

                DeviceTexture = new Texture3D(device, desc3D);
            }
        }

        public D3D11Texture(Texture2D existingTexture)
        {
            DeviceTexture = existingTexture;
            Width = (uint)existingTexture.Description.Width;
            Height = (uint)existingTexture.Description.Height;
            Depth = 1;
            MipLevels = (uint)existingTexture.Description.MipLevels;
            ArrayLayers = (uint)existingTexture.Description.ArraySize;
            Format = D3D11Formats.ToEngineFormat(existingTexture.Description.Format);
            SampleCount = D3D11Formats.ToEngineSampleCount(existingTexture.Description.SampleDescription);
        }

        public override string Name
        {
            get => _name;
            set
            {
                _name = value;
                DeviceTexture.DebugName = value;
            }
        }

        public override void Dispose()
        {
            DeviceTexture.Dispose();
        }
    }
}