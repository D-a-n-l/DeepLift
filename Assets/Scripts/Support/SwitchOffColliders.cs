using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOffColliders : MonoBehaviour
{
    [SerializeField]
    private Collider2D[] colliders;

    public void Set(bool isEnable)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = isEnable;
        }
    }

    public IEnumerator SetWithDelay(bool isEnable, float delay)
    {
        yield return new WaitForSeconds(delay);

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = isEnable;
        }
    }
}