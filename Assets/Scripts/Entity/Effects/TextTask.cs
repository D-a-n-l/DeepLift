using UnityEngine;
using System.Collections;

public class TextTask : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private float time;

    private IEnumerator Start()
    {
        canvas.enabled = true;

        yield return new WaitForSeconds(time);

        canvas.enabled = false;
    }
}