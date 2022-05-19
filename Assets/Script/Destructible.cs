using UnityEngine;

 
public class Destructible : MonoBehaviour
{
    public void Defeated()
    {   
        Destroy(gameObject);
    }
}