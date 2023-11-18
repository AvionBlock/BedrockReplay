using OpenTK.Mathematics;

namespace SharpVE.Models
{
    public struct Cube
    {
        public static readonly List<Vector3> Front = new List<Vector3>()
        {
            new Vector3(-0.5f, 0.5f, 0.5f), //Top Left Vert
            new Vector3(0.5f, 0.5f, 0.5f), //Top Right Vert
            new Vector3(0.5f, -0.5f, 0.5f), //Bottom Right Vert
            new Vector3(-0.5f, -0.5f, 0.5f), //Bottom Left Vert
        };

        public static readonly List<Vector3> Back = new List<Vector3>()
        {
            new Vector3(0.5f, 0.5f, -0.5f), //Top Left Vert
            new Vector3(-0.5f, 0.5f, -0.5f), //Top Right Vert
            new Vector3(-0.5f, -0.5f, -0.5f), //Bottom Right Vert
            new Vector3(0.5f, -0.5f, -0.5f), //Bottom Left Vert
        };

        public static readonly List<Vector3> Left = new List<Vector3>()
        {
            new Vector3(-0.5f, 0.5f, -0.5f), //Top Left Vert
            new Vector3(-0.5f, 0.5f, 0.5f), //Top Right Vert
            new Vector3(-0.5f, -0.5f, 0.5f), //Bottom Right Vert
            new Vector3(-0.5f, -0.5f, -0.5f), //Bottom Left Vert
        };

        public static readonly List<Vector3> Right = new List<Vector3>()
        {
            new Vector3(0.5f, 0.5f, 0.5f), //Top Left Vert
            new Vector3(0.5f, 0.5f, -0.5f), //Top Right Vert
            new Vector3(0.5f, -0.5f, -0.5f), //Bottom Right Vert
            new Vector3(0.5f, -0.5f, 0.5f), //Bottom Left Vert
        };

        public static readonly List<Vector3> Top = new List<Vector3>()
        {
            new Vector3(-0.5f, 0.5f, -0.5f), //Top Left Vert
            new Vector3(0.5f, 0.5f, -0.5f), //Top Right Vert
            new Vector3(0.5f, 0.5f, 0.5f), //Bottom Right Vert
            new Vector3(-0.5f, 0.5f, 0.5f), //Bottom Left Vert
        };

        public static readonly List<Vector3> Bottom = new List<Vector3>()
        {
            new Vector3(-0.5f, -0.5f, 0.5f), //Top Left Vert
            new Vector3(0.5f, -0.5f, 0.5f), //Top Right Vert
            new Vector3(0.5f, -0.5f, -0.5f), //Bottom Right Vert
            new Vector3(-0.5f, -0.5f, -0.5f), //Bottom Left Vert
        };
    }
}
