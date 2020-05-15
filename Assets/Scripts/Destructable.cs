using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public int health;
    private Color defaultColor;
    public float flashTime;
    private bool timerGo;
    private float timer;
    public GameObject disolveEffect;

    // Start is called before the first frame update
    void Start()
    {
        defaultColor = this.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            GameObject puf = Instantiate(disolveEffect);
            puf.transform.position = transform.position;
            Destroy(gameObject);
        }
        if (timerGo)
        {
            timer += Time.deltaTime;
        }
        if(timer > flashTime)
        {
            timerGo = false;
            timer = 0f;
            this.GetComponent<Renderer>().material.color = defaultColor;
        }
    }
    public void ApplyDamage()
    {
        health --;
        this.GetComponent<Renderer>().material.color = Color.white;
        timerGo = true;
    }
}
