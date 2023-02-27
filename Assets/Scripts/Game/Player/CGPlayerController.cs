using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGPlayerController : MonoBehaviour
{
    public OVRHand m_RightHand;
    public OVRHand m_LeftHand;

    public GameObject m_WallObject;
    public float m_RootWallOffset;

    // Update is called once per frame
    void Update()
    {
       if (m_RightHand.IsPointerPoseValid)
       {
            m_WallObject.transform.position = new Vector3(m_RightHand.transform.position.x, m_RightHand.transform.position.y + m_RootWallOffset, m_RightHand.transform.position.z);
       }
    }
}
