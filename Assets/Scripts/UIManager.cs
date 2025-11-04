using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Slider HPbar;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SetMaxHP(int maxHP)
    {
        HPbar.maxValue = maxHP;
        HPbar.value = maxHP;
    }

    public void UpdateHP(int currentHP)
    {
        HPbar.value = currentHP;
    }

 
}
