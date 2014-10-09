using UnityEngine;
using System.Collections;

public class GameWinDialog : MonoBehaviour {

    public GUISkin customSkin;
    public Texture backgroudTexture;

    void OnGUI()
    {
        GUI.skin = customSkin;
        GUI.DrawTexture(
            new Rect(Screen.width / 2 - 50, Screen.height / 2 - 45, 100, 90), backgroudTexture);
        GUI.Box(
            new Rect(Screen.width / 2 - 50, Screen.height / 2 - 45, 100, 90), "You win!");


        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 15, 80, 20), "Restart"))
        {
            Application.LoadLevel("gameScene");
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 15, 80, 20), "Exit"))
        {
            Application.LoadLevel("titleScene");
        }

    }
}
