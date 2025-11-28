using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{
    public Transform cam;
    public Image playerIcon;

    RectTransform rt;
    RectTransform playerIconRt;
    public RectTransform routerRt;

    void Start(){
        rt = GetComponent<RectTransform>();
        playerIconRt = playerIcon.GetComponent<RectTransform>();
    }

    void Update(){
       rt.localRotation = Quaternion.Euler(0, 0, cam.eulerAngles.y); 
       playerIconRt.localRotation = Quaternion.Euler(0, 0, -cam.eulerAngles.y); 
       routerRt.Rotate(0, 0, -100 * Time.deltaTime);
    }
}
