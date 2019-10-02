using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CutSceneScript : MonoBehaviour
{
    public RawImage image;
    public VideoClip videoToPlay;
    private VideoPlayer videoplayer;
    private VideoSource videosoure;
    // private AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        Application.runInBackground = true;
        StartCoroutine(playVideo());
    }

    IEnumerator playVideo()
    {
        videoplayer = gameObject.AddComponent<VideoPlayer>();
        //audiosource = gameObject.GetComponent<AudioSource>();
        videoplayer.playOnAwake = false;
        //audiosource.playOnAwake = false;
        //audiosource.Pause();
        videoplayer.source = VideoSource.VideoClip;
        videoplayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoplayer.EnableAudioTrack(0, true);
        // videoplayer.SetTargetAudioSource(0, audiosource);
        videoplayer.clip = videoToPlay;
        videoplayer.Prepare();
        WaitForSeconds waitTime = new WaitForSeconds(1);
        while (!videoplayer.isPrepared)
        {
            Debug.Log("Preparing video");
            yield return waitTime;
            break;
        }
        Debug.Log("Done preparing video");
        image.texture = videoplayer.texture;
        videoplayer.Play();
        // audiosource.Play();
        Debug.Log("Playing video");
        while (videoplayer.isPlaying)
        {
            Debug.LogWarning("Video Time:" + Mathf.FloorToInt((float)videoplayer.time));
            yield return null;
        }
        Debug.Log("Done playing video");
    }
}
