using UnityEngine;

public class EnemyAi : MonoBehaviour
{ 
    public GameObject player, bullet;
    public Transform proplers, firePoint;
    public float rotationSpeed, maxRotationSpeed, speed, maxSpeed, 
    avgSpeed, accelration, distance, maxDistance, yawAngle, rollAngle, 
    pitchAngle,  maxShootDistance, bulletSpeed, currTime, fireDelay;
    Vector3 direction;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        distance = Vector3.Distance(transform.position, player.transform.position);

        Vector3 yawError = new Vector3(direction.x, 0, direction.z);
        Vector3 pitchError = new Vector3(0, direction.y, direction.z);

        pitchAngle = Vector3.SignedAngle(transform.forward, pitchError, transform.right);
        yawAngle = Vector3.SignedAngle(transform.forward, yawError, transform.up);
        rollAngle = Mathf.Clamp(yawAngle, -45, 45);
        pitchAngle = Mathf.Clamp(pitchAngle, -30, 30);


        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            Quaternion.Euler(pitchAngle, transform.eulerAngles.y + yawAngle, -rollAngle),
            Time.deltaTime * rotationSpeed
        );
        if(distance > 100){
            FollowPlayer();
        }else{
            Evade();
        }

    }

    void FollowPlayer(){
        direction = player.transform.localPosition - transform.position;   
    }
    void Evade(){
        direction = transform.position - player.transform.localPosition;
    }



    // void Shoot(){
    //     if(currTime < fireDelay) return;
    //     GameObject instantiatedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
    //     Rigidbody bulletRb = instantiatedBullet.GetComponent<Rigidbody>();
    //     bulletRb.velocity = firePoint.forward * bulletSpeed;
    //     currTime = 0;
    // }
}
