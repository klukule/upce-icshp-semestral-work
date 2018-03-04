using Engine.Core;
using Engine.Windowing;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Engine.Rendering
{
    public class Window : IDisposable
    {
        public readonly Sdl2Window NativeWindow;

        //TODO: Implement? Activated, Deactivated, ClientSizeChanged events

        public string Title { get => NativeWindow.Title; set => NativeWindow.Title = value; }
        public Rectangle ClientBounds => NativeWindow.Bounds;
        public bool IsMinimized => NativeWindow.WindowState == WindowState.Minimized;
        public bool IsMouseVisible { get => NativeWindow.CursorVisible; set => NativeWindow.CursorVisible = value; }
        public IntPtr Handle => NativeWindow.Handle;
        public int Width => NativeWindow.Width;
        public int Height => NativeWindow.Height;
        public bool Focused => NativeWindow.Focused;
        public bool Visible { get => NativeWindow.Visible; set => NativeWindow.Visible = value; }
        internal bool IsFullscreen => NativeWindow.WindowState == WindowState.FullScreen || NativeWindow.WindowState == WindowState.BorderlessFullScreen;

        public Action InitCallback;
        public Action RunCallback;
        public Action ExitCallback;


        //TODO: Implement these:
        public bool AllowUserResizing;
        public Vector2 MousePosition;
        public Vector2 MouseScreenPosition;

        public Window(WindowCreateInfo windowCI)
        {
            SDL_WindowFlags flags = SDL_WindowFlags.OpenGL | SDL_WindowFlags.Resizable | GetWindowFlags(windowCI.WindowInitialState);
            if(windowCI.WindowInitialState != WindowState.Hidden)
            {
                flags |= SDL_WindowFlags.Shown;
            }
            NativeWindow  = new Sdl2Window(windowCI.WindowTitle, windowCI.X, windowCI.Y, windowCI.WindowWidth, windowCI.WindowHeight, flags, false);
        }

        public void Run()
        {
            if (InitCallback == null || RunCallback == null)
            {
                throw new DataLeakException();
            }
            InitCallback();
            try
            {
                while (NativeWindow.Exists)
                {
                    MyCore.Instance.Input.Update(NativeWindow.PumpEvents());
                    RunCallback();
                }
            }
            finally
            {
                ExitCallback?.Invoke();
            }
         }

        public void Resize(int width, int height)
        {
            NativeWindow.Width = width;
            NativeWindow.Height = height;
        }

        public bool SetIcon(string path)
        {
            return true;
            //SDL_SetWindowIcon
            //throw new NotImplementedException("TODO: Implement");
        }

        public void Focus()
        {
            //SDL_SetWindowInputFocus
            //throw new NotImplementedException("TODO: Implement");
        }

        public void Exit()
        {
            NativeWindow.Close();
        }

        public void Dispose()
        {
        }

        private SDL_WindowFlags GetWindowFlags(WindowState state)
        {
            switch (state)
            {
                case WindowState.Normal:
                    return 0;
                case WindowState.FullScreen:
                    return SDL_WindowFlags.Fullscreen;
                case WindowState.Maximized:
                    return SDL_WindowFlags.Maximized;
                case WindowState.Minimized:
                    return SDL_WindowFlags.Minimized;
                case WindowState.BorderlessFullScreen:
                    return SDL_WindowFlags.FullScreenDesktop;
                case WindowState.Hidden:
                    return SDL_WindowFlags.Hidden;
                default:
                    throw new MeteorException("Invalid WindowState: " + state);
            }
        }
    }
    

    public struct WindowCreateInfo
    {
        public int X;
        public int Y;
        public int WindowWidth;
        public int WindowHeight;
        public WindowState WindowInitialState;
        public string WindowTitle;

        public WindowCreateInfo(
            int x,
            int y,
            int windowWidth,
            int windowHeight,
            WindowState windowInitialState,
            string windowTitle)
        {
            X = x;
            Y = y;
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
            WindowInitialState = windowInitialState;
            WindowTitle = windowTitle;
        }
    }
}
