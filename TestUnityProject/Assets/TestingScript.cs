using TimboJimbo.BetterOpenURL;
using UnityEngine;
using UnityEngine.UI;

public class TestingScript : MonoBehaviour
{
    public Button OpenURLButton;
    public string URL = "https://www.google.com";
    public BetterOpenURL BetterOpenURL;

    void Awake()
    {
        OpenURLButton.onClick.AddListener(() => {
            BetterOpenURL.Open(URL);
        });
    }
}
