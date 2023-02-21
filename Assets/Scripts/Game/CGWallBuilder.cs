using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class CGWallBuilder : MonoBehaviourSingleton<CGWallBuilder>
{
    List<CGWallInfo> m_WallInfos;

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
    }

}
