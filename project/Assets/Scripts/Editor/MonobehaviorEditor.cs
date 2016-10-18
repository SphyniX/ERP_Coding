using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[CanEditMultipleObjects]
[CustomEditor(typeof(MonoBehavior), true)]
public class MonobehaviorEditor : Editor
{
	private const BindingFlags bflag = 
		BindingFlags.Instance | 
		BindingFlags.Public | 
		BindingFlags.NonPublic;

	private static bool m_Show;
	private List<FieldInfo> m_Fields;

	protected virtual void OnEnable()
	{
		m_Fields = new List<FieldInfo>();
		var type = target.GetType();

		var fields = type.GetFields(bflag);
		for (int i = 0; i < fields.Length; ++i) {
			var field = fields[i];
			var attr = System.Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
			if (attr != null) m_Fields.Add(field);
		}
	}

    private void inspectObject(string fName, System.Object obj)
    {        
        if (obj is UnityEngine.Object) {
            var unityObj = obj as UnityEngine.Object;
            EditorGUILayout.ObjectField(fName, unityObj, unityObj.GetType(), true);
        } else {
            EditorGUILayout.TextField(fName, obj == null ? "NULL" : obj.ToString());
        }
    }

    protected void ShowClassTip()
    {
        var attr = System.Attribute.GetCustomAttribute(target.GetType(), typeof(DescriptionAttribute)) as DescriptionAttribute;
        if (attr != null) {
            EditorGUILayout.LabelField(string.Format("<color=yellow>{0}</color>", attr.description), CustomEditorStyles.titleStyle);
        }
    }

	protected void ShowDescFields()
	{
		m_Show = GUILayout.Toggle(m_Show, string.Format("Show In Inspector[{0}]", m_Fields.Count));
		if (m_Show) {
			var self = target as MonoBehavior;
			++EditorGUI.indentLevel;
			EditorGUI.BeginDisabledGroup(true);
			for (int i = 0; i < m_Fields.Count; ++i) {
				var field = m_Fields[i];
				var attr = System.Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
				string fName = attr.description == null ? field.Name : attr.description;
                var obj = field.GetValue(self);
                inspectObject(fName, obj);

                if (obj != null) {
                    if (field.FieldType.IsArray) {
                        ++EditorGUI.indentLevel;
                        System.Array array = obj as System.Array;

                        for (int j = 0; j < array.Length; ++j) {
                            inspectObject("#" + j, array.GetValue(j));
                        }
                        --EditorGUI.indentLevel;
                    } else if (field.FieldType.IsGenericType) {
                        var genericType = field.FieldType.GetGenericTypeDefinition();
                        var specificType = field.FieldType;
                        if (genericType == typeof(List<>)) {
                            var itor = specificType.InvokeMember("GetEnumerator", BindingFlags.InvokeMethod, null, obj, null) as IEnumerator;
                            int j = 0;
                            ++EditorGUI.indentLevel;
                            while (itor.MoveNext()) {
                                inspectObject("#" + (j++), itor.Current);
                            }
                            --EditorGUI.indentLevel;
                        } else if (genericType == typeof(Dictionary<,>)) {
                            // TODO
                        }
                    }
                }
			}
			EditorGUI.EndDisabledGroup();
			--EditorGUI.indentLevel;
		}
	}

    protected void DefaultInspector()
    {
        ShowClassTip();
        base.OnInspectorGUI();
    }

	public override void OnInspectorGUI ()
	{
        DefaultInspector();
		EditorGUILayout.Separator();
		ShowDescFields();
	}
}
