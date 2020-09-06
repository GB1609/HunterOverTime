using System;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

namespace Script.Manager
{
    public class EnemyManager : MonoBehaviour
    {
        public int level;
        public ParticleSystem blood;
        public Transform goal;
        private Vector3 _origin;
        private NavMeshAgent _agent;
        private Animator _animator;
        public Transform playerTransform;
        private bool _onTarget;
        public float life;
        [SerializeField] private float aggression;
        [SerializeField] private float tolerance;

        private AudioSource[] audios;

        void Start()
        {
            audios = GetComponents<AudioSource>();
            _onTarget = false;
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            if (Math.Abs(goal.transform.position.x - transform.position.x) < 0.1f
                && Math.Abs(goal.transform.position.y - transform.position.y) < 0.1f
                && Math.Abs(goal.transform.position.z - transform.position.z) < 0.1f)
            {
                _agent.isStopped = true;
                _animator.SetFloat(MovementParameterEnum.Walk, MovementValuesEnum.IDLE);
            }
            else
                _animator.SetFloat(MovementParameterEnum.Walk, MovementValuesEnum.FORWARD);

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
                        _agent.isStopped = true;
                        _animator.SetFloat(MovementParameterEnum.Walk, MovementValuesEnum.IDLE);
                        if (_animator.GetFloat(MovementParameterEnum.Attack) < 1)
                            _animator.SetFloat(MovementParameterEnum.Attack, GetAttack());
                    }
                    else if (life > 0)
                    {
                        _agent.destination = playerTransform.position;
                        if (_agent.isStopped)
                        {
                            _agent.isStopped = false;
                            _animator.SetFloat(MovementParameterEnum.Walk, MovementValuesEnum.FORWARD);
                        }
                    }
                }
                else if (Vector3.Distance(transform.position, playerTransform.position) < tolerance)
                {
                    _agent.destination = playerTransform.position;
                    _agent.speed += aggression;
                    _onTarget = true;
                }
                else
                {
                    if (Vector3.Distance(transform.position, goal.position) < 5f)
                        _agent.destination = _origin;
                    if (Vector3.Distance(transform.position, _origin) < 5f)
                        _agent.destination = goal.position;
                }

                if (_animator.GetBool(MovementParameterEnum.Impact) && Utils.animatorIsPlaying(_animator))
                    _animator.SetBool(MovementParameterEnum.Impact, false);
            }
            else
                Death();
        }

        public void Impact(float damage)
        {
            if (life > 0)
            {
                blood.Play();
                audios[1].PlayOneShot(audios[1].clip);
                _animator.SetFloat(MovementParameterEnum.Walk, MovementValuesEnum.IDLE);
                _animator.SetFloat(MovementParameterEnum.Attack, 0);
                _animator.SetBool(MovementParameterEnum.Impact, true);
                life -= damage;
            }
        }

        private void Death()
        {
            if (!_animator.GetBool(MovementParameterEnum.Death))
            {
                audios[0].PlayOneShot(audios[0].clip);
                _agent.isStopped = true;
                DestroyImmediate(GetComponent<NavMeshAgent>());
                _animator.SetBool(MovementParameterEnum.Impact, false);
                _animator.SetFloat(MovementParameterEnum.Attack, 0);
                _animator.SetBool(MovementParameterEnum.Death, true);
            }
            else
            {
                Destroy(gameObject, 7f);
            }
        }

        private int GetAttack()
        {
            switch (level)
            {
                case 1: return 1;
                case 2: return new Random().Next(2) + 1;
                case 3: return new Random().Next(4) + 1;
                case 4: return new Random().Next(5) + 1;
                case 5: return new Random().Next(7) + 1;
                default: return 1;
            }
        }
    }
}