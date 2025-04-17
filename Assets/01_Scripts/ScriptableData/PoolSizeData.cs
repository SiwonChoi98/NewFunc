using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolSizeData", menuName = "Scriptable Objects/PoolSizeData", order = 0)]
public class PoolSizeData : ScriptableObject
{
    [System.Serializable]
    public class PoolSizeSetting
    {
        public PoolObjectType poolType;
        [UnityEngine.Range(1, 200)] public int maxSize = 10;
    }

    /// <summary>
    /// 사이즈 조절이 필요한 풀 오브젝트만 추가
    /// </summary>
    public List<PoolSizeSetting> poolSettings = new List<PoolSizeSetting>();
}
