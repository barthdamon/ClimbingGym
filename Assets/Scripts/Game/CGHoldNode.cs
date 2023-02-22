using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGHoldNode : MonoBehaviour
{
    public CGNodeInfo m_NodeInfo;

    public MeshRenderer m_BigMesh;
    public MeshRenderer m_FootMesh;

    public void ProcessInfo(CGNodeInfo Info)
    {
        m_NodeInfo = Info;
        gameObject.transform.localPosition = new Vector3(m_NodeInfo.m_Position.x, m_NodeInfo.m_Position.y, 0f);
        m_BigMesh.enabled = m_NodeInfo.IsBigHold();
        m_FootMesh.enabled = !m_NodeInfo.IsBigHold();
    }
}
