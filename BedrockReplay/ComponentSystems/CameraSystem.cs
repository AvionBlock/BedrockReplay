using Arch.Core;
using Arch.System;
using BedrockReplay.Components;

namespace BedrockReplay.ComponentSystems
{
    public class CameraSystem : BaseSystem<World, double>
    {
        private QueryDescription query;
        private QueryDescription meshQuery;
        public CameraSystem(World world) : base(world)
        {
            query = new QueryDescription().WithAll<TransformComponent, CameraComponent>();
            meshQuery = new QueryDescription().WithAll<TransformComponent, MeshRendererComponent>();
        }

        //Redelegate AfterUpdate to RenderUpdate.
        public override void AfterUpdate(in double d)
        {
            var delta = d;
            World.Query(in query, (ref TransformComponent transf, ref CameraComponent cam) =>
            {
                var camera = cam;
                var transform = transf;

                World.Query(in meshQuery, (ref TransformComponent meshTransform, ref MeshRendererComponent mesh) =>
                {
                    camera.ProjectionShader.BaseShader.NativeShader.SetUniform4("model", transform.Model);
                    //camera.ProjectionShader.BaseShader.NativeShader.SetUniform4("view", camera.View);
                    //camera.ProjectionShader.BaseShader.NativeShader.SetUniform4("projection", camera.View);
                    camera.ProjectionShader.Render(delta);
                    //mesh.Mesh.Render(delta);
                });
            });
        }
    }
}
