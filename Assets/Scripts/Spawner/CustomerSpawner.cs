using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : Spawner
{
    //Singleton
    private static CustomerSpawner instance;
    public static CustomerSpawner Instance { get => instance; }

    protected override void Awake()
    {
        base.Awake();
        if (CustomerSpawner.instance != null) Debug.LogError("Only one CustomerSpawner allowed to exist");
        CustomerSpawner.instance = this;
    }
}
