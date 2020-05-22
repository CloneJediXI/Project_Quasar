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
    public Sprite[] icons;
    public Sprite[] covers;
    public Vector2 coverSize;
    public Vector2 coverStartSize;

    public static string UPGRADE = "upgrade_";
    private int attackPattern = 0;
    // Start is called before the first frame update
    void Start()
    {
        

        //Determine Attack Upgrades, 0 or 1
        if(PlayerPrefs.HasKey(UPGRADE + 0))
        {
            attackPattern = 1;
        }
        if (PlayerPrefs.HasKey(UPGRADE + 1))
        {
            attackPattern = 2;
        }
        switch (attackPattern)
        {
            case 1:
                //Left and right
                bulletSpawns = new Transform[2];
                bulletSpawns[0] = gameObject.transform.GetChild(1).GetChild(0);
                bulletSpawns[1] = gameObject.transform.GetChild(2).GetChild(0);
                gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                break;
            case 2:
                //All 3
                bulletSpawns = new Transform[3];
                bulletSpawns[0] = gameObject.transform.GetChild(0).GetChild(0);
                bulletSpawns[1] = gameObject.transform.GetChild(1).GetChild(0);
                bulletSpawns[2] = gameObject.transform.GetChild(2).GetChild(0);
                break;
            default:
                //Center
                bulletSpawns = new Transform[1];
                bulletSpawns[0] = gameObject.transform.GetChild(0).GetChild(0);
                gameObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                gameObject.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
                break;
        }
        bulletIcon.sprite = icons[attackPattern];
        bulletCover.sprite = covers[attackPattern];

        bulletSpawn = gameObject.transform.GetChild(0).GetChild(0);
        coverStartSize = bulletCover.rectTransform.sizeDelta;
        coverSize = coverStartSize;
        coverSize.y = 0f;
        bulletCover.rectTransform.sizeDelta = coverSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < fireRate)
        {
            timer += Time.deltaTime;
            coverSize.y = coverStartSize.y - (coverStartSize.y * (timer/fireRate));
            bulletCover.rectTransform.sizeDelta = coverSize;
            
        }
        if ((Input.GetButtonDown("Fire1") || Input.GetAxis("Fire1") >.5f)&& timer > fireRate && !triggerDown)
        {
            for(int i=0; i<bulletSpawns.Length; i++)
            {
                GameObject firredBullet = Instantiate(bullet);
                firredBullet.transform.position = bulletSpawns[i].position;
                firredBullet.transform.rotation = bulletSpawns[i].rotation;
                firredBullet.GetComponent<Rigidbody>().velocity = firredBullet.transform.up * bulletSpeed;
            }
            
            audioSource.PlayOneShot(shotAudioClip);
            triggerDown = true;
            coverSize.y = coverStartSize.y;
            bulletCover.rectTransform.sizeDelta = coverSize;
            timer = 0f;
        }
        if(Input.GetAxis("Fire1") < .5f)
        {
            triggerDown = false;
        }
    }
}
