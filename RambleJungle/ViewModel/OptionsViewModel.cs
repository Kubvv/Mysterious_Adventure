using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RambleJungle.Model;
using System;

namespace RambleJungle.ViewModel
{
    public class OptionsViewModel : ObservableRecipient
    {
        private double currentRatio = (double)Config.JungleWidth / Config.JungleHeight;

        public static int MinJungleWidth => Config.MINJUNGLEWIDTH;
        public static int MaxJungleWidth => Config.MAXJUNGLEWIDTH;
        public static int MinJungleHeight => Config.MINJUNGLEHEIGHT;
        public static int MaxJungleHeight => Config.MAXJUNGLEHEIGHT;

        private int jungleWidth = Config.JungleWidth;
        public int JungleWidth
        {
            get => jungleWidth;
            set
            {
                if (KeepRatio)
                {
                    int newHeight = (int)Math.Round(value / currentRatio);
                    if (newHeight >= MinJungleHeight && newHeight <= MaxJungleHeight)
                    {
                        SetProperty(ref jungleHeight, (int)Math.Round(value / currentRatio));
                        OnPropertyChanged(nameof(JungleHeight));
                        SetProperty(ref jungleWidth, value);
                    }
                }
                else
                {
                    currentRatio = value / JungleHeight;
                    SetProperty(ref jungleWidth, value);
                }
            }
        }

        private int jungleHeight = Config.JungleHeight;
        public int JungleHeight
        {
            get => jungleHeight;
            set
            {
                if (KeepRatio)
                {
                    int newWidth = (int)Math.Round(value * currentRatio);
                    if (newWidth >= MinJungleWidth && newWidth <= MaxJungleWidth)
                    {
                        SetProperty(ref jungleWidth, (int)Math.Round(value * currentRatio));
                        OnPropertyChanged(nameof(JungleWidth));
                        SetProperty(ref jungleHeight, value);
                    }
                }
                else
                {
                    currentRatio = JungleWidth / value;
                    SetProperty(ref jungleHeight, value);
                }
            }
        }

        private bool keepRatio = Config.KeepRatio;
        public bool KeepRatio
        {
            get => keepRatio;
            set => SetProperty(ref keepRatio, value);
        }

        private bool superRambler = Config.SuperRambler;
        public bool SuperRambler
        {
            get => superRambler;
            set => SetProperty(ref superRambler, value);
        }

        private bool pacifistRambler = Config.PacifistRambler;
        public bool PacifistRambler
        {
            get => pacifistRambler;
            set => SetProperty(ref pacifistRambler, value);
        }

        public OptionsViewModel()
        {
            SaveOptions = new RelayCommand(ExecuteSaveOptions);
        }

        public RelayCommand SaveOptions { get; private set; }

        private void ExecuteSaveOptions()
        {
            Config.SetJungleSize(JungleWidth, JungleHeight);
            Config.SetKeepRatio(KeepRatio);
            Config.SetSuperRambler(SuperRambler);
            Config.SetPacifistRambler(PacifistRambler);
        }
    }
}
