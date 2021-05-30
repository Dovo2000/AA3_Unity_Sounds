using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSound : MonoBehaviour
{

        public AudioSource source;

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
            StartCoroutine(RandomPlayBack(minWaitBetweenPlays, maxWaitBetweenPlays, waitTimeCountdown, pitchMin, pitchMax, volMin, volMax, source));
        }

        static IEnumerator RandomPlayBack(float minWaitBetweenPlays, float maxWaitBetweenPlays, float waitTimeCountdown, float pitchMin, float pitchMax, float volMin, float volMax, AudioSource source)
        {
            while (true)
            {
                waitTimeCountdown = Random.Range(minWaitBetweenPlays, maxWaitBetweenPlays);
                source.pitch = Random.Range(pitchMin, pitchMax);
                source.volume = Random.Range(volMin, volMax);
                source.Play();
                yield return new WaitForSeconds(waitTimeCountdown);
            }
        }

}
