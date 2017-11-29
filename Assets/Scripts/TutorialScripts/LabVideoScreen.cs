using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class LabVideoScreen : MonoBehaviour {

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
    void Start () {

        if (!_audioSource)
        {
            Debug.Log("No audio source!");
        }
	}

    void Update()
    {
        //Tutorial stage 1
        if (TutorialManager.Instance.TutorialStage == 1 && _videoPlaying == false)
        {
            _videoPlayer.clip = _videoClips[0];
            StartCoroutine(PlayVideo());
            _videoPlaying = true;
        }

        //Tutorial stage 2
        if (TutorialManager.Instance.TutorialStage == 2 && _videoPlaying == false)
        {
            _videoPlayer.clip = _videoClips[1];
            StartCoroutine(PlayVideo());
            _videoPlaying = true;
        }

        //Tutorial stage 3
        if (TutorialManager.Instance.TutorialStage == 3 && _videoPlaying == false)
        {
            _videoPlayer.clip = _videoClips[2];
            StartCoroutine(PlayVideo());
            _videoPlaying = true;
        }

        //Tutorial stage 5
        if (TutorialManager.Instance.TutorialStage == 5 && _videoPlaying == false)
        {
            _videoPlayer.clip = _videoClips[3];
            StartCoroutine(PlayVideo());
            _videoPlaying = true;
        }

        //Tutorial stage 7
        if (TutorialManager.Instance.TutorialStage == 7 && _videoPlaying == false)
        {
            _videoPlayer.clip = _videoClips[4];
            StartCoroutine(PlayVideo());
            _videoPlaying = true;
        }
    }

    private IEnumerator PlayVideo()
    {
        _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        _videoPlayer.SetTargetAudioSource(0, _audioSource);
        _videoPlayer.source = VideoSource.VideoClip;
        _videoPlayer.Play();
        yield return new WaitForSeconds((float)_videoPlayer.clip.length);
        _videoPlaying = false;
        TutorialManager.Instance.TutorialStage = 1;
    }
}
