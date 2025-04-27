using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager : Singleton<ResourceManager>
{
    private Dictionary<SkillType, Base_SkillData> _baseSkillDatas = new Dictionary<SkillType, Base_SkillData>();

    

    protected override void Awake()
    {
        base.Awake();
        SetBaseSkillDatas();
    }

    private void SetBaseSkillDatas()
    {
        Addressables.LoadAssetsAsync<Base_SkillData>("SkillData", (obj) => { }).Completed += handle => {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                foreach (var obj in handle.Result)
                {
                    _baseSkillDatas[obj.SkillType] = obj;
                }
            }
            else
            {
                Debug.LogError("스킬 데이터 로드 실패!");
            }
        };
    }
    public Base_SkillData GetBaseSkillData(SkillType skillType) => _baseSkillDatas[skillType];
}
