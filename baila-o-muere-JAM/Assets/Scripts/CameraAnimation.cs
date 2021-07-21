using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    float zoomFov = 30f;
    float defaultFov = 66f;

    [SerializeField, Range(0.5f, 3f)]
    private float zoomSpeed = 1f;

    Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        
    }

    public void ZoomIn()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFovOverTime(cam.fieldOfView, zoomFov));
    }

    public void ZoomOut()
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFovOverTime(cam.fieldOfView, defaultFov));
    }

    IEnumerator ChangeFovOverTime(float currentFov, float targetFov)
    {
        float t = 0;
        while (t < 1f)
        {
            cam.fieldOfView = Mathf.SmoothStep(currentFov, targetFov, t);
            t += Time.deltaTime*zoomSpeed;
            yield return null;
        }
    }
}
