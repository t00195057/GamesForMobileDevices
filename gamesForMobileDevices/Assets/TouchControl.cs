using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchControl : MonoBehaviour
{
    Controlable my_obj;
    bool mouceDown = false;
    public Camera my_camera = new Camera();
    float countdown = 0.3f;
    bool tapped = false;
    bool tapIsUp = false;
    Plane plane;
    float initialcam_angle;
    float initialobj_angle;
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
    float initial_rotation;
    float cameraSpeed = 0.01f;
    Touch previousTouch2;
    void Start()
    {
        my_camera = Camera.main; 
    }

    // Update is called once per frame
    float timeLeft = 2;
    void Update()
    {
        
        print(Input.acceleration);
        if (Input.touchCount >0)
        {
      if(tapped)
        {
            countdown -= Time.deltaTime;
         
        }
               
                if (Input.touchCount == 2)
                {
                    Touch touch1 = Input.GetTouch(0);
                    Touch touch2 = Input.GetTouch(1);
                   
                    Vector2 touchOnePrevPos = touch1.position - touch1.deltaPosition;
                    Vector2 touchTwoPrevPos = touch2.position - touch2.deltaPosition;                     
                    float prevTouchDeltaMag = (touchOnePrevPos - touchTwoPrevPos).magnitude;
                    float touchDeltaMag = (touch1.position - touch2.position).magnitude;
                    float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
                    float pinchAmount = deltaMagnitudeDiff * 0.2f * Time.deltaTime;

                    if (deltaMagnitudeDiff > 2 || deltaMagnitudeDiff < -2)
                    {
                        if (my_obj)
                        {
                            my_obj.transform.localScale -= new Vector3(pinchAmount, pinchAmount, pinchAmount);
                       
                        }
                        if (!my_obj)
                        {
                            Vector3 focalPoint = Vector3.zero;
                            Vector3 direction = my_camera.transform.position - focalPoint;
                            float newDistance = direction.magnitude / deltaMagnitudeDiff;
                            my_camera.transform.position = newDistance * direction.normalized;
                   
                        }
                    }
               
                    else
                    {  
                        Vector2 diff = touch2.position - touch1.position;
            
                    
                  

                    var angle = Mathf.Rad2Deg * (Mathf.Atan2(diff.y, diff.x));
                         
                             previousTouch2 = touch2;
                    if (my_obj)
                        {
                         var initialobj_orie= my_obj.transform.rotation;
                        my_obj.transform.rotation = initialobj_orie*Quaternion.AngleAxis(angle - initialobj_angle, my_obj.transform.forward);
                        initialobj_angle = angle;
                    }
                        else
                        {
                             var initialcam_orie= my_camera.transform.rotation;
                            my_camera.transform.rotation = initialcam_orie*Quaternion.AngleAxis(angle- initialcam_angle, my_camera.transform.forward);
                        initialcam_angle = angle;
                        }
                   
                    }
                }
            


        
      
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                tapped = true;
               
                tapIsUp = false;
            

            }
           
                if (Input.touchCount == 1)
                {
                    Touch current_touch = Input.GetTouch(0);
                    Ray my_ray = Camera.main.ScreenPointToRay(current_touch.position);
                    RaycastHit info;

                if (countdown > 0)
                    {
                         if (Physics.Raycast(my_ray, out info))
                         {
                            Controlable object_hit = info.transform.GetComponent<Controlable>();

                            if (object_hit)
                            {
                                if (my_obj)
                                {
                                    my_obj.deselect();
                                     my_obj = null;
                                }
                                object_hit.select(object_hit);
                                my_obj = object_hit;
                            }

                        else
                        {
                            if (my_obj)
                            {
                                my_obj.deselect();
                                my_obj = null;
                            }
                        }
                    }
                        else
                        {
                            if (my_obj)
                            {
                                my_obj.deselect();
                                my_obj = null;
                        }

                        }
                }
                
                else
                {
                    if (my_obj)
                    {
                        Vector3 cameraPos = my_camera.transform.position;
                        my_obj.dragObject(my_ray, cameraPos);
                    }

                    if (!my_obj)
                    {
                        my_camera.transform.position += cameraSpeed * my_camera.transform.right * current_touch.deltaPosition.x + cameraSpeed * my_camera.transform.up * current_touch.deltaPosition.y;

                    }
                }
            }
                if(Input.GetTouch(0).phase== TouchPhase.Ended)
                { 
                tapped = false;
                countdown = 0.3f;
                }
        }
    }

}
