using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        renderer.material.color = Color.white;
    }
    void OnMouseEnter()
    {
        renderer.material.color = Color.cyan;
    }

    void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }
    
    // Update is called once per frame
    void Update () {
    
    }
}
