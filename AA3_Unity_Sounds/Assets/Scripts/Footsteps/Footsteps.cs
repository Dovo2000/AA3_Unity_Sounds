using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    [SerializeField] private AudioClip[] concreteClips;
    [SerializeField] private AudioClip[] grassClips;
    [SerializeField] private AudioClip[] dirtClips;
    [SerializeField] private AudioClip[] woodClips;

    private AudioSource audioSource;

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
        audioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomClip()
    {
        int terrainTextureIndex = terrainDetector.GetActiveTerrainTextureIdx(transform.position);

        switch (terrainTextureIndex)
        {
            case 0:
                return concreteClips.Length > 0 ? concreteClips[Random.Range(0, concreteClips.Length)] : null;
            case 1:
                return dirtClips.Length > 0 ? dirtClips[Random.Range(0, dirtClips.Length)] : null;
            case 2:
                return woodClips.Length > 0 ? woodClips[Random.Range(0, woodClips.Length)] : null;
            default:
                return grassClips.Length > 0 ? grassClips[Random.Range(0, grassClips.Length)] : null;
        }
    }
}
