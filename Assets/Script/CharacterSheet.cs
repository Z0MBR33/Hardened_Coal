using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipSlot
{
    head, shoulder_left, shoulder_right, breastplate, hand_left, hand_right, legarmor, footArmor 
}

public class CharacterSheet : MonoBehaviour {

    private float hitPoints;

    private int strenght;
    private int dexterity;

    private int ember;
    private int ghostState;

    private float movementSpeed;

    private Dictionary<EquipSlot,Armor> Equip;
    private Inventory myInventoy;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
