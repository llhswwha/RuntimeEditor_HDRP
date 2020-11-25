using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battlehub.RTCommon;
using Battlehub.RTEditor;
using Battlehub.RTEditor.HDRP;
using Mogoson.CameraExtension;
using Battlehub.RTHandles;
using System;
public class RTEManager : MonoBehaviour
{
    public static RTEManager Instance;

    public HDRPInit hdrpInit;

    public bool IsToolBarVisible=false;

    public bool IsLockCamera=false;

    public GameObject EditorExtensions;

    void Awake(){
        Instance=this;
    }

    [UnityEngine.ContextMenu("HideHandles")]
    public void HideHandles(){
        Debug.LogError("RTEManager.HideHandles");
        IsHandlesEnabled=true;
        ToolBar.DisableHandles();
        EditButton.SetActive(false);
    }

    [UnityEngine.ContextMenu("ShowHandles")]
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

        //ToggleToolbar();
        hdrpInit.EnableOutline();

        // var volumns=GameObject.FindObjectsOfType<UnityEngine.Rendering.Volumn>();
        // Debug.LogError("RTEManager.Start volumns:"+volumns.Length);

        if(IsLockCamera){
            SetLookFreeEnable(false);
        }
    }

    public bool IsEditorClosed=false;

    void Update(){
        if(IsEditorClosed){
            IsEditorClosed=false;
            ToggleToolbar();
            hdrpInit.EnableOutline();

            LoadPivot();

            Y_UIFramework.UIManager.GetInstance().ShowUIPanel("ModelSystemTreePanel");
            Y_UIFramework.MessageCenter.SendMsg(MsgType.ModelSystemTreePanelMsg.TypeName, MsgType.ModelSystemTreePanelMsg.ShowModelTree, null);
            Y_UIFramework.MessageCenter.SendMsg(MsgType.ModuleToolbarMsg.TypeName, MsgType.ModuleToolbarMsg.ShowWindow, null);
            Y_UIFramework.MessageCenter.SendMsg(MsgType.RTEditorMsg.TypeName, MsgType.RTEditorMsg.OnEditorClosed, null);
            
        }
    }

    private void InstantiateToolBar(){
        if(ToolBar==null){
            Debug.Log("Instantiate ToolBar");
            // Battlehub.RTCommon.IOC.ClearAll();
            // Battlehub.RTCommon.RTEBase.Init();
            GameObject instance=GameObject.Instantiate(ToolBarPrefab);
            instance.SetActive(true);
            ToolBar=instance.GetComponent<RTEToolBar>();
            IsToolBarVisible=false;
        }
    }

    public void HideToolbar(){
        Debug.Log("HideToolbar");

        InstantiateToolBar();

        IsToolBarVisible=false;
        ToolBar.HideToolbar();
        ToolBar.gameObject.SetActive(false);
        EditorExtensions.SetActive(false);
        LookFreeToLookAround();

    }

    public void ShowToolbar(){
        Debug.Log("ShowToolbar");

        InstantiateToolBar();

        IsToolBarVisible=true;
        ToolBar.gameObject.SetActive(true);
        ToolBar.ShowToolbar();
        EditorExtensions.SetActive(true);
        LookAroundToLookFree();
    }

    public void ToggleToolbar()
    {
        Debug.Log("ToggleToolbar");
        Debug.Log("ToolBar isnull:"+(ToolBar==null));
        if(IsToolBarVisible){
            HideToolbar();
        }
        else{
            ShowToolbar();
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
    
    public Vector3 PivotPos;

    public Vector3 CameraPos;

    public void OpenSceneEditor(){
        // IsToolBarVisible=false;
        // IsEditorOpen=true;
        // // HandleRootObj.SetActive(false);
        // ToolBar.Close();
        // ToolBar=null;

        // SavePivot();
   
        RTEditor.OnOpen();
    }

    [UnityEngine.ContextMenu("SavePivot")]
    public void SavePivot(){
        Debug.Log("SavePivot");
        if(sceneCompnent==null){
            Debug.LogError("SavePivot sceneCompnent==null");
        }
        else{
            PivotPos=sceneCompnent.Pivot;
            CameraPos=sceneCompnent.CameraPosition;
        }
    }

    [UnityEngine.ContextMenu("LoadPivot")]
    public void LoadPivot(){
        Debug.Log("LoadPivot");
        if(sceneCompnent==null){
            Debug.LogError("LoadPivot sceneCompnent==null");
        }
        else{
            sceneCompnent.Pivot=PivotPos;
            sceneCompnent.CameraPosition=CameraPos;
        }
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

        Y_UIFramework.MessageCenter.SendMsg(MsgType.ModelSystemTreePanelMsg.TypeName, MsgType.ModelSystemTreePanelMsg.CloseWindow, null);
        Y_UIFramework.MessageCenter.SendMsg(MsgType.ModuleToolbarMsg.TypeName, MsgType.ModuleToolbarMsg.CloseWindow, null);

    }

    private void OnEditorBeforeOpen(object sender)
    {
        SavePivot();

        IsToolBarVisible=false;
        IsEditorOpen=true;
        // HandleRootObj.SetActive(false);
        if (ToolBar != null)
        {
            ToolBar.Close();
            ToolBar = null;
        }
        
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

    public MouseTranslatePro mouseTranslate;
    public RuntimeSceneComponent sceneCompnent;

    [UnityEngine.ContextMenu("LookFreeToLookAround")]
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

        if(mouseTranslate==null){
            mouseTranslate = GameObject.FindObjectOfType<MouseTranslatePro>();
        }

        if (aroundAlignCamera != null)
        {
            aroundAlignCamera.enabled = enable;
        }

        if (enable)
        {
            SetRotateCenter();
        }
    }

    private void SetRotateCenter(){
        if(aroundAlignCamera!=null && sceneCompnent!=null)
        {
            //aroundAlignCamera.GetTarget().position = sceneCompnent.Pivot;
            aroundAlignCamera.SetTargetEx(sceneCompnent.Pivot
            ,sceneCompnent.CameraTransform.rotation.eulerAngles,
            sceneCompnent.OrbitDistance);

            mouseTranslate.SetTranslatePosition(sceneCompnent.Pivot,true);
        }
    }

    private void OnSelectionChanged(object sender, EventArgs e)
    {
        Debug.Log("RTEManager.OnSelectionChanged :"+sceneCompnent.Selection.activeObject+"|"+e+"|"+sender);
        Y_UIFramework.MessageCenter.SendMsg(MsgType.RTEditorMsg.TypeName, MsgType.RTEditorMsg.OnSelectionChanged, sceneCompnent.Selection.activeObject);
    }

    private void SetLookFreeEnable(bool enable)
    {
        if (sceneCompnent == null)
        {
            sceneCompnent = GameObject.FindObjectOfType<RuntimeSceneComponent>();
            sceneCompnent.SelectionChanged += OnSelectionChanged;
        }
        if(sceneCompnent!=null)
        {
            sceneCompnent.CanZoom = enable;
            sceneCompnent.CanRotate = enable;
            sceneCompnent.CanFreeMove = enable;
            sceneCompnent.CanOrbit = enable;
        }

        if (enable)
        {
            SetPivot();
        }
    }

    [UnityEngine.ContextMenu("SetPivot")]
    public void SetPivot(){
        if (aroundAlignCamera != null && sceneCompnent != null)
        {
            Vector3 pos0=sceneCompnent.Pivot;
            Vector3 pos= aroundAlignCamera.GetTargetPosition();
            Debug.LogError("SetPivot:" + pos0+"->"+pos);
            sceneCompnent.SetPivotEx(pos);
        } 
    }

    [UnityEngine.ContextMenu("SetCamPos")]
    public void SetCamPos(){
        if (aroundAlignCamera != null && sceneCompnent != null)
        {
            Debug.Log("camTransform.rotation.eulerAngles 1:"+sceneCompnent.CameraTransform.rotation.eulerAngles);
            sceneCompnent.CameraPosition=sceneCompnent.CameraPosition;
            Debug.Log("camTransform.rotation.eulerAngles 2:"+sceneCompnent.CameraTransform.rotation.eulerAngles);
        } 
    }

    [UnityEngine.ContextMenu("LookAroundToLookFree")]
    public void LookAroundToLookFree()
    {
        SetLookAroundEnable(false);
        SetLookFreeEnable(true);
    }

    public GameObject TestFocusObj;

    [UnityEngine.ContextMenu("TestFocus")]
    public void TestFocus(){
        this.FocusGO(TestFocusObj);
    }

    public void FocusGO(GameObject go){
        if(sceneCompnent!=null){
            if(sceneCompnent.Selection.activeObject==go){
                Debug.LogWarning("sceneCompnent.Selection.activeObject==go");
                return;
            }
            sceneCompnent.FocusGO(go);
            Y_UIFramework.MessageCenter.SendMsg(MsgType.ModelScaneMsg.TypeName, MsgType.ModelScaneMsg.StartSubScanners, go);
        }
        else{
            Debug.LogError("FocusGO sceneCompnent == null !!");
        }
        
    }
}
