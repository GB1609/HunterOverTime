using System;
using DevionGames.UIWidgets;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Script.Manager
{
    public class MedievalCharacterManager : MonoBehaviour
    {
        public Animator animator;

        public Rigidbody rigidbodyPlayer;
        public new UnityEngine.Camera camera;
        public int speed;
        public bool _boosted;
        private bool _swim;
        private bool _armed;
        private bool _falling;

        public int health;
        public Slider healthSlider;


        public GameObject rightHand;
        public GameObject sword;
        public GameObject hips;

        private static Transform _originalSwordTransform;

        private AudioSource[] _audioSources;
        private TooltipTrigger[] _tooltips;
        private int _stepNarration;
        private int _previousNarration;
        private int _activeNarration;
        public NotificationTrigger notification;

        void Start()
        {
            _activeNarration = _stepNarration = _previousNarration = 0;
            _originalSwordTransform = Instantiate(sword.transform);
            _audioSources = GetComponents<AudioSource>();
            _tooltips = GetComponents<TooltipTrigger>();
            _swim = false;
            _armed = false;
            _falling = false;
            _boosted = false;
            healthSlider.maxValue = health;
            healthSlider.value = health;
            animator.SetFloat(MovementParameterEnum.Walk, MovementValuesEnum.IDLE);
            animator.SetFloat(MovementParameterEnum.Run, MovementValuesEnum.IDLE);
            animator.SetFloat(MovementParameterEnum.Crouch, MovementValuesEnum.IDLE);
            animator.SetFloat(MovementParameterEnum.Attack, MovementValuesEnum.IDLE);
            animator.SetInteger(MovementParameterEnum.Swim, 0);
            animator.SetBool(MovementParameterEnum.DrawSword, false);
            animator.SetBool(MovementParameterEnum.Punch, false);
            animator.SetBool(MovementParameterEnum.Jump, false);
        }

        // Update is called once per frame
        void Update()
        {
            if (!Managers.PAUSE)
            {
                if (GetHitDistanceFroomFloor() > 5)
                {
                    _falling = true;
                    animator.SetBool(MovementParameterEnum.Fall, true);
                }
                else if (_falling && GetHitDistanceFroomFloor() < 1f)
                {
                    health -= 15;
                    healthSlider.value = health;
                    _falling = false;
                }
                else if (animator.GetBool(MovementParameterEnum.Impact))
                    animator.SetBool(MovementParameterEnum.Impact, false);
                else
                {
                    if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(3))
                        animator.SetFloat(MovementParameterEnum.Attack, 0);

                    if (_armed && Input.GetMouseButton(0))
                        Attack();
                    else if (Input.GetMouseButton(3) && animator.GetFloat(MovementParameterEnum.Attack) < 1)
                        Defend();
                    else if (!_swim && Input.GetMouseButton(0) && !_armed)
                        TakeSword();
                    else if (_armed && Input.GetKeyDown(KeyCode.F))
                        LeaveSword();
                    else if (Input.GetMouseButtonUp(0))
                        animator.SetBool(MovementParameterEnum.DrawSword, false);

                    if (!_swim && Input.GetMouseButton(1) && !animator.GetBool(MovementParameterEnum.Punch))
                        animator.SetBool(MovementParameterEnum.Punch, true);
                    else if (Input.GetMouseButtonUp(1))
                        animator.SetBool(MovementParameterEnum.Punch, false);
                    if (_swim && animator.GetInteger(MovementParameterEnum.Swim) == 2)
                    {
                        _swim = false;
                        animator.SetInteger(MovementParameterEnum.Swim, 0);
                    }
                    else if (animator.GetInteger(MovementParameterEnum.Swim) == 1 && _swim)
                        animator.SetInteger(MovementParameterEnum.Swim, 3);
                    else if (animator.GetInteger(MovementParameterEnum.Swim) == 1 && !_swim)
                        _swim = true;

                    if (!(Utils.isPressed(KeyCode.A) || Utils.isPressed(KeyCode.W) || Utils.isPressed(KeyCode.S) ||
                          Utils.isPressed(KeyCode.D)))
                    {
                        animator.SetFloat(MovementParameterEnum.Walk, MovementValuesEnum.IDLE);
                        animator.SetFloat(MovementParameterEnum.Run, MovementValuesEnum.IDLE);
                    }

                    Move();

                    Audio();
                }
            }
        }

        private void LateUpdate()
        {
            Narration();
        }

        private void Move()
        {
            //JUMP
            if (!_swim && Input.GetKey(KeyCode.Space) && !animator.GetBool(MovementParameterEnum.Jump))
            {
                if (Utils.isPressed(KeyCode.LeftShift) && Utils.isPressed(KeyCode.W))
                    RunMove.JumpForward(animator, camera, transform, speed, rigidbodyPlayer, _boosted);
                else if (Utils.isPressed(KeyCode.W))
                    NormalMove.Jump(animator, transform, camera, speed, rigidbodyPlayer);
                else
                {
                    animator.SetBool(MovementParameterEnum.Jump, true);
                    transform.position += Vector3.up * (3 * Time.deltaTime * speed);
                    rigidbodyPlayer.AddForce(Vector3.up * 3);
                }
            }
            //RUN
            else
            {
                if (!_swim)
                {
                    if (Utils.isPressed(KeyCode.LeftShift) &&
                        Utils.isPressed(KeyCode.W))
                        animator.SetFloat(MovementParameterEnum.Run, MovementValuesEnum.RUN_SLOWLY);
                    if (Utils.wasReleasedThisFrame(KeyCode.LeftShift))
                        animator.SetFloat(MovementParameterEnum.Run, MovementValuesEnum.IDLE);

                    if (Input.GetKeyDown(KeyCode.C) && animator.GetFloat(MovementParameterEnum.Crouch) < 1)
                        animator.SetFloat(MovementParameterEnum.Crouch, MovementValuesEnum.CROUCH_ANIM);

                    if (Input.GetKeyDown(KeyCode.C) && animator.GetFloat(MovementParameterEnum.Crouch) > 0)
                        animator.SetFloat(MovementParameterEnum.Crouch, MovementValuesEnum.CROUCH_IDLE);

                    if (Input.GetKeyUp(KeyCode.C))
                        animator.SetFloat(MovementParameterEnum.Crouch, MovementValuesEnum.IDLE);
                }

                //MOVE
                if (Utils.isPressed(KeyCode.W))
                    if (animator.GetFloat(MovementParameterEnum.Run) > 0)
                        RunMove.MoveForward(animator, camera, transform, speed, _boosted);
                    else if (animator.GetFloat(MovementParameterEnum.Crouch) > 0)
                        CrouchMove.MoveForward(animator, camera, transform, speed);
                    else
                        NormalMove.MoveForward(animator, camera, transform, speed);
                else if (Utils.isPressed(KeyCode.S))
                    if (animator.GetFloat(MovementParameterEnum.Crouch) > 0)
                        CrouchMove.MoveBack(animator, camera, transform, speed);
                    else
                        NormalMove.MoveBack(animator, camera, transform, speed);
                else if (Utils.isPressed(KeyCode.A))
                    if (animator.GetFloat(MovementParameterEnum.Crouch) > 0)
                        CrouchMove.MoveLeft(animator, camera, transform, speed);
                    else
                        NormalMove.MoveLeft(animator, camera, transform, speed);
                else if (Utils.isPressed(KeyCode.D))
                    if (animator.GetFloat(MovementParameterEnum.Crouch) > 0)
                        CrouchMove.MoveRight(animator, camera, transform, speed);
                    else
                        NormalMove.MoveRight(animator, camera, transform, speed);

                if (Input.GetKeyUp(KeyCode.Space) && Utils.animatorIsPlaying(animator) ||
                    !Utils.animatorIsPlaying(animator) && animator.GetBool(MovementParameterEnum.Jump))
                    animator.SetBool(MovementParameterEnum.Jump, false);
            }
        }

        private void Audio()
        {
            if (animator.GetBool(MovementParameterEnum.Jump))
            {
                var audioClip = _audioSources[3].clip;
                if (!(audioClip is null) && !_audioSources[3].isPlaying) _audioSources[3].PlayOneShot(audioClip);
            }

            if (animator.GetBool(MovementParameterEnum.Punch))
            {
                var audioClip = _audioSources[2].clip;
                if (!(audioClip is null) && !_audioSources[2].isPlaying) _audioSources[2].PlayOneShot(audioClip);
            }

            if (animator.GetFloat(MovementParameterEnum.Run) > 0)
            {
                var audioClip = _audioSources[1].clip;
                if (!(audioClip is null) && !_audioSources[1].isPlaying) _audioSources[1].PlayOneShot(audioClip);
            }
            else if (animator.GetFloat(MovementParameterEnum.Walk) > 0)
            {
                var audioClip = _audioSources[0].clip;
                if (!(audioClip is null) && !_audioSources[0].isPlaying) _audioSources[0].PlayOneShot(audioClip);
            }
        }

        private void TakeSword()
        {
            animator.SetBool(MovementParameterEnum.DrawSword, true);
            _armed = true;
            sword.gameObject.transform.SetParent(rightHand.transform);
            sword.transform.localPosition = new Vector3(0.022f, -0.269f, -0.229f);
            sword.transform.localEulerAngles = new Vector3(-47.825f, -162.295f, -8.75f);
        }

        private void LeaveSword()
        {
            _armed = false;
            sword.gameObject.transform.SetParent(hips.transform);
            sword.transform.localPosition = _originalSwordTransform.localPosition;
            sword.transform.localEulerAngles = _originalSwordTransform.localEulerAngles;
        }

        private void Attack()
        {
            var randomAttack = new Random().Next(7) + 1;
            if (animator.GetFloat(MovementParameterEnum.Attack) == 0)
                animator.SetFloat(MovementParameterEnum.Attack, randomAttack);
        }

        private void Defend()
        {
            if (animator.GetFloat(MovementParameterEnum.Attack) > 0) return;
            animator.SetFloat(MovementParameterEnum.Attack, 8);
        }

        private void Narration()
        {
            var closed = false;
            if (_stepNarration == 0 && animator.GetFloat(MovementParameterEnum.Walk) > 0)
            {
                _stepNarration += 1;
                _tooltips[_stepNarration - 1].ShowTooltip();
            }

            if (_stepNarration > _previousNarration)
            {
                _tooltips[_previousNarration].ShowTooltip();
                _previousNarration = _stepNarration;
            }

            foreach (var tooltip in _tooltips)
            {
                if (tooltip.instance.isActiveAndEnabled)
                    _activeNarration += 1;
                if (_activeNarration > 750)
                {
                    tooltip.CloseTooltip();
                    closed = true;
                }
            }

            if (closed) _activeNarration = 0;
        }

        float GetHitDistanceFroomFloor()
        {
            RaycastHit hit;
            Ray downRay = new Ray(transform.position, -Vector3.up); // this is the downward ray
            if (Physics.Raycast(downRay, out hit))
                return hit.distance;
            return 0f;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Narration"))
            {
                _stepNarration += 1;
                Destroy(other.gameObject);
                if (_stepNarration == 2)
                    notification.AddNotification(0);
            }

            if (other.gameObject.CompareTag("Selectable") && other.gameObject.name.ToLower().Contains("chest"))
            {
                health += health;
                healthSlider.value += health;
                Destroy(other.gameObject);
            }
        }

        public void Impact(int damage)
        {
            if (Math.Abs(animator.GetFloat(MovementParameterEnum.Attack) - 8) > 0.1f)
            {
                health -= damage;
                healthSlider.value = health;
                Vector3 b = new Vector3(camera.transform.forward.x, 0, camera.transform.forward.z) * -1;
                transform.position += b * (Time.deltaTime * speed);
                animator.SetBool(MovementParameterEnum.Impact, true);
            }
        }
    }
}