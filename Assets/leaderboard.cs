using LootLocker.Requests;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class leaderboard : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform m_ContentContainer;
    [SerializeField] private GameObject m_itemPrefab;
    [SerializeField] private TMP_Text leaderboardTitle;
    [SerializeField] private GameObject prevButton;
    [SerializeField] private GameObject nextButton;

    private int leaderboardIndex = GlobalReferences.initalLeaderboardIndex;

    private string[] leaderboards = new string[GlobalReferences.NUMBEROFLEVELS + 1];
    void Start()
    {
        leaderboards[0] = "AnyPercent";
        for (int i = 1; i < GlobalReferences.NUMBEROFLEVELS + 1; i++) {
            leaderboards[i] = "level" + i;
        }

        prevButton.SetActive(false);
        viewLeaderBoard(leaderboardIndex);

    }

    public void viewNextLeaderBoard()
    {
        prevButton.SetActive(true);

        unloadLeaderboard();
        viewLeaderBoard(++leaderboardIndex);
        if (leaderboardIndex == leaderboards.Length - 1)
        {
            nextButton.SetActive(false);
        }
    }

    public void viewPreviousLeaderboard() {
        nextButton.SetActive(true);


        unloadLeaderboard();
        viewLeaderBoard(--leaderboardIndex);
        if (leaderboardIndex == 0)
        {
            prevButton.SetActive(false);
        }
    }

    private void unloadLeaderboard()
    {
        leaderboardTitle.text = "";
        for (int i = 0; i < m_ContentContainer.childCount; i++)
        {
            UnityEngine.Object.Destroy(m_ContentContainer.GetChild(i).GameObject());
        }
    }

    public void viewLeaderBoard(int leaderboardIndex)
    {
        leaderboardTitle.text = leaderboards[leaderboardIndex];
        LeaderBoardGateway.getLeaderBoardEntries(leaderboards[leaderboardIndex], (LootLockerLeaderboardMember[] lootLockerLeaderBoardMembers) =>
        {
            if (lootLockerLeaderBoardMembers == null || lootLockerLeaderBoardMembers.Length == 0)
            {
                Debug.Log("null/empty leaderboard");
                return;
            }
            Debug.Log("leaderboard length for leaderboard " + leaderboards[leaderboardIndex] + ": " + lootLockerLeaderBoardMembers.Length);

            foreach (var item in lootLockerLeaderBoardMembers)
            {
                Debug.Log(item);
                Debug.Log("above is item we are loading for leaderboard");
                var item_go = Instantiate(m_itemPrefab);
                TimeSpan timespan = LeaderBoardGateway.convertScoreToTimeSpan(item.score);
                item_go.GetComponentInChildren<TMP_Text>().text = "#" + item.rank + " " + item.metadata + " - " + timespan.Minutes + ":" + timespan.Seconds + ":" + timespan.Milliseconds;
                item_go.transform.SetParent(m_ContentContainer, false);
                //item_go.transform.localScale = Vector2.one;
            }
        });
    }

    public void mainMenu()
    {
        GlobalReferences.initalLeaderboardIndex = 0;
        SceneManager.UnloadSceneAsync(SceneNames.LEADERBOARD);
        if (!GlobalEvents.PlayerStartedMoving.Invoked()) SceneManager.UnloadSceneAsync(SceneNames.MAINMENU);
        else GlobalReferences.LEVELMANAGER.unloadLevel();//if we are not loading this from level controller then uninvoke
        SceneManager.LoadSceneAsync(SceneNames.MAINMENU, LoadSceneMode.Additive);
    }

  
}
