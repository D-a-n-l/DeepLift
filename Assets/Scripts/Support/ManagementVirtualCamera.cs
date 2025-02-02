using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public struct ManagementVirtualCamera
{
    public static IEnumerator Shake(CinemachineBasicMultiChannelPerlin vCamera, float amplitude, float frequency, float time)
    {
        vCamera.AmplitudeGain = amplitude;
        vCamera.FrequencyGain = frequency;

        yield return new WaitForSeconds(time);

        vCamera.AmplitudeGain = 0;
        vCamera.FrequencyGain = 0;
    }
}