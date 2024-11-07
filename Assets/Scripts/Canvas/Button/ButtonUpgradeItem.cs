using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonUpgradeItem : MyBehaviour
{
    [SerializeField] private Animator buttonAnimation;
    private int itemID;
    public int ItemID { set => itemID = value; }

    protected override void LoadComponents()
    {
        this.buttonAnimation = GetComponent<Animator>();
    }

    private void Update()
    {
        this.buttonAnimation.SetBool("unEnable", GameManager.Instance.Player.famous < GameManager.Instance.Items[itemID].upgardeCost);
    }
}
