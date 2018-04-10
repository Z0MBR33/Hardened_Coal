using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum element
{
    fire, water, earth, lightning, holy, dark, ghost
}

public class Armor : MonoBehaviour  {
    public GameObject prefab;

    public float ph_Resistance;

    //public element element;
    public float fireResistance;
    public float waterResistance;
    public float earthResistance;
    public float lightningResistance;
    public float holyResistance;
    public float darkResistance;
    public float ghostResistance;
    public float iceResistance;

    public bool stackable
    {
        get
        {
            throw new System.NotImplementedException();
        }

        set
        {
            throw new System.NotImplementedException();
        }
    }

    //public int stackAmount { get{ return _stackAmount; } ,set { _StackAmount =  } }


    // Use this for initialization
    void Start () {
		
	}

    void getSmithed()
    {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool Use()
    {
        throw new System.NotImplementedException();
    }

    public bool Trash()
    {
        throw new System.NotImplementedException();
    }

    public bool Equip()
    {
        throw new System.NotImplementedException();
    }
}
