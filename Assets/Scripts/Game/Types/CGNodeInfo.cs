using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.Events;

public class CGNodeInfo : JSONObject
{
    string m_RawGrid;
    string m_RawPosition;
    string m_RawOrientation;

    public override void ParseJSON(JToken json)
    {
        m_RawGrid = (string)json.SelectToken("Grid");
        m_RawPosition = (string)json.SelectToken("Position");
        m_RawOrientation = (string)json.SelectToken("Orientation");
        CGLogChannels.GetOrCreateInstance().LogChannel(CGLogChannel.JSON, "Node: " + m_RawGrid + "," + m_RawPosition + "," + m_RawOrientation);
    }

    public override void AppendJSON(ref JObject json)
    {
        base.AppendJSON(ref json);
        /*
         *         json.Add("title", m_Title);
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
