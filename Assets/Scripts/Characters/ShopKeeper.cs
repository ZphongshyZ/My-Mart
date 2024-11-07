using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopKeeper : MyBehaviour
{
    [SerializeField] private Transform payPoint;

    protected override void LoadComponents()
    {
        this.LoadPayPoint();
    }

    protected void LoadPayPoint()
    {
        foreach (Transform child in this.transform)
        {
            if (child.name == "PayPoint")
            {
                this.payPoint = child;
            }
        }
    }

    private void Update()
    {
        this.Work();
    }

    private void Work()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, Vector2.Distance(transform.position, payPoint.position) + 1f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Customer"))
            {
                Customer customer = hit.collider.GetComponentInParent<Customer>();
                if (customer == null) continue;
                if (customer.IsSatisfied && !customer.IsDone)
                {
                    this.CollectMoney(customer.Pay());
                    this.CollectFamous(customer.QuantityWant);
                    continue;
                }
                continue;
            }
        }
    }

    private void CollectMoney(int totalPrice)
    {
        GameManager.Instance.Player.money += totalPrice;
    }

    private void CollectFamous(int quantity)
    {
        GameManager.Instance.Player.famous += quantity;
    }
}
