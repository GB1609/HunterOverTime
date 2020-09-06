using System;
using Script.Manager;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int damage;
    public bool isPlayer;

    // Update is called once per frame
    private void OnCollisionEnter(Collision other)
    {
        if (isPlayer && other.gameObject.CompareTag("Enemy"))
        {
            var enemyManager = other.gameObject.GetComponent<EnemyManager>();
            enemyManager.Impact(damage);
        }
        else if (!isPlayer && other.gameObject.CompareTag("Player"))
        {
            var playerManager = other.gameObject.GetComponent<MedievalCharacterManager>();
            playerManager.health -= damage;
        }
    }
}