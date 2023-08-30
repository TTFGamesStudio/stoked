using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class lookController : MonoBehaviour
{
    public Vector2 YRotClamp;
    public Vector2 xRotClamp;

    private float yRot;

    [SerializeField] private float lookSpeed;

    [SerializeField] private Vector2 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        yRot = transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        float rotAmmount = Time.deltaTime * lookSpeed;

        if (mousePosition.x < .2f && yRot > YRotClamp.x)
        {
            yRot -= rotAmmount;
        }
        else
        {
            if (mousePosition.x > 0.8f && yRot < YRotClamp.y)
            {
                yRot += rotAmmount;
            }
        }
        
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,yRot,transform.rotation.eulerAngles.z);
    }
}
