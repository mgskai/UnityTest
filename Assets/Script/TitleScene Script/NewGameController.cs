using UnityEngine;
using System.Collections;

public class NewGameController : MonoBehaviour 
{
    void Start()
    {
        renderer.material.color = Color.blue;
    }
    void OnMouseEnter()
    {
        renderer.material.color = Color.red;
    }

    void OnMouseDown()
    {
        Application.LoadLevel("gameScene");
    }

    void OnMouseExit()
    {
        renderer.material.color = Color.blue;
    }
}
