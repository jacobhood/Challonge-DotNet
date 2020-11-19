namespace Challonge.Objects
{
    internal abstract class ChallongeObjectWrapper<TObject>
        where TObject : ChallongeObject
    {
        internal abstract TObject Item { get; set; }
    }
}
