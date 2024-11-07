using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MyBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float zoomTarget;

    [SerializeField] private float multiplier = 2f, minZoom = 1f, maxZoom = 10f, smoothTime = 0.1f;
    [SerializeField] private float velocity = 0f;

    protected override void LoadComponents()
    {
        this.cam = GetComponent<Camera>();
        this.zoomTarget = cam.orthographicSize;
    }

    private void Update()
    {
        if (SceneManager.Instance.IsScening) return;
        this.Zoom();
    }

    private void Zoom()
    {
        this.zoomTarget -= Input.GetAxisRaw("Mouse ScrollWheel") * this.multiplier;
        this.zoomTarget = Mathf.Clamp(this.zoomTarget, this.minZoom, this.maxZoom);
        this.cam.orthographicSize = Mathf.SmoothDamp(this.cam.orthographicSize, this.zoomTarget, ref this.velocity, this.smoothTime);
    }
}
