using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.Events;

public class CGWallInfo : JSONObject
{
    public string m_Title;
    public List<CGNodeInfo> m_NodeInfo;

    public override void ParseJSON(JToken json)
    {
        m_Title = (string)json.SelectToken("Title");
        JToken nodesJson = json.SelectToken("Nodes");
        if (nodesJson != null)
        {
            foreach (JToken nodeJson in nodesJson.Children())
            {
                CGNodeInfo node = new CGNodeInfo();
                node.ParseJSON(nodeJson);
                m_NodeInfo.Add(node);
            }
        }
    }

    public override void AppendJSON(ref JObject json)
    {
        base.AppendJSON(ref json);
        /*
        JArray choices = new JArray();
        m_Choices.ForEach((TBLChoice choice) =>
        {
            JObject choiceJSON = new JObject();
            choice.AppendJSON(ref choiceJSON);
            choices.Add(choiceJSON);
        });
        json.Add("choices", choices);
        */
    }

}
