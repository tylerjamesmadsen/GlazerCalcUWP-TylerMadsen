using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GlazerCalcUWP_TylerMadsen
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void quantitySlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            quantity.Text = quantitySlider.Value.ToString();
        }

        private bool validateNumericInput(TextBox sender)
        {
            if (!Regex.IsMatch(sender.Text, @"^[1-9][0-9]{0,2}$"))
            {
                return false;
            }
            return true;
        }

        private void btnOrderButton_Click(object sender, RoutedEventArgs e)
        {
            //woodLength.Text = CalculateWoodLength().ToString();
            //glassArea.Text = CalculateGlassArea().ToString();
            orderDate.Text = DateTime.Now.ToShortDateString();
        }

        private double CalculateWoodLength(int width, int height)
        {
            return 2 * (width + height) * 3.25;
        }

        private int CalculateGlassArea(int width, int height)
        {
            return 2 * (width + height);
        }

        private void width_TextChanged(Object sender, TextChangedEventArgs e)
        {
            if (validateNumericInput((TextBox)sender) == false)
            {
                Clear((TextBox)sender);
                return;
            }

            if (validateNumericInput(height) == true)
            {
                woodLength.Text =
                    CalculateWoodLength(Int32.Parse(width.Text.ToString()), Int32.Parse(height.Text.ToString()))
                    .ToString();
                glassArea.Text =
                CalculateGlassArea(Int32.Parse(width.Text.ToString()), Int32.Parse(height.Text.ToString()))
                .ToString();
            }
        }

        private void height_TextChanged(Object sender, TextChangedEventArgs e)
        {
            if (validateNumericInput((TextBox)sender) == false)
            {
                Clear((TextBox)sender);
                return;
            }

            if (validateNumericInput(width) == true)
            {
                woodLength.Text =
                    CalculateWoodLength(Int32.Parse(width.Text.ToString()), Int32.Parse(height.Text.ToString()))
                    .ToString();
                glassArea.Text =
                CalculateGlassArea(Int32.Parse(width.Text.ToString()), Int32.Parse(height.Text.ToString()))
                .ToString();
            }
        }

        private void Clear(TextBox sender)
        {
            sender.Text = "";
            woodLength.Text = "";
            glassArea.Text = "";
        }
    }
}