using System;
using DevionGames.UIWidgets;
using UnityEngine;

namespace Script.Indications
{
    public class ClaudiaNarrative : MonoBehaviour
    {
        private bool _isActive;

        [SerializeField] private String claudia = "Claudia";
        [SerializeField] private Animator anim;

        private bool _first;
        private RaycastHit _hit;

        public TooltipTrigger tooltip;
        private static readonly string talk = "talk";

        private Ray _forwardRay;

        private void Start()
        {
            _isActive = false;
            anim.SetBool(talk, false);
        }

        private void Update()
        {
            if (!(UnityEngine.Camera.main is null))
            {
                _forwardRay = new Ray(UnityEngine.Camera.main.transform.position,
                    UnityEngine.Camera.main.transform.forward);
                if (Physics.Raycast(_forwardRay, out _hit, 100f))
                {
                    var selection = _hit.transform;
                    if (selection.CompareTag(claudia))
                        doAction();
                    else
                    {
                        _isActive = false;
                        removeAction();
                    }
                }
            }
        }

        private void doAction()
        {
            if (!_isActive)
            {
                _isActive = true;
                anim.SetBool(talk, true);
                tooltip.ShowTooltip();
            }
        }

        private void removeAction()
        {
            if (!_isActive)
            {
                tooltip.CloseTooltip();
                anim.SetBool(talk, false);
            }
        }
    }
}