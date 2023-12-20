using UnityEngine;

public class LightIntesivity : MonoBehaviour
{
    [SerializeField, Tooltip("ƒо какого числа будет мен€тьс€ интенсивность"), Min(1)] 
    private float intensivityChange = 1f;

    [SerializeField, Tooltip(" ак быстро будет мен€тьс€ интенсивность"), Min(1)] 
    private float timeIntensivityChange = 1f;

    private Light light;

    private void Start()
    {
        light = GetComponent<Light>();
    }

    private void Update()
    {
        light.intensity = Mathf.PingPong(Time.time * timeIntensivityChange, intensivityChange);
    }
}