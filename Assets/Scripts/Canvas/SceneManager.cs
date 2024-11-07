using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MyBehaviour
{
    //Singleton
    private static SceneManager instance;
    public static SceneManager Instance { get => instance; }

    [SerializeField] private bool isScening;
    public bool IsScening { get => this.isScening; set => this.isScening = value; }

    //Item_List
    [SerializeField] private GameObject itemListBtn;
    [SerializeField] private GameObject itemList;

    //Container_List
    [SerializeField] private GameObject containerListBtn;
    [SerializeField] private GameObject containerList;

    //ItemInContainer_List
    [SerializeField] private GameObject itemContainerList;
    [SerializeField] private GameObject itemContainerListChild;
    public GameObject ItemContainerListChild { get => this.itemContainerListChild; }

    //Unlock Container
    [SerializeField] private GameObject unlockContainer;

    protected override void Awake()
    {
        if (SceneManager.instance != null) Debug.LogError("Only one SceneManager allowed to exist");
        SceneManager.instance = this;
    }

    //Item_List
    public void OpenItemList()
    {
        if (this.isScening) return;
        this.isScening = true;
        this.itemList.SetActive(true);
    }

    public void CloseItemList()
    {
        this.isScening = false;
        this.itemList.SetActive(false);
    }

    //Container_List
    public void OpenContainerList()
    {
        if (this.isScening) return;
        this.isScening = true;
        this.containerList.SetActive(true);
    }

    public void CloseContainerList()
    {
        this.isScening = false;
        this.containerList.SetActive(false);
    }

    //ItemInContainer_List
    public void OpenItemContainerList(int index)
    {
        this.containerList.SetActive(false);
        this.itemContainerList.GetComponentInChildren<ListItemInContainerUI>().Index = index;
        this.itemContainerList.SetActive(true);
    }

    public void CloseItemContainerList()
    {
        this.containerList.SetActive(true);
        this.itemContainerList.SetActive(false);
    }

    //Unlock Container
    public void OpenUnlockContainer(Container container)
    {
        if (this.isScening) return;
        this.isScening = true;
        this.unlockContainer.GetComponent<UnlockContainerUI>().Container = container;
        this.unlockContainer.SetActive(true);
    }

    public void CloseUnlockContainer()
    {
        this.isScening = false;
        this.unlockContainer.SetActive(false);
    }
}
