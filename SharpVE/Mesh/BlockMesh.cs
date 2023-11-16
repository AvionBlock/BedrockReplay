using OpenTK.Mathematics;

namespace SharpVE.Mesh
{
    public struct BlockMesh
    {
        public List<FaceMesh> Faces;

        public BlockMesh()
        {
            Faces = new List<FaceMesh>()
            {
                //Front
                new FaceMesh()
                {
                    Culling = CullDirection.NegX,
                    Vertices = new List<Vector3>()
                    {
                        new Vector3(-0.5f, 0.5f, 0.5f), //Top Left Vert
                        new Vector3(0.5f, 0.5f, 0.5f), //Top Right Vert
                        new Vector3(0.5f, -0.5f, 0.5f), //Bottom Right Vert
                        new Vector3(-0.5f, -0.5f, 0.5f), //Bottom Left Vert
                    },
                    UV = new Vector2(2,15)
                },
                //Back
                new FaceMesh()
                {
                    Culling = CullDirection.PosX,
                    Vertices = new List<Vector3>()
                    {
                        new Vector3(0.5f, 0.5f, -0.5f), //Top Left Vert
                        new Vector3(-0.5f, 0.5f, -0.5f), //Top Right Vert
                        new Vector3(-0.5f, -0.5f, -0.5f), //Bottom Right Vert
                        new Vector3(0.5f, -0.5f, -0.5f), //Bottom Left Vert
                    },
                    UV = new Vector2(2,15)
                },
                //Left
                new FaceMesh()
                {
                    Culling = CullDirection.PosZ,
                    Vertices = new List<Vector3>()
                    {
                        new Vector3(-0.5f, 0.5f, -0.5f), //Top Left Vert
                        new Vector3(-0.5f, 0.5f, 0.5f), //Top Right Vert
                        new Vector3(-0.5f, -0.5f, 0.5f), //Bottom Right Vert
                        new Vector3(-0.5f, -0.5f, -0.5f), //Bottom Left Vert
                    },
                    UV = new Vector2(2,15)
                },
                //Right
                new FaceMesh()
                {
                    Culling = CullDirection.NegZ,
                    Vertices = new List<Vector3>()
                    {
                        new Vector3(0.5f, 0.5f, 0.5f), //Top Left Vert
                        new Vector3(0.5f, 0.5f, -0.5f), //Top Right Vert
                        new Vector3(0.5f, -0.5f, -0.5f), //Bottom Right Vert
                        new Vector3(0.5f, -0.5f, 0.5f), //Bottom Left Vert
                    },
                    UV = new Vector2(2,15)
                }
            };
        }
    }
}
