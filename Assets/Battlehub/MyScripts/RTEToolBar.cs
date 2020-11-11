using System.Collections;
using System.Collections.Generic;
using Battlehub.RTHandles;
using UnityEngine;

public class RTEToolBar : MonoBehaviour
{
    public GameObject HandlesUI;

    public GameObject Gizmo;

    public GameObject HdrpObj;

    public RuntimeToolsInput toolsInput;
    
    public RuntimeSceneInput sceneInput;

    

    public RuntimeSelectionComponent selectionComponent;

    //public Battlehub.RTCommon.RTEBase RTE;

    public static RTEToolBar Instance;
    // public GameObject EditorHandlesPrefab;
    // public GameObject EditorHandles;

    void Awake()
    {
        Instance=this;

        InitState();
    }

    void Start(){
        
    }

    [ContextMenu("InitState")]
    public void InitState(){
        ShowToolbar();
        DisableHandles();//没有轴
        HandlesUI.SetActive(false); //UI
        // Gizmo.SetActive(false); //轴显示
    }

    // void Update(){
    //     
        
    // }

    [ContextMenu("ShowToolbar")]
    public void ShowToolbar(){
        //HandlesUI.SetActive(true); 
        Gizmo.SetActive(true); 
        HdrpObj.SetActive(true);
        toolsInput.gameObject.SetActive(true);
        sceneInput.EnableSelection=true;
    }

    [ContextMenu("HideToolbar")]
    public void HideToolbar(){
        HandlesUI.SetActive(false); 
        Gizmo.SetActive(false); 
        HdrpObj.SetActive(false);
        toolsInput.gameObject.SetActive(false);
        sceneInput.EnableSelection=false;
    }

    [ContextMenu("DisableHandles")]
    public void DisableHandles(){
        HandlesUI.SetActive(false); 
        selectionComponent.IsPositionHandleEnabled=false;
        selectionComponent.IsRotationHandleEnabled=false;
        selectionComponent.IsScaleHandleEnabled=false;
        selectionComponent.IsRectToolEnabled=false;
    }

    [ContextMenu("EnableHandles")]
    public void EnableHandles(){
        HandlesUI.SetActive(true); 
        selectionComponent.IsPositionHandleEnabled=true;
        selectionComponent.IsRotationHandleEnabled=true;
        selectionComponent.IsScaleHandleEnabled=true;
        selectionComponent.IsRectToolEnabled=true;
    }

    public Battlehub.RTCommon.RTEBase HandleRTE;
    public void Close(){

        if(HandleRTE==null)
        {
            HandleRTE=GameObject.FindObjectOfType<Battlehub.RTCommon.RTEBase>();
        }

        if(HandleRTE!=null){
            //HandleRTE.gameObject.SetActive(false);
            GameObject.DestroyImmediate(HandleRTE.gameObject);
        }

        GameObject.DestroyImmediate(this.gameObject);
    }
}
