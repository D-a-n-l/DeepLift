using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public void ToDestroy(GameObject obj) => Destroy(obj);
}