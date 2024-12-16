namespace ValPackage.Common.Ui
{
    /// <summary>
    /// Дочерние компоненты в составе UiView, выполняют действия, вызываемые из UiView
    /// </summary>
    public interface IViewElement
    {
        void OnViewShow();

        void OnViewHide();
    }
}