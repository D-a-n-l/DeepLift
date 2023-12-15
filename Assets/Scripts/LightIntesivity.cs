using UnityEngine;

public class LightIntesivity : MonoBehaviour
{
    [SerializeField, Tooltip("�� ������ ����� ����� �������� �������������"), Min(1)] private float intensivityChange = 1f;
    [SerializeField, Tooltip("��� ������ ����� �������� �������������"), Min(1)] private float timeIntensivityChange = 1f;

    private Light _light;

    private void Start()
    {
        _light = GetComponent<Light>();
    }

    private void Update()
    {
        _light.intensity = Mathf.PingPong(Time.time * timeIntensivityChange, intensivityChange);
    }
}