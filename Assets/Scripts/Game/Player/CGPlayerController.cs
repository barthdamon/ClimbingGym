using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CGPlayerController : MonoBehaviour
{
    public OVRHand m_RightHand;
    public OVRHand m_LeftHand;


    public TMP_Text m_DebugText;

    public GameObject m_WallObject;
    public float m_RootWallOffset;

    private void Start()
    {
        m_DebugText.text = "Listening";
    }

    // Update is called once per frame
    void Update()
    {
       //if (m_RightHand.IsPointerPoseValid)
       //{
       //     m_DebugText.text = "Pointer Detected";
       //     m_WallObject.transform.position = new Vector3(m_RightHand.transform.position.x, m_RightHand.transform.position.y + m_RootWallOffset, m_RightHand.transform.position.z);
       //}
        if (m_RightHand.IsSystemGestureInProgress)
        {
            m_DebugText.text = "System Detected";
            m_WallObject.transform.position = new Vector3(m_RightHand.transform.position.x, m_RightHand.transform.position.y + m_RootWallOffset, m_RightHand.transform.position.z);
        }
    }
}
