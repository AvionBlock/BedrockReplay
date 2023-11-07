using OpenTK.Mathematics;

namespace BedrockReplay.World
{
    public enum Faces
    {
        FRONT,
        BACK,
        LEFT,
        RIGHT,
        TOP,
        BOTTOM
    }

    public struct FaceData
    {
        public List<Vector3> vertices;
        public List<Vector2> uv;
    }

    public struct RawFaceData
    {
        public static readonly Dictionary<Faces, List<Vector3>> rawVertexData = new Dictionary<Faces, List<Vector3>>()
        {
            { Faces.FRONT, new List<Vector3>()
            {
                new Vector3(-0.5f, 0.5f, 0.5f), //Top Left Vert
                new Vector3(0.5f, 0.5f, 0.5f), //Top Right Vert
                new Vector3(0.5f, -0.5f, 0.5f), //Bottom Right Vert
                new Vector3(-0.5f, -0.5f, 0.5f), //Bottom Left Vert
            } },
            { Faces.BACK, new List<Vector3>()
            {
                new Vector3(-0.5f, 0.5f, -0.5f), //Top Left Vert
                new Vector3(0.5f, 0.5f, -0.5f), //Top Right Vert
                new Vector3(0.5f, -0.5f, -0.5f), //Bottom Right Vert
                new Vector3(-0.5f, -0.5f, -0.5f), //Bottom Left Vert
            } },
            { Faces.LEFT, new List<Vector3>()
            {
                new Vector3(-0.5f, 0.5f, 0.5f), //Top Left Vert
                new Vector3(-0.5f, 0.5f, -0.5f), //Top Right Vert
                new Vector3(-0.5f, -0.5f, -0.5f), //Bottom Right Vert
                new Vector3(-0.5f, -0.5f, 0.5f), //Bottom Left Vert
            } },
            { Faces.RIGHT, new List<Vector3>()
            {
                new Vector3(0.5f, 0.5f, 0.5f), //Top Left Vert
                new Vector3(0.5f, 0.5f, -0.5f), //Top Right Vert
                new Vector3(0.5f, -0.5f, -0.5f), //Bottom Right Vert
                new Vector3(0.5f, -0.5f, 0.5f), //Bottom Left Vert
            } },
            { Faces.TOP, new List<Vector3>()
            {
                new Vector3(-0.5f, 0.5f, -0.5f), //Top Left Vert
                new Vector3(0.5f, 0.5f, -0.5f), //Top Right Vert
                new Vector3(0.5f, 0.5f, 0.5f), //Bottom Right Vert
                new Vector3(-0.5f, 0.5f, 0.5f), //Bottom Left Vert
            } },
            { Faces.BOTTOM, new List<Vector3>()
            {
                new Vector3(-0.5f, -0.5f, -0.5f), //Top Left Vert
                new Vector3(0.5f, -0.5f, -0.5f), //Top Right Vert
                new Vector3(0.5f, -0.5f, 0.5f), //Bottom Right Vert
                new Vector3(-0.5f, -0.5f, 0.5f), //Bottom Left Vert
            } },
        };
    }
}
