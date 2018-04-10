using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sights
{
    Camp_1,
    GraveYard_DarkWood,

}

public class ExplorationSystem : MonoBehaviour {

    public bool seenThis = false;
    public GameObject myIcon;
    public Sights me;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hi");
        if (collision.gameObject.tag == "Player")
        {
            myIcon.SetActive(true);
            //Show Name
            //GetComponent<Collider>().enabled = false;
        }
    }
}
