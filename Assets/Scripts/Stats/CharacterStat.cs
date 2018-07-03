// Base stat script. creates a list from the "StatMod" class which allows multiple stat modifiers to be added to the character at once

using UnityEngine;
using System; //allows the round function to work
using System.Collections.Generic;

[Serializable] //allows the public fields of this class to be editable through the unity inspector
public class CharacterStat
{

    public float BaseValue;


    public virtual float Value
    {
        get
        {
            if (isDirty || lastBaseValue != BaseValue)
            {
                lastBaseValue = BaseValue;
                _value = CalculateFinalVal(); // calculates final value of stat modifier list when data is changed
                isDirty = false; //prevents a constant loop of calculation
            }
            return _value;
        }
    }

    protected bool isDirty = true; //isDirty  is set to true by default
    protected float _value;
    protected float lastBaseValue = float.MinValue;

    [SerializeField] //allows the StatMod List to be seen in the Unity inspector while still not being editable
    protected readonly List<StatMod> statMods; //statMods variable will contain the list of Stat Modifiers. making it readonly makes it so that statMod is not editiable... (maybe we can look into heaps?)

    public CharacterStat() //just a parameterless constructor for CharacterStat class to prevent null reference exception due to statMod not being initialized
    {
        statMods = new List<StatMod>();
    }



    public  CharacterStat(float baseValue) : this() // creates a new instance for the statMod list
    {
        BaseValue = baseValue;
    }
      



public virtual void AddMod(StatMod mod) // a function to remove stat mods from the list
{
    isDirty = true; //will recalculate modifier list when a modifer is added
    statMods.Add(mod);
    statMods.Sort(CompareModifierOrder); //sort the modifiers according to flat or percentage type modifier by using the comparison func below
}



public virtual bool RemoveMod(StatMod mod) // a function to remove stat modifier from the list
{
    if (statMods.Remove(mod)) //only set isDirty to true when a modifier is removed
    {
        isDirty = true;
        return true;
    }
    return false;

    }

public virtual bool RemoveAllModifiersFromSource(object source) //removes all stat mods from a source (e.g remove all mods from the unequipped "sword" or when character is no longer on a color tile) 
{
    bool didRemove = false;

    for (int i = statMods.Count - 1; i >= 0; i--) //going backwards through the stat mod list for most efficient removal of modifiers
    {
        if (statMods[i].source == source)
        {
            isDirty = true;
            didRemove = true;
            statMods.RemoveAt(i);
        }

    }
    return didRemove;
}

protected virtual int CompareModifierOrder(StatMod a, StatMod b) //compares two elements in the list
{
    if (a.Order < b.Order)
        return -1;

    else if (a.Order > b.Order)
        return 1;
    return 0; //if a and b are equivalent, no changes
}

protected virtual float CalculateFinalVal() //calculates all the values in stat modifier list
{
    float finalValue = BaseValue;
    float sumPercentAdd = 0;

    for (int i = 0; i < statMods.Count; i++)
        {
        StatMod mod = statMods[i];

        if (mod.Type == StatModType.Flat)
        {
            finalValue += mod.Value;
        }

        else if (mod.Type == StatModType.PercentAdd) // When we encounter a "PercentAdd" modifier. i think we'll use this modifier the most
        {
            sumPercentAdd += mod.Value; //Start adding together all modifiers of this type

            if (i + 1 >= statMods.Count || statMods[i + 1].Type != StatModType.PercentAdd) // add percentages together until end of list or next value isn't percent type
            {
                finalValue *= 1 + sumPercentAdd; //multiply the sum of percentages with the final value
                sumPercentAdd = 0; //resets sum to 0
            }
        }

        else if (mod.Type == StatModType.PercentMult) //only use percent mult if we need to multiply percentages
        {
            finalValue *= 1 + mod.Value;
        }
    }

    return (float)Math.Round(finalValue, 4); //rounds to nearest integer from 4 significant digits.

}
	
}

