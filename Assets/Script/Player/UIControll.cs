using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControll : MonoBehaviour {

    public Stats plStats;

    public Slider healthbar;
    public Slider staminaBar;

    void Start()
    {
        plStats = GetComponent<Stats>();
    }


    void Update()
    {
        healthbar.value = plStats.health / plStats.maxHealth;
        staminaBar.value = plStats.stamina / plStats.maxStamina;
    }
}
