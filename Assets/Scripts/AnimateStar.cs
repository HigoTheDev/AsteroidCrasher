using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBg1 : MonoBehaviour
{
    public float scrollspd = 2f;
    private float spriteheight;

    void Start()
    {
        spriteheight = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        transform.Translate(Vector3.down * scrollspd * Time.deltaTime);
        if(transform.position.y <=  - spriteheight)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y + 2 * spriteheight,
                transform.position.z
             );
        }
    }
}
