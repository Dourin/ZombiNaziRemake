
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class ReceiveDamage : MonoBehaviour
    {
    
    //Maximum de points de vie
    public int maxHitPoint = 5;
    
    //Points de vie actuels
    public int hitPoint = 0;

    
    
    private void Start()
    {
        //Au début : Points de vie actuels = Maximum de points de vie
        hitPoint = maxHitPoint;

    }
    
    private void Update()
    {

        
        
        
       
    }
    
    //Permet de recevoir des dommages
    public void GetDamage(int damage)
    {
        
        
        //Applique les dommages aux points de vies actuels
        hitPoint -= damage;
        
        //S'il reste des points de vie
        if (hitPoint > 0)
        {
            //SendMessage appellera toutes les méthodes "TakeDamage" de ce GameObject
            //Exemple : "TakeDamage" est dans MonsterController
            gameObject.SendMessage("TakeDamage", SendMessageOptions.DontRequireReceiver);
        }
        //Sinon
        else
        {
            //SendMessage appellera toutes les méthodes "Defeated" de ce GameObject
            //Exemple : "Defeated" est dans MonsterController
            gameObject.SendMessage("Defeated", SendMessageOptions.DontRequireReceiver);
        }
    }
}
