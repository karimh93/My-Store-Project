using MyStore.Repositories;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyStore.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customers> GetAllCustomers();
        Customers FindMyCustomerById(int id);
        Customers UpdateCustomer(Customers customerToUpdate);
        Customers AddCustomer(Customers addedCustomer);
        void DeleteCustomer(Customers deleteCustomer);
    }
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public IEnumerable<Customers> GetAllCustomers()
        {
            return customerRepository.GetAllCustomers();
        }

        public Customers FindMyCustomerById(int id)
        {
            var myCustomer = customerRepository.GetCustomerById(id);
            if (myCustomer != null)
            {
                return myCustomer;
            }
            return null;
        }

        public Customers UpdateCustomer(Customers customerToUpdate)
        {
            if (IsUniqueCompany(customerToUpdate.Companyname))
            {
                return customerRepository.Update(customerToUpdate);
            }
            else
            {
                return null;
            }
        }
    

        public Customers AddCustomer(Customers addedCustomer)
        {
            if (IsUniqueCompany(addedCustomer.Companyname))
            {
                return customerRepository.AddCustomer(addedCustomer);
            }
            else
            {
                return null;
            }
        }

        private bool IsUniqueCompany(string name)
        {
            return customerRepository.IsUniqueCompanyName(name);
        }

        public void DeleteCustomer(Customers deleteCustomer)
        {
            customerRepository.DeleteCustomer(deleteCustomer);
        }

    }
}
