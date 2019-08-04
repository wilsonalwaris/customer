using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class CustomerRepositoryHelper : ICustomerRepositoryHelper
    {
        public bool DatabaseDoesNotExist(CustomerContext customerContext)
        {
            //if db already exists open a connection and return
            if (customerContext.Database.CanConnect())
            {
                customerContext.Database.OpenConnection();
                return false;
            }

            customerContext.Database.OpenConnection();
            return !customerContext.Database.EnsureCreated();
        }

    }
}
