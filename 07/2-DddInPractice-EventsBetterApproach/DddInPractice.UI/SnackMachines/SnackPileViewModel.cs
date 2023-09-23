using DddInPractice.Logic.SnackMachines;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace DddInPractice.UI.SnackMachines;

public class SnackPileViewModel
{
    private readonly Snack _snack;
    private readonly SnackPile _snackPile;

    public string Price => _snackPile.Price.ToString("C2", CultureInfo.GetCultureInfo("en-US"));
    public int Amount => _snackPile.Quantity;
    public int ImageWidth => GetImageWidth(_snack);
    public ImageSource Image =>
            (ImageSource)Application.Current.FindResource("img" + _snack.Name);


    public SnackPileViewModel((Snack, SnackPile) slotPile)
    {
        (_snack, _snackPile) = slotPile;
    }

    private int GetImageWidth(Snack snack)
    {
        if (snack == Snack.Chocolate)
            return 120;

        if (snack == Snack.Soda)
            return 70;

        if (snack == Snack.Gum)
            return 70;

        throw new ArgumentException();
    }
}
