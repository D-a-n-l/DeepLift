using UnityEngine.Events;
using UnityEngine;

namespace Enums
{
    public enum TypeAnimationManagement
    {
        Player,
        Button,
        PlayerAndButton
    }

    public enum TypeMovingButton
    {
        Joystick,
        Shoot,
        Sprint
    }

    public enum TypeSliderChanging
    {
        JustComponent,
        Music,
        Image,
        FrameRate
    }

    public enum TypeEventHealthControl
    {
        GetHeal,
        GetDamage,
        Dead
    }
}