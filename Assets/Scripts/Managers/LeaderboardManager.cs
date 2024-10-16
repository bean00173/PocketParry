using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

// NOTE: Make sure to include the following namespace wherever you want to access Leaderboard Creator methods
using Dan.Main;
using static UnityEditor.Progress;


public class LeaderboardManager : MonoBehaviour
{
    //[SerializeField] private TMP_Text[] _entryTextObjects;
    [SerializeField] private List<TextMeshProUGUI> ranks;
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;

    public static LeaderboardManager instance;
    public bool leaderboardScene;

    public GameObject loadingBar;

    //// Make changes to this section according to how you're storing the player's score:
    //// ------------------------------------------------------------
    //[SerializeField] private ExampleGame _exampleGame;

    //private int Score => _exampleGame.Score;
    //// ------------------------------------------------------------

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadEntries();
    }

    private void LoadEntries()
    {
        if (leaderboardScene)
        {
            loadingBar.gameObject.SetActive(true);

            foreach (TextMeshProUGUI r in ranks)
                r.text = "";
            foreach (TextMeshProUGUI n in names)
                n.text = "";
            foreach (TextMeshProUGUI s in scores)
                s.text = "";

            Leaderboards.PocketParryLeaderboard.GetEntries(entries =>
            {
                loadingBar.gameObject.SetActive(false);

                var length = Mathf.Min(names.Count, entries.Length);
                for (int i = 0; i < length; i++)
                {
                    ranks[i].text = $"{entries[i].Rank}";
                    names[i].text = $"{entries[i].Username}";
                    scores[i].text = $"{entries[i].Score}";
                }

            });
        } 
    }

    public void UploadEntry(string name, int score)
    {
        Leaderboards.PocketParryLeaderboard.UploadNewEntry(name, score, isSuccessful =>
        {
            if (isSuccessful)
            {
                LoadEntries();
                Leaderboards.PocketParryLeaderboard.ResetPlayer();
            }
                
        });

    }
}