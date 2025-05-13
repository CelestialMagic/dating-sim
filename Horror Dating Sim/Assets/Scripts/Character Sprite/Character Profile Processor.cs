using UnityEngine;

public class CharacterProfileProcessor : MonoBehaviour
{


    [SerializeField] private CharacterProfile[] _profiles; // List of character profiles to process

    private static bool _hasProcessedProfiles = false; // True if all character profiles are processed

    // Start is called before the first frame update
    void Awake()
    {
        if (!_hasProcessedProfiles)
        {
            foreach (CharacterProfile profile in _profiles)
                profile.Process();

            _hasProcessedProfiles = true;
        }

        Destroy(this);
    }
}
