namespace Sxm.UIFactory.Components
{
    public abstract record MeshDescription(bool ForceBuild = default)
    {
        public abstract IMeshBuilder ConstructBuilder();
    }
}