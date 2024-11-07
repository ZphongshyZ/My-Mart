using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Container : MyBehaviour
{
    [SerializeField] private List<ItemSO> items;
    public List<ItemSO> Items { get => this.items; }
    
    [SerializeField] private ContainerSO containerSO;
    public ContainerSO ContainerSO { get => containerSO; set { containerSO = value; } }
    
    [SerializeField] private Transform stand;
    public Transform Stand { get => stand; }

    [SerializeField] private Transform lockContainer;

    [SerializeField] private Animator animator;

    protected override void LoadComponents()
    {
        this.animator = GetComponent<Animator>();
        this.LoadStand();
        this.LoadContainerSO();
        this.LoadLock();
    }

    protected virtual void LoadContainerSO()
    {
        if (this.containerSO != null) return;
        string resPath = "Container/" + transform.name;
        this.containerSO = Resources.Load<ContainerSO>(resPath);
        Debug.Log(transform.name + " Load ContainerSO " + resPath, gameObject);
    }

    protected void LoadStand()
    {
        if (this.stand != null) return;
        foreach (Transform child in this.transform)
        {
            if (child != null && child.name == "Stand")
            {
                this.stand = child;
            }
        }
    }

    protected void LoadLock()
    {
        if (this.lockContainer != null) return;
        foreach (Transform child in this.transform)
        {
            if (child != null && child.name == "Lock")
            {
                this.lockContainer = child;
            }
        }
    }

    private void Update()
    {
        this.lockContainer.gameObject.SetActive(!this.containerSO.unlock);
        this.animator.SetBool("isLocked", !this.containerSO.unlock);
        this.ContainerSO.fillCost = this.GetFillCost();
    }

    public string RandItemName()
    {
        int rand = Random.Range(0, this.items.Count);
        return this.items[rand].name;
    }

    public int GetCostItem(string name)
    {
        int cost = 0;
        foreach(ItemSO item in this.items)
        {
            if(item.nameItem == name)
            {
                cost = item.cost;
            }
        }
        return cost;
    }

    private int GetFillCost()
    {
        int cost = 0;
        foreach (ItemSO item in this.items)
        {
            cost = (item.maxquantity - item.quantity) * item.fillCost;
        }
        return cost;
    }

    private bool QuantityGuaranteed()
    {
        foreach(ItemSO item in this.items)
        {
            if(item.quantity <= 0)
            {
                Debug.Log(item.nameItem);
                return false;
            }
        }
        return true;
    }

    public void AutoFill()
    {
        foreach (ItemSO item in this.items)
        {
            if(item.quantity < item.maxquantity)
            {
                GameManager.Instance.Player.money -= (item.maxquantity - item.quantity)*item.fillCost;
                item.quantity = item.maxquantity;
            }
        }
        return;
    }

    public bool isBought(string nameItem, int quantity)
    {
        foreach(ItemSO item in this.items)
        {
            if (item.nameItem == nameItem)
            {
                if(item.quantity >= quantity)
                {
                    Vector2 posFx = this.transform.position;
                    posFx.y += 0.5f;
                    item.quantity -= quantity;
                    Transform fx = FXSpawner.Instance.Spawn(item.nameItem, posFx, this.transform.rotation);
                    fx.gameObject.SetActive(true);
                    return true;
                }
                AutoFill();
                GameManager.Instance.Player.famous -= quantity;
                Debug.Log("Is not isSatisfied");
                return false;
            }
        }
        if (this.QuantityGuaranteed()) return true;
        this.AutoFill();
        return false;
    }

    public void Fill(int index)
    {
        if (GameManager.Instance.Player.money < ((this.items[index].maxquantity - this.items[index].quantity) * this.items[index].fillCost)) return;
        GameManager.Instance.Player.money -= ((this.items[index].maxquantity - this.items[index].quantity) * this.items[index].fillCost);
        this.items[index].quantity = this.items[index].maxquantity;
    }
}
