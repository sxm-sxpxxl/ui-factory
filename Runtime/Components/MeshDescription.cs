namespace Sxm.UIFactory.Components
{
    public abstract record MeshDescription(bool ForceBuild = default)
    {
        internal abstract IMeshBuilder ConstructBuilder();
    }
}