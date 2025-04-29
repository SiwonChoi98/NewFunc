/*using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OrderData))]
public class BaseSkillDataEditor : Editor
{
    SerializedProperty skillTypeProp;
    SerializedProperty skillBehaviourTypeProp;
    SerializedProperty targetTypeProp;
    SerializedProperty defaultDataProp;
    SerializedProperty bulletDataProp;
    SerializedProperty skillBehaviourProp;

    private void OnEnable()
    {
        skillTypeProp = serializedObject.FindProperty("SkillType");
        skillBehaviourTypeProp = serializedObject.FindProperty("SkillBehaviourType"); // 이름 수정됨
        targetTypeProp = serializedObject.FindProperty("TargetType");
        defaultDataProp = serializedObject.FindProperty("DefaultData");
        bulletDataProp = serializedObject.FindProperty("BulletData");
        skillBehaviourProp = serializedObject.FindProperty("SkillBehaviour");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(skillTypeProp);
        EditorGUILayout.PropertyField(skillBehaviourTypeProp);
        EditorGUILayout.PropertyField(targetTypeProp);
        EditorGUILayout.PropertyField(defaultDataProp, true);

        if ((SkillBehaviourType)skillBehaviourTypeProp.enumValueIndex == SkillBehaviourType.BULLET)
        {
            EditorGUILayout.PropertyField(bulletDataProp, true);
        }

        EditorGUILayout.PropertyField(skillBehaviourProp);

        serializedObject.ApplyModifiedProperties();
    }
}*/