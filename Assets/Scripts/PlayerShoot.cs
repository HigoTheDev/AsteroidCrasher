using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletprefab;
    public Transform firePointleft;
    public Transform firePointright;
    public float bulletspeed = 10f;
    public float firerate = 0.2f;
    private float nextfiretime = 0f;
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextfiretime)
        {
            Shoot(firePointleft);
        }
        if (Input.GetMouseButton(1) && Time.time >= nextfiretime)
        {
            Shoot(firePointright);
        }
    }

    void Shoot(Transform firePoint)
    {
        if (bulletprefab == null || firePoint == null) return;
        GameObject bullet = Instantiate(bulletprefab, firePoint.position, firePoint.rotation);
        bullet.transform.Rotate(0, 0, 90f);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.velocity = firePoint.up * bulletspeed;
        }
        nextfiretime = Time.time + firerate;
    }
}
