using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private Slider _healthSlider;

    private float _currentHealth;
    
    private void Start() 
    {
        _currentHealth = _maxHealth;
        _healthSlider.maxValue = _maxHealth;
        _healthSlider.value = _maxHealth;
    }

    private void OnParticleCollision(GameObject other)
    {
        float damage = other.GetComponentInParent<Weapon>().Damage;
        TakeDamage(damage);
    }

    private void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        _healthSlider.value = _currentHealth;
        if (_currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
