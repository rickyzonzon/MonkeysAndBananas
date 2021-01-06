using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float targetOrtho;
    public bool toggleZoom = true;
    [HideInInspector] public bool toggleToggleZoom = true;
    public float scroll = 0f;
    public float zoomSpeed = 5.0f;
    public float smoothSpeed = 10.0f;
    public float minOrtho = 2.5f;
    public float maxOrtho = 10.5f;
    private GameController game;

    // Start is called before the first frame update
    void Start()
    {
        targetOrtho = Camera.main.orthographicSize;
        game = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        if (toggleToggleZoom)
        {
            if (Input.GetKeyDown("c"))
            {
                toggleZoom = !toggleZoom;
            }
        }

        if (toggleZoom)
        {
            scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0.0f)
            {
                targetOrtho -= scroll * zoomSpeed;
                targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);
            }

            if (game.paused)
            {
                Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed / 100);
            }
            else
            {
                Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);
            }
        }
    }
}
