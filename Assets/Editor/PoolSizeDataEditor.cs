using UnityEditor; 
using UnityEngine;  

[CustomEditor(typeof(PoolSizeData))]
public class PoolSizeDataEditor : Editor
{
    /*
    // 인스펙터 UI를 커스텀할 때 사용하는 메서드
    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // SerializedObject의 상태를 최신으로 갱신

        // 현재 에디터가 대상하고 있는 PoolSizeData 객체 가져오기
        PoolSizeData data = (PoolSizeData)target;

        
        EditorGUILayout.HelpBox("이 설정은 사이즈 조절이 필요한 오브젝트(PoolObjectType)에만 사용하세요.", MessageType.Warning);
        
        // 제목 라벨 출력 (굵은 글씨)
        EditorGUILayout.LabelField("Pool Size Settings", EditorStyles.boldLabel);

        // 리스트 항목마다 UI 출력
        for (int i = 0; i < data.poolSettings.Count; i++)
        {
            var setting = data.poolSettings[i]; // 현재 설정 항목 가져오기

            EditorGUILayout.BeginVertical("box"); // 박스 형태로 감싸기 시작

            // 풀 타입 선택 UI (EnumPopup: 열거형 드롭다운)
            setting.poolType = (PoolObjectType)EditorGUILayout.EnumPopup("Pool Type", setting.poolType);

            // 풀 최대 크기 설정 슬라이더 (최소 1 ~ 최대 200)
            setting.maxSize = EditorGUILayout.IntSlider("Max Size", setting.maxSize, 1, 200);

            // 중복된 PoolObjectType이 있는지 검사
            bool isDuplicate = false;
            for (int j = 0; j < data.poolSettings.Count; j++)
            {
                if (j != i && data.poolSettings[j].poolType == setting.poolType)
                {
                    isDuplicate = true; // 자신이 아닌 다른 항목과 타입이 겹칠 경우 중복
                    break;
                }
            }
            
            // 중복이라면 경고 메시지 표시
            if (isDuplicate)
            {
                EditorGUILayout.HelpBox("이 Pool Type은 이미 존재합니다!", MessageType.Warning);
            }

            GUI.backgroundColor = Color.red;
            // 항목 제거 버튼
            if (GUILayout.Button("Remove"))
            {
                data.poolSettings.RemoveAt(i); // 현재 항목 제거
                break; // 리스트 수정 중이므로 루프 중단
            }

            GUI.backgroundColor = Color.white;
            
            EditorGUILayout.EndVertical(); // 박스 형태 종료
            EditorGUILayout.Space();
        }

        GUI.backgroundColor = Color.green;
        // 항목 추가 버튼
        if (GUILayout.Button("Add Pool Type"))
        {
            // 새로운 기본 항목 추가
            data.poolSettings.Add(new PoolSizeData.PoolSizeSetting());
        }
        
        GUI.backgroundColor = Color.white;

        serializedObject.ApplyModifiedProperties(); // 변경 사항 적용
        EditorUtility.SetDirty(data); // 데이터가 수정되었음을 Unity에 알림 (저장을 위해)
    }*/
    
    private SerializedProperty poolSettingsProperty;

    private void OnEnable()
    {
        // poolSettings 리스트를 SerializedProperty로 가져옴
        poolSettingsProperty = serializedObject.FindProperty("poolSettings");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // 최신 상태로 갱신

        EditorGUILayout.HelpBox("이 설정은 사이즈 조절이 필요한 오브젝트(PoolObjectType)에만 사용하세요.", MessageType.Warning);
        EditorGUILayout.LabelField("Pool Size Settings", EditorStyles.boldLabel);

        for (int i = 0; i < poolSettingsProperty.arraySize; i++)
        {
            SerializedProperty settingProp = poolSettingsProperty.GetArrayElementAtIndex(i);
            SerializedProperty typeProp = settingProp.FindPropertyRelative("poolType");
            SerializedProperty sizeProp = settingProp.FindPropertyRelative("maxSize");

            EditorGUILayout.BeginVertical("box");

            // 풀 타입 선택 (Enum)
            EditorGUILayout.PropertyField(typeProp, new GUIContent("Pool Type"));

            // 최대 크기 슬라이더
            sizeProp.intValue = EditorGUILayout.IntSlider("Max Size", sizeProp.intValue, 1, 200);

            // 중복 검사
            bool isDuplicate = false;
            for (int j = 0; j < poolSettingsProperty.arraySize; j++)
            {
                if (j == i) continue;
                var other = poolSettingsProperty.GetArrayElementAtIndex(j);
                var otherType = other.FindPropertyRelative("poolType");

                if (otherType.enumValueIndex == typeProp.enumValueIndex)
                {
                    isDuplicate = true;
                    break;
                }
            }

            if (isDuplicate)
            {
                EditorGUILayout.HelpBox("이 Pool Type은 이미 존재합니다!", MessageType.Warning);
            }

            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Remove"))
            {
                poolSettingsProperty.DeleteArrayElementAtIndex(i);
                break; // 리스트 수정했으니 루프 중단
            }
            GUI.backgroundColor = Color.white;

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Add Pool Type"))
        {
            poolSettingsProperty.InsertArrayElementAtIndex(poolSettingsProperty.arraySize);
            var newElement = poolSettingsProperty.GetArrayElementAtIndex(poolSettingsProperty.arraySize - 1);
            newElement.FindPropertyRelative("poolType").enumValueIndex = 0;
            newElement.FindPropertyRelative("maxSize").intValue = 10; // 기본값 설정
        }
        GUI.backgroundColor = Color.white;

        serializedObject.ApplyModifiedProperties(); // 변경사항 반영
    }
}
