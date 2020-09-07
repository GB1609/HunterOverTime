using System.Collections;
using DevionGames.UIWidgets;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Manager.Objects
{
    public class SacredSwordManager : MonoBehaviour
    {
        private Ray _forwardRay;
        private RaycastHit _hit;
        private TooltipTrigger[] _tooltip;
        public GameObject player;
        private UnityEngine.Camera _camera;
        private bool _isActive;
        public ParticleSystem particle;
        public GameObject cancel;
        public ParticleSystem particleSystem;
        public Image image;

        // Start is called before the first frame update
        void Start()
        {
            particleSystem.gameObject.SetActive(false);
            particle.gameObject.SetActive(true);
            _isActive = false;
            _tooltip = GetComponents<TooltipTrigger>();
            _camera = UnityEngine.Camera.main;
            image.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            _forwardRay = new Ray(_camera.transform.position, _camera.transform.forward);
            if (Physics.Raycast(_forwardRay, out _hit, 100f))
            {
                if (_hit.transform.CompareTag("Selectable") && _hit.transform.name.Equals("SacredSword"))
                {
                    if (!_isActive)
                    {
                        _tooltip[0].ShowTooltip();
                        StartCoroutine(close(_tooltip[0]));
                        _isActive = true;
                    }
                    else if (Vector3.Distance(player.transform.position, transform.position) < 4f
                             && Input.GetKey(KeyCode.E))
                        Take();
                }
            }
        }

        public void Take()
        {
            _tooltip[1].ShowTooltip();
            StartCoroutine(close(_tooltip[1]));
            var medievalCharacterManager = player.GetComponent<MedievalCharacterManager>();
            medievalCharacterManager.MoreLife(200);
            medievalCharacterManager._boosted = true;
            medievalCharacterManager.animator.SetBool(MovementParameterEnum.TakeSacred, true);
            Destroy(cancel);
            Destroy(GetComponent<AudioSource>());
            particleSystem.gameObject.SetActive(true);
            particleSystem.Play();
            image.gameObject.SetActive(true);
            Destroy(gameObject);
        }

        IEnumerator close(TooltipTrigger tooltip)
        {
            yield return new WaitForSeconds(4);
            tooltip.CloseTooltip();
        }
    }
}