using Engine.Rendering;
using Engine.Rendering.D3D11;
using Engine.Utilities;
using Engine.Windowing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Core
{
    public abstract class MyGame
    {
        private int _minFps;
        private int _maxFps;
        private Window _window;
        private string[] _startArgs;
        private TimerTick _timer;
        private long _trianglesDrawn;
        private TimeSpan _maximumElapsedTime;
        private int[] _lastUpdateCount;
        private int _nextLastUpdateCountIndex;
        private float _updateCountAverageSlowLimit;
        private double _elapsedTime;
        private bool _supressDraw;
        private TimeSpan _accumulatedElapsedGameTime;
        private TimeSpan _lastFrameElapsedGameTime;
        private int _totalFrames;
        public bool IsRunningSlowly { get; private set; }
        public TimeSpan TotalGameTime { get; private set; }
        public TimeSpan ElapsedGameTime { get; private set; }
        public int FrameCount { get; internal set; }

        public int MinFPS => _minFps;
        public int MaxFPS => _maxFps;

        public bool IsActive => !_window.IsMinimized;
        public bool IsFocued => _window.Focused;

        public bool IsFixedTimeStep { get; set; }

        public bool IsMouseVisible
        {
            get => _window.IsMouseVisible;
            set => _window.IsMouseVisible = value;
        }

        public bool IsRunning { get; private set; }
        public TimeSpan TargetElapsedTime { get; private set; }

        public Window Window => _window;
        public int FPS { get; private set; }
        public long TrianglesPerSecond { get; private set; }

        public string[] StartingArgs => _startArgs;

        public GraphicsDevice Device;
        public MyCore Core;
        public bool IsEverySecondFrame;
        public bool IsEveryThirdFrame;
        public bool IsEveryFourthFrame;

        protected MyGame(int targetFPS, string[] args)
        {
            IsRunning = false;
            _timer = new TimerTick();
            FPS = 0;
            TrianglesPerSecond = 0;
            _trianglesDrawn = 0;
            _startArgs = args;

            SetTargetFPS(targetFPS);

        }

        public void SetTargetFPS(int targetFPS)
        {
            if(targetFPS <= 0)
            {
                IsFixedTimeStep = false;
                return;
            }

            IsFixedTimeStep = true;
            _maximumElapsedTime = TimeSpan.FromMilliseconds(500);
            TargetElapsedTime = TimeSpan.FromTicks((long)10000000 / targetFPS);
            _lastUpdateCount = new int[4];
            _nextLastUpdateCountIndex = 0;
            int min = 2 * Math.Min(2, _lastUpdateCount.Length);
            _updateCountAverageSlowLimit = (float)(min + (_lastUpdateCount.Length - min)) / _lastUpdateCount.Length;
        }

        protected void CreateDeviceAndWindow()
        {
            WindowCreateInfo ci = new WindowCreateInfo
            {
                WindowWidth = 1280,
                WindowHeight = 720,
                WindowInitialState = WindowState.Normal,
                WindowTitle = "Title",
                X = 100,
                Y = 100
            };
            _window = new Window(ci);

            GraphicsDeviceOptions gdo = new GraphicsDeviceOptions();
            SwapchainDescription swapchainDescription = new SwapchainDescription(
            SwapchainSource.CreateWin32(_window.Handle, IntPtr.Zero),
            (uint)ci.WindowWidth, (uint)ci.WindowHeight,
            gdo.SwapchainDepthFormat,
            gdo.SyncToVerticalBlank);

            Device = new D3D11GraphicsDevice(gdo, swapchainDescription);
        }

        public abstract void Initialize();

        internal void InitializeBeforeRun()
        {
            if(Device == null)
            {
                throw new MeteorException("No Graphics Device found.");
            }
            IsRunning = true;
            _timer.Reset();
            TotalGameTime = TimeSpan.Zero;
            ElapsedGameTime = TimeSpan.Zero;
            IsRunningSlowly = false;
            FrameCount = 0;
            _elapsedTime = 0.0;
            _maxFps = int.MinValue;
            _minFps = int.MaxValue;
            Update();
        }

        public void SupressDraw()
        {
            _supressDraw = true;
        }

        public void Run()
        {
            Initialize();
            _window.InitCallback += InitializeBeforeRun;
            _window.RunCallback += Tick;
            Core.BeforeRun();
            _window.Run();
            OnExit();
        }

        internal void Tick()
        {
            if (!Core.IsReady)
                return;

            _timer.Tick();
            TimeSpan elapsed = _timer.ElapsedAdjustedTime;

            if(elapsed > _maximumElapsedTime)
                elapsed = _maximumElapsedTime;

            bool supressDraw = true;
            int scheduledUPS = 1;
            TimeSpan targetElapsedTime;
            if (IsFixedTimeStep)
            {
                if(Math.Abs(elapsed.Ticks - TargetElapsedTime.Ticks) < TargetElapsedTime.Ticks >> 6)
                {
                    elapsed = TargetElapsedTime;
                }

                _accumulatedElapsedGameTime += elapsed;
                scheduledUPS = (int)(_accumulatedElapsedGameTime.Ticks / TargetElapsedTime.Ticks);
                if (scheduledUPS == 0)
                    return;

                _lastUpdateCount[_nextLastUpdateCountIndex] = scheduledUPS;
                float averageUPS = 0;
                for (int i = 0; i < _lastUpdateCount.Length; i++)
                {
                    averageUPS += _lastUpdateCount[i];
                }
                averageUPS /= _lastUpdateCount.Length;
                _nextLastUpdateCountIndex = (_nextLastUpdateCountIndex + 1) % _lastUpdateCount.Length;
                IsRunningSlowly = averageUPS > _updateCountAverageSlowLimit;
                _accumulatedElapsedGameTime = new TimeSpan(_accumulatedElapsedGameTime.Ticks - (scheduledUPS * TargetElapsedTime.Ticks));
                targetElapsedTime = TargetElapsedTime;
            }
            else
            {
                Array.Clear(_lastUpdateCount, 0, _lastUpdateCount.Length);
                _nextLastUpdateCountIndex = 0;
                IsRunningSlowly = false;
            }

            _lastFrameElapsedGameTime = TimeSpan.Zero;

            while (scheduledUPS > 0 && Core.IsReady)
            {
                ElapsedGameTime = targetElapsedTime;
                //Update debug timers
                try
                {
                    Update();
                    // Update content pool
                    // Update Game manager
                    supressDraw &= _supressDraw;
                    _supressDraw = false;
                }
                finally
                {
                    _lastFrameElapsedGameTime = targetElapsedTime;
                    TotalGameTime += targetElapsedTime;
                }
                scheduledUPS--;
            }
            if (!supressDraw)
            {
                DrawFrame();
            }
        }

        private void DrawFrame()
        {
            if(Core.IsReady && IsActive)
            {
                try
                {
                    ElapsedGameTime = _lastFrameElapsedGameTime;
                    FrameCount++;
                    IsEverySecondFrame = FrameCount % 2 == 0;
                    IsEveryThirdFrame  = FrameCount % 3 == 0;
                    IsEveryFourthFrame = FrameCount % 4 == 0;
                    _elapsedTime += ElapsedGameTime.TotalSeconds;
                    _totalFrames++;
                    // trianglesDrawn ++
                    if(_elapsedTime >= 1)
                    {
                        if (_totalFrames < _minFps)
                            _minFps = _totalFrames;

                        if (_totalFrames > _maxFps)
                            _maxFps = _totalFrames;

                        FPS = _totalFrames;
                        TrianglesPerSecond = _trianglesDrawn;
                        _trianglesDrawn = 0;
                        _elapsedTime = 0;
                    }
                    //TODO: Create Rendering contexts
                    Core.Rendering.Render();
                }
                catch (Exception ex)
                {
                    Core.SendException(ex);
                    Core.SendMsg(MessageType.Error, 83, ex.Message);
                }
                finally
                {
                    _lastFrameElapsedGameTime = TimeSpan.Zero;
                }
            }
        }

        public abstract void Update();
        protected virtual void OnExit()
        {
            Device?.Dispose();
            IsRunning = false;
        }
    }
}
