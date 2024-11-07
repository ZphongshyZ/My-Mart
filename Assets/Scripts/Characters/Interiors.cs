using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interiors : MyBehaviour
{
    [SerializeField] List<Container> containers = new List<Container>();
    public List<Container> Containers { get => containers; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadContainer();
    }

    protected void LoadContainer()
    {
        this.containers.Clear();
        foreach(Transform childs in this.transform)
        {
            foreach(Transform child in childs)
            {
                Container container = child.GetComponentInChildren<Container>();
                if (container != null)
                {
                    this.containers.Add(container);
                }
            }
        }
        return;
    }

    public List<Container> ContainerUnlocked()
    {
        List<Container> list = new List<Container>();
        foreach (Container child in this.containers)
        {
            if(child.ContainerSO.unlock)
            {
                list.Add(child);
            }
        }
        return list;
    }
}
