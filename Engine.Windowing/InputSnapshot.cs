﻿using System.Collections.Generic;
using System.Numerics;

namespace Engine.Windowing
{
    public interface InputSnapshot
    {
        IReadOnlyList<KeyEvent> KeyEvents { get; }
        IReadOnlyList<MouseEvent> MouseEvents { get; }
        IReadOnlyList<char> KeyCharPresses { get; }

        bool IsMouseDown(MouseButton button);

        Vector2 MousePosition { get; }
        float WheelDelta { get; }
    }
}