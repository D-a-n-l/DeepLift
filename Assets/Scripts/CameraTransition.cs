using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    [SerializeField] private float time;

    private void Update()
    {
        var x =  Mathf.PingPong(Time.time * time, 22f);

        transform.position = new Vector3(x, 0f, -10f);
    }
}