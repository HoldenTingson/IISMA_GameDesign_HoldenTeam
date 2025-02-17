using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMapCamController : MonoBehaviour
{
    private Vector3 origin;
    private Vector3 difference;

    private Vector3 resetCamera;
    public float smoothing = 5f;
    Vector3 offset;

    [SerializeField] Transform target;


    private void Start()
    {
        resetCamera = transform.position;
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
}