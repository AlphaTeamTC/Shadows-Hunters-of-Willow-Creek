using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCharacter : MonoBehaviour
{
    public GameObject Fenrir;
    public GameObject Ambrose;
    public GameObject Navi;
    public GameObject Lucius;
    
    public float speed = 20f;

    // Update is called once per frame
    void Update()
    {
        Fenrir.transform.Rotate(Vector3.up * speed * Time.deltaTime);
        Lucius.transform.Rotate(Vector3.up * speed * Time.deltaTime);
        Navi.transform.Rotate(Vector3.up * speed * Time.deltaTime);
        Ambrose.transform.Rotate(Vector3.up * speed * Time.deltaTime);

    }
}
