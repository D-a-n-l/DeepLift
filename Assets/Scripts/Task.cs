using System.Collections;
using UnityEngine;

public class Task : MonoBehaviour
{
    [SerializeField] private GameObject task;

    private void Start()
    {
        StartCoroutine(SpawnTask());
    }

    private IEnumerator SpawnTask()
    {
        yield return new WaitForSeconds(10f);

        task.SetActive(false);
    }
}