using System;
using System.Collections.Generic;
using Sxm.UIFactory.Components;
using Sxm.UIFactory.Components.Lines;
using Sxm.UIFactory.Components.Points;
using Sxm.UIFactory.Components.Series;

namespace Sxm.UIFactory
{
    public static class UIFactoryManager
    {
        // todo@sxm: maybe need to find this value experimentally?
        private const int MeshBuilderCapacity = 10;

        public readonly struct CookedMesh
        {
            public readonly Guid AssignedMeshBuilderId;
            public readonly IEnumerable<MeshData> Result;

            public CookedMesh(Guid assignedMeshBuilderId, IEnumerable<MeshData> result)
            {
                AssignedMeshBuilderId = assignedMeshBuilderId;
                Result = result;
            }
        }

        private static readonly Guid SharedMeshBuilderId = Guid.NewGuid();

        private static readonly Dictionary<Type, Dictionary<Guid, IMeshBuilder>> CachedMeshBuilders = new()
        {
            [typeof(SolidLineMeshDescription)] = new(MeshBuilderCapacity),
            [typeof(DashLineMeshDescription)] = new(MeshBuilderCapacity),
            [typeof(FilledPointMeshDescription)] = new(MeshBuilderCapacity),
            [typeof(OutlinedPointMeshDescription)] = new(MeshBuilderCapacity),
            [typeof(LineSeriesMeshDescription)] = new(MeshBuilderCapacity),
            [typeof(PointSeriesMeshDescription)] = new(MeshBuilderCapacity),
            [typeof(LineGraphMeshDescription)] = new(MeshBuilderCapacity)
        };

        public static IEnumerable<MeshData> Build<T>(T description) where T : MeshDescription
        {
            return Build(description, SharedMeshBuilderId).Result;
        }

        public static CookedMesh Build<T>(T description, Guid? assignedMeshBuilderId) where T : MeshDescription
        {
            var meshBuilders = CachedMeshBuilders[description.GetType()];
            var actualMeshBuilderId = assignedMeshBuilderId ?? Guid.NewGuid();

            if (!meshBuilders.ContainsKey(actualMeshBuilderId))
                meshBuilders[actualMeshBuilderId] = new CachedMeshBuilder<T>(description.ConstructBuilder());

            return new CookedMesh(actualMeshBuilderId, meshBuilders[actualMeshBuilderId].Build(description));
        }

        public static CookedMesh CacheAssignedMeshBuilderId(this CookedMesh cookedMesh, ref Guid? assignedMeshBuilderId)
        {
            assignedMeshBuilderId = cookedMesh.AssignedMeshBuilderId;
            return cookedMesh;
        }

        public static CookedMesh CacheAssignedMeshBuilderId(this CookedMesh cookedMesh, ref Guid?[] assignedMeshBuilderIds, int sourceIndex)
        {
            assignedMeshBuilderIds[sourceIndex] = cookedMesh.AssignedMeshBuilderId;
            return cookedMesh;
        }
    }
}