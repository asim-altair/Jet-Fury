using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Update(){
        Destroy(gameObject, 4);
    }

    void OnCollisionEnter(Collision col){
        Destroy(gameObject);
    }
}
