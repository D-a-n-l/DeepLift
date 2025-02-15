using UnityEngine;

public class DisabledObjectsByPlatform : MonoBehaviour
{
    [SerializeField]
    private PresetObjects[] _presetObjectsForMobile;

    [Space(10)]
    [SerializeField]
    private PresetObjects[] _presetObjectsForPC;

    private void Awake()
    {
        if (Application.isMobilePlatform == true)
        {
            ManagementObjects(_presetObjectsForMobile);
        }
        else if (Application.isMobilePlatform == false)
        {
            ManagementObjects(_presetObjectsForPC);
        }
    }

    private void ManagementObjects(PresetObjects[] presetObjects)
    {
        foreach (var obj in presetObjects)
        {
            if (obj.gameObject != null)
                obj.gameObject.SetActive(obj.value);

            if (obj.canvas != null)
                obj.canvas.enabled = obj.value;
        }
    }
}

[System.Serializable]
public struct PresetObjects
{
    public GameObject gameObject;

    public Canvas canvas;

    public bool value;
}