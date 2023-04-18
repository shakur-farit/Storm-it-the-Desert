using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayRandomAudio : MonoBehaviour
{
    public AudioClip[] audioClips;
    public float minPitch = 0.8f, maxPitch = 1.1f;
    public bool playOnAwake = true;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (playOnAwake)
            PlayAudio(Random.Range(0, audioClips.Length));
    }

    public  void PlayAudio(int audioId)
    {
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(audioClips[audioId]);
    }
}
