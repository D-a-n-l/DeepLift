using UnityEngine;

public class ChangeTime : MonoBehaviour
{
    public static void Set(float value) => Time.timeScale = value;
}