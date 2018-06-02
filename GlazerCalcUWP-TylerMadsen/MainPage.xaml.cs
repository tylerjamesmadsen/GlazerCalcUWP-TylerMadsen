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

namespace GlazerCalcUWP_TylerMadsen
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void inputQuantitySlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            txtQuantity.Text = inputQuantitySlider.Value.ToString();
            if (ValidateTextBoxInput(inputWidth) && ValidateTextBoxInput(inputHeight))
            {
                DisplayTotals();
            }

            if (orderDate != null)
            {
                ClearDate();
            }
        }

        private bool ValidateTextBoxInput(TextBox sender)
        {
            if (!Regex.IsMatch(sender.Text, @"^[1-9][0-9]{0,1}$"))
            {
                return false;
            }

            return true;
        }

        private double CalculateWoodLength(int width, int height)
        {
            return 2 * (width + height) * 3.25;
        }

        private double CalculateTotalWoodlength()
        {
            return CalculateWoodLength(
                Int32.Parse(inputWidth.Text), 
                Int32.Parse(inputHeight.Text)) 
                * inputQuantitySlider.Value;
        }

        private int CalculateGlassArea(int width, int height)
        {
            return 2 * (width + height);
        }

        private double CalculateTotalGlassArea()
        {
            return CalculateGlassArea(
                Int32.Parse(inputWidth.Text),
                Int32.Parse(inputHeight.Text)) 
                * inputQuantitySlider.Value;
        }

        private void width_TextChanged(Object sender, TextChangedEventArgs e)
        {
            ClearInputErrorWidth();

            if (ValidateTextBoxInput((TextBox)sender) == false)
            {
                Clear((TextBox)sender);
                DisplayInputErrorWidth();
                return;
            }

            if (ValidateTextBoxInput(inputHeight))
            {
                ClearErrorOrder();
                DisplayResults();
            }

            ClearDate();
        }

        private void height_TextChanged(Object sender, TextChangedEventArgs e)
        {
            ClearInputErrorHeight();

            if (ValidateTextBoxInput((TextBox)sender) == false)
            {
                Clear((TextBox)sender);
                DisplayInputErrorHeight();
                return;
            }

            if (ValidateTextBoxInput(inputWidth))
            {
                ClearErrorOrder();
                DisplayResults();
            }

            ClearDate();
        }

        private void Clear(TextBox sender)
        {
            sender.Text = "";
            ClearResults();
            ClearDate();
        }

        private void ClearResults()
        {
            woodLength.Text = "";
            glassArea.Text = "";
            lblWoodLength.Visibility = Visibility.Collapsed;
            lblGlassArea.Visibility = Visibility.Collapsed;
            lblUnitsFeet.Visibility = Visibility.Collapsed;
            lblUnitsSquareMeters.Visibility = Visibility.Collapsed;

            totalWoodLength.Text = "";
            totalGlassArea.Text = "";
            lblTotalWoodLength.Visibility = Visibility.Collapsed;
            lblTotalGlassArea.Visibility = Visibility.Collapsed;
            lblUnitsTotalFeet.Visibility = Visibility.Collapsed;
            lblUnitsTotalSquareMeters.Visibility = Visibility.Collapsed;
        }

        private void DisplayResults()
        {
            lblWoodLength.Visibility = Visibility.Visible;
            lblGlassArea.Visibility = Visibility.Visible;

            woodLength.Text = CalculateWoodLength(
                Int32.Parse(inputWidth.Text),
                Int32.Parse(inputHeight.Text))
                .ToString();
            lblUnitsFeet.Visibility = Visibility.Visible;
            glassArea.Text = CalculateGlassArea(
                Int32.Parse(inputWidth.Text.ToString()),
                Int32.Parse(inputHeight.Text.ToString()))
            .ToString();
            lblUnitsSquareMeters.Visibility = Visibility.Visible;

            DisplayTotals();
        }

        private void DisplayTotals()
        {
            lblTotalWoodLength.Visibility = Visibility.Visible;
            lblTotalGlassArea.Visibility = Visibility.Visible;
            totalWoodLength.Text = CalculateTotalWoodlength().ToString();
            lblUnitsTotalFeet.Visibility = Visibility.Visible;
            totalGlassArea.Text = CalculateTotalGlassArea().ToString();
            lblUnitsTotalSquareMeters.Visibility = Visibility.Visible;
        }

        private void DisplayErrors()
        {
            DisplayInputErrorWidth();
            DisplayInputErrorHeight();
            DisplayErrorOrder();
        }

        private void DisplayInputErrorWidth()
        {
            if (ValidateTextBoxInput(inputWidth) == false)
            {
                errorWidthInput.Visibility = Visibility.Visible;
            }
        }

        private void DisplayInputErrorHeight()
        {
            if (ValidateTextBoxInput(inputHeight) == false)
            {
                errorHeightInput.Visibility = Visibility.Visible;
            }
        }

        private void DisplayErrorOrder()
        {
            errorPlaceOrder.Visibility = Visibility.Visible;
        }

        private void ClearErrors()
        {
            ClearInputErrorWidth();
            ClearInputErrorHeight();
            ClearErrorOrder();
        }

        private void ClearInputErrorWidth()
        {
            errorWidthInput.Visibility = Visibility.Collapsed;
        }

        private void ClearInputErrorHeight()
        {
            errorHeightInput.Visibility = Visibility.Collapsed;
        }

        private void ClearErrorOrder()
        {
            errorPlaceOrder.Visibility = Visibility.Collapsed;
        }

        private void btnOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateTextBoxInput(inputWidth) == false ||
                ValidateTextBoxInput(inputHeight) == false)
            {
                DisplayErrorOrder();
                return;
            }

            DisplayDate();
        }

        private void DisplayDate()
        {
            lblOrderDate.Visibility = Visibility.Visible;
            orderDate.Text = DateTime.Now.ToLongDateString();
        }

        private void ClearDate()
        {
            lblOrderDate.Visibility = Visibility.Collapsed;
            orderDate.Text = "";
        }
    }
}