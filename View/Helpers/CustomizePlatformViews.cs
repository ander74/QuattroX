#region COPYRIGHT
// ===============================================
//   Copyright 2024 - Quattro X 2.0 - A. Herrero    
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

namespace QuattroX.View.Helpers;


public static class CustomizePlatformViews {


    public static void CustomizeViews() {

        Microsoft.Maui.Handlers.TimePickerHandler.Mapper.AppendToMapping("TimePickerBorderless", (handler, view) => {
#if ANDROID

            handler.PlatformView.TextAlignment = Android.Views.TextAlignment.Center;

#endif
        });


        Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping("PickerFocusable", (handler, view) => {
#if ANDROID

            handler.PlatformView.Focusable = false;

#endif
        });

    }

}
