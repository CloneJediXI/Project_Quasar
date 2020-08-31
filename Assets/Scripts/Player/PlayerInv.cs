using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInv : MonoBehaviour
{
    public Text text;
    private int money = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Money"))
        {
            money = PlayerPrefs.GetInt("Money");
        }
        text.text = money.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Money")
        {
            money += other.GetComponent<Money>().value;
            text.text = money.ToString();
            Destroy(other.gameObject);
            PlayerPrefs.SetInt("Money", money);
        }
    }
    
}
