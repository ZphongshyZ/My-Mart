using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MyBehaviour
{
    [SerializeField] private Vector3 origin;
    [SerializeField] private Vector3 difference;
    [SerializeField] private Vector3 resetCamera;

    [SerializeField] private bool drag = false;

    protected override void LoadComponents()
    {
        this.resetCamera = Camera.main.transform.position;
    }

    private void LateUpdate()
    {
        if(SceneManager.Instance.IsScening) return;
        this.Drag();
    }

    private void Drag()
    {
        if (Input.GetMouseButton(0))
        {
            this.difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (!this.drag)
            {
                this.drag = true;
                this.origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            this.drag = false;
        }

        if (this.drag)
        {
            Camera.main.transform.position = this.origin - this.difference;
        }
    }
}
