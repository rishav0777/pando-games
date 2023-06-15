using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRtId : MonoBehaviour
{

    public string GetRtId()
    {
        string RtId = SystemInfo.deviceModel + SystemInfo.deviceName + SystemInfo.deviceType ;
        return RtId;
    }
}
