using UnityEngine;
using System.Collections;

public class NewGameController : MonoBehaviour {

    // Use this for initialization
    void OnMouseDown()
    {
        Application.LoadLevel("gameScene");
    }
    
    // Update is called once per frame
    void Update () {
    
    }
}
