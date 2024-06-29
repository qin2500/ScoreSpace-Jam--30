using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using LootLocker.LootLockerEnums;
using System;

public static class LeaderBoardGateway
{
    const int PAGESIZE = 50;



    public static int convertTimestampToScore(float timestamp)
    {
        return (int)(TimeSpan.FromSeconds(timestamp).TotalMilliseconds);
    }

    public static TimeSpan convertScoreToTimeSpan(int score)
    {
        Debug.Log("converting score to timespan: " + score);
        return TimeSpan.FromMilliseconds(score);
    }

    public static void SubmitScore(string leaderboardKey, string memberID, int score)
    {
        LootLockerSDKManager.SubmitScore(leaderboardKey + memberID, score, leaderboardKey, memberID, (response) =>
        {
            if (!response.success)
            {
                Debug.Log("Could not submit score!");
                Debug.Log(response.errorData.ToString());
                Debug.Log("traceID: " + response.errorData.trace_id);
                Debug.Log("requestID: " + response.errorData.request_id);

                return;
            }
            Debug.Log("Successfully submitted score!");

        });
    }


    public static void getLeaderBoardEntries(string leaderboardKey, Action<LootLockerLeaderboardMember[]> callback, int page = 0)
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
            callback(response.items);
            return;
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
