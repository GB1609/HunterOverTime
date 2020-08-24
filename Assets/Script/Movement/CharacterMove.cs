using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script
{
    public class CharacterMove : MonoBehaviour
    {
        public Animator animator;

        public Transform playerTransform;
        public new Camera camera;
        public int speed;
        public bool boosted;

        void Start()
        {
            animator.SetFloat(MovementParameterEnum.WALK, MovementValuesEnum.IDLE);
            animator.SetFloat(MovementParameterEnum.RUN, MovementValuesEnum.IDLE);
            animator.SetBool(MovementParameterEnum.JUMP, false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Keyboard.current.escapeKey.isPressed)
            {
                Application.Quit();
                EditorApplication.isPlaying = false;
            }

            if (!Keyboard.current.anyKey.isPressed || Keyboard.current.anyKey.wasReleasedThisFrame)
            {
                animator.SetFloat(MovementParameterEnum.WALK, MovementValuesEnum.IDLE);
                animator.SetFloat(MovementParameterEnum.RUN, MovementValuesEnum.IDLE);
                animator.StopPlayback();
            }

            if (Utils.isPressed(KeyCode.LeftShift) && Utils.isPressed(KeyCode.W))
                animator.SetFloat(MovementParameterEnum.RUN, MovementValuesEnum.RUN_SLOWLY);
            if (Utils.wasReleasedThisFrame(KeyCode.LeftShift))
                animator.SetFloat(MovementParameterEnum.RUN, MovementValuesEnum.IDLE);

            if (Utils.isPressed(KeyCode.S))
            {
                    NormalMove.MoveBack(animator,camera,transform,speed);
            }
            else if (Utils.isPressed(KeyCode.W))
            {
                if (animator.GetFloat(MovementParameterEnum.RUN) > 0)
                {
                    RunMove.MoveForward(animator,camera,transform,speed,boosted);
                }
                else
                {
                    NormalMove.MoveForward(animator,camera,transform,speed);
                }
            }
            else if (Utils.isPressed(KeyCode.A))
            {
                if(animator.GetFloat(MovementParameterEnum.RUN)>0)
                {}
                else
                {
                    NormalMove.MoveLeft(animator,camera,transform,speed);
                }
            }
            else if (Utils.isPressed(KeyCode.D))
            {
                if(animator.GetFloat(MovementParameterEnum.RUN)>0)
                {}
                else
                {
                    NormalMove.MoveRight(animator,camera,transform,speed);
                }
            }
            else if (Utils.isPressed(KeyCode.Space) && !animator.GetBool(MovementParameterEnum.JUMP))
            {
                animator.SetBool(MovementParameterEnum.JUMP, true);
                animator.SetTrigger(MovementParameterEnum.JUMP_TRIGGER);
                playerTransform.position += Vector3.up * (5 * Time.deltaTime * speed);
            }

            if (Utils.wasReleasedThisFrame(KeyCode.Space) && Utils.animatorIsPlaying(animator) ||
                (!Utils.animatorIsPlaying(animator) && animator.GetBool(MovementParameterEnum.JUMP)))
            {
                animator.SetBool(MovementParameterEnum.JUMP, false);
            }
        }

    }
}