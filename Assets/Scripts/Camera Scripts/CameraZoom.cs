using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float targetOrtho;
    public float scroll = 0f;
    public float minOrtho = 2.5f;
    public float maxOrtho = 60f;

    public float zoomSpeed = 25f;
    public float smoothSpeed = 20f;
    private float minSpeed = 10f;
    private float maxSpeed = 25f;


    // Start is called before the first frame update
    void Start()
    {
        targetOrtho = Camera.main.orthographicSize;
    }

    void Update()
    {
        scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            zoomSpeed = (((targetOrtho - minOrtho) * (maxSpeed - minSpeed)) / (maxOrtho - minOrtho)) + minSpeed;
           // smoothSpeed = (((targetOrtho - minSpeed) * (maxOrtho - minOrtho)) / (maxSpeed - minSpeed)) + minOrtho;
            targetOrtho -= scroll * zoomSpeed;
            targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);
        }

        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);
    }
}
