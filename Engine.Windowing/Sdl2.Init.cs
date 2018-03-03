﻿using System.Runtime.InteropServices;

namespace Engine.Windowing
{
    public static unsafe partial class Sdl2Native
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_Init_t(SDLInitFlags flags);

        private static SDL_Init_t s_sdl_init = LoadFunction<SDL_Init_t>("SDL_Init");

        public static int SDL_Init(SDLInitFlags flags) => s_sdl_init(flags);
    }

    public enum SDLInitFlags : uint
    {
        Video = 0x00000020u
    }
}