using UnityEngine;

public class DisabledObjectsByPlatform : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _gameObjects;

    private void Awake()
    {
        if (Application.isMobilePlatform == false)
            for (int i = 0; i < _gameObjects.Length; i++)
                _gameObjects[i].SetActive(false);
    }
}