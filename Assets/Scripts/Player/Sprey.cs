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
        Debug.Log("dzia³¹1");

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("dzia³¹2");

            //if (Time.time > nextCool)
            //{
            other.GetComponent<EnemyHealth>().TakeDamage(100);
            //    nextCool = Time.time + atackCool;
            //}
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("dzia³¹3");

        if (other.tag == "Enemy")
        {
            Debug.Log("dzia³¹4");

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
