using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buying : MyBehaviour
{
    public bool Buy(Transform containers, string name, int quantity)
    {
        bool canBuy;
        if (containers.GetComponent<Container>() == null) canBuy = false;
        canBuy = containers.GetComponent<Container>().isBought(name, quantity);
        return canBuy;
    }
}
