using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]

public class ItemSO : ScriptableObject
{
    //Image
    public Sprite itemSprite;
    //Name
    public string nameItem;
    //Quantity
    public int quantity;
    public int maxquantity;
    //Cost
    public int cost;
    public int upgardeCost;
    public int fillCost;
    //Level
    public int level;
}
