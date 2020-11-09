using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlehub.RTCommon;
public class RTSelectionTool : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Camera camera;

    public GameObject hitObj;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0)){
            if(camera==null)
            {
                camera=Camera.main;
            }
            Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if(Physics.Raycast(ray,out hitInfo)){
                hitObj=hitInfo.collider.gameObject;
                Debug.Log("Hit:"+hitObj);
                ExposeToEditor expose=hitObj.GetComponent<ExposeToEditor>();
                if(expose==null){
                    expose=hitObj.AddComponent<ExposeToEditor>();
                }
            }
            else{
                hitObj=null;
            }
        }
        
    }
}
