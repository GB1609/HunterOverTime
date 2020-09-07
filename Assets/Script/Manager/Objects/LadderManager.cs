using System.Collections;
using DevionGames.UIWidgets;
using UnityEngine;

namespace Script.Manager
{
    public class LadderManager : MonoBehaviour
    {
        private Ray _forwardRay;
        private RaycastHit _hit;
        private TooltipTrigger[] _tooltips;
        public GameObject player;
        private UnityEngine.Camera _camera;
        private bool _isActive;
        public GameObject _plane;

        // Start is called before the first frame update
        void Start()
        {
            _tooltips = GetComponents<TooltipTrigger>();
            _camera = UnityEngine.Camera.main;
            _isActive = false;
        }

        // Update is called once per frame
        void Update()
        {
            _forwardRay = new Ray(_camera.transform.position, _camera.transform.forward);
            if (Physics.Raycast(_forwardRay, out _hit, 100f))
            {
                if (_hit.transform.CompareTag("Selectable"))
                {
                    if (_hit.transform.name.ToLower().Contains("ladder"))
                    {
                        _tooltips[0].ShowTooltip();
                        StartCoroutine(close(_tooltips[0]));
                    }
                    if (Vector3.Distance(player.transform.position, transform.position) < 4f
                             && Input.GetKey(KeyCode.E))
                        player.transform.position = _plane.GetComponent<Renderer>().bounds.center;
                }
            }
        }

        IEnumerator close(TooltipTrigger tooltip)
        {
            yield return new WaitForSeconds(4);
            tooltip.CloseTooltip();
        }
    }
}