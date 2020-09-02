using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    private bool[] upgradeList;
    public int[] upgradePrices;
    public Text text;
    public static string UPGRADE = "upgrade_";
    private GameObject player;
    public GameObject cannonMid;
    public GameObject cannonR;
    public GameObject cannonL;
    public GameObject Engine;
    public Transform upgrades;
    [Header("Normal Color")]
    public ColorBlock normalColor;
    [Header("Disabled Color")]
    public ColorBlock disabledColor;
    // Start is called before the first frame update
    void Start()
    {
        upgradeList = new bool[3];
        for(int i = 0; i<upgradeList.Length; i++)
        {
            upgradeList[i] = PlayerPrefs.HasKey(UPGRADE + i);
            
        }
        ShowUpgrades();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    public void Reset()
    {
        for (int i = 0; i < upgradeList.Length; i++)
        {
            PlayerPrefs.DeleteKey(UPGRADE + i);
            upgradeList[i] = false;
        }
        ShowUpgrades();
    }
    public void Upgrade(int number)
    {
        if (!upgradeList[number])
        {
            if (PlayerPrefs.HasKey("Money"))
            {
                int money = PlayerPrefs.GetInt("Money");
                if (money >= upgradePrices[number])
                {
                    money -= upgradePrices[number];
                    PlayerPrefs.SetInt("Money", money);
                    text.text = money.ToString();

                    PlayerPrefs.SetString(UPGRADE + number, "true");
                    upgradeList[number] = true;
                    ShowUpgrades();
                }
            }
            
        }
        
        
    }
    void ShowUpgrades()
    {
        cannonL.SetActive(false);
        cannonR.SetActive(false);
        cannonMid.SetActive(true);
        Engine.SetActive(false);
        upgrades.GetChild(0).gameObject.GetComponent<Button>().colors = normalColor;
        upgrades.GetChild(1).gameObject.GetComponent<Button>().colors = normalColor;
        upgrades.GetChild(2).gameObject.GetComponent<Button>().colors = normalColor;
        if (upgradeList[0])
        {
            cannonL.SetActive(true);
            cannonR.SetActive(true);
            cannonMid.SetActive(false);
            upgrades.GetChild(0).gameObject.GetComponent<Button>().colors = disabledColor;
        }
        if (upgradeList[1])
        {
            cannonL.SetActive(true);
            cannonR.SetActive(true);
            cannonMid.SetActive(true);
            upgrades.GetChild(1).gameObject.GetComponent<Button>().colors = disabledColor;
        }
        else if(!upgradeList[0])
        {
            upgrades.GetChild(1).gameObject.GetComponent<Button>().colors = disabledColor;
        }
        if (upgradeList[2])
        {
            Engine.SetActive(true);
            upgrades.GetChild(2).gameObject.GetComponent<Button>().colors = disabledColor;

        }
    }
    public void Back()
    {
        SceneManager.LoadScene(2);
    }
}
