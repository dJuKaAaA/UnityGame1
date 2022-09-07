using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _stoppingRange = 10;
    [SerializeField] private float _aggroRange = 50;
    public float AggroRange => _aggroRange;
    private NavMeshAgent _navMeshAgent;
    private float _distanceToTarget;
    private bool _provoked = false;
    public bool Provoked => _provoked;

    private void Start() 
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() 
    {
        _distanceToTarget = Vector3.Magnitude(_target.position - transform.position);
        if (_distanceToTarget < _aggroRange && _distanceToTarget > _stoppingRange)
        {
            _navMeshAgent.SetDestination(_target.position);
            _provoked = true;
        }
        else
        {
            if (_distanceToTarget > _aggroRange)
            {
                _provoked = false;
            }
            _navMeshAgent.SetDestination(transform.position);
        }
    }
}
