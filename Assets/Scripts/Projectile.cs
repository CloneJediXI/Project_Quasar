﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float destroyDelay = 5f;
    public GameObject hitParticles;
    public GameObject collideParticles;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, destroyDelay);
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            if(collision.gameObject.tag == "Indestructable")
            {
                ContactPoint contact = collision.contacts[0];
                GameObject sparks = Instantiate(hitParticles);
                sparks.transform.position = contact.point;
                sparks.transform.LookAt(gameObject.transform);
            }
            else
            {

            }
            

            collision.gameObject.SendMessage("ApplyDamage", SendMessageOptions.DontRequireReceiver);
            Destroy(this.gameObject);

        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyBullet")
        {
            GameObject sparks = Instantiate(collideParticles);
            sparks.transform.position = transform.position;
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}