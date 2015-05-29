using UnityEngine;
using System.Collections;

public class Mana : Extender {

    public ManaInfo manaState;
    public bool manaStateChanged = false;

    public void AddMana(int howMuch)
    {
        manaState.CurrentMana += howMuch;
        manaStateChanged = true;
    }

    public void SetMana(int number)
    {
        manaState.CurrentMana = number;
        manaStateChanged = true;
    }

    public void SubtractMana(int howMuch)
    {
        manaState.CurrentMana -= howMuch;
        manaStateChanged = true;
    }
 
}

[System.Serializable]
public class ManaInfo
{
    public int currentMana;
    public int maxMana;

    public int CurrentMana
    {
        get
        {
            return currentMana;
        }
        set
        {
            currentMana = value;
            if (currentMana > maxMana)
                currentMana = maxMana;
        }
    }

    public int MaxMana
    {
        get
        {
            return maxMana;
        }
        set
        {
            maxMana = value;
            if (currentMana > maxMana)
                currentMana = maxMana;
        }
    }
}
