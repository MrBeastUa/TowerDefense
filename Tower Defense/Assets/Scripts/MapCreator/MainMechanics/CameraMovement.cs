using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public static CameraMovement instance;


    [SerializeField]
    private GameObject Cam;
    private float Y_;
    private float X_;

    public static float speed = 0.05f;

    private bool R = false;
    private bool L = false;
    private bool U = false;
    private bool D = false;

    private bool is_Mouse_Downned = false;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        Y_ = Cam.transform.position.y;
        X_ = Cam.transform.position.x;
    }

    void Update()
    {

        if (R == true)
        {
            X_ += speed;
        }
        if (L == true)
        {
            X_ -= speed;
        }
        if (U == true)
        {
            Y_ += speed;
        }
        if (D == true)
        {
            Y_ -= speed;
        }

        Cam.transform.position = new Vector3(X_, Y_, Cam.transform.position.z);
    }

    public void initCameraPosition(float x, float y)
    {
        X_ = x;
        Y_ = y;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (is_Mouse_Downned == true)
        {
            R = false;
            L = false;
            U = false;
            D = false;

            is_Mouse_Downned = false;

            if ((Mathf.Abs(eventData.delta.x)) > (Mathf.Abs(eventData.delta.y)))
            {
                if (eventData.delta.x > 0)
                {
                    print("Right");
                    R = true;

                }
                else
                {
                    print("Left");
                    L = true;
                }
            }
            else
            {
                if (eventData.delta.y > 0)
                {
                    print("Up");
                    U = true;
                }
                else
                {
                    print("Down");
                    D = true;
                }
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        is_Mouse_Downned = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        is_Mouse_Downned = false;

        R = false;
        L = false;
        U = false;
        D = false;
    }
}
