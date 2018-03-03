﻿using System;

namespace Engine.Rendering
{
    /// <summary>
    /// A platform-specific object representing a renderable surface.
    /// A SwapchainSource can be created with one of several static factory methods.
    /// A SwapchainSource is used to describe a Swapchain (see <see cref="SwapchainDescription"/>).
    /// </summary>
    public abstract class SwapchainSource
    {
        internal SwapchainSource()
        {
        }

        /// <summary>
        /// Creates a new SwapchainSource for a Win32 window.
        /// </summary>
        /// <param name="hwnd">The Win32 window handle.</param>
        /// <param name="hinstance">The Win32 instance handle.</param>
        /// <returns>A new SwapchainSource which can be used to create a <see cref="Swapchain"/> for the given Win32 window.
        /// </returns>
        public static SwapchainSource CreateWin32(IntPtr hwnd, IntPtr hinstance) => new Win32SwapchainSource(hwnd, hinstance);

        /// <summary>
        /// Creates a new SwapchainSource for a UWP SwapChain panel.
        /// </summary>
        /// <param name="swapChainPanel">A COM object which must implement the <see cref="SharpDX.DXGI.ISwapChainPanelNative"/>
        /// or <see cref="SharpDX.DXGI.ISwapChainBackgroundPanelNative"/> interface. Generally, this should be a SwapChainPanel
        /// or SwapChainBackgroundPanel contained in your application window.</param>
        /// <param name="logicalDpi">The logical DPI of the swapchain panel.</param>
        /// <returns>A new SwapchainSource which can be used to create a <see cref="Swapchain"/> for the given UWP panel.
        /// </returns>
        public static SwapchainSource CreateUwp(object swapChainPanel, float logicalDpi)
            => new UwpSwapchainSource(swapChainPanel, logicalDpi);

        /// <summary>
        /// Creates a new SwapchainSource from the given Xlib information.
        /// </summary>
        /// <param name="display">An Xlib Display.</param>
        /// <param name="window">An Xlib Window.</param>
        /// <returns>A new SwapchainSource which can be used to create a <see cref="Swapchain"/> for the given Xlib window.
        /// </returns>
        public static SwapchainSource CreateXlib(IntPtr display, IntPtr window) => new XlibSwapchainSource(display, window);

        /// <summary>
        /// Creates a new SwapchainSource for the given NSWindow.
        /// </summary>
        /// <param name="nsWindow">A pointer to an NSWindow.</param>
        /// <returns>A new SwapchainSource which can be used to create a Metal <see cref="Swapchain"/> for the given NSWindow.
        /// </returns>
        public static SwapchainSource CreateNSWindow(IntPtr nsWindow) => new NSWindowSwapchainSource(nsWindow);

        /// <summary>
        /// Creates a new SwapchainSource for the given UIView.
        /// </summary>
        /// <param name="uiView">THe UIView's native handle.</param>
        /// <returns>A new SwapchainSource which can be used to create a Metal <see cref="Swapchain"/> for the given UIView.
        /// </returns>
        public static SwapchainSource CreateUIView(IntPtr uiView) => new UIViewSwapchainSource(uiView);
    }

    internal class Win32SwapchainSource : SwapchainSource
    {
        public IntPtr Hwnd { get; }
        public IntPtr Hinstance { get; }

        public Win32SwapchainSource(IntPtr hwnd, IntPtr hinstance)
        {
            Hwnd = hwnd;
            Hinstance = hinstance;
        }
    }

    internal class UwpSwapchainSource : SwapchainSource
    {
        public object SwapChainPanelNative { get; }
        public float LogicalDpi { get; }

        public UwpSwapchainSource(object swapChainPanelNative, float logicalDpi)
        {
            SwapChainPanelNative = swapChainPanelNative;
            LogicalDpi = logicalDpi;
        }
    }

    internal class XlibSwapchainSource : SwapchainSource
    {
        public IntPtr Display { get; }
        public IntPtr Window { get; }

        public XlibSwapchainSource(IntPtr display, IntPtr window)
        {
            Display = display;
            Window = window;
        }
    }

    internal class NSWindowSwapchainSource : SwapchainSource
    {
        public IntPtr NSWindow { get; }

        public NSWindowSwapchainSource(IntPtr nsWindow)
        {
            NSWindow = nsWindow;
        }
    }

    internal class UIViewSwapchainSource : SwapchainSource
    {
        public IntPtr UIView { get; }

        public UIViewSwapchainSource(IntPtr uiView)
        {
            UIView = uiView;
        }
    }
}