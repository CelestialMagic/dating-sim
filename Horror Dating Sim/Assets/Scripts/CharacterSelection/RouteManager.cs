using UnityEngine;
using UnityEngine.SceneManagement; 

public class RouteManager : MonoBehaviour
{
    private PlayerData playerData;

    private static string routeToFollow; 

    [SerializeField]
    private GameObject startRouteButton;

    [SerializeField]
    private string benRouteName, janeRouteName; 



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerData = FindObjectOfType<PlayerData>(); 
        routeToFollow = ""; 
        startRouteButton.SetActive(false);
    }
    public static void SetRouteStart(string routeName){
        routeToFollow = routeName; 
        
    }

    private void Update(){
        if(routeToFollow != "")
            startRouteButton.SetActive(true);
    }



    public void LoadRouteStart(){
        switch(routeToFollow){
            case "Ben":
            SceneManager.LoadScene(benRouteName);

            break;

            case "Jane":
            SceneManager.LoadScene(janeRouteName);
             break;

             default:
             break;

        }
        
    }

    
}
