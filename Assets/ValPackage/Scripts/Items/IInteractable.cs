namespace ValPackage.Common.Items
{
    public interface IInteractable
    {
        bool CanInteract { get; }
        bool TryInteract();
    }
}