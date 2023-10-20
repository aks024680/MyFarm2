using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorAbandon : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.tag);
        if (collision.tag == "animal") {
            
            collision.gameObject.GetComponent<chickenController>().isTouchDoor = true;
            print("动物进:" + collision.gameObject.GetComponent<chickenController>().isTouchDoor);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "animal")
        {
            
            collision.gameObject.GetComponent<chickenController>().isTouchDoor = false;
            print("动物出:" + collision.gameObject.GetComponent<chickenController>().isTouchDoor);
        }
    }


}
