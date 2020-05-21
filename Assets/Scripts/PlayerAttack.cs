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
    private bool triggerDown;
    public float fireRate;
    private float timer;
    public Image bulletCover;
    private Vector2 coverSize;
    private Vector2 coverStartSize;

    private Vector3 localForward;
    // Start is called before the first frame update
    void Start()
    {
        bulletSpawn = gameObject.transform.GetChild(0).GetChild(0);
        localForward = transform.worldToLocalMatrix.MultiplyVector(transform.forward);
        
        
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
            GameObject firredBullet = Instantiate(bullet);
            firredBullet.transform.position = bulletSpawn.position;
            firredBullet.transform.rotation = bulletSpawn.rotation;
            firredBullet.GetComponent<Rigidbody>().velocity = firredBullet.transform.up * bulletSpeed;
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
