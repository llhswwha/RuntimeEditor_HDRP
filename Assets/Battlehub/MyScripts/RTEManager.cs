using System.Collections;
using System.Collections.Generic;
using Battlehub.RTHandles;
using UnityEngine;

public class RTEManager : MonoBehaviour
{

    public bool IsToolBarVisible=false;

    public bool IsHandlesEnabled=false;

    public bool IsEditorOpen=false;

    public GameObject HandlesUI;

    public GameObject Gizmo;

    public GameObject HdrpObj;

    public RuntimeToolsInput toolsInput;
    
    public RuntimeSceneInput sceneInput;

    

    public RuntimeSelectionComponent selectionComponent;

    //public Battlehub.RTCommon.RTEBase RTE;

    public static RTEManager Instance;
    // public GameObject EditorHandlesPrefab;
    // public GameObject EditorHandles;

    void Awake()
    {
        Instance=this;
    }

    void Start(){
        InitState();
    }

    [ContextMenu("InitState")]
    public void InitState(){
        ShowToolbar();
        DisableHandles();//没有轴

        IsToolBarVisible=false;
        HandlesUI.SetActive(false); //UI
        // Gizmo.SetActive(false); //轴显示
    }

    // void Update(){
    //     if(RTE==null){
    //         RTE=GameObject.FindObjectOfType<Battlehub.RTCommon.RTEBase>();
    //     }
        
    // }

    public void ToggleToolbar()
    {
        if(IsToolBarVisible){
            HideToolbar();
        }
        else{
            ShowToolbar();
        }
    }

    [ContextMenu("ShowToolbar")]
    public void ShowToolbar(){
        IsToolBarVisible=true;
        HandlesUI.SetActive(true); 
        Gizmo.SetActive(true); 
        HdrpObj.SetActive(true);
        toolsInput.gameObject.SetActive(true);
        sceneInput.EnableSelection=true;

        //Editor
        RTEditor.gameObject.SetActive(true);
    }

    [ContextMenu("HideToolbar")]
    public void HideToolbar(){
        IsToolBarVisible=false;
        HandlesUI.SetActive(false); 
        Gizmo.SetActive(false); 
        HdrpObj.SetActive(false);
        toolsInput.gameObject.SetActive(false);
        sceneInput.EnableSelection=false;

        // selectionComponent.TryToClearSelection();

         //Editor
        RTEditor.gameObject.SetActive(false);
    }

    [ContextMenu("DisableHandles")]
    public void DisableHandles(){
        IsHandlesEnabled=false;
        selectionComponent.IsPositionHandleEnabled=false;
        selectionComponent.IsRotationHandleEnabled=false;
        selectionComponent.IsScaleHandleEnabled=false;
        selectionComponent.IsRectToolEnabled=false;
    }

    [ContextMenu("EnableHandles")]
    public void EnableHandles(){
        IsHandlesEnabled=true;
        selectionComponent.IsPositionHandleEnabled=true;
        selectionComponent.IsRotationHandleEnabled=true;
        selectionComponent.IsScaleHandleEnabled=true;
        selectionComponent.IsRectToolEnabled=true;
    }

    
    public void ToggleHandles()
    {
        if(selectionComponent.IsPositionHandleEnabled){
            DisableHandles();
        }
        else{
            EnableHandles();
        }
    }

    public GameObject HandleRootObj;
    public Battlehub.RTCommon.CreateEditor RTEditor;

    public void OpenSceneEditor(){
        IsEditorOpen=true;
        HandleRootObj.SetActive(false);
        RTEditor.OnOpen();
    }

    public void CloseSceneEditor(){
        IsEditorOpen=false;
        HandleRootObj.SetActive(true);
        RTEditor.Editor.Close();
    }

    public void ToggleEditor()
    {
        if(IsEditorOpen){
            CloseSceneEditor();
        }
        else{
            OpenSceneEditor();
        }
    }

    public void ShowProBuilder(){

    }

    public void ShowProperies(){

    }

    public void ShowMaterialProperties(){

    }
}
