using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class leaderboard : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform m_ContentContainer;
    [SerializeField] private GameObject m_itemPrefab;
    [SerializeField] private TMP_Text leaderboardTitle;
    [SerializeField] private GameObject prevButton;
    [SerializeField] private GameObject nextButton;

    private int leaderboardIndex = 0;

    private string[] leaderboards = new string[GlobalReferences.NUMBEROFLEVELS];
    void Start()
    {
        leaderboards[0] = "AnyPercent";
        for (int i = 1; i < GlobalReferences.NUMBEROFLEVELS; i++) {
            leaderboards[i] = "level" + i;
        }

        prevButton.SetActive(false);
        viewLeaderBoard(leaderboardIndex);
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Object.Destroy(m_ContentContainer.GetChild(i).GameObject());
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
                item_go.GetComponentInChildren<TMP_Text>().text = "#" + item.rank + " " + item.metadata + " - " + item.score;
                item_go.transform.SetParent(m_ContentContainer, false);
                //item_go.transform.localScale = Vector2.one;
            }
        });
    }
}
