using API.Base;
using API.Context;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        private readonly MyContext myContext;
        public IConfiguration _configuration;
        public EmployeesController(EmployeeRepository employeeRepository, MyContext myContext, IConfiguration configuration) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this.myContext = myContext;
            this._configuration = configuration;
        }

        [Route("Register")]
        [HttpPost]
        public ActionResult Register(RegisterVM registerVM)
        {
            var result = employeeRepository.Register(registerVM);
            if (result == 2)
            {
                var nik = registerVM.NIK;
                return BadRequest(new { status = HttpStatusCode.BadRequest, NIK = nik, message = "Data gagal dimasukkan: NIK yang Anda masukkan sudah terdaftar!" });
            }
            else if (result == 3)
            {
                var phone = registerVM.Phone;
                return BadRequest(new { status = HttpStatusCode.BadRequest, phone = phone, message = "Data gagal dimasukkan: Phone yang Anda masukkan sudah terdaftar!" });
            }
            else if (result == 4)
            {
                var email = registerVM.Email;
                return BadRequest(new { status = HttpStatusCode.BadRequest, email = email, message = "Data gagal dimasukkan: Email yang Anda masukkan sudah terdaftar!" });
            }
            else if (result == 5)
            {
                var univId = registerVM.UniversityId;
                return BadRequest(new { status = HttpStatusCode.BadRequest, University_id = univId, message = "Data gagal dimasukkan: ID University yang Anda masukkan tidak terdaftar!" });
            }
            return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data berhasil dimasukkan" });
        }

        /*[Authorize(Roles = "Director, Manager")]*/
        [Route("Profile")]
        [HttpGet]
        public ActionResult<RegisterVM> GetProfile()
        {
            var getProfile = employeeRepository.GetProfile();
            if (getProfile.ToList().Count > 0)
            {
                return Ok(getProfile);
            }
            else
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = getProfile, message = "Tidak ada data di sini" });
            }
        }

        [Authorize(Roles = "Director")]
        [Route("SignManager")]
        [HttpPost]
        public ActionResult SignManager(SignManagerVM signManagerVM)
        {
            var result = employeeRepository.SignManager(signManagerVM);
            if (result == 2)
            {
                var nik = signManagerVM.NIK;
                return BadRequest(new 
                { 
                    status = HttpStatusCode.BadRequest, 
                    NIK = nik, 
                    message = "Data gagal dimasukkan: NIK yang Anda masukkan tidak terdaftar!" 
                });
            }
            return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data berhasil dimasukkan" });
        }

        /*[Authorize]
        [Route("GetMyProfile")]
        [HttpPost]
        public ActionResult Get(SignManagerVM signManagerVM)
        {
            var result = employeeRepository.SignManager(signManagerVM);
            if (result == 2)
            {
                var nik = signManagerVM.NIK;
                return BadRequest(new { status = HttpStatusCode.BadRequest, NIK = nik, message = "Data gagal dimasukkan: NIK yang Anda masukkan tidak terdaftar!" });
            }
            return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data berhasil dimasukkan" });
        }*/

        [HttpGet("Profile/{NIK}")]
        public ActionResult GetProfil(string NIK)
        {
            var get = employeeRepository.GetProfil(NIK);
            if (get != null)
            {
                /*return Ok(new { status = HttpStatusCode.OK, result = get, message = "Data berhasil ditampilkan" });*/
                return Ok(get);
            }
            else
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = get, message = $"Data dengan NIK {NIK} tidak ditemukan" });
            }
        }

        [Route("Login")]
        [HttpPost]
        public ActionResult Login(LoginVM loginVM)
        {
            var result = employeeRepository.Login(loginVM);
            if (result == 2)
            {
                var email = loginVM.Email;
                return BadRequest(new 
                { 
                    status = HttpStatusCode.BadRequest, 
                    Email = email, 
                    message = "Login gagal: Email yang Anda masukkan tidak terdaftar!" 
                });
            }
            else if (result == 3)
            {
                return BadRequest(new 
                { 
                    status = HttpStatusCode.BadRequest, 
                    message = "Login gagal: Password yang Anda masukkan salah!" 
                });
            }
            var getUserData = (from e in myContext.Employees
                               join a in myContext.Accounts on e.NIK equals a.NIK
                               join ar in myContext.AccountRoles on a.NIK equals ar.NIK
                               join r in myContext.Roles on ar.RoleId equals r.RoleId
                               orderby e.NIK
                               select new
                               {
                                   NIK = e.NIK,
                                   Email = e.Email,
                                   Role = r.RoleName
                               }).Where(e => e.Email == loginVM.Email).ToList();
            List<string> listRole = new List<string>();
            foreach (var item in getUserData)
            {
                listRole.Add(item.Role);
            }
            var data = new LoginVM()
            {
                Email = loginVM.Email,
                Role = listRole.ToArray()
            };
            var claims = new List<Claim>
            {
                new Claim("email", data.Email)
            };
            foreach (var item in data.Role)
            {
                claims.Add(new Claim("roles", item.ToString()));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
                (
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn
                );
            var idToken = new JwtSecurityTokenHandler().WriteToken(token);
            claims.Add(new Claim("TokenSecurity", idToken.ToString()));
            return Ok(new JWTokenVM
            {
                /*Status = HttpStatusCode.OK,*/
                Token = idToken,
                Message = "Login berhasil"
            });
        }

        [Authorize]
        [HttpGet("TestJWT")]
        public ActionResult TestJWT()
        {
            return Ok("Test JWT Berhasil");
        }

        [Route("Gender")]
        [HttpGet]
        public ActionResult<RegisterVM> GetGender()
        {
            var getGender = employeeRepository.GetGender();
            if (getGender != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result = getGender, message = "Data berhasil ditampilkan" });
            }
            else
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = getGender, message = "Tidak ada data di sini" });
            }
        }

        [Route("GetRole")]
        [HttpGet]
        public ActionResult<RegisterVM> GetRole()
        {
            var getRole = employeeRepository.GetRole();
            if (getRole != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result = getRole, message = "Data berhasil ditampilkan" });
            }
            else
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = getRole, message = "Tidak ada data di sini" });
            }
        }

        [Route("GetSalary")]
        [HttpGet]
        public ActionResult<RegisterVM> GetSalary()
        {
            var getSalary = employeeRepository.GetSalary();
            if (getSalary != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result = getSalary, message = "Data berhasil ditampilkan" });
            }
            else
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = getSalary, message = "Tidak ada data di sini" });
            }
        }

        [Route("GetSalary2")]
        [HttpGet]
        public ActionResult<RegisterVM> GetSalary2()
        {
            var getSalary2 = employeeRepository.GetSalary2();
            if (getSalary2 != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result = getSalary2, message = "Data berhasil ditampilkan" });
            }
            else
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = getSalary2, message = "Tidak ada data di sini" });
            }
        }

        [Route("GetDegree")]
        [HttpGet]
        public ActionResult<RegisterVM> GetDegree()
        {
            var getDegree = employeeRepository.GetDegree();
            if (getDegree != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result = getDegree, message = "Data berhasil ditampilkan" });
            }
            else
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = getDegree, message = "Tidak ada data di sini" });
            }
        }
    }

    /*public class EmployeesController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepository;

        public EmployeesController(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        //ini dicomment
        *//*[HttpPost]
        public ActionResult Post(Employee employee)
        {
            var result = employeeRepository.Insert(employee);
            return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data berhasil dimasukkan" });
        }*//*

        //ini dicomment
        *//*[HttpPost]
        public ActionResult Post(Employee employee)
        {
            if (employeeRepository.Get(employee.NIK) == null)
            {
                var result = employeeRepository.Insert(employee);
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data berhasil dimasukkan" });
            }
            else
            {
                var result = employee.NIK;
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "NIK yang Anda masukkan sudah terdaftar!" });
            }
        }*//*

        [HttpPost]
        public ActionResult Post(Employee employee)
        {
            int output = employeeRepository.Insert(employee);
            if (output == 1)
            {
                var result = employee.NIK;
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data gagal dimasukkan: NIK yang Anda masukkan sudah terdaftar!" });
            }
            else if (output == 2)
            {
                var result = employee.Phone;
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data gagal dimasukkan: Phone yang Anda masukkan sudah terdaftar!" });
            }
            else if (output == 3)
            {
                var result = employee.Email;
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data gagal dimasukkan: Email yang Anda masukkan sudah terdaftar!" });
            }
            else
            {
                var result = employeeRepository.Insert(employee);
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data berhasil dimasukkan" });
            }
        }

        //ini dicomment
        *//*[HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
            IEnumerable<Employee> empty = Enumerable.Empty<Employee>();
            return employeeRepository.Get();
        }*//*

        [HttpGet]
        public ActionResult GetEmployees()
        {
            var getEmployee = employeeRepository.Get();
            if (getEmployee.ToList().Count > 0)
            {
                return Ok(new {status = HttpStatusCode.OK, result = getEmployee, message = "Data berhasil ditampilkan"});
            }
            else
            {
                return NotFound(new {status = HttpStatusCode.NotFound, result = getEmployee, message = "Tidak ada data di sini"});
            }
        }

        [HttpGet("{NIK}")]
        public ActionResult Get(string NIK)
        {
            var ada = employeeRepository.Get(NIK);
            if (ada != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result = ada, message = "Data berhasil ditampilkan" });
            }
            else
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = ada, message = $"Data dengan NIK {NIK} tidak ditemukan" });
            }
        }

        [HttpDelete("{NIK}")]
        public ActionResult Delete(string NIK)
        {
            var exist = employeeRepository.Get(NIK);
            try
            {
                var result = employeeRepository.Delete(exist.NIK);
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data berhasil dihapus" });
            }
            catch
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = exist, message = $"Data dengan NIK {NIK} tidak ditemukan" });
            }
        }

        [HttpPut("{NIK}")]
        public ActionResult Update(Employee employee, string NIK)
        {
            try
            {
                var result = employeeRepository.Update(employee);
                return Ok(new { status = HttpStatusCode.OK, message = $"Data dengan NIK {employee.NIK} berhasil diupdate" });
            }
            catch
            {
                return NotFound(new { status = HttpStatusCode.NotFound, message = "Data dengan NIK tersebut tidak ditemukan" });
            }
        }
    }*/
}
