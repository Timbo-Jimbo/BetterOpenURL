using TimboJimbo.BetterOpenURL;
using UnityEngine;
using UnityEngine.UI;

public class TestingScript : MonoBehaviour
{
    public Button OpenURLButton;
    public Button StartAuthenticationButton;
    public string URL = "https://www.google.com";
    public string ExampleAuthURL = "https://practicetestautomation.com/practice-test-login/";
    public BetterOpenURL BetterOpenURL;

    void Awake()
    {
        OpenURLButton.onClick.AddListener(() => {
            BetterOpenURL.Open(URL);
        });
        
        StartAuthenticationButton.onClick.AddListener(() => {
            BetterOpenURL.StartAuthentication(ExampleAuthURL);
        });
        
        Application.deepLinkActivated += (url) => {
            Debug.Log($"Deep link activated: {url}");
        };
    }
}
