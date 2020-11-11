using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTEManager : MonoBehaviour
{
    public bool IsToolBarVisible=false;

    public void ToggleToolbar()
    {
        Debug.Log("ToggleToolbar");

        if(ToolBar==null){
            GameObject instance=GameObject.Instantiate(ToolBarPrefab);
            ToolBar=instance.GetComponent<RTEToolBar>();
            IsToolBarVisible=false;
        }

        if(IsToolBarVisible){
            IsToolBarVisible=false;
            ToolBar.HideToolbar();
        }
        else{
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
    public Battlehub.RTCommon.CreateEditor RTEditor;

    public void OpenSceneEditor(){
        IsEditorOpen=true;
        // HandleRootObj.SetActive(false);
        ToolBar.Close();
        ToolBar=null;

        RTEditor.OnOpen();
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
