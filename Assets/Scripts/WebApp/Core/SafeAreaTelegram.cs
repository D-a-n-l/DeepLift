using UnityEngine;
using System.Runtime.InteropServices;

public class SafeAreaTelegram : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern int GetSafeAreaTop();

    [DllImport("__Internal")]
    private static extern int GetSafeAreaRight();

    [DllImport("__Internal")]
    private static extern int GetSafeAreaLeft();

    [DllImport("__Internal")]
    private static extern int GetContentSafeAreaTop();

    [DllImport("__Internal")]
    private static extern int GetContentSafeAreaRight();

    [DllImport("__Internal")]
    private static extern int GetContentSafeAreaLeft();

    private void Start()
    {
        ApplySafeArea();
    }

    private void ApplySafeArea()
    {
        float right = GetSafeAreaRight() + GetContentSafeAreaRight();
        float left = GetSafeAreaLeft() + GetContentSafeAreaLeft();
        float top = GetSafeAreaTop() + GetContentSafeAreaTop();

        RectTransform canvasRect = GetComponent<RectTransform>();

        Vector2 offsetMax = new Vector2(-right * 2, -top);
        Vector2 offsetMin = new Vector2(left, 0);

        canvasRect.offsetMax += offsetMax;
        canvasRect.offsetMin += offsetMin;
    }

    private class SafeAreaData
    {
        public int top;

        public int right;

        public int left;

        public SafeAreaData(int top, int right, int left)
        {
            this.top = top;

            this.right = right;

            this.left = left;
        }
    }
}