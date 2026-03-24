# UI Factory

## [5.0.0] - 2026-03-24

### Bug Fixes
- Fixed `CachedMeshBuilder` caching all accumulated mesh data instead of only newly built data — `startIndex` now tracks where new results begin, preventing mesh duplication on cache miss
- Fixed collection change detection in mesh descriptions — replaced O(n) element-by-element comparison (`CollectionEquality`) with O(1) version-based comparison via `Snapshot<T>` struct

### Breaking Changes
- Collection fields in mesh descriptions changed from `IList<Vector2>` / `HashSet<int>` to `Snapshot<VersionedList<Vector2>>` / `Snapshot<VersionedHashSet<int>>?` — clients must wrap collections in `VersionedList`/`VersionedHashSet` and pass them via `new Snapshot<T>(collection)` when constructing descriptions

### Performance
- Collection equality in descriptions is now O(1) regardless of collection size — `Snapshot<T>` compares reference identity + version counter instead of iterating elements

### Internal
- Added `IVersioned` interface, `VersionedList<T>`, and `VersionedHashSet<T>` — collection wrappers that increment a version counter on every mutation
- Added `Snapshot<T>` readonly struct — captures collection reference + version at creation time for lightweight equality
- Replaced `IList<Vector2>` with `Snapshot<VersionedList<Vector2>>` and `HashSet<int>` with `Snapshot<VersionedHashSet<int>>?` in `GraphMeshDescription`, `SeriesMeshDescription`, `PointSeriesMeshDescription`, `LineSeriesMeshDescription`
- Removed `CollectionEquality` utility and custom `Equals`/`GetHashCode` overrides from all description records — record-generated equality now works correctly via `Snapshot<T>.Equals`
- Removed `MeshDescription.Snapshot()` virtual method and all overrides — no longer needed with version-based comparison
- Extracted `ResizeHandles` from `LineSeriesMeshBuilder`/`PointSeriesMeshBuilder` into `MeshHandleExtensions` extension method
- `DashLineMeshBuilder` and `OutlinedPointMeshBuilder` now use `VersionedList<Vector2>` as persistent field instead of `ListPool` allocation

## [4.0.1] - 2026-03-23

### Bug Fixes
- Fixed grid not updating when `Labels Offset` changed — `DashLineMeshBuilder` reused the same `_positions` list instance across builds, causing `CachedMeshBuilder` to always compare the list with itself (reference equality → always cache hit → stale mesh). Now each build allocates a fresh list from `ListPool` and releases the previous one after comparison
- Removed incorrect `ForceBuild` propagation in `DashLineMeshBuilder` — inner `LineSeriesMeshDescription` and `SolidLineMeshDescription` no longer blindly pass through `ForceBuild`, relying on correct structural equality instead

### Internal
- Added `CollectionEquality` utility with `SequenceEqual`/`SetEqual` and corresponding `GetHashCode` methods for `IList<T>` and `HashSet<T>`
- Added structural `Equals`/`GetHashCode` overrides to `SeriesMeshDescription`, `PointSeriesMeshDescription`, and `GraphMeshDescription` — C# records use reference equality for collection fields by default, which broke `CachedMeshBuilder` comparison

## [4.0.0] - 2026-03-19

### Breaking Changes
- Changed public API: `UIFactoryManager.Build()` replaced with `BuildMesh()` extension method on `MeshGenerationContext` — mesh data now writes directly to UIElements context instead of returning `IReadOnlyList<MeshData>`
- Removed `UIElementsMeshDataAdapter` (public `SetData` extension) — functionality absorbed into `BuildMesh`
- `MeshData` changed from `public` to `internal`

### Performance
- Migrated mesh geometry calculations (line, rectangle, circle, triangle) to Burst-compiled procedures via `Unity.Mathematics`
- Replaced managed `Vertex[]`/`ushort[]` arrays in `MeshData` with `NativeArray<Vertex>`/`NativeArray<ushort>` (persistent allocator, no GC pressure)
- Added smart resizing of mesh handles in `LineSeriesMeshBuilder` and `PointSeriesMeshBuilder` — only excess handles are disposed on count change instead of disposing all and recreating
- Reduced GC allocations with `ListPool` for temporary lists (positions in `DashLineMeshBuilder`, handles in series builders)
- Pre-allocated static arrays for intermediate vertex calculations (circle, rectangle, triangle circumference vertices)
- Reduced circle resolution for outlined points (from 32 to 8 segments)

### Bug Fixes
- Fixed `MeshHandle.ReleaseBuilder()` — builder was set to `null` before `Pool.Release()`, so builders were never returned to the pool
- Fixed `MeshHandle` dispose of mesh handle with incorrect type change detection
- Fixed negative line count exception in `LineSeriesMeshBuilder` when positions list has 0-1 elements

### Internal
- Added `MeshBuilder<T>.Init()` virtual method for pre-allocating mesh data on builder acquisition
- Cached mesh results in `CachedMeshBuilder` now use `ListPool` instead of raw allocation
- `MeshHandle` constructor restricted to `internal`
- Added `[CanBeNull]` annotations to `MeshHandle` fields
- Added `[UsedImplicitly]` to `IsExternalInit` compiler shim
- Explicit enum values for `PointShape`
- Added `com.unity.burst` 1.8.28 as package dependency

## [3.1.1] - 2026-02-23

- Optimization of graph mesh data creation

## [3.1.0] - 2026-02-23

- Added ignore point rendering by position indices

## [3.0.0] - 2026-02-18

- Changed namespace from `Sxm.UIFactory` to `SxmTools.UIFactory`

## [2.1.1] - 2026-01-25

- Removed redundant comments from code

## [2.1.0] - 2026-01-25

- Added optional points creation for Graph

## [2.0.1] - 2026-01-25

- Fixed Outlined/Triangle mesh creation

## [2.0.0] - 2026-01-25

- Removed dash line support for graph
- LineGraph renamed to Graph

## [1.0.3] - 2025-08-05

- Added missing metafile

## [1.0.2] - 2025-08-05

- Added OpenUPM info to README

## [1.0.1] - 2025-08-05

- Added preview.png

## [1.0.0] - 2025-08-05

Use it to create various UI components, from primitives (line, point) to complex objects (graph).
