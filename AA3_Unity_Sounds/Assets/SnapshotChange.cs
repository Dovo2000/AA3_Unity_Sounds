using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapshotChange : MonoBehaviour
{

    FMOD.Studio.EventInstance reverbSnap;
    // Start is called before the first frame update
    void Start()
    {
        reverbSnap = FMODUnity.RuntimeManager.CreateInstance("snapshot:/ReverbChurch");
    }


    private void OnTriggerEnter(Collider other)
    {
        reverbSnap.start();
    }

    private void OnTriggerExit(Collider other)
    {
        reverbSnap.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
