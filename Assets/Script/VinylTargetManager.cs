using UnityEngine;
using Vuforia;

public class VinylTargetManager : MonoBehaviour
{
    private ObserverBehaviour observerBehaviour;
    public VinylInfo vinylInfo; // Data asset for this vinyl
    private bool targetTracked = false; // To track state change

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
    }

    void Update()
    {
        // Ensure the observerBehaviour is assigned
        if (observerBehaviour != null)
        {
            // Check the current target status
            TargetStatus status = observerBehaviour.TargetStatus;
            bool isTracked = (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED);

            // If we've just started tracking
            if (isTracked && !targetTracked)
            {
                targetTracked = true;
                // Show the song list for this vinyl
                SongListManager.Instance.ShowSongsForVinyl(vinylInfo);
            }
            // If the target is lost
            else if (!isTracked && targetTracked)
            {
                targetTracked = false;
                // Hide the song list
                SongListManager.Instance.HideSongList();
            }
        }
    }
}
