using UnityEngine;
using System.Collections;

public class Spaceship : MonoBehaviour
{
    /// <summary>
    /// This class is used to detail the spaceship game object
    /// </summary>

    Mesh mesh;

    public Material material;
    public float theta = 2f;


    // Start is called before the first frame update
    void Start()
    {
        // Add a MeshFilter and MeshRenderer to the Empty GameObject
        mesh = gameObject.AddComponent<MeshFilter>().mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;

      


        // CLear all vertex and index data from the mesh
        mesh.Clear();

        // Create a triangles with points
        mesh.vertices = new Vector3[]
        {
            // Point no. (google sheet)
            new Vector3(0f, 1.2f), // 1
            new Vector3(-0.4f, 0.6f), // 2
            new Vector3(0.4f, 0.6f), // 3
            new Vector3(-0.4f, -0.2f), // 4
            new Vector3(-0.6f, -0.3f), // 5
            new Vector3(-0.4f, -0.3f), // 6
            new Vector3(-0.7f, -0.65f), // 7
            new Vector3(-0.4f, -0.6f), // 8
            new Vector3(0.4f, -0.2f), // 9
            new Vector3(0.6f, -0.3f), // 10
            new Vector3(0.4f, -0.3f), // 11
            new Vector3(0.7f, -0.65f), // 12
            new Vector3(0.4f, -0.6f), // 13
            new Vector3(-0.2f, -0.6f), // 14
            new Vector3(-0.3f, -0.7f), // 15
            new Vector3(0.2f, -0.6f), // 16
            new Vector3(0.3f, -0.7f), // 17
            new Vector3(-0.2f, -0.7f), // 18
            new Vector3(-0.3f, -1.0f), // 19
            new Vector3(-0.2f, -0.9f), // 20
            new Vector3(0.2f, -0.7f), // 21
            new Vector3(0.3f, -1.0f), // 22
            new Vector3(0.2f, -0.9f), // 23            
            new Vector3(0.0f, -1.4f) // 24
        };

        // This section sets the colour of the spaceships vertices
        Vector3[] vertices = mesh.vertices;
        Color[] colors = new Color[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            // vertices will be orange
            colors[i] = new Color(0.8f, 0.3f, 0.3f, 1.0f);
        }
        mesh.colors = colors;
        

        // Set vertex indices
        mesh.triangles = new int[]
        {
            0, 1, 2, // A
            1, 2, 7, // B            
            2, 7, 12, // C            
            4, 3, 5, // D            
            4, 5, 6, // E            
            8, 9, 10, // F            
            9, 10, 11, // G            
            13, 14, 15, // H
            14, 15, 16, // I            
            17, 18, 19, // J            
            17, 19, 20, // K            
            19, 20, 22, // L            
            20, 21, 22, // M             
            19, 22, 23 // N
        };

    }



    // Update is called once per frame
    void Update()
    {
        Vector3[] vertices = mesh.vertices;

        // Create the rotation matrix
        Matrix3x3 M = IGB283Transform.Rotate(theta * Time.deltaTime);

        // Apply the rotation to all the points in the mesh
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = M.MultiplyPoint(vertices[i]);
        }

        // Write the points back to the mesh
        mesh.vertices = vertices;

        mesh.RecalculateBounds();
    }

    
}
