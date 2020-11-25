using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class GUIDAssetInfo : MonoBehaviour
{
    public string guid;
    // Start is called before the first frame update

    public string path;
    void Start()
    {
        
    }

    [ContextMenu("GetPath")]
    public void GetPath(){
        path=AssetDatabase.GUIDToAssetPath(guid);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
