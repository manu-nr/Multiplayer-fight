using Photon.Voice;
using Photon.Voice.Unity;
using UnityEngine;

public class MicTest : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Recorder _recorder;

    bool _isRecorderSet = false;

    void Start()
    {
        if(_recorder == null)
            _recorder = GetComponent<Recorder>();

        string[] devices = Microphone.devices;

        Debug.Log("nrm Current mic: " + devices[0]);
        _recorder.MicrophoneDevice = new DeviceInfo(devices[0]);
        //AudioSource audioSource = GetComponent<AudioSource>();
        _audioSource.clip = Microphone.Start(null, true, 3, 44100);
        _audioSource.loop = true;
        _audioSource.Play();
    }

    void Update()
    {
        
    }
}
