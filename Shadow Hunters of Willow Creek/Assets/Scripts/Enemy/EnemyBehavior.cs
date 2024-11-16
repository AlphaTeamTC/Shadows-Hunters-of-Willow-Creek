using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyBehavior : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Transform _player;

        [SerializeField] private LayerMask _groundLayerMask, _playerLayerMask;

        [SerializeField] private Vector3 _walkPoint;
        private bool _walkPointSet;
        [SerializeField] private float _walkPointRange;

        [SerializeField] private float _timeBetweenAttacks;
        private bool _alreadyAttacked;

        [SerializeField] private float _sightRange, _attackRange;
        [SerializeField] private bool _playerInSightRange, _playerInAttackRange;

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _playerLayerMask);
            _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _playerLayerMask);

            if (!_playerInSightRange && !_playerInAttackRange) Patrolling();
            if (_playerInSightRange && !_playerInAttackRange) ChasePlayer();
            if (_playerInSightRange && _playerInAttackRange) AttackPlayer();
        }

        private void Patrolling()
        {
            Debug.Log("Patrolling");
            if (!_walkPointSet) SearchWalkPoint();

            if (_walkPointSet) _agent.SetDestination(_walkPoint);

            Vector3 distanceToWalkPoint = transform.position - _walkPoint;

            if (distanceToWalkPoint.magnitude < 1f) _walkPointSet = false;
        }

        private void SearchWalkPoint()
        {
            float randomZ = Random.Range(-_walkPointRange, _walkPointRange);
            float randomX = Random.Range(-_walkPointRange, _walkPointRange);

            _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            if (Physics.Raycast(_walkPoint, -transform.up, 2f, _groundLayerMask)) _walkPointSet = true;
        }

        private void ChasePlayer()
        {
            Debug.Log("Chasing");
            _agent.SetDestination(_player.position);
        }

        private void AttackPlayer()
        {
            _agent.SetDestination(transform.position);
            transform.LookAt(_player);

            if (!_alreadyAttacked)
            {
                Debug.Log("Attacking");

                _alreadyAttacked = true;
                Invoke(nameof(ResetAttack), _timeBetweenAttacks);
            }
        }

        private void ResetAttack()
        {
            _alreadyAttacked = false;
        }
    }
}