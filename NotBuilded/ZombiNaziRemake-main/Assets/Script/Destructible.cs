using UnityEngine;

 
public class Destructible : MonoBehaviour
{
    public PlayerFPS PlayerFPS;

    public void Defeated()
    {   PlayerFPS = FindObjectOfType<PlayerFPS>();
        PlayerFPS.points+=100;
        Destroy(gameObject);

    }
}