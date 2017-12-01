using System.Collections;
using UnityEngine.Video;
using UnityEngine;

public class LabVideoScreen : MonoBehaviour {

    //serialized variables
    [SerializeField]
    private VideoClip[] _videoClips;

    //private variables
    private VideoPlayer _videoPlayer;
    private AudioSource _audioSource;
    private bool _videoPlaying = false;

    //locate components in VideoScreen object and children
    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        _audioSource = GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        //load and play video resource in VideoSceen -> VideoPlayer array based on tutorial stage
        //Tutorial stage 0
        if (TutorialManager.Instance.TutorialStage == 0)
        {
            _videoPlayer.clip = _videoClips[5];
            _videoPlayer.source = VideoSource.VideoClip;
            _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            _videoPlayer.SetTargetAudioSource(0, _audioSource);
            _videoPlayer.Play();

        }

        //Tutorial stage 1
        if (TutorialManager.Instance.TutorialStage == 1 && _videoPlaying == false)
        {
            _videoPlayer.clip = _videoClips[0];
            _videoPlaying = true;
            StartCoroutine(PlayVideo());
        }

        //Tutorial stage 2
        if (TutorialManager.Instance.TutorialStage == 2 && _videoPlaying == false)
        {
            _videoPlayer.clip = _videoClips[1];
            _videoPlaying = true;
            StartCoroutine(PlayVideo());
        }

        //Tutorial stage 3
        if (TutorialManager.Instance.TutorialStage == 3 && _videoPlaying == false)
        {
            _videoPlayer.clip = _videoClips[2];
            _videoPlaying = true;
            StartCoroutine(PlayVideo());
        }

        //Tutorial stage 5
        if (TutorialManager.Instance.TutorialStage == 5 && _videoPlaying == false)
        {
            _videoPlayer.clip = _videoClips[3];
            _videoPlaying = true;
            StartCoroutine(PlayVideo());
        }

        //Tutorial stage 7
        if (TutorialManager.Instance.TutorialStage == 7 && _videoPlaying == false)
        {
            _videoPlayer.clip = _videoClips[4];
            _videoPlaying = true;
            StartCoroutine(PlayVideo());
        }

        //Tutorial stage 8
        if (TutorialManager.Instance.TutorialStage == 8 && _videoPlaying == false)
        {
            _videoPlayer.clip = _videoClips[5];
            _videoPlaying = true;
            StartCoroutine(PlayVideo());
        }
    }

    private IEnumerator PlayVideo()
    {
        //play video and wait for length of video to advance tutorial stage
        _videoPlayer.source = VideoSource.VideoClip;
        _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        _videoPlayer.SetTargetAudioSource(0, _audioSource);
        _videoPlayer.Play();
        yield return new WaitForSeconds((float)_videoPlayer.clip.length);
        _videoPlaying = false;
        TutorialManager.Instance.TutorialStage = 1;
    }
}
