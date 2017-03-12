using MvvmXF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmXF.Services
{
    public class CustomerInfoService
    {
        public List<CustomerInfo> GetCustomerInfo()
        {
            List<CustomerInfo> customerInfoList = new List<CustomerInfo>
            {
                new CustomerInfo
                {
                    CustomerName = "Hello",
                    CustomerAge = 33
                },
                new CustomerInfo
                {
                    CustomerName = "Xamarin Fiesta",
                    CustomerAge = 33
                }
            };

            return customerInfoList;
        }
    }
}
