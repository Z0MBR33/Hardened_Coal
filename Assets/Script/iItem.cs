using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iItem
{
    bool stackable
    {
        get; set;
    }


    int stackAmount
    {
        get; set;
    }

    bool Use();
    bool Trash();
    bool Equip();
}
