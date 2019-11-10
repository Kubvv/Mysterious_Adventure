﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using RumbleJungle.Model;
using System;

namespace RumbleJungle.ViewModel
{
    public class OptionsViewModel : ViewModelBase
    {
        private double currentRatio = Config.JungleWidth / Config.JungleHeight;

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
                        Set(nameof(JungleHeight), ref jungleHeight, (int)Math.Round(value / currentRatio));
                        Set(ref jungleWidth, value);
                    }
                    else
                    {
                        Set(ref jungleWidth, jungleWidth);
                    }
                }
                else
                {
                    currentRatio = value / JungleHeight;
                    Set(ref jungleWidth, value);
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
                        Set(nameof(JungleWidth), ref jungleWidth, (int)Math.Round(value * currentRatio));
                        Set(ref jungleHeight, value);
                    }
                    else
                    {
                        Set(ref jungleHeight, jungleHeight);
                    }
                }
                else
                {
                    currentRatio = JungleWidth / value;
                    Set(ref jungleHeight, value);
                }
            }
        }

        private bool keepRatio = Config.KeepRatio;
        public bool KeepRatio
        {
            get => keepRatio;
            set => Set(ref keepRatio, value);
        }

        private RelayCommand saveOptions;
        public RelayCommand SaveOptions => saveOptions ?? (saveOptions = new RelayCommand(() => ExecuteSaveOptions()));

        private void ExecuteSaveOptions()
        {
            Config.SetJungleSize(JungleWidth, JungleHeight);
            Config.SetKeepRatio(KeepRatio);
        }
    }
}
