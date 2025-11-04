using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHP = 1;
    private int currentHP;
    public int scoreval = 1;
    public GameObject explosioneffect;
    private Rigidbody2D rb;
    public float moveSpeed = 2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        currentHP = maxHP;
    }


    private void FixedUpdate()
    {
        rb.velocity = Vector2.down * moveSpeed;
        rb.angularVelocity = 0f;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        StartCoroutine(HitShake());
        if(currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if(explosioneffect != null)
        {
            Instantiate(explosioneffect, transform.position, Quaternion.identity);
        }

        ScoreManager.Instance.AddScore(scoreval);

        Destroy(gameObject);
    }

    IEnumerator HitShake()
    {
        Vector3 origianlPos = transform.position;   
        float elapsed = 0f;
        float duration = 0.1f;
        float magnitude = 0.1f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.position = origianlPos + new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = origianlPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerCtrl.Instance.TakeDamage(maxHP);
            Die();
        }
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
    }
}
