using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigo : MonoBehaviour
{
    private Vector3 target;
    private float speed = 1;
    private Vector3 positionOld = new Vector3();
    public GameObject jogador;
    private static bool primeiro = true;
    private static AudioSource somMata;
    private static AudioSource somMorre;


    // Start is called before the first frame update
    void Start()
    {
        //target = new Vector3(Random.Range(-20,20), 0, Random.Range(-20,20));

        somMata = GetComponents<AudioSource>()[1];
        somMorre = GetComponents<AudioSource>()[0];

        if(primeiro){
            primeiro = false;
            for (int i=0; i<10; i++){
                Vector3 posicao = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20,20));
                GameObject outro = Instantiate(gameObject, posicao, transform.rotation) as GameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        target = jogador.transform.position;

        positionOld = transform.position;

        float dist = Vector3.Distance (transform.position, target);
        if (dist <= 0.1){
           // target = new Vector3 (Random.Range(-20, 20), 0, Random.Range(-20, 20));
        }
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        
    }

    void OnCollisionEnter(Collision collison){
        transform.position = positionOld;
         target = new Vector3 (Random.Range(-20, 20), 0, Random.Range(-20, 20));

         if(collison.gameObject.CompareTag("jogador")){
             somMata.Play();
             Controller.vidas--;
         }
    }
    
    void OnTriggerEnter(Collider other){
      if (other.gameObject.CompareTag("tiro")){
          somMorre.Play();
          Destroy(gameObject);
      }
    }

}
