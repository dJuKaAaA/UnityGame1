using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
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
            Destroy(gameObject);
        }
    }
}