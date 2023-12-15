using UnityEngine;

public class LightEnabled : MonoBehaviour
{
    [SerializeField] Light flashLight;

    private void OnTriggerEnter2D(Collider2D col)
    {
        EnabledOrDisableLight(col, true);
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        EnabledOrDisableLight(col, false);
    }

    private void EnabledOrDisableLight(Collider2D col, bool trueOrFalse)
    {
        if (col.CompareTag("Player")) { flashLight.enabled = trueOrFalse; }
    }
}