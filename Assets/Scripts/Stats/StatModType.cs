using System.Collections;
using System.Collections.Generic;

public enum StatModType // allows percentages and flat values to be calculated for stat modifiers
{
    Flat = 100,
    PercentAdd = 200, //add percentages
    PercentMult = 300,
    //initializing these three variables allows for the order of statmodtype to be customizable. for instance if there was need for a flat modifier to be applied between percentAdd and percent Mult we can assign it an Order number between 201-299
}
