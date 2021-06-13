using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    //[SerializeField] private AudioClip[] concreteClips;
    //[SerializeField] private AudioClip[] grassClips;
    //[SerializeField] private AudioClip[] dirtClips;
    //[SerializeField] private AudioClip[] woodClips;
    //[SerializeField] private AudioClip[] jumpClips;
    //[SerializeField] private AudioClip pistacho;

    //[SerializeField] private AudioSource audioSource;
    //[SerializeField] private AudioSource audioSourceAction;
    //private AudioClip clip;

    //public float minVolume = 0.8f;
    //public float maxVolume = 1.0f;
    //public float minVolumeJump = 0.7f;
    //public float maxVolumeJump = 1.0f;

    //public float minPitch = 0.9f;
    //public float maxPitch = 1.1f;
    //public float minPitchJump = 0.7f;
    //public float maxPitchJump = 1.3f;
    [FMODUnity.EventRef]
    public string fmodEventPathFS = "";
    [FMODUnity.EventRef]
    public string fmodEventPathJump = "";
    [FMODUnity.EventRef]
    public string fmodEventPathPistacho = "";

    private FMOD.Studio.EventInstance footstepsInstance;
    private FMOD.Studio.EventInstance jumpInstance;
    private FMOD.Studio.EventInstance pistachoInstance;
    private bool jumping;
    private bool speaking;

    private int iSurface = 0;
    
    [SerializeField] private float totalCD;

    private TerrainDetector terrainDetector;

    private void Awake()
    {
        terrainDetector = new TerrainDetector();
        jumping = false;
        speaking = false;

    }
    private void Start()
    {
        footstepsInstance = FMODUnity.RuntimeManager.CreateInstance(fmodEventPathFS);
        jumpInstance = FMODUnity.RuntimeManager.CreateInstance(fmodEventPathJump);
        pistachoInstance = FMODUnity.RuntimeManager.CreateInstance(fmodEventPathPistacho);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            jumping = true;
            speaking = false;
            //audioSourceAction.clip = jumpClips[Random.Range(0, jumpClips.Length)];
            //audioSourceAction.volume = Random.Range(minVolumeJump, maxVolumeJump);
            //audioSourceAction.pitch = Random.Range(minPitchJump, maxPitchJump);
            //audioSourceAction.Play();
            jumpInstance.start();
        }
        if (Input.GetKeyDown(KeyCode.E) && !speaking)
        {
            speaking = true;
            //audioSourceAction.clip = pistacho;
            //audioSourceAction.volume = Random.Range(minVolume, maxVolume);
            //audioSourceAction.pitch = Random.Range(minPitch, maxPitch);
            //audioSourceAction.Play();
            pistachoInstance.start();
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
        if(!jumping)
        {
            //audioSource.clip = clip;
            //audioSource.volume = Random.Range(minVolume, maxVolume);
            //audioSource.pitch = Random.Range(minPitch, maxPitch);
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                footstepsInstance.start();
                //audioSource.Play();
        }
    }

    private void GetRandomClip()
    {
        int terrainTextureIndex = terrainDetector.GetActiveTerrainTextureIdx(transform.position);
        switch (terrainTextureIndex)
        {
            case 0:
                iSurface = 1;
                break;
            case 1:
                iSurface = 0;
                break;
            case 2:
                iSurface = 2;
                break;
            default:
                iSurface = 3;
                break;
        }
    }

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag != "Terrain")
        {
            switch (hit.gameObject.tag)
            {
                case "Stone":
                    iSurface = 2;
                    break;
                default:
                    iSurface = 3;
                    break;
            }
            //activeSurfaceGroup = surfaceGroupArray[iSurface];
        }
        else
        {
            GetRandomClip();
        }
        footstepsInstance.setParameterByName("Parameter 1", iSurface);
        jumping = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
           GetRandomClip();
    }
    private void StopSteps()
    {
        // audioSource.Stop();
        //footstepsInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
