using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


public class SongListManager : MonoBehaviour
{
    // Fields
    public static SongListManager Instance;
    public GameObject songListPanel;
    public Transform songListContent;
    public GameObject songButtonPrefab;
    public AudioSource songAudioSource;
    public GameObject scrollPanel;

    private List<GameObject> currentSongItems = new List<GameObject>();
    public TVPlacementManager tvPlacementManager; // Reference to TV Placement Manager

    void Awake()
    {
        Instance = this;
        songListPanel.SetActive(false);
        scrollPanel.SetActive(false);
        tvPlacementManager = FindObjectOfType<TVPlacementManager>();
    }

    public void ShowSongsForVinyl(VinylInfo vinyl)
    {
        // Clear previous song items
        foreach (var item in currentSongItems)
        {
            Destroy(item);
        }
        currentSongItems.Clear();
        scrollPanel.SetActive(true);

        if (vinyl != null && vinyl.songs != null)
        {
            foreach (SongData song in vinyl.songs)
            {
                GameObject newItem = Instantiate(songButtonPrefab, songListContent);
                TextMeshProUGUI itemText = newItem.GetComponentInChildren<TextMeshProUGUI>();
                if (itemText != null)
                    itemText.text = song.songName;

                Button btn = newItem.GetComponent<Button>();
                if (btn != null)
                {
                    btn.onClick.AddListener(() => { PlaySong(song); });
                }
                currentSongItems.Add(newItem);
            }
        }

        // Force the layout to update
        LayoutRebuilder.ForceRebuildLayoutImmediate(songListContent.GetComponent<RectTransform>());

        // Make sure the scroll view starts at the top.
        ScrollRect scrollRect = songListPanel.GetComponentInChildren<ScrollRect>();
        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 1f;
        }
        songListPanel.SetActive(true);
    }

    public void HideSongList()
    {
        songListPanel.SetActive(false);
    }

    public void PlaySong(SongData song)
    {
        if (song != null && song.songClip != null && songAudioSource != null)
        {
            songAudioSource.Stop();
            songAudioSource.clip = song.songClip;
            songAudioSource.Play();
            Debug.Log("Playing song: " + song.songName);
        }
    }

    void UpdateAllTVs(string songTitle, Sprite albumCover)
    {
        List<GameObject> tvs = tvPlacementManager.GetAllPlacedTVs();

        foreach (GameObject tv in tvs)
        {
            TVDisplay tvDisplay = tv.GetComponent<TVDisplay>();
            if (tvDisplay != null)
            {
                tvDisplay.UpdateDisplay(songTitle, albumCover);
            }
        }
    }
}
