using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LPFOnCol : MonoBehaviour
{
    public AudioMixer audioMixer;

    const int MAX_FREQ = 22000;
    const int MIN_FREQ = 2001;
    const int DEFAULT_ADD_AMMOUNT = 2000;



    IEnumerator LPFCoroutine(string varName, int addAmmount, float delay = 0.1f)
    {
        float cutOffFreq;
        bool varFound = audioMixer.GetFloat(varName, out cutOffFreq);

        if (varFound && addAmmount != 0 && delay > 0)
        {
            if (addAmmount > 0)
            {
                while (cutOffFreq < MAX_FREQ)
                {
                    audioMixer.SetFloat(varName, cutOffFreq);
                    cutOffFreq += addAmmount;

                    yield return new WaitForSeconds(delay);
                }

                audioMixer.SetFloat(varName, MAX_FREQ);
            }
            else
            {
                while (cutOffFreq > MIN_FREQ)
                {
                    audioMixer.SetFloat(varName, cutOffFreq);
                    cutOffFreq += addAmmount;

                    yield return new WaitForSeconds(delay);
                }

                audioMixer.SetFloat(varName, MIN_FREQ);
            }

            //Debug.Log(varName + ": " + cutOffFreq);

        }
        else
        {
            Debug.LogWarning("Smth went wrong!");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("In");
            StartCoroutine(LPFCoroutine("sfxLPF", -DEFAULT_ADD_AMMOUNT));
            StartCoroutine(LPFCoroutine("musicLPF", -DEFAULT_ADD_AMMOUNT));

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Out");
            StartCoroutine(LPFCoroutine("sfxLPF", DEFAULT_ADD_AMMOUNT));
            StartCoroutine(LPFCoroutine("musicLPF", DEFAULT_ADD_AMMOUNT));

        }
    }

}
