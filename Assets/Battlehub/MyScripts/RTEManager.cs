using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlehub.RTCommon;
using Battlehub.RTEditor;
using Battlehub.RTEditor.HDRP;
using Mogoson.CameraExtension;
using Battlehub.RTHandles;

public class RTEManager : MonoBehaviour
{
    public static RTEManager Instance;

    public HDRPInit hdrpInit;

    public bool IsToolBarVisible=false;

    void Awake(){
        Instance=this;
    }

    public void HideHandles(){
        Debug.LogError("RTEManager.HideHandles");
        IsHandlesEnabled=true;
        ToolBar.EnableHandles();
        EditButton.SetActive(false);
    }

    public void ShowHandles(){
        Debug.LogError("RTEManager.ShowHandles");
        IsHandlesEnabled=true;
        ToolBar.EnableHandles();
        EditButton.SetActive(true);
    }

    void Start(){
        EditButton=RTEditor.EditButon.gameObject;
        RTEditor.Created+=OnEditorCreated;
        RTEditor.Closed+=OnEditorClosed; 
        RTEditor.BeforeOpen+=OnEditorBeforeOpen;

        ToggleToolbar();
        hdrpInit.EnableOutline();

        // var volumns=GameObject.FindObjectsOfType<UnityEngine.Rendering.Volumn>();
        // Debug.LogError("RTEManager.Start volumns:"+volumns.Length);
    }

    public bool IsEditorClosed=false;

    void Update(){
        if(IsEditorClosed){
            IsEditorClosed=false;
            ToggleToolbar();
            hdrpInit.EnableOutline();
        }
    }

    public void ToggleToolbar()
    {
        Debug.Log("ToggleToolbar");
        Debug.Log("ToolBar isnull:"+(ToolBar==null));

        if(ToolBar==null){
            Debug.Log("Instantiate ToolBar");
            // Battlehub.RTCommon.IOC.ClearAll();
            // Battlehub.RTCommon.RTEBase.Init();
            GameObject instance=GameObject.Instantiate(ToolBarPrefab);
            instance.SetActive(true);
            ToolBar=instance.GetComponent<RTEToolBar>();
            IsToolBarVisible=false;
        }

        if(IsToolBarVisible){
            Debug.Log("HideToolbar");
            IsToolBarVisible=false;
            ToolBar.HideToolbar();
        }
        else{
            Debug.Log("ShowToolbar");
            IsToolBarVisible=true;
            ToolBar.ShowToolbar();
        }
    }

    public bool IsHandlesEnabled=false;

    public void ToggleHandles()
    {
        if(IsHandlesEnabled){
            HideHandles();
        }
        else{
            ShowHandles();
        }
    }

    public bool IsEditorOpen=false;
    public GameObject ToolBarPrefab;
    public RTEToolBar ToolBar;
    public CreateEditor RTEditor;

    public GameObject EditButton;

    public void OpenSceneEditor(){
        // IsToolBarVisible=false;
        // IsEditorOpen=true;
        // // HandleRootObj.SetActive(false);
        // ToolBar.Close();
        // ToolBar=null;
   
        RTEditor.OnOpen();
    }

    private void OnDestroy()
    {
        if(RTEditor)
        {
            RTEditor.Created-=OnEditorCreated;
            RTEditor.Closed-=OnEditorClosed;  
            RTEditor.BeforeOpen-=OnEditorBeforeOpen;
        }  
    }

    private void OnEditorDestroyed(object sender)
    {
        RuntimeEditor editor=sender as RuntimeEditor;
        Debug.LogError("OnEditorDestroyed:"+sender+"|"+editor.Id);
        //IsEditorOpen=false;
        IsEditorClosed=true;
        //ToggleToolbar();

        // Battlehub.RTCommon.IRTE m_editor = Battlehub.RTCommon.IOC.Resolve<Battlehub.RTCommon.IRTE>();
        // Debug.LogError("m_editor:"+m_editor);
        // Debug.LogError("RTEDeps:"+Battlehub.RTEditor.RTEDeps.Instance);
    }

    
    private void OnEditorCreated(object sender)
    {
        RuntimeEditor editor=sender as RuntimeEditor;
        Debug.LogError("OnEditorCreated:"+sender+"|"+editor.Id+"|"+RTEDeps.Instance);
        // editor.Destroyed+=OnEditorDestroyed;
        RTEDeps.Instance.Destroyed+=OnEditorDestroyed;
        // editor.TestDoDestroyed();
    }

    private void OnEditorBeforeOpen(object sender)
    {
        IsToolBarVisible=false;
        IsEditorOpen=true;
        // HandleRootObj.SetActive(false);
        ToolBar.Close();
        ToolBar=null;
    }

    private void OnEditorClosed(object sender)
    {
        RuntimeEditor editor=sender as RuntimeEditor;
        Debug.LogError("OnEditorClosed:"+sender+"|"+editor.Id);
        IsEditorOpen=false;
        // Battlehub.RTCommon.IOC.ClearAll();
        // ToggleToolbar();
    }

    public void CloseSceneEditor(){
        IsEditorOpen=false;

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

    public AroundAlignCamera aroundAlignCamera;

    public RuntimeSceneComponent sceneCompnent;

    public void LookFreeToLookAround()
    {
        SetLookAroundEnable(true);
        SetLookFreeEnable(false);
    }

    private void SetLookAroundEnable(bool enable)
    {
        if (aroundAlignCamera == null)
        {
            aroundAlignCamera = GameObject.FindObjectOfType<AroundAlignCamera>();
        }

        if (aroundAlignCamera != null)
        {
            aroundAlignCamera.enabled = enable;
        }

        if (enable)
        {
            if(aroundAlignCamera.GetTarget().position != sceneCompnent.Pivot)
                aroundAlignCamera.GetTarget().position = sceneCompnent.Pivot;
        }
    }

    private void SetLookFreeEnable(bool enable)
    {
        if (sceneCompnent == null)
        {
            sceneCompnent = GameObject.FindObjectOfType<RuntimeSceneComponent>();
        }
        sceneCompnent.CanZoom = enable;
        sceneCompnent.CanRotate = enable;
        //sceneCompnent.CanFreeMove = false;
        //sceneCompnent.CanOrbit = false;
    }

    public void LookAroundToLookFree()
    {
        SetLookAroundEnable(false);
        SetLookFreeEnable(true);
    }
}
