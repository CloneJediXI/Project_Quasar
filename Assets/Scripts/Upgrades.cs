using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    private bool[] upgradeList;
    public static string UPGRADE = "upgrade_";
    private GameObject player;
    public GameObject cannonMid;
    public GameObject cannonR;
    public GameObject cannonL;
    public GameObject Engine;
    public Transform upgrades;
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
        PlayerPrefs.SetString(UPGRADE + number, "true");
        upgradeList[number] = true;
        ShowUpgrades();
        
    }
    void ShowUpgrades()
    {
        cannonL.SetActive(false);
        cannonR.SetActive(false);
        cannonMid.SetActive(true);
        Engine.SetActive(false);
        upgrades.GetChild(0).gameObject.GetComponent<Button>().interactable = true;
        upgrades.GetChild(1).gameObject.GetComponent<Button>().interactable = true;
        upgrades.GetChild(2).gameObject.GetComponent<Button>().interactable = true;
        if (upgradeList[0])
        {
            cannonL.SetActive(true);
            cannonR.SetActive(true);
            cannonMid.SetActive(false);
            upgrades.GetChild(0).gameObject.GetComponent<Button>().interactable = false;
        }
        if (upgradeList[1])
        {
            cannonL.SetActive(true);
            cannonR.SetActive(true);
            cannonMid.SetActive(true);
            upgrades.GetChild(1).gameObject.GetComponent<Button>().interactable = false;
        }
        if (upgradeList[2])
        {
            Engine.SetActive(true);
            upgrades.GetChild(2).gameObject.GetComponent<Button>().interactable = false;

        }
    }
}
public enum Upgrade
{
    DoubleCannon, TrippleCannon, FasterEngines
}
