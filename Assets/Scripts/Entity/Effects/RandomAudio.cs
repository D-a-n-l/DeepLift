using UnityEngine;

public class RandomAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource mainSource;

    [SerializeField]
    private PresetSound[] sound;

    private void Awake()
    {
        int index = Random.Range(0, sound.Length);

        mainSource.clip = sound[index].audioClip;

        mainSource.Play();
    }
}