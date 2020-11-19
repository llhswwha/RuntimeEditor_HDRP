using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MsgType 
{
    /// <summary>
    /// 人员定位底部工具栏的子工具栏管理
    /// </summary>
    public class PersonSubsystemManageMsg
    {
        public const string TypeName = "PersonSubsystemManageMsg";
        public const string ChangeSearchToggleState = "ChangeSearchToggleState";
    }
    #region 人员管理界面
    /// <summary>
    /// 人员管理信息
    /// </summary>
    public class PersonnelManagePanelMsg
    {
        public const string TypeName = "PersonnelManagePanelMsg";
        public const string PersonnelsMsg = "PersonnelsMsg";//人员列表信息
        public const string ShowpersonnelSearchWindow = "ShowpersonnelSearchWindow";
        public const string AddPersonnelInfo = "AddPersonnelInfo";
        public const string DeletePersonnelInfo = "DeletePersonnelInfo";
        public const string ShowPersonnelInfo = "ShowPersonnelInfo";
        public const string SetIsGetPersonData = "SetIsGetPersonData";
        public const string StartPerSearchUI = "StartPerSearchUI";
        
    }

    /// <summary>
    /// 人员管理-添加人员
    /// </summary>
    public class AddPersonnelPanelMsg
    {
        public const string TypeName = "AddPersonnelPanelMsg";
        public const string SetTagName = "SetTagName";
        public const string ShowAddPerWindowParam = "ShowAddPerWindowParam";
        public const string ShowAddPerWindow = "ShowAddPerWindow";
        public const string GetJobsManagementData = "GetJobsManagementData";
        public const string GetAddJobsData = "GetAddJobsData";
        public const string SetDepartText = "SetDepartText";
        public const string SetCurrentId = "SetCurrentId";
        public const string CloseAddPerWindow = "CloseAddPerWindow";
        public const string SetAddPerWindow = "SetAddPerWindow";
        public const string AddDepartmentData = "AddDepartmentData";
        public const string SetChooseTag = "SetChooseTag";
        
    }

    public class EditPersonnelPanelMsg
    {
        public const string TypeName = "EditPersonnelPanelMsg";
        public const string SetTagName = "SetTagName";
        public const string ShowJobInfo = "ShowJobInfo";
        public const string ShowEditPersonnelWindow = "ShowEditPersonnelWindow";
        public const string ShowDepartmentInfo = "ShowDepartmentInfo";
        public const string ShowAndCloseEditPersonnelInfo = "ShowAndCloseEditPersonnelInfo";
        public const string RefreshEditJobInfo = "RefreshEditJobInfo";
        public const string GetPersonnelInformation = "GetPersonnelInformation";
        public const string GetJobInfo = "GetJobInfo";
        public const string SetDepartmentText = "SetDepartmentText";
        public const string SetCurrentId = "SetCurrentId";
        public const string CloseEditPersonnelWindow = "CloseEditPersonnelWindow";
        public const string SetTagCode = "SetTagCode";
        public const string SetLocationCardData = "SetLocationCardData";
        public const string SetPerCode = "SetPerCode";
        public const string SetPeraonnelData = "SetPeraonnelData";
    }

    public class PersonnelSelectTagPanelMsg
    {
        public const string TypeName = "PersonnelSelectTagPanelMsg";
        public const string SetTogGroupAllowSwitchOff = "SetTogGroupAllowSwitchOff";
        public const string AddTagCardInfo = "AddTagCardInfo";
        public const string SetChooseTag = "SetChooseTag";
        public const string GetLocationCardRoleType = "GetLocationCardRoleType";
    }
    public class PersonDepartSelectPanelMsg
    {
        public const string TypeName = "PersonDepartSelectPanelMsg";
        public const string GetDepartmentListData = "GetDepartmentListData";
        public const string ShowDepartmentListUI = "ShowDepartmentListUI";
        public const string ShowAndCloseAddDepartmentListUI = "ShowAndCloseAddDepartmentListUI";
        public const string ShowAddDepartmentInfo = "ShowAddDepartmentInfo";
        public const string DepartListItemRemove = "DepartListItemRemove";
        public const string ScreenListItemRemove = "ScreenListItemRemove";
        public const string DepartListInsert = "DepartListInsert";
        public const string ScreenListInsert = "ScreenListInsert";
        public const string SetPegeNumText = "SetPegeNumText";
        public const string SetInputDepartmentPage = "SetInputDepartmentPage";
        public const string SetDepSelectedText = "SetDepSelectedText";
    }

    public class PersonDepAddPanelMsg
    {
        public const string TypeName = "PersonDepAddPanelMsg";
        public const string ShowData = "ShowData";
        public const string SetIsAdd = "SetIsAdd";
        public const string GetDepartmentList = "GetDepartmentList";
        
    }

    public class PersonJobSelectPanelMsg
    {
        public const string TypeName = "PersonJobSelectPanelMsg";
        public const string ShowJobListWindow = "ShowJobListWindow";
        public const string ShowAndClosePostInfo = "ShowAndClosePostInfo";
        public const string ShowAddPostInfo = "ShowAddPostInfo";
        public const string RemovePostItem = "RemovePostItem";
        public const string InsertPostItemInFirst = "InsertPostItemInFirst";
        public const string SetPageNumText = "SetPageNumText";
        public const string SetJobSelectedText = "SetJobSelectedText";
        public const string InputJobPage = "InputJobPage";
        public const string GetJobListData = "GetJobListData";
    }

    public class PersonJobAddPanelMsg
    {
        public const string TypeName = "PersonJobAddPanelMsg";
        public const string SetIsAdd = "SetIsAdd";
        public const string GetPostList = "GetPostList";
    }

    public class PersonnelTagEditPanelMsg
    {
        public const string TypeName = "PersonnelTagEditPanelMsg";
        public const string SetTogGroupAllowSwitchOff = "SetTogGroupAllowSwitchOff";
        public const string GetCardRoleData = "GetCardRoleData";
        public const string SetChooseTag = "SetChooseTag";
        public const string SetTagCode = "SetTagCode";
        public const string GetLocationCardRoleType = "GetLocationCardRoleType";
    }

    public class PersonDepartEidtSelectPanelMsg
    {
        public const string TypeName = "PersonDepartEidtSelectPanelMsg";
        public const string ShowEditDepartmentInfo = "ShowEditDepartmentInfo";
        public const string GetDepartmentListData = "GetDepartmentListData";
        public const string ShowDepartmentListUI = "ShowDepartmentListUI";
        public const string RemoveDepartmentItem = "RemoveDepartmentItem";
        public const string InsertDepartmentItemInFirst = "InsertDepartmentItemInFirst";
        public const string SetPageNumText = "SetPageNumText";
        public const string InputDepartmentPage = "InputDepartmentPage";
        public const string SetDepSelectedText = "SetDepSelectedText";
    }

    public class PersonJobEditSelectPanelMsg
    {
        public const string TypeName = "PersonJobEditSelectPanelMsg";
        public const string ShowPostInfo = "ShowPostInfo";
        public const string ShowJobListWindow = "ShowJobListWindow";
        public const string RemoveJobItem = "RemoveJobItem";
        public const string InsertJobItemInFirst = "InsertJobItemInFirst";
        public const string SetPageNumText = "SetPageNumText";
        public const string SetJobSelectedText = "SetJobSelectedText";
        public const string InputJobPage = "InputJobPage";
        public const string GetJobListData = "GetJobListData";
        

    }
    #endregion

    #region 人员告警界面

    public class PersonnelAlarmsPanelMsg
    {
        public const string TypeName = "PersonnelAlarmsPanelMsg";
        public const string PerAlarmSearchBut_Click = "PerAlarmSearchBut_Click";
        public const string ClosePersonAlarmUI = "ClosePersonAlarmUI"; 
        public const string ShowPersonAlarmUI = "ShowPersonAlarmUI";
    }
    #endregion

    #region 定位历史界面

    public class MultHistoryPanelMsg
    {
        public const string TypeName = "MultHistoryPanelMsg";
        public const string SetLight = "SetLight";
    }

    public class HistoryPersonsSearchPanelMsg
    {
        public const string TypeName = "HistoryPersonsSearchPanelMsg";
        public const string ShowEx = "ShowEx";
        public const string SetSelectPersonnelItemToggle = "SetSelectPersonnelItemToggle";
        public const string RemoveSelectPersonnelItem = "RemoveSelectPersonnelItem";
        public const string AddSelectPersonnelItem = "AddSelectPersonnelItem";
        public const string CloseWindow = "CloseWindow";
    }

    public class HistoricalPathStatisticsPanelMsg
    {
        public const string TypeName = "HistoricalPathStatisticsPanelMsg";
        public const string CloseWindow = "CloseWindow";
        public const string OpenHistoricalPathStatisticsInfo = "OpenHistoricalPathStatisticsInfo";
    }

    public class HistoryAlarmPushPanelMsg
    {
        public const string TypeName = "HistoryAlarmPushPanelMsg";
        public const string ShowAlarmPushWindow = "ShowAlarmPushWindow";
        public const string HideAndClearInfo = "HideAndClearInfo";
        public const string OnDeviceAlarmRecieved = "OnDeviceAlarmRecieved";
        public const string OnLocationAlarmRecieved = "OnLocationAlarmRecieved";
        public const string OnCameraAlarmsRecieved = "OnCameraAlarmsRecieved";
        public const string TryGetAllAlarm = "TryGetAllAlarm";
    }
    public class HistoryAlarmDetailPanelMsg
    {
        public const string TypeName = "HistoryAlarmDetailPanelMsg";
        public const string ShowInfo = "ShowInfo";
    }
    #endregion

    #region 区域范围编辑

    public class RangeEditPanelMsg
    {
        public const string TypeName = "RangeEditPanelMsg";
        public const string ShowEx = "ShowEx";
    }

    #endregion

    #region 定位卡管理
    public class LocationCardManagePanelMsg
    {
        public const string TypeName = "LocationCardManagePanelMsg";
        public const string GetLocationCardManagementData = "GetLocationCardManagementData";
        public const string CloseLocationCardWindow = "CloseLocationCardWindow";
        public const string CloseAllCardRoleWindow = "CloseAllCardRoleWindow";
        public const string InsertTagFirst = "InsertTagFirst";
        public const string SetLocationRoleText = "SetLocationRoleText";
        public const string ShowCardRoleInfo = "ShowCardRoleInfo";
        public const string GetLocationCardRoleType = "GetLocationCardRoleType";
        public const string RemoveTagItem = "RemoveTagItem";
        public const string SetPageNumText = "SetPageNumText";
        public const string ShowLocationCardManagementWindow = "ShowLocationCardManagementWindow";
        public const string SetCardRoleDropdown = "SetCardRoleDropdown";
    }

    public class LocationCardAddPanelMsg
    {
        public const string TypeName = "LocationCardAddPanelMsg";
        public const string RolePowerInstruction = "RolePowerInstruction";
        public const string ShowAddPosCardWindow = "ShowAddPosCardWindow";
    }

    public class CardAddPermissionsEditPanelMsg
    {
        public const string TypeName = "CardAddPermissionsEditPanelMsg";
        public const string ShowRoleWindow = "ShowRoleWindow";
        public const string GetCardRoleDataInfo = "GetCardRoleDataInfo";
        public const string GetCardRoleData = "GetCardRoleData";
        //public const string SetPreviousID = "SetPreviousID";
        public const string CloseRoleWindow = "CloseRoleWindow";
        public const string SetCurrentId = "SetCurrentId";
        public const string SaveAreaPermissionCurrentData = "SaveAreaPermissionCurrentData";    
    }
    public class CardAddRoleEditPanelMsg
    {
        public const string TypeName = "CardAddRoleEditPanelMsg";
    }

    public class CardAddLocationPerEditPanelMsg
    {
        public const string TypeName = "CardAddLocationPerEditPanelMsg";
        public const string GetAddLocationPerInfo = "GetAddLocationPerInfo";      
    }

    public class LocationCardEditPanelMsg
    {
        public const string TypeName = "LocationCardEditPanelMsg";
        public const string SetCurrentObjInt = "SetCurrentObjInt";
        public const string OpenEditCardAndDataNull = "OpenEditCardAndDataNull";
        public const string RolePowerInstruction = "RolePowerInstruction";
        public const string ShowEditCardInfo = "ShowEditCardInfo";
        
    }

    public class CardRolePermissionsEditPanelMsg
    {
        public const string TypeName = "CardRolePermissionsEditPanelMsg";
        public const string ClearGrid = "ClearGrid";
        public const string GetCardRoleData = "GetCardRoleData";
        public const string GetCardRoleDatas = "GetCardRoleDatas";
        public const string ShowEditCardInfo = "ShowEditCardInfo";
        public const string CloseRoleWindow = "CloseRoleWindow";
        public const string SetCurrentId = "SetCurrentId";
        public const string SaveAreaPermissionData = "SaveAreaPermissionData";
        public const string SetWindowState = "SetWindowState";
    }

    public class CardEidtLocationPerEditPanelMsg
    {
        public const string TypeName = "CardEidtLocationPerEditPanelMsg";
        public const string GetEditPersonnelRoleInfo = "GetEditPersonnelRoleInfo";
        
    }

    public class CardEditRoleEditPanelMsg
    {
        public const string TypeName = "CardEditRoleEditPanelMsg";
    }

    public class AuthorityTimeMangePanelMsg
    {
        public const string TypeName = "AuthorityTimeMangePanel";
        public const string ShowInfo = "ShowInfo";
    }

    #endregion

    #region 设备管理部分


    /// <summary>
    /// 区域设备拓扑树
    /// </summary>
    public class AreaDevTreePanelMsg
    {
        public const string TypeName = "AreaDevTreePanelMsg";
        public const string ResizeTree = "ResizeTree";
        public const string TryGetAreaNode = "TryGetAreaNode";
        public const string ScaleWindow = "ScaleWindow";
        public const string SetSelectNode = "SetSelectNode";
        public const string TweenBack = "TweenBack";
        
    }

    /// <summary>
    /// 设备搜索界面
    /// </summary>
    public class DeviceSearchPanelMsg
    {
        public const string TypeName = "DeviceSearchPanel";
        public const string InitSearchInfo = "InitSearchInfo";//初始化搜索信息
        public const string SearchDevByInputText = "SearchDevByInputText";//通过输入框中的文本，进行搜索
        public const string CloseDevSearchWindow = "CloseDevSearchWindow";//关闭设备搜索界面
        public const string SaveSelection = "SaveSelection";//保留选中项 cll
        public const string ShowTotalPage = "ShowTotalPage";//没有筛选时的切页列表 cll
        public const string SetInstantiateLine = "SetInstantiateLine";
    }
    /// <summary>
    /// 设备告警界面
    /// </summary>
    public class DevAlarmListPanelMsg
    {
        public const string TypeName = "DevAlarmListPanel";
        public const string GetScreenDevAlarmLevel = "InitSearchInfo";//告警等级筛选
        public const string GetScreenAlarmType = "GetScreenAlarmType";//告警类型筛选
        public const string ShowDevAlarmWindow = "ShowDevAlarmWindow";//显示设备告警界面
        public const string CloseDevAlarmWindow = "CloseDevAlarmWindow";//关闭设备告警界面
    }
    /// <summary>
    /// 设备编辑（拖拽创建）
    /// </summary>
    public class ObjectAddListPanelMsg
    {
        public const string TypeName = "ObjectAddListPanel";
        public const string InitInfo = "InitInfo";//初始化信息
        public const string HidePanel = "HidePanel";//关闭窗口，清除信息
        public const string CloseWindow = "CloseWindow";//仅关闭窗口，不清除信息
        public const string GetObjectAddListTypeInfoByName = "GetObjectAddListTypeInfoByName";//根据大类名称获取大类相关信息
        public const string ShowBackButton = "ShowBackButton";//显示返回按钮
        public const string ShowUpperStoryButton = "ShowUpperStoryButton";//显示返回上一层按钮
    }
    /// <summary>
    /// 设备编辑（名称修改）
    /// </summary>
    public class DeviceEditUIPanelMsg
    {
        public const string TypeName = "DeviceEditUIPanel";
        public const string ShowSingleDev = "ShowSingleDev";//显示单个设备
        public const string ShowMultiDev = "ShowMultiDev";//显示多个设备
        public const string SetEmptyValue = "SetEmptyValue";//设置默认值
        public const string ChangePos = "ChangePos";//修改坐标信息
        public const string DeleteDev = "DeleteDev";//删除设备
        public const string RemoveFollowUI = "RemoveFollowUI";//移除漂浮UI
        public const string Close = "Close";
        public const string HideMultiDev = "HideMultiDev";//清除多个设备的显示界面
        public const string RefleshGizmoPosition = "RefleshGizmoPosition";//刷新设备位置
        public const string ChangeDevFollowInfo = "ChangeDevFollowInfo";//改变设备漂浮信息
    }

    public class RoamManagePanelMsg
    {
        public const string TypeName = "RoamManagePanel";
        public const string ChangeFPSGravity = "ChangeFPSGravity";//更改人物重力
        public const string EntranceRoam = "EntranceRoam";//进入漫游
        public const string ShowRoamWindow = "ShowRoamWindow";//显示漫游窗口
        public const string EntranceIndoor = "EntranceIndoor";//进入室内
        public const string SetLight = "SetLight";//设置灯光
        public const string EntranceRoamShowBox = "EntranceRoamShowBox";
        public const string ShowEntracneWindow = "ShowEntracneWindow";
        public const string ExitRoam = "ExitRoam";
    }
    /// <summary>
    /// 漫游生产设备信息
    /// </summary>
    public class RoamDevInfoPanelMsg
    {
        public const string TypeName = "RoamDevInfoPanel";
        public const string ClosePanel = "ClosePanel";
        public const string ShowDevInfo = "ShowDevInfo";
        public const string SetDevInfoCheckState = "SetDevInfoCheckState";
    }
    /// <summary>
    /// 视频监控界面
    /// </summary>
    public class CameraVideoRtspPanelMsg
    {
        public const string TypeName = "CameraVideoRtspPanelMsg";
        public const string RecoverParent = "RecoverParent";
        public const string Close = "Close";
        public const string SetNewParent = "SetNewParent";
        public const string ShowEx = "ShowEx";
        
    }

    #endregion

    #region 移动巡检
    /// <summary>
    /// 漂浮UI管理窗口
    /// </summary>
    public class MobileInspectionFollowUIPanelMsg
    {
        public const string TypeName = "MobileInspectionFollowUIPanel";
        public const string DateUpdate = "DateUpdate";
        public const string Hide = "Hide";
    }
    /// <summary>
    /// 移动巡检列表
    /// </summary>
    public class MobileInspectionUIPanelMsg
    {
        public const string TypeName = "MobileInspectionUIPanel";
        //public const string ShowEx = "ShowEx";
        public const string Hide = "Hide";
        public const string AddToToggleGroup = "AddToToggleGroup";
        public const string GetMobileNum = "GetMobileNum";
    }
    /// <summary>
    /// 移动巡检路径
    /// </summary>
    public class MobileInspectionDetailsPanelMsg
    {
        public const string TypeName = "MobileInspectionDetailsPanel";
        public const string Show = "Show";
        public const string SetWindowActive = "SetWindowActive";
        public const string GetTitleText = "GetTitleText";
    }

    public class MobileInspectionDetailsInfoPanelMsg
    {
        public const string TypeName = "MobileInspectionDetailsInfoPanel";
        public const string GetInspectionPointList = "GetInspectionPointList";
    }

    public class MobileInspectionRouteDetailsPanelMsg
    {
        public const string TypeName = "MobileInspectionRouteDetailsPanel";
        public const string Show = "Show";
    }

    public class MobileInspectionHistoryPanelMsg
    {
        public const string TypeName = "MobileInspectionHistoryPanel";
        public const string GetSearchInput = "GetSearchInput";
        public const string ShowData = "ShowData";
    }
    public class MobileInspectionHistoryDetailPanelMsg
    {
        public const string TypeName = "MobileInspectionHistoryDetailPanel";
        public const string DataUpdate = "DataUpdate";
    }
    public class MobileInspectionHistoryRoutePanelMsg
    {
        public const string TypeName = "MobileInspectionHistoryRoutePanel";
        public const string ShowData = "ShowData";
    }
    #endregion

    #region 两票系统
    /// <summary>
    /// 两票系统界面
    /// </summary>
    public class TwoTicketSystemPanelMsg
    {
        public const string TypeName = "TwoTicketSystemPanel";
        //public const string ShowWindow = "ShowWindow";
        public const string HideWindow = "HideWindow";
        public const string ToggleGroupAdd = "ToggleGroupAdd";
    }
    /// <summary>
    /// 工作票窗口
    /// </summary>
    public class WorkTicketDetailsPanelMsg
    {
        public const string TypeName = "WorkTicketDetailsPanel";
        public const string ShowData = "ShowData";
        public const string SetWindowActive = "SetWindowActive";        
    }
    /// <summary>
    /// 操作票窗口
    /// </summary>
    public class OperationTicketDetailsPanelMsg
    {
        public const string TypeName = "OperationTicketDetailsPanel";
        public const string ShowData = "ShowData";
        public const string SetWindowActive = "SetWindowActive";
    }
    /// <summary>
    /// 两票系统历史窗口
    /// </summary>
    public class TwoTicketHistoryPanelMsg
    {
        public const string TypeName = "TwoTicketHistoryPanel";
        public const string StartShow = "StartShow";
        public const string CloseBtn_OnClick = "CloseBtn_OnClick";
    }
    /// <summary>
    /// 工作票历史详情窗口
    /// </summary>
    public class WorkTicketHistoryDetailsPanelMsg
    {
        public const string TypeName = "WorkTicketHistoryDetailsPanel";
        public const string CloseBtn_OnClick = "CloseBtn_OnClick";
        public const string ShowData = "ShowData";
    }
    /// <summary>
    /// 操作票历史详情窗口
    /// </summary>
    public class OperationTicketHistoryDetailsPanelMsg
    {
        public const string TypeName = "OperationTicketHistoryDetailsPanel";
        public const string CloseBtn_OnClick = "CloseBtn_OnClick";
        public const string ShowData = "ShowData";
    }
    
    public class TwoTicketSystemManagePanelMsg
    {
        public const string TypeName = "TwoTicketSystemManagePanel";
        public const string HideTwoTicketHistoryPaths = "HideTwoTicketHistoryPaths";
        public const string ShowWorkTicketPath = "ShowWorkTicketPath"; 
        public const string ShowOperationTicketPath = "ShowOperationTicketPath";
    }
    #endregion
    #region 工具栏拓扑树

    public class ModuleToolbarMsg
    {
        public const string TypeName = "ModuleToolbarMsg";
        public const string ChangeImage = "ChangeImage";
        public const string SetViewMode = "SetViewMode";

        public const string CloseWindow = "CloseWindow";

        public const string ShowWindow = "ShowWindow";
    }

    public class FunctionSwitchBarMsg
    {
        public const string TypeName = "FunctionSwitchBarMsg";
        public const string SetTransparentToggle = "SetTransparentToggle";
        public const string SetlightToggle = "SetlightToggle";
        public const string ExitSetTheBarSystem = "ExitSetTheBarSystem";
        public const string GetTransparentToggleIsOn = "GetTransparentToggleIsOn";
        public const string GetLightToggleIsOn = "GetLightToggleIsOn";
        public const string GetBuildingToggleIsOn = "GetBuildingToggleIsOn";
        public const string SetOtherUIStateInHistoricalMode = "SetOtherUIStateInHistoricalMode";
        public const string GetCameraToggleIsOn = "GetCameraToggleIsOn";
        public const string GetEnteranceGuardToggleIsOn = "GetEnteranceGuardToggleIsOn";
        public const string GetDevInfoToggleIsOn = "GetDevInfoToggleIsOn";
        public const string GetArchorInfoToggleIsOn = "GetArchorInfoToggleIsOn";
        public const string SetCameraToggle = "SetCameraToggle";
        public const string TweenBack = "TweenBack";
        public const string SetEntraceGuardToggle = "SetEntraceGuardToggle";
        public const string SetArchorInfoToggle = "SetArchorInfoToggle";
    }

    public class StartOutBarMsg
    {
        public const string TypeName = "StartOutBarMsg";
        public const string ShowDevEditButton = "ShowDevEditButton";
        public const string SetMainPageAndBackState = "SetMainPageAndBackState";
        public const string HideBackButton = "HideBackButton";
        public const string SetUpperStoryButtonActive = "SetUpperStoryButtonActive";
        public const string ShowBackButton = "ShowBackButton";
        public const string TweenBack = "TweenBack";
        
    }

    public class PersonnelTreePanelMsg
    {
        public const string TypeName = "PersonnelTreePanelMsg";
        public const string SelectPerson = "SelectPerson";
        public const string AreaDivideTreeAddChild = "AreaDivideTreeAddChild";
        public const string RefreshAddDepartTree = "RefreshAddDepartTree";
        public const string AreaSelectNodeById = "AreaSelectNodeById";
        public const string ClosePersonnelWindow = "ClosePersonnelWindow";
        public const string RemoveAreaNode = "RemoveAreaNode";
        public const string GetPerson = "GetPerson";
        public const string SetDepartAndJob_Bg = "SetDepartAndJob_Bg";
        public const string FindAreaNode = "FindAreaNode";
        public const string GetAllPersonnels = "GetAllPersonnels";
        public const string ReshRemoveDepartTree = "ReshRemoveDepartTree";
        public const string GetTopoTree = "GetTopoTree";
        public const string ShowAreaDivideTree = "ShowAreaDivideTree";
        public const string ScaleWindow = "ScaleWindow";
        public const string AreaDivideTreeRefreshPersonnel = "AreaDivideTreeRefreshPersonnel";
        public const string DepartmentTreeRefreshPerson = "DepartmentTreeRefreshPerson";
        public const string GetPersonnelByTagId = "GetPersonnelByTagId";
        public const string DeselectPerson = "DeselectPerson";
        public const string HideOffLinePerson = "HideOffLinePerson";
        public const string ShowAllPerson = "ShowAllPerson";
        public const string ResizeTree = "ResizeTree";
        public const string TweenBack = "TweenBack";
        public const string RefrshMonitorName = "RefrshMonitorName";
    }

    public class SmallMapPanelMsg
    {
        public const string TypeName = "SmallMapPanelMsg";
        public const string ShowMapByDepNode = "ShowMapByDepNode";
        public const string ResetPos = "ResetPos";
        public const string TweenBack = "TweenBack";
        
    }

    public class ParkInfoPanelMsg
    {
        public const string TypeName = "ParkInfoPanelMsg";
        public const string SetDevToggle = "SetDevToggle";
        public const string SetTitleText = "SetTitleText";
        public const string GetArrowTogison = "GetArrowTogison";
        public const string RefreshParkInfo = "RefreshParkInfo";
        public const string SetPersonToggle = "SetPersonToggle";
        public const string StartRefreshData = "StartRefreshData";
        public const string SetIsGetPerData = "SetIsGetPerData";
        public const string SetFactoryLocatedPerson = "SetFactoryLocatedPerson"; 
        public const string LoadData = "LoadData"; 
        public const string DevLoadData = "DevLoadData";
        public const string TweenBack = "TweenBack";
        
    }

    public class AlarmPushSimplifyPanelMsg
    {
        public const string TypeName = "AlarmPushSimplifyPanelMsg";
        public const string TryGetAllCameraAlarm = "TryGetAllCameraAlarmc";
        public const string DeletePushAlarms = "DeletePushAlarms";
        public const string TryGetAllAlarm = "TryGetAllAlarm";
        public const string TweenBack = "TweenBack";
        
    }

    #endregion

    #region 告警推送管理界面

    public class AlarmPushDeleteManagePanelMsg
    {
        public const string TypeName = "AlarmPushDeleteManagePanel";
        public const string ShowWindow = "ShowWindow";
        public const string ToggleItem = "ToggleItem";
        public const string Close = "Close";
    }
    #endregion

    #region 其他

    /// <summary>
    /// 经过门禁（24小时内）界面
    /// </summary>
    public class AfterEntranceGuardPanelMsg
    {
        public const string TypeName = "AfterEntranceGuardPanelMsg";
        public const string GetEntranceGuardData = "GetEntranceGuardData";

    }

    /// <summary>
    /// 人员视频监控界面
    /// </summary>
    public class NearPersonnelCameraPanelMsg
    {
        public const string TypeName = "NearPersonnelCameraPanelMsg";
        public const string SetScrollBar = "SetScrollBar";
        public const string GetSelectNameAction = "GetSelectNameAction";
        public const string GetNearPerCamData = "GetNearPerCamData";
        public const string SetIsRefresh = "SetIsRefresh";
        
    }

    /// <summary>
    /// 锅炉监控信息界面
    /// </summary>
    public class FacilityDevPanelMsg
    {
        public const string TypeName = "FacilityDevPanelMsg";
        public const string RecoverParent = "RecoverParent";
        public const string SetNewParent = "SetNewParent";
        public const string ShowInfo = "ShowInfo";
        public const string InitSubSytemTree = "InitSubSytemTree";

    }

    public class BaoXinDeviceAlarmPanelMsg
    {
        public const string TypeName = "BaoXinDeviceAlarmPanelMsg";
        public const string ShowDevAlarm = "ShowDevAlarm";
        public const string GetDevAlarmList = "GetDevAlarmList";
        public const string GetScreenAlarmType = "GetScreenAlarmType";

    }


    #endregion

    #region 首页大屏
    /// <summary>
    /// 大屏管理
    /// </summary>
    public class BigScreenPanel
    {
        public const string TypeName = "BigScreenPanel";
        public const string ShowWindow = "ShowWindow";
        public const string HideWindow = "HideWindow";
        public const string ChangePart = "ChangePart";
    }


    #endregion

    #region 培训演练界面
    /// <summary>
    /// 培训演练
    /// </summary>
    public class FireAreaPanelMsg
    {
        public const string TypeName = "FireAreaPanel";
        public const string ShowFirePanel = "ShowFirePanel";
    }
    #endregion

    #region 区域路径界面
    /// <summary>
    /// 培训演练
    /// </summary>
    public class AreaPathPanelMsg
    {
        public const string TypeName = "AreaPathPanelMsg";
        public const string ShowPathPanel = "ShowPathPanel";
    }
    #endregion

    #region 路径编辑界面
    public class EditPathPanelMsg
    {
        public const string TypeName = "EditPathPanelMsg";
        public const string ShowFirePanel = "ShowFirePanel";
    }
    #endregion

    #region 删除告警
    public class DeleteAlarmPanelMsg
    {
        public const string TypeName = "DeleteAlarmPanelMsg";
        public const string ShowFirePanel = "ShowFirePanel";
    }
    #endregion

    #region 删除提示
    public class DeleteTipPanelMsg
    {
        public const string TypeName = "DeleteTipPanelMsg";
        public const string ShowFirePanel = "ShowFirePanel";
    }
    #endregion

    #region 路径保存提示
    public class SaveTipPanelMsg
    {
        public const string TypeName = "SaveTipPanelMsg";
        public const string ShowFirePanel = "ShowFirePanel";
    }
    #endregion

    #region 消防路线界面
    public class RountePanellMsg
    {
        public const string TypeName = "RountePanellMsg";
        public const string ShowFirePanel = "ShowFirePanel";
    }
    #endregion

    #region 消防标题
    public class TitlePanelMsg
    {
        public const string TypeName = "TitlePanelMsg";
        public const string ShowFirePanel = "ShowFirePanel";
    }
    #endregion

    #region 工艺流程
    public class TechnologyProPanelMsg 
    {
        public const string TypeName = "TechnologyProPanel";
    }
    #endregion

    #region 自动漫游
    public class AutoRoamPanelMsg
    {
        public const string TypeName = "AutoRoamPanelMsg";
        public const string SetInfoTxt = "SetInfoTxt";
        public const string ShowBuildDevices = "ShowBuildDevices";
        
    }
    #endregion

    #region 门禁
    public class DoorAccessControlHistoryPanelMsg
    {
        public const string TypeName = "DoorAccessControlHistoryPanelMsg";
        public const string GetSearchInput = "GetSearchInput";
        public const string ShowData = "ShowData";
        public const string GetData = "GetData";
    }
    #endregion

    #region 消防没有路径提示
    public class FirePathTipPanelMsg 
    {
        public const string TypeName = "FirePathTipPanelMsg";
        public const string ShowFirePanel = "ShowFirePanel";
    }
    #endregion
	
	 #region 文档管理（上传下载）
    public class FileTransferPanelMsg
    {
        public const string TypeName = "FileTransferPanel";
        public const string ShowInfo = "ShowInfo";
        public const string HideInfo = "HideInfo";
        public const string TransferItem = "TransferItem";
        public const string AddDropDown = "AddDropDown";
        public const string GetManualTypes = "GetManualTypes";
        public const string GetDevTypes = "GetDevTypes";
        public const string RefreshSearchingItem = "RefreshSearchingItem";
        public const string SetTransferDot = "SetTransferDot";
        public const string RefreshItemInfo = "RefreshItemInfo";
        public const string IsTransferInfoInTransferTask = "IsTransferInfoInTransferTask";
        public const string SetCancelSingleButtonState = "SetCancelSingleButtonState";
    }
    
    public class UploadFileChoosePanelMsg
    {
        public const string TypeName = "UploadFileChoosePanel";
        public const string ShowInfo = "ShowInfo";
    }
    public class FileTransferModifyPanelMsg
    {
        public const string TypeName = "FileTransferModifyPanel";
        public const string ShowInfo = "ShowInfo";
    }
    #endregion

    #region 设备分组编辑
    public class DevEditGroupPanelMsg
    {
        public const string TypeName = "DevEditGroupPanel";
        public const string ShowWindow = "ShowWindow";
        public const string CloseWindow = "CloseWindow";
        public const string SetWindowState = "SetWindowState";
        public const string ShowBackButton = "ShowBackButton";
        public const string ShowUpperStoryButton = "ShowUpperStoryButton";
    }

    public class ModelGroupPanelMsg
    {
        public const string TypeName = "ModelGroupPanel";
        public const string ShowWindow = "ShowWindow";
        public const string CloseWindow = "CloseWindow";
        public const string SetWindowState = "SetWindowState";
        public const string GetTree = "GetTree";
        public const string SelectDev = "SelectDev";
        public const string MoveSelectNodeToTarget = "MoveSelectNodeToTarget";
        public const string GetSelectNodeList = "GetSelectNodeList";
        public const string AddDevGroup = "AddDevGroup";
        public const string RemoveDevGroup = "RemoveDevGroup";
        public const string RefreshTree = "RefreshTree";
        public const string SelectNode = "SelectNode";
    }

    public class ModelGroupNodeSelectPanelMsg
    {
        public const string TypeName = "ModelGroupNodeSelectPanel";
        public const string ShowSelectWindow = "ShowSelectWindow";
    }

    public class ModelGroupNamePanelMsg
    {
        public const string TypeName = "ModelGroupNamePanel";
        public const string SetInfo = "SetInfo";
    }
    #endregion

    #region 设备修改
    public class ModelModifyPanelMsg
    {
        public const string TypeName = "ModelModifyPanel";
        public const string ShowWindow = "ShowWindow";
        public const string CloseWindow = "CloseWindow";
        public const string MaterialChange = "MaterialChange";
    }

    public class ModelSystemTreePanelMsg
    {
        public const string TypeName = "ModelSystemTreePanel";
        public const string ShowModelTree = "ShowModelTree";
        public const string CloseWindow = "CloseWindow";
        public const string SetBackButtonState = "BackToDepView";
    }

    public class ModelGizmoEditPanelMsg
    {
        public const string TypeName = "ModelGizmoEditPanel";
        public const string OnTranslationToggleChanged = "OnTranslationToggleChanged";
        public const string OnScaleToggleChanged = "OnScaleToggleChanged";
        public const string OnRotationToggleChanged = "OnRotationToggleChanged";
        public const string CloseWindow = "CloseWindows";
    }

    public class ModelMeasurePanelMsg
    {
        public const string TypeName = "ModelMeasurePanel";
        public const string SetMeasureState = "SetMeasureState";
    }

    public class SmartModelInfoPanelMsg
    {
        public const string TypeName = "SmartModelInfoPanel";
        public const string ShowInfo = "ShowInfo";
        public const string ShowPageInfo = "ShowPageInfo";
    }
    #endregion

    public class LoadModelMsg
    {
        public const string TypeName="LoadModelMsg";
        public const string OnBeforeLoadModel="OnBeforeLoadModel";
        public const string OnAfterLoadModel="OnAfterLoadModel";
    }

    public class ModelScaneMsg
    {
        public const string TypeName="ModelScaneMsg";
        public const string StartSubScanners="StartSubScanners";
    }

    public class RTEditorMsg
    {
        public const string TypeName="RTEditorMsg";
        public const string OnEditorClosed="EditorClosed";

        public const string OnSelectionChanged="OnSelectionChanged";
    }
}
