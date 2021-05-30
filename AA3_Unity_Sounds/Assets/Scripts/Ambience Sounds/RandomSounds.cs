using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSounds : MonoBehaviour
{
    private AudioSource source;
    public List<AudioClip> audioClips;
   
    public float minWaitBetweenPlays = 2.0f;
    public float maxWaitBetweenPlays = 8.0f;
    public float waitTimeCountdown = 0.0f;


    public float pitchMin = 0.8f;
    public float pitchMax = 1.2f;

    public float volMin = 0.1f;
    public float volMax = 0.5f;



    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(RandomPlayBack(minWaitBetweenPlays, maxWaitBetweenPlays, waitTimeCountdown, pitchMin, pitchMax, volMin, volMax, source, audioClips));
    }

    static IEnumerator RandomPlayBack(float minWaitBetweenPlays, float maxWaitBetweenPlays, float waitTimeCountdown, float pitchMin, float pitchMax, float volMin, float volMax, AudioSource source, List<AudioClip> audioClips)
    {
        while (true)
        {
            waitTimeCountdown = Random.Range(minWaitBetweenPlays, maxWaitBetweenPlays);
            source.clip = audioClips[Random.Range(0, audioClips.Count)];
            source.pitch = Random.Range(pitchMin, pitchMax);
            source.volume = Random.Range(volMin, volMax);
            source.panStereo = Random.Range(-1.0f, 1.0f);
            source.Play();
            yield return new WaitForSeconds(waitTimeCountdown);
        }
    }

    void Update()
    {
    }
}
