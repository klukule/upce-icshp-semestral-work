﻿using SharpDX.Direct3D11;
using System;

namespace Engine.Rendering.D3D11
{
    internal class D3D11ResourceFactory : ResourceFactory, IDisposable
    {
        private readonly D3D11GraphicsDevice _gd;
        private readonly Device _device;
        private readonly D3D11ResourceCache _cache;

        public override GraphicsBackend BackendType => GraphicsBackend.Direct3D11;

        public D3D11ResourceFactory(D3D11GraphicsDevice gd)
        {
            _gd = gd;
            _device = gd.Device;
            _cache = new D3D11ResourceCache(_device);
        }

        public override CommandList CreateCommandList(ref CommandListDescription description)
        {
            return new D3D11CommandList(_gd, ref description);
        }

        public override Framebuffer CreateFramebuffer(ref FramebufferDescription description)
        {
            return new D3D11Framebuffer(_device, ref description);
        }

        public override Pipeline CreateGraphicsPipeline(ref GraphicsPipelineDescription description)
        {
            return new D3D11Pipeline(_cache, ref description);
        }

        public override Pipeline CreateComputePipeline(ref ComputePipelineDescription description)
        {
            return new D3D11Pipeline(_cache, ref description);
        }

        public override ResourceLayout CreateResourceLayout(ref ResourceLayoutDescription description)
        {
            return new D3D11ResourceLayout(ref description);
        }

        public override ResourceSet CreateResourceSet(ref ResourceSetDescription description)
        {
            return new D3D11ResourceSet(ref description);
        }

        public override Sampler CreateSampler(ref SamplerDescription description)
        {
            return new D3D11Sampler(_device, ref description);
        }

        public override Shader CreateShader(ref ShaderDescription description)
        {
            return new D3D11Shader(_device, description);
        }

        protected override Texture CreateTextureCore(ref TextureDescription description)
        {
            return new D3D11Texture(_device, ref description);
        }

        protected override TextureView CreateTextureViewCore(ref TextureViewDescription description)
        {
            return new D3D11TextureView(_device, ref description);
        }

        protected override DeviceBuffer CreateBufferCore(ref BufferDescription description)
        {
            return new D3D11Buffer(_device, description.SizeInBytes, description.Usage, description.StructureByteStride);
        }

        public override Fence CreateFence(bool signaled)
        {
            return new D3D11Fence(signaled);
        }

        public override Swapchain CreateSwapchain(ref SwapchainDescription description)
        {
            return new D3D11Swapchain(_device, ref description);
        }

        public void Dispose()
        {
            _cache.Dispose();
        }
    }
}