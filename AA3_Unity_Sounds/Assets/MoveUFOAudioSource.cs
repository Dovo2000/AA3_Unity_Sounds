using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUFOAudioSource : MonoBehaviour
{
    private GameObject audioSourceGO;
    private float maxRange;
    
    //private AudioSource audioSource;
    //private GameObject player;

    const int MARGIN = 5;

    // Start is called before the first frame update
    void Start()
    {
        audioSourceGO = transform.Find("AudioSourceGO").gameObject;
        maxRange = GetComponent<CapsuleCollider>().height;
        
        //audioSource = audioSourceGO.GetComponent<AudioSource>();
        //player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 startPosition = new Vector3(transform.position.x, transform.position.y - MARGIN, transform.position.z);
        //if (Physics.Raycast(startPosition, transform.TransformDirection(Vector3.down), out hit, maxRange))
        //{
        //    audioSourceGO.transform.position = hit.point;
        //    if(Vector3.Distance(player.transform.position, audioSourceGO.transform.position) < audioSource.maxDistance)
        //    {
        //        Debug.Log("Should play");
        //    }
        //    else
        //    {
        //        Debug.Log("Shouldn't play");
        //    }
        //}
    }
}
