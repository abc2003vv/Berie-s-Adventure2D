using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinVFX : MonoBehaviour
{
    public ParticleSystem coinParticleSystem;
    public ParticleSystem pigCoinParticleSystem;

    void Start()
    {

    }

    public void PlayVFX(Vector3 position)
    {
        transform.position = position;
        coinParticleSystem.Play();
    }

    public void PlayPigCoinVFX(Vector3 position)
    {
        transform.position = position;
        pigCoinParticleSystem.Play();
    }
}
