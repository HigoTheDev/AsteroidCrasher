using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerModel : MonoBehaviour
{
    public float rotatespeed = 200f;

    void Update()
    {
        ditheochuot();
    }

    void ditheochuot()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        mousepos.z = 0f;

        Vector3 huong = mousepos - transform.position;

        float angle = Mathf.Atan2(huong.y, huong.x) * Mathf.Rad2Deg - 90f;
       
        Quaternion muctieu = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, muctieu, rotatespeed * Time.deltaTime);
    }
}
