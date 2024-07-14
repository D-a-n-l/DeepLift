using NTC.Pool;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField]
    private HealthControl healthControl;

    private void Start()
    {
        healthControl.OnDead.AddListener(On);
    }

    public void On()
    {
        NightPool.Despawn(gameObject);
    }
}