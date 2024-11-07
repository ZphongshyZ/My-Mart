using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ListItemInContainerUI : MyBehaviour
{
    [SerializeField] private int index;
    public int Index { set =>  index = value; }
    
    protected override void OnEnable()
    {
        this.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(GameManager.Instance.Containers[this.index].Items.Count * 189f, 300f);
        this.DeleteAllChildrenExceptFirst(this.transform);
        GameObject itemTemplate = transform.GetChild(0).gameObject;
        GameObject g;
        for (int i = 0; i < GameManager.Instance.Containers[this.index].Items.Count; i++)
        {
            int indexItem = i;
            g = Instantiate(itemTemplate, transform);
            g.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.Instance.Containers[this.index].Items[i].itemSprite;
            g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.Containers[this.index].Items[i].nameItem;
            g.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.Containers[this.index].Items[i].quantity.ToString();
            g.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate ()
            {
                GameManager.Instance.Containers[this.index].Fill(indexItem);
                this.transform.GetChild(indexItem).GetChild(2).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.Containers[this.index].Items[indexItem].quantity.ToString();
            });
        }
        Destroy(itemTemplate);
    }
}
