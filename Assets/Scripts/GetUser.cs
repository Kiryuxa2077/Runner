using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetUser : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public InputField _inputName;
    [SerializeField] private Text _textScore; 

    void Start()
    {
        _inputName.onValueChanged.AddListener(SubmitName);

        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(OnClickPlay);

        //var s = StartCoroutine(SendRequest("http://192.168.0.108:7500/api/User/Kirill"));
    }

    private void SubmitName(string arg0)
    {
        Debug.Log(arg0);
    }

    void OnClickPlay()
    {
        StartCoroutine(SendRequest($"http://192.168.0.108:7500/api/User/{_inputName.text}"));
    }

    IEnumerator SendRequest(string url)
    {

        using UnityWebRequest request = UnityWebRequest.Get(url);
        
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(string.Format("Error: {0}", request.error));
        }

        else
        {
            // Response can be accessed through: request.downloadHandler.text
            var response = JsonConvert.DeserializeObject<User>(request.downloadHandler.text);

            _textScore.text = $"Score: {response.Score}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Score { get; set; }
}