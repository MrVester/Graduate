using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSrc;

    public static AudioController current;
    void Awake()
    {
        current = this;
    }
    [SerializeField]private AudioSource robotBallHitSource;
    [SerializeField]private AudioSource robotBallRollingSource;
    [SerializeField]private AudioSource whipAttackSource;
    [SerializeField]private AudioSource hitSource;
    [SerializeField]private AudioSource playerStepsSource;
    [SerializeField]private AudioSource growSource;
    [SerializeField]private AudioSource robotBallExplosionSource;

    public void PlayGrowSound()
    {
        SetnPlaySound(growSource);
    }
    public void PlayRobotBallExplosionSound()
    {
        SetnPlaySound(robotBallExplosionSource);
    }
    public void PlayRobotBallHitSound()
    {
        SetnPlaySound(robotBallHitSource);
    }

    public void PlayRobotBallRollingSound()
    {
        SetnPlaySound(robotBallRollingSource);
    }
    public void StopRobotBallRollingSound()
    {
      robotBallRollingSource.Stop();
    }
    public void PlayWhipAttackSound()
    {
        SetnPlaySound(whipAttackSource);
    }
    public void PlayHitSound()
    {
        SetnPlaySound(hitSource);
    }
    public void PlayPlayerStepsSound()
    {
        SetnPlaySound(playerStepsSource);
    }
    public void StopPlayerStepsSound()
    {
        playerStepsSource.Stop();
    }
    public bool isPlayerStepsPlaying()
    {
        return playerStepsSource.isPlaying;
    }
    private void SetnPlaySound(AudioSource audioSource)
    {
        audioSource.volume = JSONSave.GetFloat("SaveVolume");
        audioSource.Play();
    }

}
