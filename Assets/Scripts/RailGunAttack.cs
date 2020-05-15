using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGunAttack : MonoBehaviour
{
    public GameObject bullet;
    private Transform bulletSpawn;
    public float shotDelay = .5f;
    private float flashduration = .15f;
    public float bulletSpeed = 11f;
    private float timer;
    private Light light;
    private float intensity;

    private bool canAttack;
    // Start is called before the first frame update
    void Start()
    {
        light = transform.GetChild(1).gameObject.GetComponent<Light>();
        bulletSpawn = transform.GetChild(0).transform;
        intensity = light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > flashduration)
        {
            light.intensity = intensity;
        }
        if(timer > shotDelay && canAttack)
        {
            GameObject shot = Instantiate(bullet);
            shot.transform.position = bulletSpawn.position;
            shot.GetComponent<Rigidbody>().velocity = -bulletSpawn.right * bulletSpeed;
            light.intensity = intensity * 1.5f;
            timer = 0f;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canAttack = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canAttack = false;
        }
    }
}
