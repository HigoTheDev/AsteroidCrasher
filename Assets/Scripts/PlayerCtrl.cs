using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl Instance { get; private set; }

    [Header("Player companents")]
    public PlayerModel playerModel;
    public PlayerMove playerMove;
    public PlayerShoot playerShoot;

    [Header("Player Stats")]
    public int maxHP = 10;
    public int currentHP;
    public bool isalive = true;
    public GameObject explosioneffect;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        playerModel = GetComponent <PlayerModel>(); 
        playerMove = GetComponent <PlayerMove>();
        playerShoot = GetComponent <PlayerShoot>();
    }

    private void Start()
    {
        currentHP = maxHP;
        UIManager.Instance.SetMaxHP(maxHP);
    }

    public void TakeDamage (int damage)
    {
        if (!isalive) return;
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);
        if (UIManager.Instance != null)
            UIManager.Instance.UpdateHP(currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(2); 
        }
    }


    private void Die()
    {
        if(explosioneffect != null)
        {
            Instantiate(explosioneffect,transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
        if (GameFlowManager.Instance != null)
        {
            GameFlowManager.Instance.OnPlayerDead();
        }
    }

}
