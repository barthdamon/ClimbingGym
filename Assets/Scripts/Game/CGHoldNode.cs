using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGHoldNode : MonoBehaviour
{
    public CGNodeInfo m_NodeInfo;

    public List<MeshRenderer> m_BigMeshes;
    public List<MeshRenderer> m_FootMeshs;

    public float m_AxisScale;

    float m_MeterConversion = 0.001f;

    public void ProcessInfo(CGNodeInfo Info)
    {
        m_NodeInfo = Info;
        gameObject.transform.localPosition = new Vector3(HorizontalPosition(), VerticalPosition(), 0f);
        gameObject.transform.localEulerAngles = new Vector3(m_NodeInfo.m_Orientation, 0f, 0f);
        for (int i = 0; i < m_BigMeshes.Count; ++i)
        {
            m_BigMeshes[i].enabled = m_NodeInfo.IsBigHold();
        }
        for (int i = 0; i < m_FootMeshs.Count; ++i)
        {
            m_FootMeshs[i].enabled = !m_NodeInfo.IsBigHold();
        }
    }


    private float VerticalPosition()
    {
        return m_MeterConversion * m_NodeInfo.m_Position.y * 125f;
    }

    private float HorizontalPosition()
    {
        return m_MeterConversion * m_NodeInfo.m_Position.x * 125f;
    }

    /*
Inter holes vertical distance = 125 mm (tolerance ± 1 mm)
Inter-panel – vertical distance = 375 mm (tolerance ± 2 mm)
Edge (horizontal) to hole vertical distance = 187.5 mm (tolerance ±1 mm) Inter-panel – horizontal distance = 250 mm (tolerance ±1 mm)
Inter-holes – horizontal distance = 125 mm (tolerance ± 1 mm)
     * 
     */
}
