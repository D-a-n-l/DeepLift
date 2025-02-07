﻿using System;

namespace Assets.PlayId.Scripts.Enums
{
    [Flags]
    public enum Platform
    {
        Any = 0,
        Google = 1,
        Apple = 2,
        Facebook = 4,
        X = 8,
        Telegram = 16,
        Microsoft = 32,
        VK = 64,
        Discord
    }
}