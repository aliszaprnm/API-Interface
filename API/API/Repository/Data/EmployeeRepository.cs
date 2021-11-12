using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        private readonly MyContext myContext;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        public int Register(RegisterVM registerVM)
        {
            Employee employee = new Employee();
            University university = new University();
            var checkUniversity = myContext.Universities.Find(registerVM.UniversityId);
            var checkdata = myContext.Employees.Find(registerVM.NIK);
            var checkPhone = myContext.Employees.Where(x => x.Phone == registerVM.Phone).FirstOrDefault();
            var checkEmail = myContext.Employees.Where(x => x.Email == registerVM.Email).FirstOrDefault();
            employee.NIK = registerVM.NIK;
            if (checkdata != null)
            {
                return 2;
            }
            if (checkPhone != null)
            {
                return 3;
            }
            if (checkEmail != null)
            {
                return 4;
            }
            if (checkUniversity == null)
            {
                return 5;
            }
            
            employee.FirstName = registerVM.FirstName;
            employee.LastName = registerVM.LastName;
            employee.Phone = registerVM.Phone;
            employee.BirthDate = registerVM.BirthDate;
            employee.Salary = registerVM.Salary;
            employee.Email = registerVM.Email;
            employee.Gender = (Models.Gender)registerVM.Gender;
            myContext.Employees.Add(employee);
            myContext.SaveChanges();

            Account account = new Account();
            account.NIK = registerVM.NIK;
            account.Password = BCrypt.Net.BCrypt.HashPassword(registerVM.Password, GetRandomSalt());
            myContext.Accounts.Add(account);
            myContext.SaveChanges();

            Education education = new Education();
            education.Degree = registerVM.Degree;
            education.GPA = registerVM.GPA;
            education.UniversityId = registerVM.UniversityId;
            myContext.Educations.Add(education);
            myContext.SaveChanges();

            Profiling profiling = new Profiling();
            profiling.NIK = registerVM.NIK;
            profiling.EducationId = education.EducationId;
            myContext.Profilings.Add(profiling);
            myContext.SaveChanges();

            AccountRole accountRole = new AccountRole();
            accountRole.NIK = registerVM.NIK;
            accountRole.RoleId = registerVM.RoleId;
            myContext.AccountRoles.Add(accountRole);
            var result = myContext.SaveChanges();
            return result;
        }

        public IEnumerable<RegisterVM> GetProfile()
        {
            var query = (from e in myContext.Employees
                         join a in myContext.Accounts on e.NIK equals a.NIK
                         join p in myContext.Profilings on a.NIK equals p.NIK
                         join ed in myContext.Educations on p.EducationId equals ed.EducationId
                         join u in myContext.Universities on ed.UniversityId equals u.UniversityId
                         join ar in myContext.AccountRoles on a.NIK equals ar.NIK
                         join r in myContext.Roles on ar.RoleId equals r.RoleId
                         orderby e.NIK
                         select new RegisterVM
                         {
                             NIK = e.NIK,
                             FirstName = e.FirstName,
                             LastName = e.LastName,
                             Phone = e.Phone,
                             BirthDate = e.BirthDate,
                             Salary = e.Salary,
                             Email = e.Email,
                             Gender = (ViewModel.Gender)e.Gender,
                             Password = a.Password,
                             Degree = ed.Degree,
                             GPA = ed.GPA,
                             UniversityId = ed.UniversityId,
                             RoleId = r.RoleId
                         }).ToList();
            return query;
        }
        public IEnumerable<RegisterVM> GetProfil(string NIK)
        {
            var query = (from e in myContext.Employees
                         join a in myContext.Accounts on e.NIK equals a.NIK
                         join p in myContext.Profilings on a.NIK equals p.NIK
                         join ed in myContext.Educations on p.EducationId equals ed.EducationId
                         join u in myContext.Universities on ed.UniversityId equals u.UniversityId
                         join ar in myContext.AccountRoles on a.NIK equals ar.NIK
                         join r in myContext.Roles on ar.RoleId equals r.RoleId
                         orderby e.NIK
                         select new RegisterVM
                         {
                             NIK = e.NIK,
                             FirstName = e.FirstName,
                             LastName = e.LastName,
                             Phone = e.Phone,
                             BirthDate = e.BirthDate,
                             Salary = e.Salary,
                             Email = e.Email,
                             Gender = (ViewModel.Gender)e.Gender,
                             Password = a.Password,
                             Degree = ed.Degree,
                             GPA = ed.GPA,
                             UniversityId = ed.UniversityId,
                             RoleId = r.RoleId
                         }).Where(e => e.NIK == NIK).ToList();
            return query;
        }
        public int Login(LoginVM loginVM)
        {
            var checkEmail = myContext.Employees.Where(x => x.Email == loginVM.Email).FirstOrDefault();
            if (checkEmail == null)
            {
                return 2;
            }
            var checkNIK = checkEmail.NIK;
            var checkAcc = myContext.Accounts.Find(checkNIK);

            bool validPass = BCrypt.Net.BCrypt.Verify(loginVM.Password, checkAcc.Password);
            if (validPass == false)
            {
                return 3;
            }
            return 4;
        }

        public int SignManager(SignManagerVM signManagerVM)
        {
            Employee employee = new Employee();
            
            var checkdata = myContext.Employees.Find(signManagerVM.NIK);
            employee.NIK = signManagerVM.NIK;
            if (checkdata == null)
            {
                return 2;
            }
            AccountRole accountRole = new AccountRole();
            accountRole.NIK = signManagerVM.NIK;
            accountRole.RoleId = 1;
            myContext.AccountRoles.Add(accountRole);
            var result = myContext.SaveChanges();
            return result;
        }

        public IEnumerable GetGender()
        {
            var result = from emp in myContext.Employees
                         group emp by emp.Gender into x
                         select new
                         {
                             gender = x.Key,
                             value = x.Count()
                         };
            return result;
        }

        public IEnumerable GetRole()
        {
            var result = from ar in myContext.AccountRoles
                         group ar by ar.RoleId into x
                         select new
                         {
                             role = x.Key,
                             value = x.Count()
                         };
            return result;
        }

        public IEnumerable GetSalary()
        {
            var result = from e in myContext.Employees
                         group e by e.Salary into x
                         select new
                         {
                             salary = x.Key,
                             value = x.Count()
                         };
            return result;
        }

        public object[] GetSalary2()
        {
            var label1 = (from emp in myContext.Employees
                          select new
                          {
                              label = "Rp2.000.000 - Rp5.000.000",
                              value = myContext.Employees.Where(a => a.Salary <= 5000000 && a.Salary >= 2000000).Count()
                          }).First();
            var label2 = (from emp in myContext.Employees
                          select new
                          {
                              label = "Rp5.000.001 - Rp10.000.000",
                              value = myContext.Employees.Where(a => a.Salary <= 10000000 && a.Salary >= 5000001).Count()
                          }).First();
            var label3 = (from emp in myContext.Employees
                          select new
                          {
                              label = "< Rp2.000.000",
                              value = myContext.Employees.Where(a => a.Salary < 2000000).Count()
                          }).First();
            var label4 = (from emp in myContext.Employees
                          select new
                          {
                              label = "> Rp10.000.000",
                              value = myContext.Employees.Where(a => a.Salary > 10000000).Count()
                          }).First();
            List<Object> result = new List<Object>();
            result.Add(label3);
            result.Add(label1);
            result.Add(label2);
            result.Add(label4);
            return result.ToArray();
        }

        public IEnumerable GetDegree()
        {
            var result = from p in myContext.Profilings
                         join e in myContext.Educations
                         on p.EducationId equals e.EducationId
                         group e by e.Degree into x
                         select new
                         {
                             degree = x.Key,
                             value = x.Count()
                         };
            return result;
        }
        /*public IEnumerable<RegisterVM> GetEmployeeProfile()
        {
            var query = (from e in myContext.Employees
                         join a in myContext.Accounts on e.NIK equals a.NIK
                         join p in myContext.Profilings on a.NIK equals p.NIK
                         join ed in myContext.Educations on p.EducationId equals ed.EducationId
                         join u in myContext.Universities on ed.UniversityId equals u.UniversityId
                         join ar in myContext.AccountRoles on a.NIK equals ar.NIK
                         join r in myContext.Roles on ar.RoleId equals r.RoleId
                         orderby e.NIK
                         select new RegisterVM
                         {
                             NIK = e.NIK,
                             FirstName = e.FirstName,
                             LastName = e.LastName,
                             Phone = e.Phone,
                             BirthDate = e.BirthDate,
                             Salary = e.Salary,
                             Email = e.Email,
                             Gender = (ViewModel.Gender)e.Gender,
                             Password = a.Password,
                             Degree = ed.Degree,
                             GPA = ed.GPA,
                             UniversityId = ed.UniversityId,
                             RoleId = r.RoleId
                         }).Where(e => e.Email == Register.Email).ToList();
            return query;
        }*/
    }
}
