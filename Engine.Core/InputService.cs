﻿using Engine.Core;
using Engine.Windowing;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Engine
{
    public sealed class InputService : Service
    {
        private HashSet<Key> _currentlyPressedKeys = new HashSet<Key>();
        private HashSet<Key> _newKeysThisFrame = new HashSet<Key>();

        private HashSet<MouseButton> _currentlyPressedMouseButtons = new HashSet<MouseButton>();
        private HashSet<MouseButton> _newMouseButtonsThisFrame = new HashSet<MouseButton>();

        public Vector2 MousePosition;
        public static InputSnapshot FrameSnapshot { get; private set; }

        public InputService(MyCore core) : base(core)
        {

        }

        public void Update(InputSnapshot snapshot)
        {
            FrameSnapshot = snapshot;
            _newKeysThisFrame.Clear();
            _newMouseButtonsThisFrame.Clear();

            MousePosition = snapshot.MousePosition;
            for (int i = 0; i < snapshot.KeyEvents.Count; i++)
            {
                KeyEvent ke = snapshot.KeyEvents[i];
                if (ke.Down)
                {
                    KeyDown(ke.Key);
                }
                else
                {
                    KeyUp(ke.Key);
                }
            }
            for (int i = 0; i < snapshot.MouseEvents.Count; i++)
            {
                MouseEvent me = snapshot.MouseEvents[i];
                if (me.Down)
                {
                    MouseDown(me.MouseButton);
                }
                else
                {
                    MouseUp(me.MouseButton);
                }
            }
        }

        public override void Dispose()
        {
        }

        public bool GetKey(Key key)
        {
            return _currentlyPressedKeys.Contains(key);
        }

        public bool GetKeyDown(Key key)
        {
            return _newKeysThisFrame.Contains(key);
        }

        public bool GetMouseButton(MouseButton button)
        {
            return _currentlyPressedMouseButtons.Contains(button);
        }

        public bool GetMouseButtonDown(MouseButton button)
        {
            return _newMouseButtonsThisFrame.Contains(button);
        }

        private void MouseUp(MouseButton mouseButton)
        {
            _currentlyPressedMouseButtons.Remove(mouseButton);
            _newMouseButtonsThisFrame.Remove(mouseButton);
        }

        private void MouseDown(MouseButton mouseButton)
        {
            if (_currentlyPressedMouseButtons.Add(mouseButton))
            {
                _newMouseButtonsThisFrame.Add(mouseButton);
            }
        }

        private void KeyUp(Key key)
        {
            _currentlyPressedKeys.Remove(key);
            _newKeysThisFrame.Remove(key);
        }

        private void KeyDown(Key key)
        {
            if (_currentlyPressedKeys.Add(key))
            {
                _newKeysThisFrame.Add(key);
            }
        }

        internal bool Init()
        {
            return false;
        }
    }
}
