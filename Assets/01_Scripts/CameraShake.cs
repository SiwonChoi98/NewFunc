using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineBasicMultiChannelPerlin noise;
    private void Awake()
    {
        noise = GetComponent<CinemachineBasicMultiChannelPerlin>();
    }
    
    
    public void Shake(float intensity, float duration)
    {
        //중복 방지
        if (noise.AmplitudeGain != 0)
            return;
        
        //StartCoroutine(ShakeRoutine(intensity, duration));
        ShakeRoutineUniTask(intensity, duration);
    }

    /*private IEnumerator ShakeRoutine(float intensity, float duration)
    {
        noise.AmplitudeGain = intensity;
        yield return new WaitForSeconds(duration);
        noise.AmplitudeGain = 0;
    }*/

    private async void ShakeRoutineUniTask(float intensity, float duration)
    {
        noise.AmplitudeGain = intensity;
        await UniTask.Delay(TimeSpan.FromSeconds(duration));
        noise.AmplitudeGain = 0;
    }
}
