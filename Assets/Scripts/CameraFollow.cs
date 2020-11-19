using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float followSmoothing = 0.05f;
    public float panSmoothing = 5f;
    public float panSpeed = 10f;
    public float panBorderThickness = 40f;
    private Vector2 maxPos;
    private Vector2 minPos;
    private Vector2 convert;
    private CameraZoom zoom;

    // KEEP THESE VALUES CONSTANT
    private float ORTHO_MAX = 2.5f;               
    private float ORTHO_MIN = 10f;
    private float MAX_X = 18.5f;
    private float MIN_X = 5f;
    private float MAX_Y = 8f;
    private float MIN_Y = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        zoom = this.GetComponent<Camera>().GetComponent<CameraZoom>();
    }

    void Update()
    {
        // Switch focus
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.transform != null)
            {
                UnityEngine.Debug.Log(hit.transform.gameObject.name);

                if (hit.transform.gameObject.name.Contains("monkey"))
                {
                    target = hit.transform;

                    zoom.scroll = 5f;
                    zoom.targetOrtho -= zoom.scroll * zoom.zoomSpeed;
                    zoom.targetOrtho = Mathf.Clamp(zoom.targetOrtho, 4f, zoom.maxOrtho);

                    Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, zoom.targetOrtho, zoom.smoothSpeed * Time.deltaTime);
                }
            }
            else
            {
                target = null;
            }
        }

        //Free camera movement with edge-scrolling and standard movement keys
        if (target == null)
        {
            Vector3 targetPos = transform.position;

            if (Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                targetPos.y += panSpeed * Time.deltaTime;
            }
            else if (Input.mousePosition.y <= panBorderThickness)
            {
                targetPos.y -= panSpeed * Time.deltaTime;
            }
            else if (Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                targetPos.x += panSpeed * Time.deltaTime;
            }
            else if (Input.mousePosition.x <= panBorderThickness)
            {
                targetPos.x -= panSpeed * Time.deltaTime;
            }

            targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);
            transform.position = Vector3.Lerp(transform.position, targetPos, panSmoothing);
        }
        // Follow the specified target
        else
        {
            if (transform.position != target.position)
            {
                Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
                targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
                targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);
                transform.position = Vector3.Lerp(transform.position, targetPos, followSmoothing);
            }
        }

        BoundCamera();
    }

    // Sets boundaries for the camera
    void BoundCamera()
    {
        convert.x = (((Camera.main.orthographicSize - ORTHO_MIN) * (MAX_X - MIN_X)) / (ORTHO_MAX - ORTHO_MIN)) + MIN_X;
        convert.y = (((Camera.main.orthographicSize - ORTHO_MIN) * (MAX_Y - MIN_Y)) / (ORTHO_MAX - ORTHO_MIN)) + MIN_Y;
        minPos = new Vector2(-convert.x, -convert.y);
        maxPos = new Vector2(convert.x, convert.y);
    }
}
