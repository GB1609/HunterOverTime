using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

namespace Script.Manager
{
    public class EnemyManager : MonoBehaviour
    {
        public ParticleSystem blood;
        public Transform goal;
        private Vector3 _origin;
        private NavMeshAgent _agent;
        private Animator _animator;
        public Transform playerTransform;
        private bool _onTarget;
        [SerializeField] private float life;
        [SerializeField] private float aggression;
        [SerializeField] private float tolerance;

        void Start()
        {
            _onTarget = false;
            _animator = GetComponent<Animator>();
            _animator.SetFloat(MovementParameterEnum.Walk, 3);
            _agent = GetComponent<NavMeshAgent>();
            _agent.destination = goal.position;
            _origin = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }

        private void Update()
        {
            if (life > 0)
            {
                if (_onTarget)
                {
                    if (Vector3.Distance(transform.position, playerTransform.position) < 2f)
                    {
                        _agent.destination = transform.position;
                        if (_animator.GetFloat(MovementParameterEnum.Attack) < 1)
                        {
                            var randomAttack = new Random().Next(4) + 1;
                            _animator.SetFloat(MovementParameterEnum.Attack, randomAttack);
                        }
                    }
                    else
                        _agent.destination = playerTransform.position;
                }
                else if (Vector3.Distance(transform.position, playerTransform.position) < tolerance)
                {
                    _agent.destination = playerTransform.position;
                    _agent.speed += aggression;
                    _onTarget = true;
                }
                else
                {
                    if (Vector3.Distance(transform.position, goal.position) < 1f)
                        _agent.destination = _origin;
                    if (Vector3.Distance(transform.position, _origin) < 1f)
                        _agent.destination = goal.position;
                }

                if (_animator.GetBool(MovementParameterEnum.Impact) && Utils.animatorIsPlaying(_animator))
                    _animator.SetBool(MovementParameterEnum.Impact, false);
            }
            else
                Death();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("PlayerSword") || other.gameObject.CompareTag("Punch")
                                                           || other.gameObject.CompareTag("Kick"))
            {
                blood.Play();
                _animator.SetFloat(MovementParameterEnum.Attack,0);
                _animator.SetBool(MovementParameterEnum.Impact, true);
                life -= 10f;
            }
        }

        private void Death()
        {
            if (!_animator.GetBool(MovementParameterEnum.Death))
            {
                Destroy(GetComponent<NavMeshAgent>());
                _animator.SetFloat(MovementParameterEnum.Attack,0);
                _animator.SetBool(MovementParameterEnum.Death, true);
            }
            else if (Utils.animatorIsPlaying(_animator))
                Destroy(gameObject, 7f);
        }
    }
}