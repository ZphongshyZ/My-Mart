using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockContainerUI : MyBehaviour
{
    [SerializeField] private Container container;
    public Container Container { get => this.container; set => this.container = value; }

    protected override void OnEnable()
    {
        this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = this.container.ContainerSO.nameContainer;
        this.transform.GetChild(1).GetComponent<Image>().sprite = this.container.ContainerSO.containerSprite;
        this.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = this.container.ContainerSO.unlockCost.ToString();
        this.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate ()
        {
            GameManager.Instance.UnlockContainer(this.container);
        });
    }
}
