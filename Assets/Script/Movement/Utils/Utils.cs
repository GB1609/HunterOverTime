using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class Utils
{
    public static bool isPressed(KeyCode key)
        {
            switch (key)
            {
                case KeyCode.D:
                    return Keyboard.current.dKey.isPressed;
                case KeyCode.A:
                    return Keyboard.current.aKey.isPressed;
                case KeyCode.S:
                    return Keyboard.current.sKey.isPressed;
                case KeyCode.W:
                    return Keyboard.current.wKey.isPressed;
                case KeyCode.Space:
                    return Keyboard.current.spaceKey.isPressed;
                case KeyCode.LeftShift:
                    return Keyboard.current.leftShiftKey.isPressed;
                default: return false;
            }
        }

        public static bool wasReleasedThisFrame(KeyCode key)
        {
            switch (key)
            {
                case KeyCode.D:
                    return Keyboard.current.dKey.wasReleasedThisFrame;
                case KeyCode.A:
                    return Keyboard.current.aKey.wasReleasedThisFrame;
                case KeyCode.S:
                    return Keyboard.current.sKey.wasReleasedThisFrame;
                case KeyCode.W:
                    return Keyboard.current.wKey.wasReleasedThisFrame;
                case KeyCode.Space:
                    return Keyboard.current.spaceKey.wasReleasedThisFrame;
                case KeyCode.LeftShift:
                    return Keyboard.current.leftShiftKey.wasReleasedThisFrame;
                default: return false;
            }
        }

        public static bool wasPressedThisFrame(KeyCode key)
        {
            switch (key)
            {
                case KeyCode.D:
                    return Keyboard.current.dKey.wasPressedThisFrame;
                case KeyCode.A:
                    return Keyboard.current.aKey.wasPressedThisFrame;
                case KeyCode.S:
                    return Keyboard.current.sKey.wasPressedThisFrame;
                case KeyCode.W:
                    return Keyboard.current.wKey.wasPressedThisFrame;
                case KeyCode.Space:
                    return Keyboard.current.spaceKey.wasPressedThisFrame;
                case KeyCode.LeftShift:
                    return Keyboard.current.leftShiftKey.wasPressedThisFrame;
                case KeyCode.Mouse0:
                    return Mouse.current.leftButton.wasPressedThisFrame;
                default: return false;
            }
        }

        public static bool animatorIsPlaying(Animator animator)
        {
            return animator.GetCurrentAnimatorStateInfo(0).length >=
                   animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        }
}
