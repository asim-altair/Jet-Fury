using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraScript : MonoBehaviour
{
    //Input 
    private float inputX, inputY, horizontal, vertical;
    //Camera speed
    public float forwardSpeed, cameraRotSpeed, increament, decreament, minSpeed, maxSpeed, minSpeedSta, maxSpeedSta;
    //Plane speed
    public float planeMoveSpeed, rollAmount, proplersRotation;
    //Transforms
    public Transform plane, cameraTarget, proplers;
    //Ui
    public TextMeshProUGUI altitudeUi, speedUi;
    //Particles
    public ParticleSystem speedEffect;

    void Update(){
        GetInput();
        CameraMovement();
        PlaneMovement();
        UiController();
    }

    void GetInput(){
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        inputX += horizontal * cameraRotSpeed * Time.deltaTime;
        inputY += vertical * cameraRotSpeed * Time.deltaTime;
    }

    void CameraMovement(){
        transform.localRotation = Quaternion.Euler(Vector3.right * inputY + Vector3.up * inputX);
        transform.position += transform.forward * forwardSpeed * Time.deltaTime;
    }

    void PlaneMovement(){
        plane.localPosition = Vector3.Lerp(plane.localPosition, cameraTarget.position, Time.deltaTime * planeMoveSpeed);
        Quaternion roll = Quaternion.Euler(0, 0, -horizontal * rollAmount);
        plane.rotation = cameraTarget.rotation * roll;
        proplers.transform.Rotate(0, 0, forwardSpeed * proplersRotation * Time.deltaTime);

        //increasing speed
        if(Input.GetKey(KeyCode.LeftShift)){
            if(forwardSpeed >= maxSpeed) return;
            forwardSpeed += increament * Time.deltaTime * 2;
        }else if(Input.GetKey(KeyCode.Space)){
            if(forwardSpeed <= minSpeed)return;
            forwardSpeed -= decreament * Time.deltaTime * 2;
        }
        if(forwardSpeed > maxSpeedSta){
            forwardSpeed = Mathf.MoveTowards(forwardSpeed, maxSpeedSta, Time.deltaTime);
        }else if(forwardSpeed < minSpeedSta){
            forwardSpeed = Mathf.MoveTowards(forwardSpeed, minSpeedSta, Time.deltaTime);
        }
        cameraTarget.localPosition = new Vector3(0, -4, 10 + forwardSpeed * 2);
        if(forwardSpeed > maxSpeedSta){
            speedEffect.Play();
        }else{
            speedEffect.Stop();
        }
    }
    void UiController(){
        altitudeUi.text = Mathf.CeilToInt(plane.position.y).ToString() + " M";
        speedUi.text = Mathf.CeilToInt(forwardSpeed * 100).ToString() + " Kmh";
    }
}
