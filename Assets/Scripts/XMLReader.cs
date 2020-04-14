//Utility class for reading XML files with settings
using UnityEngine;
using System;
using System.Xml;

public static class XMLReader
{
    public static float ReadIntervalFromFile(TextAsset xmlText)
    {        
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlText.text);
        XmlElement root = xmlDoc.DocumentElement;
        foreach (XmlNode node in root)
        {
            if (node.Name == "interval")
            {
                float interval = -1f;
                if (Single.TryParse(node.InnerText, out interval))
                    return interval;
                else
                    return -1f;
            }
        }
        return -1f;
    }
}
