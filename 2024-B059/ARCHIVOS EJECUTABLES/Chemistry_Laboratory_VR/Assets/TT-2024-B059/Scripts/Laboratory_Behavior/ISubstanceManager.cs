public interface ISubstanceManager
{
    void RegisterTracker(SubstanceTracker tracker);
    void OnSubstanceAdded(SubstanceTracker tracker, string substance);
}