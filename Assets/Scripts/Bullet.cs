using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages the civil car's behavior
public class Bullet : MonoBehaviour 
{
    public float bulletSpeed = 10;
    public float timeUntilDisappear = 2f;
    public float destroyTime = 0.3f;
    private float timeElapsed;

    void Start()
    {
        timeElapsed = 0;
        GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
    }

    public void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > timeUntilDisappear)
        {
            Destroy(gameObject);
        }
    }

    // This is called when the bullet collides with another object
    private void OnCollisionEnter2D(Collision2D collidingObject)
    {
        StartCoroutine(StartDestroyingBullet());
    }

    IEnumerator StartDestroyingBullet()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
