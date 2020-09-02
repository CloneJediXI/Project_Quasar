using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    private Color defaultColor;
    private Color defaultColor2;
    public float flashTime = .1f;
    private bool timerGo;
    private float timer;
    public GameObject explosionEffect;
    private Renderer rend;
    private Renderer rend2;
    public GameObject moneyDrop;
    // Start is called before the first frame update
    void Start()
    {
        rend = transform.GetChild(0).GetComponent<Renderer>();
        rend2 = transform.GetChild(1).GetComponent<Renderer>();
        defaultColor = rend.material.color;
        defaultColor2 = rend2.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            GameObject puf = Instantiate(explosionEffect);
            puf.transform.position = transform.position;
            GameObject money = Instantiate(moneyDrop);
            money.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            int chance = Random.Range(0, 10);
            if(chance > 7){
                money = Instantiate(moneyDrop);
                money.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
            Destroy(gameObject);
        }
        if (timerGo)
        {
            timer += Time.deltaTime;
        }
        if (timer > flashTime)
        {
            timerGo = false;
            timer = 0f;
            rend.material.color = defaultColor;
            rend2.material.color = defaultColor2;
        }
    }
    void ApplyDamage()
    {
        health --;
        rend.material.color = Color.white;
        rend2.material.color = Color.white;
        timerGo = true;
    }
}
