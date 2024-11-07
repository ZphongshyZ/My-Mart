using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSpawner : Spawner
{
    //Singleton
    private static FXSpawner instance;
    public static FXSpawner Instance { get => instance; }

    protected override void Awake()
    {
        base.Awake();
        if (FXSpawner.instance != null) Debug.LogError("Only one FXSpawner allowed to exist");
        FXSpawner.instance = this;
    }
}
