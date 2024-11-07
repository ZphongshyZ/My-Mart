using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ListItemUI : MyBehaviour
{
    protected override void OnEnable()
    {
        this.DeleteAllChildrenExceptFirst(this.transform);
        GameObject itemTemplate = transform.GetChild(0).gameObject;
        GameObject g;
        for(int i = 0; i < GameManager.Instance.Items.Count; i++)
        {
            int index = i;
            g = Instantiate(itemTemplate, transform);
            g.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.Instance.Items[i].itemSprite;
            g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.Items[i].nameItem;
            g.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Level " + GameManager.Instance.Items[i].level.ToString()+ ".";
            if(g.transform.GetChild(3).GetComponent<ButtonUpgradeItem>() == null)
            {
                g.transform.GetChild(3).AddComponent<ButtonUpgradeItem>();
            }
            g.transform.GetChild(3).GetComponent<ButtonUpgradeItem>().ItemID = index;
            g.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.Items[i].upgardeCost.ToString();
            g.GetComponentInChildren<Button>().onClick.AddListener(delegate ()
            {
                GameManager.Instance.UpgradeItem(index);
                this.transform.GetChild(index).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Level " + GameManager.Instance.Items[index].level.ToString() + ".";
                this.transform.GetChild(index).GetChild(4).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.Items[index].upgardeCost.ToString();
            });
        }

        Destroy(itemTemplate);
    }
}
