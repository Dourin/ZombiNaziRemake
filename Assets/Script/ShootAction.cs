using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class ShootAction : MonoBehaviour
{
    //Dommage que le Gun inflige
    public int gunDamage = 1;
 
    //Portée du tir
    public float weaponRange = 200f;
 
    //Force de l'impact du tir
    public float hitForce = 100f;
 
    //La caméra
    private Camera fpsCam;
 
    //Temps entre chaque tir (en secondes) 
    public float fireRate = 0.01f;
 
    //Float : mémorise le temps du prochain tir possible
    private float nextFire;
 
    //Détermine sur quel Layer on peut tirer
    public LayerMask layerMask;

    // Effets sonores des armes
    public AudioSource gunAS;
    
    // Effet sonore : tir de l'AK
    public AudioClip shootAC;

    // Effet visuel : tir de l'AK
    public ParticleSystem muzzleflash;

    // Boucle pour jouer l'effet visuel une seule fois
    IEnumerator WeaponEffects()
    {
        //commence l'effet visuel
        muzzleflash.Play();

        // Attend
        yield return new WaitForSeconds(0.2f);

        // Termine l'effet visuel
        muzzleflash.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //Référence de la caméra. GetComponentInParent<Camera> permet de chercher une Camera
        //dans ce GameObject et dans ses parents.
        fpsCam = GetComponentInParent<Camera>();

        
        // Reference des effets sonores des armes
        gunAS = GetComponent<AudioSource>();
        
    }
 
    // Update is called once per frame
    void Update()
    {
        // Vérifie si le joueur a pressé le bouton pour faire feu, rester appuyé permet de tirer de manière automatique (ex:bouton gauche souris)
        // Time.time > nextFire : vérifie si suffisament de temps s'est écoulé pour pouvoir tirer à nouveau
        // SceneManager.GetActiveScene().name == "game_scene" : vérifie si le joueur est dans la scene du jeu (permet d'éviter que l'arme tire dans le menu principal)
        if (Input.GetButton("Fire1") && Time.time > nextFire && SceneManager.GetActiveScene().name == "game_scene")
        {
            //Nouveau tir
 
            //Met à jour le temps pour le prochain tir
            //Time.time = Temps écoulé depuis le lancement du jeu
            //temps du prochain tir = temps total écoulé + temps qu'il faut attendre
            nextFire = Time.time + fireRate;
 
            print(nextFire);
            
            // Joue une fois l'effet sonore attribué à shootAC
            gunAS.PlayOneShot(shootAC);

            // Appel la boucle qui joue une fois l'effet visuel de l'arme
            StartCoroutine(WeaponEffects());


            //On va lancer un rayon invisible qui simulera les balles du gun

            //Crée un vecteur au centre de la vue de la caméra
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
 
            //RaycastHit : permet de savoir ce que le rayon a touché
            RaycastHit hit;
 
             
            // Vérifie si le raycast a touché quelque chose
            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange, layerMask))
            {
                print("Target");
 
                // Vérifie si la cible a un RigidBody attaché
                if (hit.rigidbody != null)
                {
 
                    //AddForce = Ajoute Force = Pousse le RigidBody avec la force de l'impact
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
 
                    //S'assure que la cible touchée a un composant ReceiveAction
                    if (hit.collider.gameObject.GetComponent<ReceiveDamage>() != null)
                    {
                        //Envoie les dommages à la cible
                        hit.collider.gameObject.GetComponent<ReceiveDamage>().GetDamage(gunDamage);
                    }
                }
            }
        }
    }
}