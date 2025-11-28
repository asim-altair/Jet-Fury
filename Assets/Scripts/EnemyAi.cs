using UnityEngine;
using UnityEngine.UI;

public class EnemyAi : MonoBehaviour
{ 
    public GameObject player, bullet, cam;
    public Transform proplers, firePoint;
    public float rotationSpeed, maxRotationSpeed, speed, maxSpeed, 
    avgSpeed, accelration, distance, maxDistance, yawAngle, rollAngle, 
    pitchAngle,  maxShootDistance, bulletSpeed, currTime, fireDelay;
    Vector3 direction;
    //LifeTime
    public float health;
    private bool dead = false;
    //Radar
    public Image enemyIcon;
    public Image radar;
    private Image enemyIconInRadar;
    //Health bar
    public Slider healthBar;

    void Start(){
        enemyIconInRadar = Instantiate(enemyIcon);
        enemyIconInRadar.transform.SetParent(radar.transform, false);
    }

    void Update()
    {
        Move();
        Radar();
        Die();
        currTime += Time.deltaTime;
    }

    void FollowPlayer(){
        direction = player.transform.localPosition - transform.position;  
    }
    void Evade(){
        direction = transform.position - player.transform.localPosition;
    }
    void Move(){
        if(dead) return;
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


    void Shoot(){
        if(currTime < fireDelay) return;
        GameObject instantiatedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        Rigidbody bulletRb = instantiatedBullet.GetComponent<Rigidbody>();
        Vector3 directionToPlayer = (player.transform.position - firePoint.position).normalized;
        bulletRb.velocity = directionToPlayer * bulletSpeed;
        currTime = 0;
    }

    void Radar(){
        if(dead)return;
        Vector3 enemyPositionRelativeToPlayer = cam.transform.InverseTransformPoint(transform.position);
        RectTransform rt = enemyIconInRadar.GetComponent<RectTransform>();
        Vector3 enemyIconPositionInRadar = new Vector3(enemyPositionRelativeToPlayer.x, enemyPositionRelativeToPlayer.z, 0) / 4;

        float radarImageRadius = radar.rectTransform.rect.width / 2;
        if(enemyIconPositionInRadar.magnitude > radarImageRadius){
            rt.localPosition = enemyIconPositionInRadar.normalized * radarImageRadius;
        }else{
            rt.localPosition = enemyIconPositionInRadar;
        }
    }

    void Die(){
        healthBar.value = health;
        healthBar.transform.LookAt(Camera.main.transform);
        healthBar.transform.Rotate(0, 180, 0);
        if(health <= 0){
            dead = true;
            transform.Rotate(30 * Time.deltaTime, 40 * Time.deltaTime, 0);
            transform.position += new Vector3(0, -20 * Time.deltaTime, 0);
            Destroy(gameObject, 20);
            Destroy(enemyIconInRadar);
        }
    }
    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "bullet"){
            health -= 5;
        }
    }
}
