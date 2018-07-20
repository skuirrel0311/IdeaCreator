using System.IO;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class GameData
{
    public string Title;
    public int USRanking;
    public int TopCount;
    public int Potential
    {
        get
        {
            int potential = 0;

            if (potential > 0)
            {
                potential += TopCount / 10;
            }

            if (USRanking < 10) potential += 3;
            else if (USRanking < 20) potential += 2;
            else if (USRanking < 30) potential += 1;

            return Mathf.Clamp(potential, 0 , 10);
        }
    }
}

public class TextImpoter
{
    public static int StringToInt(string str)
    {
        int temp = 0;

        int.TryParse(str, out temp);

        return temp;
    }

    public static List<string[]> LoadText(string fileName)
    {
        List<string[]> csvData = new List<string[]>();

        TextAsset csvFile = MyAssetStore.Instance.GetAsset<TextAsset>(fileName, "CSV/");

        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            csvData.Add(line.Split(','));
        }

        return csvData;
    }

    void SaveText(string fileName, string text)
    {

    }
}
