using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] private AudioClip[] concreteClips;
    [SerializeField] private AudioClip[] grassClips;
    [SerializeField] private AudioClip[] dirtClips;
    [SerializeField] private AudioClip[] woodClips;

    private AudioSource audioSource;

    public float minVolume = 0.8f;
    public float maxVolume = 1.0f;

    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;

    private TerrainDetector terrainDetector;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        terrainDetector = new TerrainDetector();
    }

    //Step is an event from the Animation itself, everytime the animation fires "Step" , a Clip gets played
    private void Step()
    {
        AudioClip clip = GetRandomClip();
        if(clip!=null)
        {
            audioSource.clip = clip;
            audioSource.volume = Random.Range(minVolume, maxVolume);
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.Play();
        }
    }

    private AudioClip GetRandomClip()
    {
        int terrainTextureIndex = terrainDetector.GetActiveTerrainTextureIdx(transform.position);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            switch (terrainTextureIndex)
            {
                case 0:
                    return grassClips.Length > 0 ? grassClips[Random.Range(0, grassClips.Length)] : null;
                case 1:
                    return dirtClips.Length > 0 ? dirtClips[Random.Range(0, dirtClips.Length)] : null;
                case 2:
                    return concreteClips.Length > 0 ? concreteClips[Random.Range(0, concreteClips.Length)] : null;
                default:
                    return woodClips.Length > 0 ? woodClips[Random.Range(0, woodClips.Length)] : null;
            }
        }
        else return null;
    }
    private void StopSteps()
    {
        audioSource.Stop();
    }
}
