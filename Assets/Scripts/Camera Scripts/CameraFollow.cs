using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public bool toggleFollow = true;
    [HideInInspector] public bool toggleToggleFollow = true;
    public float followSmoothing = 0.05f;
    public float panSmoothing = 5f;
    public float panSpeed = 10f;
    public float panBorderRadius = 40f;
    private Vector2 maxPos;
    private Vector2 minPos;
    private Vector2 convert;
    private CameraZoom zoom;
    private GameController game;

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
        game = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        //Toggle free camera movement
        if (toggleToggleFollow)
        {
            if (Input.GetKeyDown("c"))
            {
                toggleFollow = !toggleFollow;
                target = null;
            }
        }

        // Switch focus
        if (Input.GetMouseButtonDown(0))
        {
            if (toggleFollow)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                if (hit.transform != null)
                {
                    if (hit.transform.gameObject.tag == "monkey")
                    {
                        target = hit.transform;
                        Focus();
                    }
                    else if (hit.transform.gameObject.tag == "tree")
                    {
                        target = hit.transform;
                        Focus();
                    }
                }
                else
                {
                    target = null;
                }
            }
            else
            {
                target = null;
            }
        }

        //Free camera movement with mouse
        if (target == null)
        {
            if (toggleFollow)
            {
                Vector3 targetPos = Input.mousePosition;

                if ((targetPos.x - Screen.width / 2) * (targetPos.x - Screen.width / 2) + (targetPos.y - Screen.height / 2) * (targetPos.y - Screen.height / 2) >= panBorderRadius * panBorderRadius)
                {
                    targetPos = Camera.main.ScreenToWorldPoint(targetPos);

                    if (targetPos.y > transform.position.y)
                    {
                        targetPos.y += panSpeed * Time.deltaTime;
                    }
                    else if (targetPos.y < transform.position.y)
                    {
                        targetPos.y -= panSpeed * Time.deltaTime;
                    }
                    else if (targetPos.x > transform.position.x)
                    {
                        targetPos.x += panSpeed * Time.deltaTime;
                    }
                    else if (targetPos.x < transform.position.x)
                    {
                        targetPos.x -= panSpeed * Time.deltaTime;
                    }

                    targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
                    targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);

                    if (game.paused)
                    {
                        transform.position = Vector3.Lerp(transform.position, targetPos, panSmoothing / 100);
                    }
                    else
                    {
                        transform.position = Vector3.Lerp(transform.position, targetPos, panSmoothing * Time.deltaTime);
                    }
                }
            }
        }
        // Follow the specified target
        else
        {
            if (transform.position != target.position)
            {
                Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
                targetPos.x = Mathf.Clamp(targetPos.x, minPos.x, maxPos.x);
                targetPos.y = Mathf.Clamp(targetPos.y, minPos.y, maxPos.y);

                if (game.paused)
                {
                    transform.position = Vector3.Lerp(transform.position, targetPos, followSmoothing / 100);
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, targetPos, followSmoothing * Time.deltaTime);
                }
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

    void Focus()
    {
        zoom.scroll = 5f;
        zoom.targetOrtho -= zoom.scroll * zoom.zoomSpeed;
        zoom.targetOrtho = Mathf.Clamp(zoom.targetOrtho, 4f, zoom.maxOrtho);

        if (game.paused)
        {
            Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, zoom.targetOrtho, zoom.smoothSpeed / 100);
        }
        else
        {
            Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, zoom.targetOrtho, zoom.smoothSpeed * Time.deltaTime);
        }
    }
}
