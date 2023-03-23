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
    string m_RawRotGrid;
    string m_RawPosition;
    string m_RawOrientation;
    string m_Kind;

    int m_XGrid;
    int m_YGrid;
    int m_XCoord;
    int m_YCoord;

    int m_XRotGrid;
    int m_YRotGrid;
    int m_XRotCoord;
    int m_YRotCoord;

    Vector2 m_RotPosition;

    public Vector2 m_Position;
    public float m_Orientation;

    public override void ParseJSON(JToken json)
    {
        m_RawGrid = (string)json.SelectToken("Grid");
        m_RawPosition = (string)json.SelectToken("Position");
        // will have to make a fake grid with this and find the angle given the two points
        m_RawOrientation = (string)json.SelectToken("Orientation");
        m_Kind = (string)json.SelectToken("Kind");
        m_RawRotGrid = (string)json.SelectToken("RotGrid");

        if (m_RawGrid.Contains("SN"))
        {
            m_XGrid = 0;
        }
        else
        {
            m_XGrid = 1;
        }

        if (m_RawRotGrid.Contains("SN"))
        {
            m_XRotGrid = 0;
        }
        else
        {
            m_XRotGrid = 1;
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

        m_YGrid = int.Parse(numberstring) - 1;

        string rotnumberstring = "";
        if (m_RawRotGrid.Length <= 3)
        {
            rotnumberstring = m_RawRotGrid.Substring(m_RawRotGrid.Length - 1);
        }
        else
        {
            rotnumberstring = m_RawRotGrid.Substring(m_RawRotGrid.Length - 2);
        }

        m_YRotGrid = int.Parse(rotnumberstring) - 1;

        string numbersOnly = Regex.Replace(m_RawPosition, "[^0-9]", "");
        m_YCoord = int.Parse(numbersOnly);

        string[] letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "L", "M" };
        for (int i = 0; i < letters.Length; ++i)
        {
            if (m_RawPosition.Contains(letters[i]))
            {
                m_XCoord = i;
            }
        }


        float xMultiplier = .125f;
        float gridSpacer = .1875f;

        float x = (m_XGrid * 10 * xMultiplier) + (m_XGrid * gridSpacer) + (m_XCoord * xMultiplier);
        float y = (m_YGrid * 10 * xMultiplier) + (m_YGrid * gridSpacer) + (m_YCoord * xMultiplier);
        m_Position = new Vector2(x, y);








        string numbersOnlyRot = Regex.Replace(m_RawOrientation, "[^0-9]", "");
        m_YRotCoord = int.Parse(numbersOnlyRot);
        for (int i = 0; i < letters.Length; ++i)
        {
            if (m_RawOrientation.Contains(letters[i]))
            {
                m_XRotCoord = i;
            }
        }

        int xRot = (m_XRotGrid * 10) + m_XRotCoord;
        int yRot = (m_YRotGrid * 10) + m_YRotCoord;

        m_RotPosition = new Vector2(xRot, yRot);

        float xCos = xRot - x;
        float yCos = yRot - y;
        if (yCos > 0)
        {
            m_Orientation = Mathf.Atan(xCos / yCos);
        }

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

    public bool IsBigHold()
    {
        return m_Kind.Contains("BIG");
    }
}
