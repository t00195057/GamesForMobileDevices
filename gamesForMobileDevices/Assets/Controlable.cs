using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlable : MonoBehaviour
{ Vector3 des_Destionation;
    Renderer my_renderer;
    Controlable my_obj;
 
    // Start is called before the first frame update
    void Start()
    {
        des_Destionation = transform.position;
        my_renderer = GetComponent<Renderer>();
        my_obj = GetComponent<Controlable>();
   
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, des_Destionation, 0.2f);
    }

    internal void move_up()
    {
        my_renderer.material.color = Color.yellow;

    }

    internal void dragObject(Ray r, Vector3 cameraPos)
    {
        var objectDistance = Vector3.Distance(my_obj.transform.position, cameraPos);

        my_obj.newDragPosition(r.GetPoint(objectDistance));

    }
    internal void scaleObject()
    {
        my_renderer.material.color = Color.yellow;

    }
    internal void rotateObject()
    {
        my_renderer.material.color = Color.yellow;

    }
  
    internal void newDragPosition(Vector3 vector)
    {
        des_Destionation = vector;
    
    }
    internal void go_red()
    {
        my_renderer.material.color = Color.red;
    }

    internal void go_white()
    {
        my_renderer.material.color = Color.white;
    }

 

    internal void select(Controlable tempObJ)
    {
        go_red();
        my_obj = tempObJ;
    }

    internal void deselect()
    {
     go_white();
        my_obj = null;
    }
}
