using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    Mesh mesh;
    public Material material;

    public float theta = 10f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        // Get the Mesh from the MeshFilter
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        // Set the material to the material we have selected
        GetComponent<MeshRenderer>().material = material;

        // Clear all vertex and index data from the mesh
        mesh.Clear();


        mesh.vertices = new Vector3[]
        {
            // A
            new Vector3(-0.4f, 0.6f),
            new Vector3(0.4f, 0.6f),
            new Vector3(0f, 1.2f),

            // B
            new Vector3(-0.4f, 0.6f),
            new Vector3(0.4f, 0.6f),
            new Vector3(-0.4f, -0.6f),

            // C
            new Vector3(0.4f, 0.6f),
            new Vector3(-0.4f, -0.6f),
            new Vector3(0.4f, -0.6f),

            // D
            new Vector3(-0.6f, -0.3f),
            new Vector3(-0.4f, -0.2f),
            new Vector3(-0.4f, -0.3f),

            // E
            new Vector3(-0.6f, -0.3f),
            new Vector3(-0.4f, -0.3f),
            new Vector3(-0.7f, -0.65f),

            // F
            new Vector3(0.4f, -0.2f),
            new Vector3(0.6f, -0.3f),
            new Vector3(0.4f, -0.3f),

            // G
            new Vector3(0.6f, -0.3f),
            new Vector3(0.4f, -0.3f),
            new Vector3(0.7f, -0.65f),

            // H
            new Vector3(-0.2f, -0.6f),
            new Vector3(-0.3f, -0.7f),
            new Vector3(0.2f, -0.6f),

            // I
            new Vector3(-0.3f, -0.7f),
            new Vector3(0.2f, -0.6f),
            new Vector3(0.3f, -0.7f),

            // J
            new Vector3(-0.2f, -0.7f),
            new Vector3(-0.3f, -1.0f),
            new Vector3(-0.2f, -0.9f),

            // K
            new Vector3(-0.2f, -0.7f),
            new Vector3(-0.2f, -0.9f),
            new Vector3(0.2f, -0.7f),

            // L
            new Vector3(-0.2f, -0.9f),
            new Vector3(0.2f, -0.7f),
            new Vector3(0.2f, -0.9f),

            // M
            new Vector3(0.2f, -0.7f),
            new Vector3(0.3f, -1.0f),
            new Vector3(0.2f, -0.9f),

            // N
            new Vector3(-0.2f, -0.9f),
            new Vector3(0.2f, -0.9f),
            new Vector3(0.0f, -1.4f)
        };
        // Set the colour of the triangle
        mesh.colors = new Color[]
        {
            new Color(0.8f, 0.3f, 0.3f, 1.0f),
            new Color(0.8f, 0.3f, 0.3f, 1.0f),
            new Color(0.8f, 0.3f, 0.3f, 1.0f)
        };

        // Set vertex indices
        mesh.triangles = new int[]
        {
            0, 1, 2,
            3, 4, 5,
            6, 7, 8,
            9, 10, 11,
            12, 13, 14,
            15, 16, 17,
            18, 19, 20,
            21, 22, 23,
            24, 25, 26,
            27, 28, 29,
            30, 31, 32,
            33, 34, 35,
            36, 37, 38,
            39, 40, 41
        };
    }

    // Update is called once per frame
    void Update()
    {
        Vector3[] vertices = mesh.vertices;

        Matrix3x3 M = Rotate(theta * Time.deltaTime);

        // Apply the rotation to all the points in the mesh
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = M.MultiplyPoint(vertices[i]);
        }
        mesh.vertices = vertices;

        mesh.RecalculateBounds();
    }

    Matrix3x3 Rotate(float angle)
    {
        Matrix3x3 matrix = new Matrix3x3();

        matrix.SetRow(0, new Vector3(Mathf.Cos(angle), -Mathf.Sin(angle), 0));
        matrix.SetRow(1, new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0));
        matrix.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));

        return matrix;
    }
}
