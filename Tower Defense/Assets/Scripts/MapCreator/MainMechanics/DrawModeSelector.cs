using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawModeSelector : MonoBehaviour
{
    public static DrawModeSelector instance;

    [SerializeField]
    public Camera camera;
    [SerializeField]
    private GameObject pencil;
    public DrawMode drawMode = DrawMode.WatchMode;

    private GameObject currentTool;

    public void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        switch (drawMode)
        {
            case DrawMode.Pencil:
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            currentTool = Instantiate(pencil);
                            currentTool.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                            break;
                        case TouchPhase.Moved:
                            if (currentTool != null)
                                currentTool.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
                            break;
                        case TouchPhase.Ended:
                            Object.Destroy(currentTool);
                            currentTool = null;
                            break;
                    }
                }
                break;
            case DrawMode.WatchMode:
                
                break;
        }

     
    }

    public void SelectPencil()
    {
        drawMode = DrawMode.Pencil;
    }

    public void SelectWatchMode()
    {
        drawMode = DrawMode.WatchMode;
    }
}

public enum DrawMode
{
    WatchMode,
    Pencil
}
