using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpark : MonoBehaviour
{
    [SerializeField] private ParticleSystem _sparkParticles;

    private void OnParticleCollision(GameObject other)  
    {
        _sparkParticles.transform.position = other.transform.position;
        _sparkParticles.Play();
    }
}
