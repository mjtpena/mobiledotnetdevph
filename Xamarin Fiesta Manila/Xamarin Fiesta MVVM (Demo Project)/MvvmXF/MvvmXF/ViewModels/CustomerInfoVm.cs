using MvvmXF.Models;
using MvvmXF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MvvmXF.ViewModels
{
    public class CustomerInfoVm : INotifyPropertyChanged
    {
        public CustomerInfoVm()
        {
            InitializeGetCustomerInfoData();
        }

        CustomerInfoService customerInfoService = new CustomerInfoService();
        private List<CustomerInfo> _listOfCustomerInfo;

        public List<CustomerInfo> ListOfCustomerInfo
        {
            get { return _listOfCustomerInfo; }
            set
            {
                _listOfCustomerInfo = value;
                OnPropertyChanged();
            }
        }

        public void InitializeGetCustomerInfoData()
        {
            ListOfCustomerInfo = customerInfoService.GetCustomerInfo();
        }

        // Contains my PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
