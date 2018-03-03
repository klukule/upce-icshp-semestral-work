using Engine.Debug;
using Engine.Rendering;
using Engine.Utilities;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace Engine.Core
{
    public sealed class MyCore
    {
        public static readonly string DefaultLanguage = "en";
        public static string EngineStartupPath;
        public static MyCore Instance;
        public static string LogFilePath;

        private GraphicsDevice _device;
        private bool _ready;
        private string _lang;
        private Thread _mainThread;
        private Process _currentProcess;
        private Action _coreReady;
        private MyGame _game;
        public DebugService Debug;
        public InputService Input;
        public RenderingService Rendering;
        public MyGame Game => _game;
        public string Language
        {
            get => _lang;
            set { _lang = value; Debug.LoadLanguage(_lang); }
        }

        public bool IsReady => _ready;
        public Process CurrentProcess => _currentProcess;
        public GraphicsDevice Device => _device;
        public MyCore(MyGame game, Action coreReady)
        {
            _ready = false;
            _lang = DefaultLanguage;
            _currentProcess = Process.GetCurrentProcess();
            _mainThread = Thread.CurrentThread;
            EngineStartupPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            _coreReady = coreReady;
            _game = game;
            Instance = this;
        }

        public void Initialization()
        {
            if (_ready)
                KillNow("Cannot init Core");

            //TODO: Load engine config

            _lang = DefaultLanguage;
            _device = _game.Device;
            Debug = new DebugService(this);
            Debug.LoadConfig("Config\\Debug.xml");
            SendMsg(MessageType.Info, 63, Language);
            if (Debug != null && !string.IsNullOrEmpty(Debug.LogFilePath))
            {
                LogFilePath = this.Debug.LogFilePath;
            }
            SendMsg(MessageType.Info, 38, "3.0");
            SendMsg(MessageType.Info, 30, CurrentProcess.Id, CurrentProcess.StartTime);
            SendMsg(MessageType.Info, 31, EngineStartupPath);
            // SendMsg(MessageType.Info, 32, PhysicalMemory / 1024/1024); //NOTE: No way to get Physical memory yet
            SendMsg(MessageType.Info, 33, Environment.ProcessorCount, Environment.MachineName);
            SendMsg(MessageType.Info, 34, RuntimeInformation.OSDescription, Environment.Is64BitOperatingSystem ? 64 : 32);
            SendMsg(MessageType.Info, 35, Environment.SystemDirectory);
            SendMsg(MessageType.Info, 36, Tools.BytesToText(Environment.SystemPageSize));
            SendMsg(MessageType.Info, 37, Environment.UserName);
            // Print GPU Info
            Debug.WriteGround();

            // Setup assimp
            // Create content pool
            // Load engine content

            Input = new InputService(this);
            Rendering = new RenderingService(this);

            SendMsg(MessageType.Info, 27);
            if (Debug.Init())
            {
                CannotInitService(Debug);
            }
            if (Rendering.Init())
            {
                CannotInitService(Rendering);
            }
            if (Input.Init())
            {
                CannotInitService(Input);
            }
            SendMsg(MessageType.Info, 28);
            // Init physics engine
            SendMsg(MessageType.Info, 26);
        }

        public void FinalPrepare()
        {
            if (_ready)
                return;
            SendMsg(MessageType.Info, 52);
            _ready = true;
            _coreReady?.Invoke();
            SendMsg(MessageType.Info, 53);
            Debug.WriteGround();
            // Parse startup args
            // Load game world
            // Focus game window
        }

        internal void BeforeRun()
        {
        }

        public void SendException(Exception ex)
        {
            SendMsg(MessageType.Warning, 192, ex.Message, ex.StackTrace);
        }

        private void CannotInitService(Service service)
        {
            Debug.Write(MessageType.Fatal, 9, service.ToString());
        }

        public void SendMsg(MessageType type, int msgIndex, params object[] args)
        {
            if(Debug != null)
            {
                Debug.Write(type, msgIndex, args);
                return;
            }
            if(type == MessageType.Fatal)
            {
                string text = msgIndex.ToString();
                text += ", Args: ";
                if(args == null || args.Length == 0){
                    text += "None.";
                }
                else
                {
                    int i = 0;
                    for (; i < args.Length -1; i++)
                    {
                        text += args[i].ToString() + ", ";
                    }
                    text += args[i].ToString() + ".";
                }
                KillNow("Fatal message. No Debug Service. message: " + text);
            }
        }

        public void KillNow() => KillNow(null);

        public void KillNow(string reason)
        {
            // Display message and send crash report
            Environment.Exit(-1);
        }
    }
}
