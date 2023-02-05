using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WesleyDavies;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] _patrolTurns;
    [SerializeField] private AudioClip _touchFolder;
    [SerializeField] private AudioClip _leaveFolder;
    [SerializeField] private AudioClip _fillFolder;
    [SerializeField] private AudioClip _unfillFolder;
    [SerializeField] private AudioClip _fillSuccess;
    [SerializeField] private AudioClip _firewallAppears;
    [SerializeField] private AudioClip _cureAppears;
    [SerializeField] private AudioClip _cured;
    [SerializeField] private AudioClip _firewallSpread;
    [SerializeField] private AudioClip _firewallRetract;
    [SerializeField] private AudioClip _exclam;

    [SerializeField] private AudioSource _audioSource1;
    [SerializeField] private AudioSource _audioSource2;
    [SerializeField] private AudioSource _audioSource3;
    [SerializeField] private AudioSource _audioSource4;

    [System.NonSerialized] public bool isExclam = false;
    private bool _exclamWasPlayed = false;


    //[SerializeField] private AudioSource[] _firewallAudioSources;

    private void Awake()
    {
        Services.AudioManager = this;
        _audioSource1 = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Services.FolderManager.onFolderFill += PlayFillSuccess;
    }

    private void Update()
    {
        if (isExclam && !_exclamWasPlayed)
        {
            PlayExclam();
            _exclamWasPlayed = true;
        }
        if (!isExclam && _exclamWasPlayed)
        {
            _exclamWasPlayed = false;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _audioSource3.PlayOneShot(_patrolTurns[0]);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _audioSource3.PlayOneShot(_patrolTurns[1]);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _audioSource3.PlayOneShot(_patrolTurns[2]);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            _audioSource3.PlayOneShot(_patrolTurns[3]);
        }
    }

    private void OnDestroy()
    {
        Services.FolderManager.onFolderFill -= PlayFillSuccess;
    }

    //public void PlayPatrolTurn()
    //{
    //    AudioClip randomClip = _patrolTurns.PickRandom();
    //    _audioSource3.PlayOneShot(randomClip);
    //}

    public void PlayTouchFolder()
    {
        _audioSource1.clip = _touchFolder;
        _audioSource1.Play();
    }   

    public void PlayLeaveFolder()
    {
        _audioSource1.clip = _leaveFolder;
        _audioSource1.Play();
    }

    public void PlayFillFolder()
    {
        _audioSource2.clip = _fillFolder;
        _audioSource2.Play();
    }

    public void PlayUnfillFolder()
    {
        _audioSource2.Stop();
        //_audioSource2.clip = _unfillFolder;
        //_audioSource2.Play();
    }

    public void PlayFillSuccess(Folder folder)
    {
        _audioSource2.Stop();
        _audioSource1.PlayOneShot(_fillSuccess);
    }

    public void PlayFirewallSpawn()
    {
        _audioSource1.PlayOneShot(_firewallAppears);
        PlayCureSpawn();
    }

    public void PlayCureSpawn()
    {
        _audioSource1.PlayOneShot(_cureAppears);
    }

    public void PlayCured()
    {
        _audioSource1.PlayOneShot(_cured);
    }

    public void PlayFirewallExpand(Firewall firewall)
    {
        firewall.AudioSource.Stop();
        firewall.AudioSource.clip = _firewallSpread;
        firewall.AudioSource.Play();
    }

    public void PlayFirewallRetract(Firewall firewall)
    {
        firewall.AudioSource.Stop();
        firewall.AudioSource.clip = _firewallRetract;
        firewall.AudioSource.Play();
    }

    public void PlayExclam()
    {
        _audioSource1.PlayOneShot(_exclam);
    }
}
