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
            meshQuery = new QueryDescription().WithAll<TransformComponent, ChunkMeshComponent>();
        }

        //Redelegate AfterUpdate to RenderUpdate.
        public override void AfterUpdate(in double d)
        {
            var delta = d;
            World.Query(in query, (ref TransformComponent transform, ref CameraComponent cam) =>
            {
                var camera = cam;
                camera.ProjectionShader.BaseShader.SetUniform4("view", camera.GetView(transform));
                camera.ProjectionShader.BaseShader.SetUniform4("projection", camera.GetProjection());

                World.Query(in meshQuery, (ref TransformComponent meshTransform, ref ChunkMeshComponent mesh) =>
                {
                    camera.ProjectionShader.BaseShader.SetUniform4("model", meshTransform.ModelMatrix);
                    camera.ProjectionShader.Render(delta);
                    mesh.Mesh.Render(delta);
                });
            });
        }
    }
}
