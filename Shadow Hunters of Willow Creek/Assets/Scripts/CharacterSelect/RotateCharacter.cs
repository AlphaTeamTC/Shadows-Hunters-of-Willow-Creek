using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCharacter : MonoBehaviour
{
    public GameObject Fenrir;
    public GameObject Ambrose;
    public GameObject Navi;
    public GameObject Lucius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fenrir.transform.Rotate(Vector3.up * 10f * Time.deltaTime);
        Lucius.transform.Rotate(Vector3.up * 10f * Time.deltaTime);
        Navi.transform.Rotate(Vector3.up * 10f * Time.deltaTime);
        Ambrose.transform.Rotate(Vector3.up * 10f * Time.deltaTime);

    }
}
