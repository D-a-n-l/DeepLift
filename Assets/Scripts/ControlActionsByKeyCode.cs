using UnityEngine;

public class ControlActionsByKeyCode : MonoBehaviour
{
    public void SetIsCan(bool value) => ActionsBy.IsCan = value;
}

public static class ActionsBy
{
    public static bool IsCan = true;
}