using Mogoson.CameraExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTranslatePro : MouseTranslate
{
    /// <summary>
    /// 设置场景移动到该位置
    /// </summary>
    public void SetTranslatePosition(Vector3 pos,bool immediate=false)
    {
        targetOffset = pos - areaSettings.GetPos();
        if(immediate)
            CurrentOffset = targetOffset;
    }
    public void ResetTranslateOffset()
    {
        targetOffset = Vector3.zero;
    }

}
