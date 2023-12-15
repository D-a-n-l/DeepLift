using UnityEngine;

public class ChangeMode : MonoBehaviour
{
    private void Start()
    {
        Physics2D.simulationMode = SimulationMode2D.FixedUpdate;
    }
}