using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Container", menuName = "ScriptableObject/Container")]
public class ContainerSO : ScriptableObject
{
    public bool unlock = false;
    public Sprite containerSprite;
    public string nameContainer;
    public int level;
    public int totalQuantity;
    public int fillCost;
    public int unlockCost;
}
