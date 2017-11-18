using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
[System.Serializable]
public class ExportData
{
    public List<string> list = new List<string>();
}

public class XmlExporter : MonoBehaviour
{
    ExportData exportData = new ExportData();
    public void SaveHeroesDatabase()
    {
        StreamWriter streamWriter = new StreamWriter(new FileStream(Application.dataPath + "/Resources/XML/Hero_Data.xml", FileMode.Create), System.Text.Encoding.UTF8);
        XmlSerializer serializer = new XmlSerializer(typeof(HeroDatabase));
        XmlSerializer xmlSerializer = new XmlSerializer(exportData.GetType());

        xmlSerializer.Serialize(streamWriter, exportData);
        streamWriter.Close();
    }
    public void LoadHeroesDatabase()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(HeroDatabase));
        FileStream stream = new FileStream(Application.dataPath + "/Resources/XML/Hero_Data.xml", FileMode.Open);
        exportData = serializer.Deserialize(stream) as ExportData;
        stream.Close();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
