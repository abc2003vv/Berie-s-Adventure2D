using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioSFX;

    public AudioClip bg;
    public AudioClip click;

    void Start()
    {
        audioSource.clip = bg;
        audioSource.Play();
    }

    public void PlayChosse(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
