using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    //DestroyObjectÇ∆è’ìÀÇµÇΩObjectÇîjä¸Ç∑ÇÈ
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CarTag")
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "TrafficConeTag")
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "CoinTag")
        {
            Destroy(other.gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {

    }

}
