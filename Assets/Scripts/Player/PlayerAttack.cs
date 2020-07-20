using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip shotAudioClip;
    public GameObject bullet;
    public float bulletSpeed = 15f;
    private Transform bulletSpawn;
    private Transform[] bulletSpawns;
    private bool triggerDown;
    public float fireRate;
    private float timer;
    public Image bulletCover;
    public Image bulletIcon;
    public GameObject[] icons;
    private Vector2 coverSize;
    private Vector2 coverMaxSize;

    public static string UPGRADE = "upgrade_";
    private int attackPattern = 0;

    private GameState gs;
    // Start is called before the first frame update
    void Start()
    {
        gs = Camera.main.GetComponent<GameState>();

        //Determine Attack Upgrades, 0 or 1
        if(PlayerPrefs.HasKey(UPGRADE + 0))
        {
            attackPattern = 1;
        }
        if (PlayerPrefs.HasKey(UPGRADE + 1))
        {
            attackPattern = 2;
        }
        bulletSpawns = new Transform[attackPattern+1];
        switch (attackPattern)
        {
            case 1:
                //Left and right
                bulletSpawns[0] = gameObject.transform.GetChild(1).GetChild(0);
                bulletSpawns[1] = gameObject.transform.GetChild(2).GetChild(0);
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                break;
            case 2:
                //All 3
                bulletSpawns[0] = gameObject.transform.GetChild(0).GetChild(0);
                bulletSpawns[1] = gameObject.transform.GetChild(1).GetChild(0);
                bulletSpawns[2] = gameObject.transform.GetChild(2).GetChild(0);
                break;
            default:
                //Center
                bulletSpawns[0] = gameObject.transform.GetChild(0).GetChild(0);
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
                gameObject.transform.GetChild(2).gameObject.SetActive(false);
                break;
        }
        icons[attackPattern].SetActive(true);
        bulletCover = icons[attackPattern].transform.GetChild(0).GetComponent<Image>();
        bulletIcon = icons[attackPattern].transform.GetChild(1).GetComponent<Image>();

        coverMaxSize = bulletCover.rectTransform.sizeDelta;
        coverSize = coverMaxSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < fireRate)
        {
            timer += Time.deltaTime;
            coverSize.y = (coverMaxSize.y * (timer/fireRate));
            bulletCover.rectTransform.sizeDelta = coverSize;

        }
        if (!gs.Paused)
        {
            if ((Input.GetButtonDown("Fire1") || Input.GetAxis("Fire1") > .5f) && timer > fireRate && !triggerDown)
            {
                for (int i = 0; i < bulletSpawns.Length; i++)
                {
                    GameObject firredBullet = Instantiate(bullet);
                    firredBullet.transform.position = bulletSpawns[i].position;
                    firredBullet.transform.rotation = bulletSpawns[i].rotation;
                    firredBullet.GetComponent<Rigidbody>().velocity = firredBullet.transform.up * bulletSpeed;
                }

                audioSource.PlayOneShot(shotAudioClip);
                triggerDown = true;
                coverSize.y =0f;
                bulletCover.rectTransform.sizeDelta = coverSize;
                timer = 0f;
            }
        }
        if(Input.GetAxis("Fire1") < .5f)
        {
            triggerDown = false;
        }
    }
}
