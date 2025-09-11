using System;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] GameObject pourStream;
    [SerializeField] GameObject fillMask;
    void Start()
    {
        
    }

    void Pour()
    {
        pourStream.SetActive(true);
        fillMask.transform.position += new Vector3(0, 0.01f, 0);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Pour();
            Console.WriteLine("sadfasf");
        }
        else
        {
            pourStream.SetActive(false);
        }
    }

    void StopPouring()
    {
       pourStream.SetActive(false);
    }
}
