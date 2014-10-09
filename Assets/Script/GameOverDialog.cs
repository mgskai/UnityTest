using UnityEngine;
using System.Collections;

public class GameOverDialog : MonoBehaviour {

    public GUISkin customSkin;
    public Texture backgroudTexture;

    void OnGUI()
    {
        GUI.skin = customSkin;
        GUI.Box(
            new Rect(Screen.width/2 - 50, Screen.height/2 - 45, 100, 90), "Game Over!");
        GUI.DrawTexture(
            new Rect(Screen.width / 2 - 50, Screen.height / 2 - 45, 100, 90), backgroudTexture);

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
