using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockContainer : MyBehaviour
{
    private void OnMouseDown()
    {
        if (this.transform.GetComponent<Container>().ContainerSO.unlock) return;
        SceneManager.Instance.OpenUnlockContainer(this.transform.GetComponent<Container>());
    }
}
