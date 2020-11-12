using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlehub.RTCommon;
using Battlehub.RTEditor;
public class RTEManager : MonoBehaviour
{
    public bool IsToolBarVisible=false;

    void Start(){
        RTEditor.Created+=OnEditorCreated;
        RTEditor.Closed+=OnEditorClosed; 

        ToggleToolbar();
    }

    public bool IsEditorClosed=false;

    void Update(){
        if(IsEditorClosed){
            IsEditorClosed=false;
            ToggleToolbar();
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
            IsHandlesEnabled=false;
            ToolBar.DisableHandles();
        }
        else{
            IsHandlesEnabled=true;
            ToolBar.EnableHandles();
        }
    }

    public bool IsEditorOpen=false;
    public GameObject ToolBarPrefab;
    public RTEToolBar ToolBar;
    public CreateEditor RTEditor;

    public void OpenSceneEditor(){
        IsToolBarVisible=false;
        IsEditorOpen=true;
        // HandleRootObj.SetActive(false);
        ToolBar.Close();
        ToolBar=null;
   
        RTEditor.OnOpen();
    }

    private void OnDestroy()
    {
        if(RTEditor)
            RTEditor.Closed-=OnEditorClosed;    
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
}
