using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    public AudioClip lv;
    public AudioClip death;
    public AudioClip win;
    public AudioClip collectCoin;
    public AudioClip coinChest;
    public AudioClip collectBlood;
    public AudioClip hit;
    public AudioClip jump;
    public AudioClip throwSlippers;
    public AudioClip attack1;
    public AudioClip boom;
    public AudioClip hitEnemy;
    public AudioClip fallingTrap;
    public AudioClip box;
    public AudioClip click;

    void Start()
    {
        musicSource.clip = lv;
        musicSource.Play();
    }


    public void PlaySFX(AudioClip audioClip)
    {
        SFXSource.PlayOneShot(audioClip);
    }
}
