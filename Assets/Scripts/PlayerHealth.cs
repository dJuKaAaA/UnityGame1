using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _health = 100f;

    private void OnParticleCollision(GameObject other)
    {
        float damage = other.GetComponentInParent<Weapon>().Damage;
        TakeDamage(damage);
    }

    private void TakeDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
