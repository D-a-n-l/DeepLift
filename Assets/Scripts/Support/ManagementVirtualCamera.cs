using UnityEngine;
using Cinemachine;
using System.Collections;

public struct ManagementVirtualCamera
{
    public static IEnumerator Shake(CinemachineBasicMultiChannelPerlin vCamera, float amplitude, float frequency, float time)
    {
        vCamera.m_AmplitudeGain = amplitude;
        vCamera.m_FrequencyGain = frequency;

        yield return new WaitForSeconds(time);

        vCamera.m_AmplitudeGain = 0;
        vCamera.m_FrequencyGain = 0;
    }
}