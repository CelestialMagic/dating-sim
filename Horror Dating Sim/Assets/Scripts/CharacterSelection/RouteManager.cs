using UnityEngine;
using UnityEngine.SceneManagement; 

public class RouteManager : MonoBehaviour
{
    private PlayerData playerData;

    private static CharacterSelect.CharacterName routeToFollow; 

    [SerializeField]
    private GameObject startRouteButton;

    [SerializeField]
    private string benRouteName, janeRouteName; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerData = FindObjectOfType<PlayerData>(); 
        routeToFollow = CharacterSelect.CharacterName.NA; 
        startRouteButton.SetActive(false);
    }
    public static void SetRouteStart(CharacterSelect.CharacterName characterName){
        routeToFollow = characterName; 
        
    }

    private void Update(){
        if(routeToFollow != CharacterSelect.CharacterName.NA)
            startRouteButton.SetActive(true);
    }



    public void LoadRouteStart(){
        switch(routeToFollow){
            case CharacterSelect.CharacterName.Ben:
            SceneManager.LoadScene(benRouteName);

            break;

            case CharacterSelect.CharacterName.Jane:
            SceneManager.LoadScene(janeRouteName);
             break;

             default:
             break;

        }
        
    }

    
}
