using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MyBehaviour
{
    //Singleton
    private static GameManager instance;
    public static GameManager Instance { get => instance; }

    [SerializeField] private TextMeshProUGUI money_Text;
    [SerializeField] private TextMeshProUGUI famous_Text;

    [SerializeField] private PlayerSO player;
    public PlayerSO Player { get => player; set => player = value; }

    [SerializeField] private Interiors interiors;
    [SerializeField] private List<Container> containers = new List<Container>();
    public List<Container> Containers { get => this.containers; set => this.containers = value; }

    [SerializeField] private List<ItemSO> items = new List<ItemSO>();
    public List<ItemSO> Items { get => items;}

    [SerializeField] private float timer = 0f;
    [SerializeField] private float timeSpawn = 5f;
    [SerializeField] Transform[] spawnPos = new Transform[] { };

    protected override void Awake()
    {
        if (GameManager.instance != null) Debug.LogError("Only one GameManager allowed to exist");
        GameManager.instance = this;
        this.LoadContainer();
        this.LoadItems();
    }

    protected virtual void LoadContainer()
    {
        this.containers = this.interiors.ContainerUnlocked();
    }

    protected virtual void LoadItems()
    {
        this.items.Clear();
        foreach (Container container in this.containers)
        {
            foreach(ItemSO item in container.Items)
            {
                this.items.Add(item);
            }
        }
        return;
    }

    private void Update()
    {
        this.SpawnCustomer();
        this.MoneyUI();
    }

    public string GetItemName(int index)
    {
        return this.containers[index].RandItemName();
    }

    public Container GetContainer(int index)
    {
        return this.containers[index];
    }

    public Transform RandSpawnPos()
    {
        int ranPos = Random.Range(0, this.spawnPos.Length);
        return this.spawnPos[ranPos];
    }

    private void SpawnCustomer()
    {
        this.timer += Time.deltaTime;
        if (this.timer < this.timeSpawn) return;
        Transform pos = this.RandSpawnPos();
        Transform customer = CustomerSpawner.Instance.Spawn(CustomerSpawner.Instance.RandomPrefab(), pos.position, pos.rotation);
        customer.gameObject.SetActive(true);
        this.timer = 0f;
        return;
    }

    private void MoneyUI()
    {
        this.money_Text.text = this.Player.money.ToString();
        this.famous_Text.text = this.Player.famous.ToString();
    }

    public void UpgradeItem(int index)
    {
        if (this.Player.famous < this.Items[index].upgardeCost) return;
        this.Items[index].quantity = this.Items[index].maxquantity;
        this.Player.famous -= this.Items[index].upgardeCost;
        this.Items[index].level += 1;
        this.Items[index].cost += (int)(this.Items[index].cost*0.6);
        this.Items[index].upgardeCost *= 2;
        this.Items[index].fillCost += (int)(this.Items[index].fillCost * 0.6);
        this.Items[index].maxquantity += 50;
    }

    public void UnlockContainer(Container container)
    {
        if (this.Player.money < container.ContainerSO.unlockCost) return;
        container.ContainerSO.unlock = true;
        this.Player.money -= container.ContainerSO.unlockCost;
        this.LoadContainer();
        this.LoadItems();
        SceneManager.Instance.CloseUnlockContainer();
    }
}
