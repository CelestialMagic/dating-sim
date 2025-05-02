using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class WobblingText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textToWobble;

    private Mesh mesh;
    private Vector3[] vertices;

    [SerializeField]
    private float firstModifier, secondModifier; 




    // Start is called before the first frame update


    private Vector2 Wobble(float time){
        return new Vector2(Mathf.Sin(time * firstModifier), Mathf.Cos(time * secondModifier));
    }

    // Update is called once per frame
    void Update()
    {
        textToWobble.ForceMeshUpdate();
        mesh = textToWobble.mesh;
        vertices = mesh.vertices;

        for(int i = 0; i < vertices.Length; i++){
            Vector3 offset = Wobble(Time.time + i);

            vertices[i] = vertices[i] + offset;
        }
        mesh.vertices = vertices;
        textToWobble.canvasRenderer.SetMesh(mesh);
    }
        
    
}
