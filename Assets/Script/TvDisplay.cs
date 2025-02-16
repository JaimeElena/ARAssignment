using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TVDisplay : MonoBehaviour
{
    public TextMeshProUGUI songTitleText; // Assign in Inspector
    public Image albumCoverImage; // Assign in Inspector
    public Sprite defaultAlbumCover; // Default image when no song plays

    void Start()
    {
        UpdateDisplay(null, null); // Set to default state on start
    }

    public void UpdateDisplay(string songTitle, Sprite albumCover)
    {
        if (!string.IsNullOrEmpty(songTitle))
        {
            songTitleText.text = songTitle;
            albumCoverImage.sprite = albumCover;
        }
        else
        {
            songTitleText.text = "No song currently playing";
            albumCoverImage.sprite = defaultAlbumCover;
        }
    }
}