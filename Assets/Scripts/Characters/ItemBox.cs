using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MyBehaviour
{
    [SerializeField] private GameObject itemInBox;
    [SerializeField] private List<ItemSO> items = new List<ItemSO>();
    public List<ItemSO> Items { get => this.items; set => this.items = value; }

    [SerializeField] private bool onObj;
    public bool OnObj { get => onObj; set { onObj = value; } }

    [SerializeField] private float timer = 0f;
    [SerializeField] private float timeOff = 3f;

    protected override void OnEnable()
    {
        SpriteRenderer spriteRenderer = this.itemInBox.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) return;
        if(this.items.Count == 0) return;
        spriteRenderer.sprite = this.items[0].itemSprite;
    }

    protected override void LoadComponents()
    {
        this.LoadItemInBox();
    }

    protected void LoadItemInBox()
    {
        foreach(Transform child in this.transform)
        {
            if(child.name == "Item_Image")
            {
                this.itemInBox = child.gameObject;
            }
        }
    }

    private void Update()
    {
        this.TurnOff();
    }

    private void OnMouseOver()
    {
        this.timer = 0f;
        this.onObj = true;
    }

    private void OnMouseExit()
    {
        this.onObj = false;
    }

    private void TurnOff()
    {
        if (!this.gameObject.activeSelf) return;
        if (this.onObj) return;
        this.timer += Time.deltaTime;
        if (this.timer < this.timeOff) return;
        this.gameObject.SetActive(false);
        this.timer = 0f;
    }
}
