namespace mpDrawOrderByLayer
{
    using Autodesk.AutoCAD.ApplicationServices.Core;
    using Autodesk.AutoCAD.Runtime;

    public class DrawOrderByLayerFunction
    {
        DrawOrderByLayer _drawOrderByLayer;

        [CommandMethod("ModPlus", "MpDrawOrderByLayer", CommandFlags.Modal)]
        public void StartFunction()
        {
#if !DEBUG
            ModPlusAPI.Statistic.SendCommandStarting(new ModPlusConnector());
#endif

            if (_drawOrderByLayer == null)
            {
                _drawOrderByLayer = new DrawOrderByLayer();
                var mainViewModel = new MainViewModel { ParentWindow = _drawOrderByLayer };
                _drawOrderByLayer.DataContext = mainViewModel;
                _drawOrderByLayer.Closed += (sender, args) =>
                {
                    _drawOrderByLayer = null;
                    Autodesk.AutoCAD.Internal.Utils.SetFocusToDwgView();
                };
            }

            if (_drawOrderByLayer.IsLoaded)
                _drawOrderByLayer.Activate();
            else
                Application.ShowModelessWindow(Application.MainWindow.Handle, _drawOrderByLayer);
        }
    }
}