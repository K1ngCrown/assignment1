using UnityEngine;
using System.Collections;

public class SecondSpaceship : MonoBehaviour
{
    Mesh mesh;

    public Material material;
    public float theta = 1f;
    public float speed = 0f;

    private float right_planet_x = 7f;
    private float left_planet_x = -7f;

    private Vector3 offset;

    public float size;
    public float increaseSize = 1.0004f;
    public float decreaseSize = 0.9996f;
   
    // Start is called before the first frame update
    void Start()
    {
        Vector3 StartingPos = new Vector3(0f, 3f, 0);
        
        mesh = gameObject.AddComponent<MeshFilter>().mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
       
        mesh.Clear();

        // Create a triangles with points
        mesh.vertices = new Vector3[]
        {
            // Point no. (google sheet)
            new Vector3(0f, 0.95f), // 1
            new Vector3(-0.35f, 0.5f), // 2
            new Vector3(0.35f, 0.5f), // 3
            new Vector3(-0.35f, 0.0f), // 4
            new Vector3(-0.55f, -0.1f), // 5
            new Vector3(-0.35f, -0.1f), // 6
            new Vector3(-0.65f, -0.4f), // 7
            new Vector3(-0.35f, -0.3f), // 8
            new Vector3(0.35f, 0.0f), // 9
            new Vector3(0.55f, -0.1f), // 10
            new Vector3(0.35f, -0.1f), // 11
            new Vector3(0.65f, -0.4f), // 12
            new Vector3(0.35f, -0.3f), // 13
            new Vector3(-0.2f, -0.3f), // 14
            new Vector3(-0.25f, -0.4f), // 15
            new Vector3(0.2f, -0.3f), // 16
            new Vector3(0.25f, -0.4f), // 17
            new Vector3(-0.2f, -0.4f), // 18
            new Vector3(-0.3f, -0.7f), // 19
            new Vector3(-0.2f, -0.6f), // 20
            new Vector3(0.2f, -0.4f), // 21
            new Vector3(0.3f, -0.7f), // 22
            new Vector3(0.2f, -0.6f), // 23            
            new Vector3(0.0f, -0.95f) // 24
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

        Matrix3x3 TopShipTranslate = IGB283Transform.Translate(StartingPos);

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = TopShipTranslate.MultiplyPoint(vertices[i]);
        }

        // Write the points back to the mesh
        mesh.vertices = vertices;

        mesh.RecalculateBounds();
        offset = mesh.bounds.center;
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

        //Scale the size of the shape
        if (speed > 0 && speed != 0)
        {
            size = increaseSize;
        }
        else if (speed < 0 && speed != 0)
        {
            size = decreaseSize;
        }
        

        Matrix3x3 RotateShip = IGB283Transform.Rotate(theta * Time.deltaTime * 3f);
        Matrix3x3 Translate = IGB283Transform.Translate(offset + new Vector3(speed / 150, 0, 0));
        Matrix3x3 Scale = IGB283Transform.Scale(size, size);
        Matrix3x3 Translate_Back = IGB283Transform.Translate(-offset);
        Matrix3x3 Transformation = Translate * RotateShip * Scale * Translate_Back;
        Matrix3x3 NoScaleTransformation = Translate * RotateShip * Translate_Back;

        // Apply the transformation to all the points in the mesh
        for (int i = 0; i < vertices.Length; i++)
        {
            if (speed != 0)
            {
                vertices[i] = Transformation.MultiplyPoint(vertices[i]);
            }
            else
            {
                vertices[i] = NoScaleTransformation.MultiplyPoint(vertices[i]);
            }
        }

        // Write the points back to the mesh
        mesh.vertices = vertices;

        mesh.RecalculateBounds();
        offset = mesh.bounds.center;

        //Change speed of spaceship
        if (Input.GetKeyDown("w") && speed >= 1) 
        {
            speed += 1f;
        }
        else if (Input.GetKeyDown("s") && speed == 0)
        {
            speed += 1f;
        }
        else if (Input.GetKeyDown("l") && speed > 1) 
        {
            speed -= 1f;
        }
        else if (Input.GetKeyDown("l") && speed == 1)
        {
            speed = 0;
        }
    }

}
