using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ListContainerUI : MyBehaviour
{
    protected override void OnEnable()
    {
        this.DeleteAllChildrenExceptFirst(this.transform);
        GameObject itemTemplate = transform.GetChild(0).gameObject;
        GameObject g;
        for (int i = 0; i < GameManager.Instance.Containers.Count; i++)
        {
            int index = i;
            g = Instantiate(itemTemplate, transform);
            g.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.Instance.Containers[i].ContainerSO.containerSprite;
            g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.Containers[i].ContainerSO.nameContainer;
            g.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate ()
            {
                SceneManager.Instance.OpenItemContainerList(index);
            });
            if (g.transform.GetChild(2).GetComponent<ButtonFillAll>() == null)
            {
                g.transform.GetChild(2).AddComponent<ButtonFillAll>();
            }
            g.transform.GetChild(2).GetComponent<ButtonFillAll>().ContainerID = index;
            g.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate ()
            {
                GameManager.Instance.Containers[index].AutoFill();
            });
        }

        Destroy(itemTemplate);
    }
}
