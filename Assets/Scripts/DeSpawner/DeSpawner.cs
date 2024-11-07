using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeSpawner : MyBehaviour
{
    protected virtual void FixedUpdate()
    {
        this.DeSpawning();
    }

    protected virtual void DeSpawning()
    {
        if (!this.CanDeSpawn()) return;
        this.DeSpawnObj();
    }

    public virtual void DeSpawnObj()
    {
        Destroy(transform.parent.gameObject);
    }

    protected abstract bool CanDeSpawn();
}
