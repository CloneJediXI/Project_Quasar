using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform player;
    public Vector3 rotationOffset;
    private Vector3 diff;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        diff = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(diff);
        transform.Rotate(rotationOffset);
    }
}
