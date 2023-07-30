using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgressiveAttack : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefub;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == playerPrefub)
        {
            playerPrefub.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == playerPrefub)
        {
            playerPrefub.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }
}
