using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    //Transforms
    public Transform firePointL, firePointR;
    //Bullet prefeb
    public GameObject bulletPrefeb;
    //Shooting
    public int clip;
    public float fireRate, bulletSpeed, clipReloadTime, clipWaitTime;
    private float currTime = 1f;
    public float delayTime;
    //Particles
    public ParticleSystem muzzleFlashL, muzzleFlashR;

    void Update(){
        Shoot();
        Clip();
    }

    void Shoot(){
        
        if(clip <= 0){
            muzzleFlashL.Stop();
            muzzleFlashR.Stop();
        }
        currTime += Time.deltaTime;
        if(Input.GetButton("Fire1")){
            if(clip <= 0) return;
            if(currTime * fireRate > delayTime){
                GameObject bulletL = Instantiate(bulletPrefeb, firePointL.position, firePointL.rotation);
                GameObject bulletR = Instantiate(bulletPrefeb, firePointR.position, firePointR.rotation);
                Rigidbody bulletLrb = bulletL.GetComponent<Rigidbody>();
                Rigidbody bulletRrb = bulletR.GetComponent<Rigidbody>();
                bulletLrb.velocity = bulletL.transform.forward * bulletSpeed;
                bulletRrb.velocity = bulletR.transform.forward * bulletSpeed;
                clip -= 2;
                currTime = 0;
                //Particles
                if(muzzleFlashL.isPlaying && muzzleFlashR.isPlaying) return;
                muzzleFlashL.Play();
                muzzleFlashR.Play();
            }
        }else if(Input.GetButtonUp("Fire1")){
            muzzleFlashL.Stop();
            muzzleFlashR.Stop();
        }
    }

    void Clip(){
        if(clip <= 0){
            clipWaitTime += Time.deltaTime;
            if(clipWaitTime > clipReloadTime){
                clip = 50;
                clipWaitTime = 0;
            }
        }
        
    }

}
