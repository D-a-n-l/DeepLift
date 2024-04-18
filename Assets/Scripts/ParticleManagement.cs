using UnityEngine;
using UnityEngine.Events;

public class ParticleManagement : MonoBehaviour
{
    [SerializeField]
    private PresetParticle[] particles;

    [Space(10)]
    [SerializeField]
    private PresetActions actions;

    [HideInInspector]
    public UnityEvent OnSpawn;

    private void Start()
    {
        if (actions.healthControl != null)
        {
            if (actions.eventHealthControl == Enums.TypeEventHealthControl.GetHeal)
                actions.healthControl.OnGetHeal.AddListener(Spawn);
            else if (actions.eventHealthControl == Enums.TypeEventHealthControl.GetDamage)
                actions.healthControl.OnGetDamage.AddListener(Spawn);
            else
                actions.healthControl.OnDead.AddListener(Spawn);
        }

        if (actions.shooting != null)
            actions.shooting.OnShot.AddListener(Spawn);

        if (actions.sprint != null)
            actions.sprint.OnSprint.AddListener(Spawn);
    }

    public void Spawn()
    {
        for (int i = 0; i < particles.Length; i++)
        {
            if (particles[i].spawnPosition != null)
                Instantiate(particles[i].particle, particles[i].spawnPosition.position, particles[i].spawnPosition.rotation);
            else
                Instantiate(particles[i].particle, particles[i].dynamicSpawnPosition, Quaternion.Euler(particles[i].dynamicSpawnRotation));
        }

        OnSpawn?.Invoke();
    }

    public void SetPositionAndRotation(Vector2 newPosition, float newRotation)
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].dynamicSpawnPosition = newPosition;
            particles[i].dynamicSpawnRotation = new (0f, newRotation, 0f);
        }
    }
}

[System.Serializable]
public struct PresetParticle
{
    public ParticleSystem particle;

    public Transform spawnPosition;

    public Vector2 dynamicSpawnPosition;

    public Vector3 dynamicSpawnRotation;
}