using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paying : MyBehaviour
{
    public int Pay(int quantity, int cost)
    { 
        return quantity * cost;
    }
}
