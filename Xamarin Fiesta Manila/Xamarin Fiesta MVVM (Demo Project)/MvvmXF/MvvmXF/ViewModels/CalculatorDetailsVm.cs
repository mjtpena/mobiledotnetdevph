using MvvmXF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MvvmXF.ViewModels
{
    public class CalculatorDetailsVm : INotifyPropertyChanged
    {
        CalculatorDetails _calculatorDetails = new CalculatorDetails();

        public int FirstNumber
        {
            get { return _calculatorDetails.FirstNumber; }
            set
            {
                _calculatorDetails.FirstNumber = value;
                GetSum = _calculatorDetails.FirstNumber + _calculatorDetails.SecondNumber;
                OnPropertyChanged();
            }
        }

        public int SecondNumber
        {
            get { return _calculatorDetails.SecondNumber; }
            set
            {
                _calculatorDetails.SecondNumber = value;
                GetSum = _calculatorDetails.FirstNumber + _calculatorDetails.SecondNumber;
                OnPropertyChanged();
            }
        }

        private int _getSum;

        public int GetSum
        {
            get { return _getSum; }
            set
            {
                _getSum = value;
                OnPropertyChanged();
            }
        }

        public Command GetSumCommand
        {
            get
            {
                return new Command(() => {
                    GetSum = _calculatorDetails.FirstNumber + _calculatorDetails.SecondNumber;
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
