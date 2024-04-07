using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float pitch;
    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float rotateSpeed;
    private float currentZoom = 10f;
    private float currentRotate = 0f;
    private void Update()
    {
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        currentRotate -= Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;
    }
    private void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target, Vector3.up * pitch);
        transform.RotateAround(target.position, Vector3.up, currentRotate);
    }
}