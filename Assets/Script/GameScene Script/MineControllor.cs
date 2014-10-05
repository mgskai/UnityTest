using UnityEngine;
using System.Collections;

public class MineControllor : MonoBehaviour {
    
    public TextMesh mineText;
    
    private bool _IsMine;
    public bool IsMine
    {
        get
        {
            return _IsMine;
        }
        set
        {
            _IsMine = value;
        }
    }

    
    void start()
    {
        renderer.material.color = Color.green;
        mineText.text = "2";
    }

    void OnMouseEnter()
    {
        renderer.material.color = Color.cyan;
    }

    void OnMouseExit()
    {
        renderer.material.color = Color.green;
    }

    void OnMouseUp()
    {
        if (mineText.text == "3")
        {
            mineText.text = "2";
        } 
        else
        {
            mineText.text = "3";
        }
    }
}
