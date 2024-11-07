using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBehaviour : MonoBehaviour
{
    protected virtual void Start()
    {

    }

    protected virtual void Reset()
    {
        this.LoadComponents();
        this.ResetValue();
    }

    protected virtual void Awake()
    {
        this.LoadComponents();
        this.ResetValue();
    }

    protected virtual void LoadComponents()
    {
        //For override
    }

    protected virtual void ResetValue()
    {
        //For override
    }

    protected virtual void OnEnable()
    {
        //For ovverride
    }

    public virtual void DeleteAllChildrenExceptFirst(Transform parent)
    {
        if (parent.childCount > 1)
        {
            for (int i = parent.childCount - 1; i > 0; i--)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }
    }
}
