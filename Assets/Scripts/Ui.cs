using UnityEngine;
using TMPro;

public class Ui : MonoBehaviour
{
    public TextMeshProUGUI messageUi;
    public string text;
    public float uiTime;

    void Update(){
        UiUpdate(text);
    }

    public void UiUpdate(string message){
        uiTime += Time.deltaTime;
        messageUi.text = message.ToString();
        Color c = messageUi.color;
        messageUi.color = c;
    }
}
