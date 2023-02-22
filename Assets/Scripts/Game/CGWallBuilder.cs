using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class CGWallBuilder : MonoBehaviourSingleton<CGWallBuilder>
{
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
    }

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
        }
    }

}
