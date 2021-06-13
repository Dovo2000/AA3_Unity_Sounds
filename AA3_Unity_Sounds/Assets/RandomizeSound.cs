using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
public class RandomizeSound : MonoBehaviour
{

        public float minWaitBetweenPlays = 2.0f;
        public float maxWaitBetweenPlays = 8.0f;
        public float waitTimeCountdown = 0.0f;

        void Start()
        {
            StartCoroutine(RandomPlayBack(minWaitBetweenPlays, maxWaitBetweenPlays, waitTimeCountdown));
        }

        static IEnumerator RandomPlayBack(float minWaitBetweenPlays, float maxWaitBetweenPlays, float waitTimeCountdown)
        {
        while (true)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Sound FX/3D FX/CreakWoodShip");
            Debug.Log("se deberia reproducir"); 
            waitTimeCountdown = Random.Range(minWaitBetweenPlays, maxWaitBetweenPlays);
            yield return new WaitForSeconds(waitTimeCountdown);
        }

        }

}
