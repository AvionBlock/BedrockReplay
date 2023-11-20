using OpenTK.Mathematics;

namespace SharpVE.Blocks
{
    public enum BlockType
    {
        Air,
        BookShelf,
        Dirt,
        Grass
    }
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
        public List<Vector3> Vertices;
        public List<Vector2> UV;
        public List<float> Brightness;
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
                new Vector3(0.5f, 0.5f, -0.5f), //Top Left Vert
                new Vector3(-0.5f, 0.5f, -0.5f), //Top Right Vert
                new Vector3(-0.5f, -0.5f, -0.5f), //Bottom Right Vert
                new Vector3(0.5f, -0.5f, -0.5f), //Bottom Left Vert
            } },
            { Faces.LEFT, new List<Vector3>()
            {
                new Vector3(-0.5f, 0.5f, -0.5f), //Top Left Vert
                new Vector3(-0.5f, 0.5f, 0.5f), //Top Right Vert
                new Vector3(-0.5f, -0.5f, 0.5f), //Bottom Right Vert
                new Vector3(-0.5f, -0.5f, -0.5f), //Bottom Left Vert
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
                new Vector3(-0.5f, -0.5f, 0.5f), //Top Left Vert
                new Vector3(0.5f, -0.5f, 0.5f), //Top Right Vert
                new Vector3(0.5f, -0.5f, -0.5f), //Bottom Right Vert
                new Vector3(-0.5f, -0.5f, -0.5f), //Bottom Left Vert
            } }
        };
    }
}
