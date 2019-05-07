using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class NetworkManager : MonoBehaviour
{
    string Name,GuessNumber;
    static SocketIOComponent socket;
    public InputField playerNameInput;
    public InputField playerGuessNumber;
    public Text resultText;

    // Start is called before the first frame update
    void Start()
    {
        socket = GetComponent<SocketIOComponent>();
        socket.On("ans", OnResult);
        socket.On("much", ToMuch);
        socket.On("less",LessThan);
       
    }
    public void ToMuch(SocketIOEvent e){
        resultText.text =" Guess too much";
    }
    public void LessThan(SocketIOEvent e){
        resultText.text = "Guess less";
    }
    public void OnResult(SocketIOEvent e)
    {
        resultText.text = e.data.ToString();
        print(e.data.ToString());
    }

    public void GuessButtonClick(){
        JSONObject jsonObj = new JSONObject(NameAndNumber());
        socket.Emit("guess",jsonObj);
    }

    string NameAndNumber(){
        Name = playerNameInput.text;
        GuessNumber = playerGuessNumber.text;
        string jsonString = string.Format(@"{{""name"":""{0}"",""gnumber"":""{1}""}}",Name,GuessNumber); 
        return jsonString;
    }
}
