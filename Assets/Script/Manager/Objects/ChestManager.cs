using System.Collections;
using DevionGames.UIWidgets;
using UnityEngine;

namespace Script.Manager
{
    public class ChestManager : MonoBehaviour
    {
        public int health;
        private Ray _forwardRay;
        private RaycastHit _hit;
        private TooltipTrigger _tooltip;
        public GameObject player;
        private UnityEngine.Camera _camera;
        private ParticleSystem _particle;

        void Start()
        {
            _particle = GetComponentInChildren<ParticleSystem>();
            _tooltip = GetComponent<TooltipTrigger>();
            _camera = UnityEngine.Camera.main;
        }

        void Update()
        {
            if (GetComponent<Renderer>().isVisible)
                _particle.Play();
            else if (_particle.isPlaying)
                _particle.Stop();

            _forwardRay = new Ray(_camera.transform.position, _camera.transform.forward);
            if (Physics.Raycast(_forwardRay, out _hit, 100f))
            {
                if (_hit.transform.CompareTag("Selectable"))
                    doAction(_hit.transform);
            }

            if (Vector3.Distance(player.transform.position, transform.position) < 6f &&
                Input.GetKey(KeyCode.E))
            {
                player.GetComponent<MedievalCharacterManager>().MoreLife(health);
                DestroyImmediate(gameObject);
            }
        }

        private void doAction(Transform selection)
        {
            if (selection.name.ToLower().Contains("chest"))
            {
                _tooltip.ShowTooltip();
                StartCoroutine(close(_tooltip));
            }
        }

        IEnumerator close(TooltipTrigger tooltip)
        {
            yield return new WaitForSeconds(4);
            tooltip.CloseTooltip();
        }
    }
}