using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class PlantationVideoScreen : MonoBehaviour
{

    //serialized variables
    [SerializeField]
    private VideoClip[] _videoClips;

    //private variables
    private VideoPlayer _videoPlayer;
    private AudioSource _audioSource;
    private bool _videoPlaying = false;

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _audioSource = GetComponentInChildren<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {

        if (!_audioSource)
        {
            Debug.Log("No audio source!");
        }
    }

    void Update()
    {
        if (_videoPlaying == false)
        {
            _videoPlayer.clip = _videoClips[0];
            _videoPlaying = true;
            StartCoroutine(PlayVideo());
        }
    }

    private IEnumerator PlayVideo()
    {
        _videoPlayer.source = VideoSource.VideoClip;
        _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        _videoPlayer.SetTargetAudioSource(0, _audioSource);
        _videoPlayer.Play();
        yield return new WaitForSeconds((float)_videoPlayer.clip.length);
        _videoPlaying = false;
    }
}
