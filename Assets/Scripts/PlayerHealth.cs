using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject shield;
    public Renderer shieldRend;
    private Color startColor;
    public Color brokenColor;
    private bool shieldUp;
    public float maxHealth;
    private float health;
    public float shieldVisibleDuration;
    private float timer;

    public GameObject hitParticles;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        shieldRend = shield.GetComponent<Renderer>();
        startColor = shieldRend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > shieldVisibleDuration)
        {
            shield.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyBullet")
        {
            health--;
            shieldRend.material.color = Color.Lerp(brokenColor, startColor, health / maxHealth);
            timer = 0f;
            shield.SetActive(true);
            GameObject sparks = Instantiate(hitParticles);
            sparks.transform.position = other.transform.position;
            sparks.transform.LookAt(this.transform);
            Destroy(other.gameObject);
        }
    }
}
