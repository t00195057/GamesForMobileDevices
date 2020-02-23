using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIscript : MonoBehaviour
{
    Controlable my_obj;
    // Start is called before the first frame update
    void Start()
    {
        my_obj = GetComponent<Controlable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnGUI()
    {
      if(GUI.Button(new Rect(0,0,150,150),"Restart"))
        {
            Application.LoadLevel(0);
        }
    }
}
