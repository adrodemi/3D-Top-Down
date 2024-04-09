using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    public static void DeleteAllSavings()
    {
        PlayerPrefs.DeleteAll();
    }
    public static void SetPlayerPosition(Vector3 position)
    {
        PlayerPrefs.SetFloat("PlayerPositionX", position.x);
        PlayerPrefs.SetFloat("PlayerPositionY", position.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", position.z);
    }
    public static Vector3 GetPlayerPosition()
    {
        if (PlayerPrefs.HasKey("PlayerPositionX"))
        {
            return new Vector3(PlayerPrefs.GetFloat("PlayerPositionX"),
                PlayerPrefs.GetFloat("PlayerPositionY"),
                PlayerPrefs.GetFloat("PlayerPositionY"));
        }
        else
        {
            return new Vector3(10f, 0.5f, 20f);
        }
    }
    public static void SetDoneQuests(List<Quest> quests)
    {
        foreach (var quest in quests)
            PlayerPrefs.SetInt($"Quest{quest.id}", 1);
    }
}