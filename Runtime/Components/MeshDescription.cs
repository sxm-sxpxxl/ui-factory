namespace SxmTools.UIFactory.Components
{
    public abstract record MeshDescription(bool ForceBuild = default)
    {
        public bool ForceBuild { get; set; } = ForceBuild;

        internal abstract IMeshBuilder ConstructBuilder();
    }
}