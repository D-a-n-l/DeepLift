using UnityEngine;

public class ControlActionsByKeyCode : MonoBehaviour
{
    private void Awake() => SetIsCan(true);

    public void SetIsCan(bool value) => ActionsBy.IsCan = value;
}

public static class ActionsBy
{
    public static bool IsCan = true;
}