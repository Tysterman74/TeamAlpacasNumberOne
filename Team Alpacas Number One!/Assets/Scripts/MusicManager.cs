using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{

    public AudioClip[] BGMPlaylist; //MUST BE ASSIGNED!
    private int currentBGM = 0; //the playlist should be a queue, but it's easier on non-programmers if we make it an array
    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void FixedUpdate()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            currentBGM += 1;
            if (currentBGM >= BGMPlaylist.Length) //we've reached the end of the array and need to return to the beginning
                currentBGM = 0;
            GetComponent<AudioSource>().clip = BGMPlaylist[currentBGM];
            GetComponent<AudioSource>().Play();
        }
    }
}
