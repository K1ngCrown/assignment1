using UnityEngine;
using System.Collections;

public class Spaceship : MonoBehaviour
{
    /// <summary>
    /// This class is used to detail the spaceship game object
    /// </summary>

    Mesh mesh;

    public Material material;
    public float theta = 1f;
    public float speed = 1f;

    private float right_planet_x = 8f;
    private float left_planet_x = -8f;

    private Vector3 offset;

    public float size;
    public float increaseSize = 1.0003f;
    public float decreaseSize = 0.9997f;

    // Start is called before the first frame update
    void Start()
    {    
        size = increaseSize;
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

        // Calculate the bounds of the shape
        offset.x = mesh.bounds.size.x / 2;
        offset.y = mesh.bounds.size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] vertices = mesh.vertices;
        Color[] colors = new Color[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            // vertices will be orange
            if (offset.x > 0)
            {
                colors[i] = new Color(0.8f, 0.3f, 0.3f, 1.0f);
                
            }
            else
            {
                colors[i] = new Color(0, 0, 1, 1);
                
            }

        }
        mesh.colors = colors;

      
       

       
        if (offset.x > right_planet_x || offset.x < left_planet_x)
        {
            speed *= -1;
        }
        if (speed == 1)
        {
            size = increaseSize;
        }
        else if (speed == -1)
        {
            size = decreaseSize;
        }
       
        Matrix3x3 RotateShip = IGB283Transform.Rotate(theta * Time.deltaTime * 3f);
        Matrix3x3 Translate = IGB283Transform.Translate(offset + new Vector3(speed / 150, 0, 0));
        Matrix3x3 Scale = IGB283Transform.Scale(size, size);
        Matrix3x3 Translate_Back = IGB283Transform.Translate(-offset);
        Matrix3x3 Transformation = Translate * RotateShip * Scale * Translate_Back;

        // Apply the transformation to all the points in the mesh
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = Transformation.MultiplyPoint(vertices[i]);
        }

        // Write the points back to the mesh
        mesh.vertices = vertices;

        mesh.RecalculateBounds();
        offset = mesh.bounds.center;

        //Change the speed of shape rotation
        if ((Input.GetKeyDown("k")) && theta < 7.0f)
        {
            theta += 0.1f;    
        }
        else if ((Input.GetKeyDown("l")) && theta > 0f)
        {
            theta -= 0.1f;
            if(theta < 0.1f)
            {
                theta = 0f;
            }
        }
    }

}