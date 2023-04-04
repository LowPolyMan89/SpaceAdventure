using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SBuffDatabase", menuName = "ScriptableObjects/SBuffDatabase", order = 3)]
public class SBuffDatabase : ScriptableObject
{
    [SerializeField] private List<SBuffData> _buffDatas = new List<SBuffData>();
    
    public Dictionary<string, SBuffData> AllBuffs = new Dictionary<string, SBuffData>();
    
    
    public void Init()
    {
        foreach (var b in _buffDatas)
        {
            AllBuffs.Add(b.BuffId, b);
        }
    }
    
    public SBuffData GetBuff(string buffId)
    {
        SBuffData value;
        AllBuffs.TryGetValue(buffId, out value);
        return value;
    }
    
#if UNITY_EDITOR 
    public void RefreshDatabase()
    {
        _buffDatas.Clear();
        AllBuffs.Clear();
        var guids = AssetDatabase.FindAssets("t:SBuffData");
        
        foreach (string guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            SBuffData so = AssetDatabase.LoadAssetAtPath<SBuffData>(path);

            if (string.IsNullOrEmpty(so.BuffId))
            {
                Debug.LogError($@"Отсутствует ID в объекте:  {path}" );
                continue;
            }
            
            if (!AllBuffs.ContainsKey(so.BuffId))
            {
                _buffDatas.Add(so);
                AllBuffs.Add(so.BuffId, so);
                Debug.Log("Add item to data base: " + path);
                EditorUtility.SetDirty(this);
                AssetDatabase.SaveAssets();
                continue;
            }
            else
            {
                Debug.LogError($@"Одинаковые ID в базе:  {so.BuffId} {path}" );
            }
            
        }
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(SBuffDatabase))]
public class BuffsDatabaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SBuffDatabase myScript = (SBuffDatabase)target;
        if(GUILayout.Button("Refresh Database"))
        {
            myScript.RefreshDatabase();
        }
    }
}
#endif



