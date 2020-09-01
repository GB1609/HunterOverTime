using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Movement
{
    public class CharacterManager : MonoBehaviour
    {
        public Animator animator;

        public Rigidbody rigidbodyPlayer;
        public new UnityEngine.Camera camera;
        public int speed;
        public bool boosted;
        private bool _swim;

        void Start()
        {
            _swim = false;
            animator.SetFloat(MovementParameterEnum.WALK, MovementValuesEnum.IDLE);
            animator.SetFloat(MovementParameterEnum.RUN, MovementValuesEnum.IDLE);
            animator.SetFloat(MovementParameterEnum.CROUCH, MovementValuesEnum.IDLE);
            animator.SetInteger(MovementParameterEnum.SWIM, 0);
            animator.SetBool(MovementParameterEnum.JUMP, false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Keyboard.current.escapeKey.isPressed)
            {
                Application.Quit();
            }

            if (!Managers.PAUSE)
            {
                if (rigidbodyPlayer.velocity.y > 1)
                    animator.SetBool(MovementParameterEnum.FALL, true);
                else
                {
                    if (_swim && animator.GetInteger(MovementParameterEnum.SWIM) == 2)
                    {
                        _swim = false;
                        animator.SetInteger(MovementParameterEnum.SWIM, 0);
                    }
                    else if (animator.GetInteger(MovementParameterEnum.SWIM) == 1 && _swim)
                        animator.SetInteger(MovementParameterEnum.SWIM, 3);
                    else if (animator.GetInteger(MovementParameterEnum.SWIM) == 1 && !_swim)
                        _swim = true;

                    if (!(Utils.isPressed(KeyCode.A) || Utils.isPressed(KeyCode.W) || Utils.isPressed(KeyCode.S) ||
                          Utils.isPressed(KeyCode.D)))
                    {
                        animator.SetFloat(MovementParameterEnum.WALK, MovementValuesEnum.IDLE);
                        animator.SetFloat(MovementParameterEnum.RUN, MovementValuesEnum.IDLE);
                        animator.StopPlayback();
                    }

                    //JUMP
                    if (!_swim && Input.GetKey(KeyCode.Space) && !animator.GetBool(MovementParameterEnum.JUMP))
                    {
                        if (Utils.isPressed(KeyCode.LeftShift) && Utils.isPressed(KeyCode.W))
                            RunMove.JumpForward(animator, camera, transform, speed, rigidbodyPlayer, boosted);
                        else if (Utils.isPressed(KeyCode.W))
                            NormalMove.Jump(animator, transform, camera, speed, rigidbodyPlayer);
                        else
                        {
                            animator.SetBool(MovementParameterEnum.JUMP, true);
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
                                animator.SetFloat(MovementParameterEnum.RUN, MovementValuesEnum.RUN_SLOWLY);
                            if (Utils.wasReleasedThisFrame(KeyCode.LeftShift))
                                animator.SetFloat(MovementParameterEnum.RUN, MovementValuesEnum.IDLE);

                            if (Input.GetKeyDown(KeyCode.C) && animator.GetFloat(MovementParameterEnum.CROUCH) < 1)
                                animator.SetFloat(MovementParameterEnum.CROUCH, MovementValuesEnum.CROUCH_ANIM);

                            if (Input.GetKeyDown(KeyCode.C) && animator.GetFloat(MovementParameterEnum.CROUCH) > 0)
                                animator.SetFloat(MovementParameterEnum.CROUCH, MovementValuesEnum.CROUCH_IDLE);

                            if (Input.GetKeyUp(KeyCode.C))
                                animator.SetFloat(MovementParameterEnum.CROUCH, MovementValuesEnum.IDLE);
                        }

                        //MOVE
                        if (Utils.isPressed(KeyCode.W))
                            if (animator.GetFloat(MovementParameterEnum.RUN) > 0)
                                RunMove.MoveForward(animator, camera, transform, speed, boosted);
                            else if (animator.GetFloat(MovementParameterEnum.CROUCH) > 0)
                                CrouchMove.MoveForward(animator, camera, transform, speed);
                            else
                                NormalMove.MoveForward(animator, camera, transform, speed);
                        else if (Utils.isPressed(KeyCode.S))
                            if (animator.GetFloat(MovementParameterEnum.CROUCH) > 0)
                                CrouchMove.MoveBack(animator, camera, transform, speed);
                            else
                                NormalMove.MoveBack(animator, camera, transform, speed);
                        else if (Utils.isPressed(KeyCode.A))
                            if (animator.GetFloat(MovementParameterEnum.CROUCH) > 0)
                                CrouchMove.MoveLeft(animator, camera, transform, speed);
                            else
                                NormalMove.MoveLeft(animator, camera, transform, speed);
                        else if (Utils.isPressed(KeyCode.D))
                            if (animator.GetFloat(MovementParameterEnum.CROUCH) > 0)
                                CrouchMove.MoveRight(animator, camera, transform, speed);
                            else
                                NormalMove.MoveRight(animator, camera, transform, speed);

                        if (Input.GetKeyUp(KeyCode.Space) && Utils.animatorIsPlaying(animator) ||
                            !Utils.animatorIsPlaying(animator) && animator.GetBool(MovementParameterEnum.JUMP))
                            animator.SetBool(MovementParameterEnum.JUMP, false);
                    }
                }
            }
        }
    }
}