using OpenTK.Mathematics;

namespace SharpVE.Models
{
    public struct Face
    {
        public Vector3[] Vertices;
        public CullCheck CullDirection;

        public Face()
        {
            Vertices = new Vector3[4];
            CullDirection = new CullCheck();
        }
    }

    public enum CullCheck
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
