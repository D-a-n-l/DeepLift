using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    [SerializeField]
    private PresetSound[] sound;

    [Space(10)]
    [SerializeField]
    private PresetActions actions;

    private void Start()
    {
        if (actions.healthControl != null)
        {
            if (actions.eventHealthControl == Enums.TypeEventHealthControl.GetHeal)
                actions.healthControl.OnGetHeal.AddListener(Play);
            else if (actions.eventHealthControl == Enums.TypeEventHealthControl.GetDamage)
                actions.healthControl.OnGetDamage.AddListener(Play);
            else
                actions.healthControl.OnDead.AddListener(Play);
        }

        if (actions.shooting != null)
            actions.shooting.OnShot.AddListener(Play);

        if (actions.sprint != null)
            actions.sprint.OnSprint.AddListener(Play);

        if (actions.particle != null)
            actions.particle.OnSpawn.AddListener(Play);
    }

    public void Play()
    {
        for (int i = 0; i < sound.Length; i++)
        {
            if (sound[i].maxAmountPlay != 0 && sound[i].currentAmountPlay >= sound[i].maxAmountPlay)
                return;

            if (sound[i].defaultPitch == false)
                sound[i].source.pitch = Random.Range(sound[i].pitch.min, sound[i].pitch.max);

            if (sound[i].playOneShot == true)
                sound[i].source.PlayOneShot(sound[i].audioClip);
            else
            {
                sound[i].source.clip = sound[i].audioClip;

                sound[i].source.Play();
            }

            if (sound[i].maxAmountPlay != 0)
                sound[i].currentAmountPlay++;
        }
    }

    public void PlayWithChangeSpitch(float newSpitch)
    {
        for (int i = 0; i < sound.Length; i++)
        {
            if (sound[i].maxAmountPlay != 0 && sound[i].currentAmountPlay >= sound[i].maxAmountPlay)
                return;

            if (sound[i].defaultPitch == false)
                sound[i].source.pitch = newSpitch;

            if (sound[i].playOneShot == true)
                sound[i].source.PlayOneShot(sound[i].audioClip);
            else
            {
                sound[i].source.clip = sound[i].audioClip;

                sound[i].source.Play();
            }

            if (sound[i].maxAmountPlay != 0)
                sound[i].currentAmountPlay++;
        }
    }
}

[System.Serializable]
public struct PresetSound
{
    public AudioSource source;

    public AudioClip audioClip;

    [Min(0)]
    public int maxAmountPlay;

    [HideInInspector]
    public int currentAmountPlay;

    public bool playOneShot;

    public bool defaultPitch;

    public PresetPitch pitch;
}

[System.Serializable]
public struct PresetPitch
{
    public float min;

    public float max;
}