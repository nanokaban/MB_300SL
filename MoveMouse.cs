using UnityEngine;
using System.Collections;

public class MoveMouse : MonoBehaviour
{
    public GameObject go;
    public float Sensitivity = 2f;
    private Camera goCamera;
    private float x;
    private float y;

    void Start()
    {
        goCamera = GetComponent<Camera>();
        go = goCamera.transform.parent.gameObject;
    }

    void Update()
    {
        x = Input.GetAxis("Mouse X") * Sensitivity;
    }

    void FixedUpdate()
    {     
        goCamera.transform.RotateAround(go.transform.position, goCamera.transform.up, x);
    }
}
