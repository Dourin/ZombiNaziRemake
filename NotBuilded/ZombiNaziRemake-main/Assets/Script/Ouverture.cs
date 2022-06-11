using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ouverture : MonoBehaviour
{

    public PlayerFPS PlayerFPS;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        PlayerFPS = FindObjectOfType<PlayerFPS>();
    }
    void Update()
    {
        OnTrigerEnter();
    }

    void OnTrigerEnter(){
        if(Input.GetKeyDown(KeyCode.E) && PlayerFPS.points>=1000){
        animator.SetBool("Ouverture", true);
        PlayerFPS.points-=1000;
        }
    }

    
    
}
