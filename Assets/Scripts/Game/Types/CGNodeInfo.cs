using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.Events;
using System.Text.RegularExpressions;
using UnityEngine.UIElements;

public class CGNodeInfo : JSONObject
{
    string m_RawGrid;
    string m_RawPosition;
    string m_RawOrientation;

    int m_XGrid;
    int m_YGrid;
    int m_XCoord;
    int m_YCoord;

    public Vector2 m_Position;
    public float m_Orientation;

    public override void ParseJSON(JToken json)
    {
        m_RawGrid = (string)json.SelectToken("Grid");
        m_RawPosition = (string)json.SelectToken("Position");
        // will have to make a fake grid with this and find the angle given the two points
        m_RawOrientation = (string)json.SelectToken("Orientation");

        if (m_RawGrid.Contains("sn"))
        {
            m_XGrid = 0;
        }
        else
        {
            m_XGrid = 1;
        }

        string numberstring = "";
        if (m_RawGrid.Length <= 3)
        {
            numberstring = m_RawGrid.Substring(m_RawGrid.Length - 1);
        }
        else
        {
            numberstring = m_RawGrid.Substring(m_RawGrid.Length - 2);
        }

        m_YGrid = int.Parse(numberstring);

        string numbersOnly = Regex.Replace(m_RawPosition, "[^0-9]", "");
        m_XCoord = int.Parse(numbersOnly);

        string[] letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "L", "M" };
        for (int i = 0; i < letters.Length; ++i)
        {
            if (m_RawPosition.Contains(letters[i]))
            {
                m_YCoord = i;
            }
        }

        int x = (m_XGrid * 10) + m_XCoord;
        int y = (m_YGrid * 10) + m_YCoord;
        m_Position = new Vector2(x, y);

        CGLogChannels.GetOrCreateInstance().LogChannel(CGLogChannel.JSON, "Node: " + m_RawGrid + "," + m_RawPosition + "," + m_RawOrientation + " == (" + x + "," + y + ")");
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
