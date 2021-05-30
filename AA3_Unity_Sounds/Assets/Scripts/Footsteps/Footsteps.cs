using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] private AudioClip[] concreteClips;
    [SerializeField] private AudioClip[] grassClips;
    [SerializeField] private AudioClip[] dirtClips;
    [SerializeField] private AudioClip[] woodClips;
    [SerializeField] private AudioClip[] jumpClips;
    [SerializeField] private AudioClip pistacho;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSourceAction;
    private AudioClip clip;

    public float minVolume = 0.8f;
    public float maxVolume = 1.0f;
    public float minVolumeJump = 0.7f;
    public float maxVolumeJump = 1.0f;

    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;
    public float minPitchJump = 0.7f;
    public float maxPitchJump = 1.3f;

    private bool jumping;
    private bool speaking;
    
    [SerializeField] private float totalCD;

    private TerrainDetector terrainDetector;
    private void Awake()
    {
        terrainDetector = new TerrainDetector();
        jumping = false;
        speaking = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            jumping = true;
            speaking = false;
            audioSourceAction.clip = jumpClips[Random.Range(0, jumpClips.Length)];
            audioSourceAction.volume = Random.Range(minVolumeJump, maxVolumeJump);
            audioSourceAction.pitch = Random.Range(minPitchJump, maxPitchJump);
            audioSourceAction.Play();
        }
        if (Input.GetKeyDown(KeyCode.E) && !speaking)
        {
            speaking = true;
            audioSourceAction.clip = pistacho;
            audioSourceAction.volume = Random.Range(minVolume, maxVolume);
            audioSourceAction.pitch = Random.Range(minPitch, maxPitch);
            audioSourceAction.Play();
            StartCoroutine(Speak());
        }
    }
    IEnumerator Speak()
    {
        yield return new WaitForSeconds(totalCD);
        speaking = false;
    }
    //Step is an event from the Animation itself, everytime the animation fires "Step" , a Clip gets played
    private void Step()
    {
        if(clip!=null && !jumping)
        {
            audioSource.clip = clip;
            audioSource.volume = Random.Range(minVolume, maxVolume);
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                audioSource.Play();
        }
    }

    private AudioClip GetRandomClip()
    {
        int terrainTextureIndex = terrainDetector.GetActiveTerrainTextureIdx(transform.position);
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

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag != "Terrain")
        {
            switch (hit.gameObject.tag)
            {
                case "Stone":
                    clip = concreteClips[Random.Range(0, dirtClips.Length)];
                    break;
                default:
                    clip = woodClips[Random.Range(0, woodClips.Length)];
                    break;
            }
            //activeSurfaceGroup = surfaceGroupArray[iSurface];
        }
        else
        {
            clip = GetRandomClip();
        }

        jumping = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
            clip = GetRandomClip();
    }
    private void StopSteps()
    {
        audioSource.Stop();
    }
}
