using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class CGWallBuilder : MonoBehaviourSingleton<CGWallBuilder>
{
    public Transform m_WallRoot;
    public Transform m_HoldParent;
    public GameObject m_HoldNodePrefab;
    public List<CGHoldNode> m_SpawnedHolds = new List<CGHoldNode>();

    List<CGWallInfo> m_WallInfos = new List<CGWallInfo>();

    public void LoadWallData(JToken json)
    {
        JToken wallsJSON = json.SelectToken("Walls");
        if (wallsJSON != null)
        {
            foreach (JToken wallJSON in wallsJSON.Children())
            {
                CGWallInfo newWall = new CGWallInfo();
                newWall.ParseJSON(wallJSON);
                m_WallInfos.Add(newWall);
            }
        }

        LoadWall(0);
        RecalibrateWall(new Vector3(0f, 0f, 0f))
;    }

    void LoadWall(int WallIndex)
    {
        if (m_WallInfos.Count > WallIndex)
        {
            foreach (CGNodeInfo info in m_WallInfos[WallIndex].m_NodeInfos)
            {
                GameObject newHold = Instantiate(m_HoldNodePrefab, m_HoldParent);
                CGHoldNode holdNode = newHold.GetComponent<CGHoldNode>();
                m_SpawnedHolds.Add(holdNode);
                holdNode.ProcessInfo(info);
            }
            //m_HoldParent.rotation = Quaternion.Euler(0f, 90f, 0f);
        }
    }

    public void RecalibrateWall(Vector3 handPosition)
    {
        Vector3 startingPosition = m_SpawnedHolds[0].transform.position;
        Vector3 parentPosition = m_WallRoot.transform.position;
        Vector3 transformedStart = m_WallRoot.InverseTransformPoint(startingPosition);
        Vector3 transformedHand = m_WallRoot.InverseTransformPoint(handPosition);
        Vector3 newRoot = parentPosition + (transformedHand - transformedStart);
        m_WallRoot.transform.position = newRoot;
    }
}
