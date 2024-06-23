using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Admin;
using LootLocker.Requests;
using LootLocker.LootLockerEnums;
using LootLocker.Extension;




//just meant to hold leaderboard API stuff

public class LeaderBoardGateway : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    const int PAGESIZE = 50;



    public void SubmitScore(string leaderboardKey, string memberID, int score)
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


    public void getLeaderBoardEntries(string leaderboardKey, int page=0)
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

    public void getLeaderBoardEntry(string leaderboardKey, string memberID)
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
