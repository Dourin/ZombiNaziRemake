using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;





public class MainMenu : MonoBehaviour
{
    //Make sure to attach these Buttons in the Inspector

    public void Jouer() {  
        SceneManager.LoadScene("game_scene");  
    }

    public void Quitter() {  
        Application.Quit();
    }

}