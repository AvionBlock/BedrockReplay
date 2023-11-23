using OpenTK.Mathematics;

namespace SharpVE.Models
{
    public struct Cube
    {
        public static readonly List<Face> Faces = new List<Face>()
        {
            new Face()
            {
                CullDirection = CullCheck.PosZ,
                Vertices = new Vector3[]
                {
                    new Vector3(-0.5f, 0.5f, 0.5f), //Top Left Vert
                    new Vector3(0.5f, 0.5f, 0.5f), //Top Right Vert
                    new Vector3(0.5f, -0.5f, 0.5f), //Bottom Right Vert
                    new Vector3(-0.5f, -0.5f, 0.5f), //Bottom Left Vert
                },
                UsesUV = 0
            },
            new Face()
            {
                CullDirection = CullCheck.NegZ,
                Vertices = new Vector3[]
                {
                    new Vector3(0.5f, 0.5f, -0.5f), //Top Left Vert
                    new Vector3(-0.5f, 0.5f, -0.5f), //Top Right Vert
                    new Vector3(-0.5f, -0.5f, -0.5f), //Bottom Right Vert
                    new Vector3(0.5f, -0.5f, -0.5f), //Bottom Left Vert
                },
                UsesUV = 1
            },
            new Face()
            {
                CullDirection = CullCheck.NegX,
                Vertices = new Vector3[]
                {
                    new Vector3(-0.5f, 0.5f, -0.5f), //Top Left Vert
                    new Vector3(-0.5f, 0.5f, 0.5f), //Top Right Vert
                    new Vector3(-0.5f, -0.5f, 0.5f), //Bottom Right Vert
                    new Vector3(-0.5f, -0.5f, -0.5f), //Bottom Left Vert
                },
                UsesUV = 2
            },
            new Face()
            {
                CullDirection = CullCheck.PosX,
                Vertices = new Vector3[]
                {
                    new Vector3(0.5f, 0.5f, 0.5f), //Top Left Vert
                    new Vector3(0.5f, 0.5f, -0.5f), //Top Right Vert
                    new Vector3(0.5f, -0.5f, -0.5f), //Bottom Right Vert
                    new Vector3(0.5f, -0.5f, 0.5f), //Bottom Left Vert
                },
                UsesUV = 3
            },
            new Face()
            {
                CullDirection = CullCheck.PosY,
                Vertices = new Vector3[]
                {
                    new Vector3(-0.5f, 0.5f, -0.5f), //Top Left Vert
                    new Vector3(0.5f, 0.5f, -0.5f), //Top Right Vert
                    new Vector3(0.5f, 0.5f, 0.5f), //Bottom Right Vert
                    new Vector3(-0.5f, 0.5f, 0.5f), //Bottom Left Vert
                },
                UsesUV = 4
            },
            new Face()
            {
                CullDirection = CullCheck.NegY,
                Vertices = new Vector3[]
                {
                    new Vector3(-0.5f, -0.5f, 0.5f), //Top Left Vert
                    new Vector3(0.5f, -0.5f, 0.5f), //Top Right Vert
                    new Vector3(0.5f, -0.5f, -0.5f), //Bottom Right Vert
                    new Vector3(-0.5f, -0.5f, -0.5f), //Bottom Left Vert
                },
                UsesUV = 5
            }
        };
    };
}