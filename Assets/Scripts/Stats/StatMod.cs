using System.Collections;
using System.Collections.Generic;

public class StatMod // represents stat modifiers
{
    public readonly float Value;
    public readonly StatModType Type;
    public readonly int Order; //this is to organize the order of operations when statmodifiers are calculated
    public readonly object source; //stores info on where a modifier came from (e.g equipment, spells, consumables, even tile colors)


    public StatMod(float value, StatModType type, int order, object Source) //"Main" constructor. requires all variables
    {
        Value = value;
        Type = type;
        Order = order;
        Source = source;
    }


    public StatMod(float value, StatModType type) : this(value, type, (int)type, null) { } // constructor that only requires value and type

    public StatMod(float value, StatModType type, int order) : this(value, type, order, null) { } // only requires value, type, and order

    public StatMod(float value, StatModType type, object source) : this(value, type, (int)type, source) { } // requires value, type, order, and source

}

