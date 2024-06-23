using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Admin;
using LootLocker.Requests;
using LootLocker.LootLockerEnums;
using LootLocker.Extension;
using System;




//just meant to hold leaderboard API stuff

public static class LeaderBoardGateway
{
    const int PAGESIZE = 50;



    public static int convertTimestampToScore(float timestamp)
    {
        return TimeSpan.Parse("0:10:0:0").Milliseconds - (int)timestamp;
    }

    public static void SubmitScore(string leaderboardKey, string memberID, int score)
    {
        LootLockerSDKManager.SubmitScore(memberID, score, leaderboardKey, (response) =>
        {
            if (!response.success)
            {
                Debug.Log("Could not submit score!");
                Debug.Log(response.errorData.ToString());
                return;
            }
            Debug.Log("Successfully submitted score!");

        });
    }


    public static void getLeaderBoardEntries(string leaderboardKey, int page=0)
    {
        int count = 50;

        Debug.Log("gettingLeaderBoardEntries");


        LootLockerSDKManager.GetScoreList(leaderboardKey, count, page*PAGESIZE, (response) =>
        {
            if (!response.success)
            {
                Debug.Log("Could not get score list!");
                Debug.Log(response.errorData.ToString());
                return;
            }
            Debug.Log("Successfully got score list!");
        });
    }

    public static void getLeaderBoardEntry(string leaderboardKey, string memberID)
    {
        Debug.Log("gettingLeaderBoardEntry");
        LootLockerSDKManager.GetMemberRank(leaderboardKey, memberID, (response) =>
        {
            if (!response.success)
            {
                Debug.Log("Could not get the entry!");
                Debug.Log(response.errorData.ToString());
                return;
            }
            Debug.Log("Successfully got entry!\nPlayer:" + response.player + "\nScore: " + response.score );
        });
    }

}
