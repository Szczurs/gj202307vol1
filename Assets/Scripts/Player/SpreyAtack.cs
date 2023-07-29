using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreyAtack : MonoBehaviour
{
    [SerializeField] private GameObject atackPositon1;
    [SerializeField] private GameObject atackPositon2;
    [SerializeField] private GameObject atackPrefub;
    private float nextCoolPositon1 = 0f;
    private float nextCoolPositon2 = 0f;
    private float coolPositon1 = 2f;
    private float coolPositon2 = 2f;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time > nextCoolPositon1)
            {
                Instantiate(atackPrefub, atackPositon1.transform.position, transform.rotation);
                nextCoolPositon1 = Time.time + coolPositon1;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (Time.time > nextCoolPositon2)
            {
                Instantiate(atackPrefub, atackPositon2.transform.position, transform.rotation);
                nextCoolPositon2 = Time.time + coolPositon2;

            }
        }
    }
}
