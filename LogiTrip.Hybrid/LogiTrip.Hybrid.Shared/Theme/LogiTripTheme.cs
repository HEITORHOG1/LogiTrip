using MudBlazor;
using MudBlazor.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogiTrip.Hybrid.Shared.Theme
{
    public class LogiTripTheme
    {
        public static MudTheme DefaultTheme => new MudTheme
        {
            PaletteLight = new PaletteLight
            {
                Primary = new MudColor("#512BD4"),
                Secondary = new MudColor("#8A6FE8"),
                AppbarBackground = new MudColor("#512BD4"),
                Background = new MudColor("#F5F5F5"),
                DrawerBackground = new MudColor("#FFF"),
                DrawerText = "rgba(0,0,0, 0.7)",
                Success = new MudColor("#4CAF50")
            },
            LayoutProperties = new LayoutProperties
            {
                DefaultBorderRadius = "4px"
            },
            Typography = new Typography
            {
                Default = new Default
                {
                    FontFamily = new[] { "Roboto", "Helvetica", "Arial", "sans-serif" },
                    FontSize = "0.875rem",
                    FontWeight = 400,
                    LineHeight = 1.43,
                    LetterSpacing = ".01071em"
                },
                H1 = new H1
                {
                    FontSize = "2.5rem",
                    FontWeight = 300,
                    LineHeight = 1.167,
                    LetterSpacing = "-.01562em"
                }
                // Add other typography settings as needed
            }
        };
    }
}
