using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDeSpawner : DeSpawnByTime
{
    public override void DeSpawnObj()
    {
        CustomerSpawner.Instance.Despawn(this.transform.parent);
    }

    protected override bool CanDeSpawn()
    {
        Customer customer = this.transform.parent.GetComponent<Customer>();
        if(customer == null) return false;
        if(customer.IsDone)
        {
            this.timer += Time.fixedDeltaTime;
            if (this.timer > this.delay) return true;
            return false;
        }
        return false;
    }
}
