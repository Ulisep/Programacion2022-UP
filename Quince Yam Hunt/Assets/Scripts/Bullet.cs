using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{   
    public AudioSource impact;    

    void Start()
    {
       
    }
    
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        impact.Play();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "cubtata")
        {
            Debug.Log (other.gameObject.tag);
            Debug.Log ("padre "+other.transform.parent.name);
            Conter.IncrementarC1(other);
            
        }       

        if (other.gameObject.tag == "cubrillo")
        {
            Debug.Log(other.gameObject.tag);
            Debug.Log("padre " + other.transform.parent.name);
            Conter.IncrementarC2(other);            
        }        
    }
    
}
