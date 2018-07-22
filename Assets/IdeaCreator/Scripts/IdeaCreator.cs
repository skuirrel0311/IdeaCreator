using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public struct Idea
{
    public GameData GameData;
    public string changePointText;
}

public class IdeaCreator
{
    List<GameData> gameDataList;
    List<string> changePointList;
    int[] potentialArray;

    public IdeaCreator()
    {
        gameDataList = ImportGameData();
        changePointList = ImportChangePointData();

        List<int> gameDataPotentialList = new List<int>();
        for (int i = 0; i < gameDataList.Count; i++)
        {
            gameDataPotentialList.Add(gameDataList[i].Potential);
        }
        potentialArray = gameDataPotentialList.ToArray();
    }

    public List<GameData> ImportGameData()
    {
        if (gameDataList != null) return gameDataList;

        List<string[]> csvData = TextImpoter.LoadText("GameData");
        gameDataList = new List<GameData>();
        string[] temp;
        GameData data;

        for (int i = 0; i < csvData.Count; i++)
        {
            temp = csvData[i];
            data = new GameData();
            data.Title = temp[0];
            data.TopCount = TextImpoter.StringToInt(temp[1]);
            data.USRanking = TextImpoter.StringToInt(temp[2]);
            data.Publisher = temp[3];

            gameDataList.Add(data);
        }

        return gameDataList;
    }

    public List<string> ImportChangePointData()
    {
        if (changePointList != null) return changePointList;

        List<string[]> csvData = TextImpoter.LoadText("ChangePointData");

        changePointList = new List<string>();
        for (int i = 0; i < csvData.Count; i++)
        {
            changePointList.Add(csvData[i][0]);
        }

        return changePointList;
    }

    public Idea Create()
    {
        Idea idea;
        idea.GameData = GetRandomGame();
        idea.changePointText = GetRandomChangePointText();

        return idea;
    }

    GameData GetRandomGame()
    {
        int index = KKUtilities.GetRandomIndexWithWeight(potentialArray);

        return gameDataList[index];
    }
    string GetRandomChangePointText()
    {
        return changePointList[Random.Range(0, changePointList.Count)];
    }
}
