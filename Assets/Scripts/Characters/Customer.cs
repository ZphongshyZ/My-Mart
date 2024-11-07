using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : MyBehaviour
{
    [SerializeField] private CharacterMovement movement;
    [SerializeField] private BFSFindingPath findingPath;
    [SerializeField] private Buying buying;

    [SerializeField] private Transform targetPoint;
    [SerializeField] private Transform payPoint;
    [SerializeField] private Transform outPoint;

    [SerializeField] private string itemWant;
    [SerializeField] private int quantityWant;
    public int QuantityWant { get => quantityWant; }
    [SerializeField] private int totalPrice;
    [SerializeField] private float timeChosing;

    [SerializeField] private bool isSatisfied = false;
    public bool IsSatisfied { get => isSatisfied; set => isSatisfied = value;}

    [SerializeField] private bool isDone = false;
    public bool IsDone { get =>  isDone; set => isDone = value;}

    protected override void OnEnable()
    {
        this.isSatisfied = false;
        this.isDone = false;
        StartCoroutine(this.Shopping());
    }

    protected override void LoadComponents()
    {
        this.movement = GetComponentInChildren<CharacterMovement>();
        this.findingPath = GetComponentInChildren<BFSFindingPath>();
        this.buying = GetComponent<Buying>();
    }

    private void SetDesire()
    {
        int randContainer = Random.Range(0, GameManager.Instance.Containers.Count);
        this.itemWant = GameManager.Instance.GetContainer(randContainer).RandItemName();
        this.quantityWant = Random.Range(1, 11);
        this.targetPoint = GameManager.Instance.GetContainer(randContainer).Stand;
        this.totalPrice = GameManager.Instance.GetContainer(randContainer).GetCostItem(this.itemWant) * this.quantityWant;
        this.outPoint = GameManager.Instance.RandSpawnPos();
    }

    private IEnumerator Shopping()
    {
        yield return StartCoroutine(this.Searching());
        yield return StartCoroutine(this.movement.MoveAlongPath(this.findingPath.PathBFS(this.transform.position, this.targetPoint.position)));
        yield return IsChosing();
        yield return StartCoroutine(this.movement.MoveAlongPath(this.findingPath.PathBFS(this.targetPoint.position, this.payPoint.position)));
        yield return new WaitForSeconds(1f);
        if(this.isSatisfied)
        {
            Vector2 fxPos = this.payPoint.parent.position;
            fxPos.y += 1.5f;
            Transform fx = FXSpawner.Instance.Spawn("Coin", fxPos, this.payPoint.parent.rotation);
            fx.gameObject.SetActive(true);
        }
        yield return StartCoroutine(this.movement.MoveAlongPath(this.findingPath.PathBFS(this.payPoint.position, this.outPoint.position)));
        yield break;
    }    

    private IEnumerator Searching()
    {
        this.SetDesire();
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator IsChosing()
    {
        yield return new WaitForSeconds(this.timeChosing);
        this.isSatisfied = this.buying.Buy(this.targetPoint.parent, this.itemWant, this.quantityWant);
        yield return null;
    }

    public int Pay()
    {
        this.isDone = true;
        return totalPrice;
    }
}
