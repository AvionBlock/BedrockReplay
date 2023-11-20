using OpenTK.Mathematics;

namespace SharpVE.Models
{
    public struct Cube
    {
        public static readonly List<Face> Faces = new List<Face>()
        {
            new Face()
            {
                CullDirection = CullCheck.None,
                Vertices = new Vector3[]
                {
                    new Vector3(-0.5f, 0.5f, 0.5f), //Top Left Vert
                    new Vector3(0.5f, 0.5f, 0.5f), //Top Right Vert
                    new Vector3(0.5f, -0.5f, 0.5f), //Bottom Right Vert
                    new Vector3(-0.5f, -0.5f, 0.5f), //Bottom Left Vert
                }
            },
            new Face()
            {
                CullDirection = CullCheck.None,
                Vertices = new Vector3[]
                {
                    new Vector3(0.5f, 0.5f, -0.5f), //Top Left Vert
                    new Vector3(-0.5f, 0.5f, -0.5f), //Top Right Vert
                    new Vector3(-0.5f, -0.5f, -0.5f), //Bottom Right Vert
                    new Vector3(0.5f, -0.5f, -0.5f), //Bottom Left Vert
                }
            },
            new Face()
            {
                CullDirection = CullCheck.None,
                Vertices = new Vector3[]
                {
                    new Vector3(-0.5f, 0.5f, -0.5f), //Top Left Vert
                    new Vector3(-0.5f, 0.5f, 0.5f), //Top Right Vert
                    new Vector3(-0.5f, -0.5f, 0.5f), //Bottom Right Vert
                    new Vector3(-0.5f, -0.5f, -0.5f), //Bottom Left Vert
                }
            },
            new Face()
            {
                CullDirection = CullCheck.None,
                Vertices = new Vector3[]
                {
                    new Vector3(0.5f, 0.5f, 0.5f), //Top Left Vert
                    new Vector3(0.5f, 0.5f, -0.5f), //Top Right Vert
                    new Vector3(0.5f, -0.5f, -0.5f), //Bottom Right Vert
                    new Vector3(0.5f, -0.5f, 0.5f), //Bottom Left Vert
                }
            },
            new Face()
            {
                CullDirection = CullCheck.None,
                Vertices = new Vector3[]
                {
                    new Vector3(-0.5f, 0.5f, -0.5f), //Top Left Vert
                    new Vector3(0.5f, 0.5f, -0.5f), //Top Right Vert
                    new Vector3(0.5f, 0.5f, 0.5f), //Bottom Right Vert
                    new Vector3(-0.5f, 0.5f, 0.5f), //Bottom Left Vert
                }
            },
            new Face()
            {
                CullDirection = CullCheck.None,
                Vertices = new Vector3[]
                {
                    new Vector3(-0.5f, -0.5f, 0.5f), //Top Left Vert
                    new Vector3(0.5f, -0.5f, 0.5f), //Top Right Vert
                    new Vector3(0.5f, -0.5f, -0.5f), //Bottom Right Vert
                    new Vector3(-0.5f, -0.5f, -0.5f), //Bottom Left Vert
                }
            }
        };
    };
}