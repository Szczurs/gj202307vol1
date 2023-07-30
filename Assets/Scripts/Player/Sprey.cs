using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprey : MonoBehaviour
{
    [SerializeField] private float atackCool = 1f;
    private float nextCool = 0f;
    private float deathTime = 3f;
    private float timeSpend = 0f;

    private void FixedUpdate()
    {
        MoveTogo();
        if (timeSpend > deathTime)
            Destroy(gameObject);
        timeSpend += Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("dzia1");

        if (other.CompareTag("Enemy1"))
        {
            Debug.Log("dzia2");

            //if (Time.time > nextCool)
            //{
            other.GetComponent<EnemyHealth>().TakeDamage(100);
            //    nextCool = Time.time + atackCool;
            //}
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("dzia3");

        if (other.tag == "Enemy")
        {
            Debug.Log("dzia4");

            //if (Time.time > nextCool)
            //{
            other.GetComponent<EnemyHealth>().TakeDamage(100);
            //    nextCool = Time.time + atackCool;
            //}
        }
    }

    private void MoveTogo()
    {

    }

}
