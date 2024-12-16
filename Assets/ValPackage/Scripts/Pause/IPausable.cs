namespace ValPackage.Common.PauseSystem
{
    /// <summary>
    /// Элемент, воспринимающий постановку игры на паузу
    /// </summary>
    public interface IPausable
    {
        void Resume();
        void Pause();
        void PauseImmidiatly();
    }
}