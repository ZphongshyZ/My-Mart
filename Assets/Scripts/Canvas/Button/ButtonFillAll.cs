using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFillAll : MyBehaviour
{
    [SerializeField] private Animator buttonAnimation;
    private int containerID;
    public int ContainerID { set => containerID = value; }

    protected override void LoadComponents()
    {
        this.buttonAnimation = GetComponent<Animator>();
    }

    private void Update()
    {
        this.buttonAnimation.SetBool("unEnable", GameManager.Instance.Player.money < GameManager.Instance.Containers[containerID].ContainerSO.fillCost);
    }
}
