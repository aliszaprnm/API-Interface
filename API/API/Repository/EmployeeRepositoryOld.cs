using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    public class EmployeeRepositoryOld : IEmployeeRepository
    {
        private readonly MyContext context;
        public EmployeeRepositoryOld(MyContext context)
        {
            this.context = context;
        }
        public int Delete(string NIK)
        {
            var item = context.Employees.Find(NIK);
            context.Remove(item);
            var result = context.SaveChanges();
            return result;
        }

        public IEnumerable<Employee> Get()
        {
            return context.Employees.ToList();
        }

        public Employee Get(string NIK)
        {
            return context.Employees.Find(NIK);
        }

        public int Insert(Employee employee)
        {
            var checkdata = context.Employees.Find(employee.NIK);
            var checkPhone = context.Employees.Where(e => e.Phone == employee.Phone).FirstOrDefault();
            var checkEmail = context.Employees.Where(e => e.Email == employee.Email).FirstOrDefault();
            if (checkdata == null)
            {
                if (checkPhone == null)
                {
                    if (checkEmail == null)
                    {
                        context.Employees.Add(employee);
                        context.SaveChanges();
                        return 4;
                    }
                    return 3;
                }
                return 2;
            }
            return 1;
        }

        public int Update(Employee employee)
        {
            context.Entry(employee).State = EntityState.Modified;
            var result = context.SaveChanges();
            return result;
        }
    }
}
