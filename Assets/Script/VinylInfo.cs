using UnityEngine;

[CreateAssetMenu(fileName = "NewVinyl", menuName = "Vinyl/VinylInfo")]
public class VinylInfo : ScriptableObject
{
    public string vinylName;      // Name of the vinyl (optional).
    public SongData[] songs;      // Array of songs associated with this vinyl.
}