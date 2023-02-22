using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGHoldNode : MonoBehaviour
{
    public CGNodeInfo m_NodeInfo;

    public void ProcessInfo(CGNodeInfo Info)
    {
        m_NodeInfo = Info;
        gameObject.transform.position = new Vector3(m_NodeInfo.m_Position.x, 0f, m_NodeInfo.m_Position.y);
    }
}
