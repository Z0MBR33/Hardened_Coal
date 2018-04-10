using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour {

    public Stats enStats;

    public GameObject statBar;
    public Image liveBar;
    public Text _name;

    void Start()
    {
        enStats = GetComponent<Stats>();
        _name.text = gameObject.name;
    }
	
	// Update is called once per frame
	void Update () {
        if (enStats.statBar)
        {
            statBar.SetActive(true);
        }
        else if(statBar.activeSelf == true)
        {
            statBar.SetActive(false);
        }

        liveBar.GetComponent<Image>().fillAmount = enStats.health / enStats.maxHealth;
	}
}
