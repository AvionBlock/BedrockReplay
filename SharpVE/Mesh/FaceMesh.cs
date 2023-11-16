using OpenTK.Mathematics;

namespace SharpVE.Mesh
{
    public struct FaceMesh
    {
        public List<Vector3> Vertices;
        public Vector2 UV;
        public CullDirection Culling;
    }

    public enum CullDirection
    {
        None,
        PosX,
        PosY,
        PosZ,
        NegX,
        NegY,
        NegZ
    }
}
