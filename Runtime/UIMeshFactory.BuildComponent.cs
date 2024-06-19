using System;
using System.Collections.Generic;
using Sxm.UIFactory.Components;

namespace Sxm.UIFactory
{
    public static partial class UIMeshFactory
    {
        private const int ParametersCapacity = 10;
        private const int ComponentsCapacity = 100;

        public readonly struct CachedMeshInstance
        {
            public readonly Guid CachedComponentId;
            public readonly IReadOnlyList<MeshData> Data;

            public CachedMeshInstance(Guid cachedComponentId, IReadOnlyList<MeshData> data)
            {
                CachedComponentId = cachedComponentId;
                Data = data;
            }
        }

        private static readonly Dictionary<Type, Type> ComponentRegistry = new()
        {
            {typeof(SolidLineParameters), null},
            {typeof(SolidLineSegmentParameters), typeof(SolidLineSegmentComponent)},
            {typeof(DashLineParameters), null},
            {typeof(DashLineSegmentParameters), typeof(DashLineSegmentComponent)},
            {typeof(LineSeriesParameters), typeof(LineSeriesComponent)},
            {typeof(PointSeriesParameters), typeof(PointSeriesComponent)},
            {typeof(CircleFilledDotParameters), null},
            {typeof(CircleFilledPointParameters), typeof(CircleFilledPointComponent)},
            {typeof(CircleOutlinedDotParameters), null},
            {typeof(CircleOutlinedPointParameters), typeof(CircleOutlinedPointComponent)},
            {typeof(SquareFilledDotParameters), null},
            {typeof(SquareFilledPointParameters), typeof(SquareFilledPointComponent)},
            {typeof(SquareOutlinedDotParameters), null},
            {typeof(SquareOutlinedPointParameters), typeof(SquareOutlinedPointComponent)},
            {typeof(TriangleFilledDotParameters), null},
            {typeof(TriangleFilledPointParameters), typeof(TriangleFilledPointComponent)},
            {typeof(TriangleOutlinedDotParameters), null},
            {typeof(TriangleOutlinedPointParameters), typeof(TriangleOutlinedPointComponent)},
            {typeof(GraphParameters), typeof(GraphComponent)},
        };

        private static readonly Dictionary<Type, Dictionary<Guid, IMeshComponent>> CachedComponents = new(capacity: ParametersCapacity);

        public static IReadOnlyList<MeshData> Build(Dictionary<string, object> rawParameters)
        {
            var parameters = BuildParameters(rawParameters);
            return Build(parameters);
        }

        public static IReadOnlyList<MeshData> Build<T>(T parameters) where T : MeshParameters
        {
            var parametersType = parameters.GetType();
            var targetComponent = CreateMeshComponentBy(parametersType);

            return targetComponent.Build(parameters);
        }

        public static CachedMeshInstance Build(Dictionary<string, object> rawParameters, Guid? cachedComponentId)
        {
            var parameters = BuildParameters(rawParameters);
            return Build(parameters, cachedComponentId);
        }

        public static CachedMeshInstance Build<T>(T parameters, Guid? cachedComponentId) where T : MeshParameters
        {
            var parametersType = parameters.GetType();

            Guid componentId;
            IMeshComponent targetComponent;

            if (CachedComponents.TryGetValue(parametersType, out var foundComponents))
            {
                if (cachedComponentId.HasValue && foundComponents.TryGetValue(cachedComponentId.Value, out var foundComponent))
                {
                    componentId = cachedComponentId.Value;
                    targetComponent = foundComponent;
                }
                else
                {
                    (componentId, targetComponent) = CreateCachedMeshComponentBy(parametersType);
                    foundComponents.Add(componentId, targetComponent);
                }
            }
            else
            {
                (componentId, targetComponent) = CreateCachedMeshComponentBy(parametersType);
                foundComponents = new Dictionary<Guid, IMeshComponent>(capacity: ComponentsCapacity) {{componentId, targetComponent}};
                CachedComponents.Add(parametersType, foundComponents);
            }

            return new CachedMeshInstance(componentId, targetComponent.Build(parameters));
        }

        public static CachedMeshInstance CacheComponentIdTo(this CachedMeshInstance cachedMeshInstance, ref Guid? source)
        {
            source = cachedMeshInstance.CachedComponentId;
            return cachedMeshInstance;
        }

        public static CachedMeshInstance CacheComponentIdTo(this CachedMeshInstance cachedMeshInstance, ref Guid?[] sources, int sourceIndex)
        {
            sources[sourceIndex] = cachedMeshInstance.CachedComponentId;
            return cachedMeshInstance;
        }

        private static (Guid, IMeshComponent) CreateCachedMeshComponentBy(Type parametersType) => (Guid.NewGuid(), CreateMeshComponentBy(parametersType));

        private static IMeshComponent CreateMeshComponentBy(Type parametersType) => (IMeshComponent) Activator.CreateInstance(ComponentRegistry[parametersType]);
    }
}