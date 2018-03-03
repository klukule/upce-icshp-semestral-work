﻿namespace Engine.Rendering
{
    /// <summary>
    /// A device object responsible for the creation of graphics resources.
    /// </summary>
    public abstract class ResourceFactory
    {
        /// <summary>
        /// Gets the <see cref="GraphicsBackend"/> of this instance.
        /// </summary>
        public abstract GraphicsBackend BackendType { get; }

        /// <summary>
        /// Creates a new <see cref="Pipeline"/>.
        /// </summary>
        /// <param name="description">The desired properties of the created object.</param>
        /// <returns>A new <see cref="Pipeline"/>.</returns>
        public Pipeline CreateGraphicsPipeline(GraphicsPipelineDescription description) => CreateGraphicsPipeline(ref description);

        /// <summary>
        /// Creates a new <see cref="Pipeline"/> object.
        /// </summary>
        /// <param name="description">The desired properties of the created object.</param>
        /// <returns>A new <see cref="Pipeline"/> which, when bound to a CommandList, is used to dispatch draw commands.</returns>
        public abstract Pipeline CreateGraphicsPipeline(ref GraphicsPipelineDescription description);

        /// <summary>
        /// Creates a new compute <see cref="Pipeline"/> object.
        /// </summary>
        /// <param name="description">The desirede properties of the created object.</param>
        /// <returns>A new <see cref="Pipeline"/> which, when bound to a CommandList, is used to dispatch compute commands.</returns>
        public Pipeline CreateComputePipeline(ComputePipelineDescription description) => CreateComputePipeline(ref description);

        /// <summary>
        /// Creates a new compute <see cref="Pipeline"/> object.
        /// </summary>
        /// <param name="description">The desirede properties of the created object.</param>
        /// <returns>A new <see cref="Pipeline"/> which, when bound to a CommandList, is used to dispatch compute commands.</returns>
        public abstract Pipeline CreateComputePipeline(ref ComputePipelineDescription description);

        /// <summary>
        /// Creates a new <see cref="Framebuffer"/>.
        /// </summary>
        /// <param name="description">The desired properties of the created object.</param>
        /// <returns>A new <see cref="Framebuffer"/>.</returns>
        public Framebuffer CreateFramebuffer(FramebufferDescription description) => CreateFramebuffer(ref description);

        /// <summary>
        /// Creates a new <see cref="Framebuffer"/>.
        /// </summary>
        /// <param name="description">The desired properties of the created object.</param>
        /// <returns>A new <see cref="Framebuffer"/>.</returns>
        public abstract Framebuffer CreateFramebuffer(ref FramebufferDescription description);

        /// <summary>
        /// Creates a new <see cref="Texture"/>.
        /// </summary>
        /// <param name="description">The desired properties of the created object.</param>
        /// <returns>A new <see cref="Texture"/>.</returns>
        public Texture CreateTexture(TextureDescription description) => CreateTexture(ref description);

        /// <summary>
        /// Creates a new <see cref="Texture"/>.
        /// </summary>
        /// <param name="description">The desired properties of the created object.</param>
        /// <returns>A new <see cref="Texture"/>.</returns>
        public Texture CreateTexture(ref TextureDescription description)
        {
            return CreateTextureCore(ref description);
        }

        // TODO: private protected
        /// <summary>
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        protected abstract Texture CreateTextureCore(ref TextureDescription description);

        /// <summary>
        /// Creates a new <see cref="TextureView"/>.
        /// </summary>
        /// <param name="target">The target <see cref="Texture"/> used in the new view.</param>
        /// <returns>A new <see cref="TextureView"/>.</returns>
        public TextureView CreateTextureView(Texture target) => CreateTextureView(new TextureViewDescription(target));

        /// <summary>
        /// Creates a new <see cref="TextureView"/>.
        /// </summary>
        /// <param name="description">The desired properties of the created object.</param>
        /// <returns>A new <see cref="TextureView"/>.</returns>
        public TextureView CreateTextureView(TextureViewDescription description) => CreateTextureView(ref description);

        /// <summary>
        /// Creates a new <see cref="TextureView"/>.
        /// </summary>
        /// <param name="description">The desired properties of the created object.</param>
        /// <returns>A new <see cref="TextureView"/>.</returns>
        public TextureView CreateTextureView(ref TextureViewDescription description)
        {
            return CreateTextureViewCore(ref description);
        }

        // TODO: private protected
        /// <summary>
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        protected abstract TextureView CreateTextureViewCore(ref TextureViewDescription description);

        /// <summary>
        /// Creates a new <see cref="DeviceBuffer"/>.
        /// </summary>
        /// <param name="description">The desired properties of the created object.</param>
        /// <returns>A new <see cref="DeviceBuffer"/>.</returns>
        public DeviceBuffer CreateBuffer(BufferDescription description) => CreateBuffer(ref description);

        /// <summary>
        /// Creates a new <see cref="DeviceBuffer"/>.
        /// </summary>
        /// <param name="description">The desired properties of the created object.</param>
        /// <returns>A new <see cref="DeviceBuffer"/>.</returns>
        public DeviceBuffer CreateBuffer(ref BufferDescription description)
        {
            return CreateBufferCore(ref description);
        }

        // TODO: private protected
        /// <summary>
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        protected abstract DeviceBuffer CreateBufferCore(ref BufferDescription description);

        /// <summary>
        /// Creates a new <see cref="Sampler"/>.
        /// </summary>
        /// <param name="description">The desired properties of the created object.</param>
        /// <returns>A new <see cref="Sampler"/>.</returns>
        public Sampler CreateSampler(SamplerDescription description) => CreateSampler(ref description);

        /// <summary>
        /// Creates a new <see cref="Sampler"/>.
        /// </summary>
        /// <param name="description">The desired properties of the created object.</param>
        /// <returns>A new <see cref="Sampler"/>.</returns>
        public abstract Sampler CreateSampler(ref SamplerDescription description);

        /// <summary>
        /// Creates a new <see cref="Shader"/>.
        /// </summary>
        /// <param name="description">The desired properties of the created object.</param>
        /// <returns>A new <see cref="Shader"/>.</returns>
        public Shader CreateShader(ShaderDescription description) => CreateShader(ref description);

        /// <summary>
        /// Creates a new <see cref="Shader"/>.
        /// </summary>
        /// <param name="description">The desired properties of the created object.</param>
        /// <returns>A new <see cref="Shader"/>.</returns>
        public abstract Shader CreateShader(ref ShaderDescription description);

        /// <summary>
        /// Creates a new <see cref="CommandList"/>.
        /// </summary>
        /// <returns>A new <see cref="CommandList"/>.</returns>
        public CommandList CreateCommandList() => CreateCommandList(new CommandListDescription());

        /// <summary>
        /// Creates a new <see cref="CommandList"/>.
        /// </summary>
        /// <param name="description">The desired properties of the created object.</param>
        /// <returns>A new <see cref="CommandList"/>.</returns>
        public CommandList CreateCommandList(CommandListDescription description) => CreateCommandList(ref description);

        /// <summary>
        /// Creates a new <see cref="CommandList"/>.
        /// </summary>
        /// <param name="description">The desired properties of the created object.</param>
        /// <returns>A new <see cref="CommandList"/>.</returns>
        public abstract CommandList CreateCommandList(ref CommandListDescription description);

        /// <summary>
        /// Creates a new <see cref="ResourceLayout"/>.
        /// </summary>
        /// <param name="description">The desired properties of the created object.</param>
        /// <returns>A new <see cref="ResourceLayout"/>.</returns>
        public ResourceLayout CreateResourceLayout(ResourceLayoutDescription description) => CreateResourceLayout(ref description);

        /// <summary>
        /// Creates a new <see cref="ResourceLayout"/>.
        /// </summary>
        /// <param name="description">The desired properties of the created object.</param>
        /// <returns>A new <see cref="ResourceLayout"/>.</returns>
        public abstract ResourceLayout CreateResourceLayout(ref ResourceLayoutDescription description);

        /// <summary>
        /// Creates a new <see cref="ResourceSet"/>.
        /// </summary>
        /// <param name="description">The desired properties of the created object.</param>
        /// <returns>A new <see cref="ResourceSet"/>.</returns>
        public ResourceSet CreateResourceSet(ResourceSetDescription description) => CreateResourceSet(ref description);

        /// <summary>
        /// Creates a new <see cref="ResourceSet"/>.
        /// </summary>
        /// <param name="description">The desired properties of the created object.</param>
        /// <returns>A new <see cref="ResourceSet"/>.</returns>
        public abstract ResourceSet CreateResourceSet(ref ResourceSetDescription description);

        /// <summary>
        /// Creates a new <see cref="Fence"/> in the given state.
        /// </summary>
        /// <param name="signaled">A value indicating whether the Fence should be in the signaled state when created.</param>
        /// <returns>A new <see cref="Fence"/>.</returns>
        public abstract Fence CreateFence(bool signaled);

        public Swapchain CreateSwapchain(SwapchainDescription description) => CreateSwapchain(ref description);

        public abstract Swapchain CreateSwapchain(ref SwapchainDescription description);
    }
}