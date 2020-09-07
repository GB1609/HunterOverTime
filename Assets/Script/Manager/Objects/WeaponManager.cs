using UnityEngine;

namespace Script.Manager
{
    public class WeaponManager : MonoBehaviour
    {
        public int damage;
        public bool isPlayer;

        // Update is called once per frame
        private void OnCollisionEnter(Collision other)
        {
            if (isPlayer && other.gameObject.CompareTag("Enemy"))
            {
                var playerManager = gameObject.GetComponentInParent<MedievalCharacterManager>();
                var multiplier = (playerManager._boosted) ? 2 : 1;
                if (playerManager.animator.GetFloat(MovementParameterEnum.Attack) > 0
                    && Utils.animatorIsPlaying(playerManager.animator))
                    other.gameObject.GetComponent<EnemyManager>().Impact(damage * multiplier);
            }

            if (!isPlayer && other.gameObject.CompareTag("Player"))
                other.gameObject.GetComponent<MedievalCharacterManager>().Impact(damage);
        }
    }
}