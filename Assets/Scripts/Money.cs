using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int value;
    // Start is called before the first frame update
    void Start()
    {
        float speed = Random.Range(3, 17);
        StartCoroutine(spin(speed));
    }

    private IEnumerator spin(float speed)
    {
        while (true)
        {
            transform.Rotate(new Vector3(0, speed, 0));
            yield return new WaitForSeconds(0.05f);
        }
    }
}
