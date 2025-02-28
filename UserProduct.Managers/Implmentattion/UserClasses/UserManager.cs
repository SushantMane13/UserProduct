
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserProduct.Managers.DTO.UserDTO;
using UserProduct.Managers.Interface.UserInterfaces;
using UserProduct.Services.Interface.UserInterfaces;
using UserProduct.Services.Models.Entity;
using UserProduct.Managers.Exceptions;

namespace UserProduct.Managers.Implmentattion.UserClasses
{
    public class UserManager : IUserManager
    {
        private readonly IUserService userService;
        private readonly IConfiguration configuration;
        private readonly IUserAddressManager userAddressManager;
        private readonly IUserEmailManager userEmailManager;
        private readonly IUserPersonalManager userPersonalManager;
        private readonly IUserPhoneManager userPhoneManager;
        private readonly DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

        public UserManager(IUserService userService,IConfiguration configuration, IUserAddressManager userAddressManager, IUserEmailManager userEmailManager, IUserPersonalManager userPersonalManager, IUserPhoneManager userPhoneManager)
        {
            this.userService = userService;
            this.configuration = configuration;
            this.userAddressManager = userAddressManager;
            this.userEmailManager = userEmailManager;
            this.userPersonalManager = userPersonalManager;
            this.userPhoneManager = userPhoneManager;
        }

        public async Task<List<ResponseUserDTO>> GetAllUsers()
        {
            var res= await userService.GetAllUsers();
            return res.Select(x => ResponseUserDTO.MapToUsersDTO(x)).ToList();
        }

        public async Task<ResponseUserDTO> GetUserById(int id)
        {

            List<string> exception = [];
            if (id <= 0)
                exception.Add("Enter Correct Id");

            var res = await userService.GetUserById(id);
            if (res == null)
                exception.Add("User is Not Present");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            return ResponseUserDTO.MapToUsersDTO(res);
        } 

        public async Task<RequestUserDTO> CreateUser(RequestUserDTO userDTO)
        {
            List<string> exception = [];
            if (string.IsNullOrEmpty(userDTO.Username.Trim()) || userDTO.Password.Trim().Length < 6 ||
                string.IsNullOrEmpty(userDTO.CreatedBy.Trim()) || !userDTO.CreatedBy.Contains('@')  || userDTO.ChangedBy != null )
                exception.Add("Enter Correct Details");

            var userByUsername = await userService.GetUserByUsername(userDTO.Username);
            if (userByUsername != null)
                exception.Add("Username Already Exist");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            User user= RequestUserDTO.MapToUser(userDTO);
            user.CreatedOn=currentDate;

            //UserAddress userAddress = UserAddressDTOforCreate.MapTouserAddress(userDTO.UserAddressDTO);
            //userAddress.CreatedOn=currentDate;

            //UserPersonal userPersonal=UserPersonalDTOforCreate.MapToUserPersonal(userDTO.UserPersonalDTO);
            //userPersonal.CreatedOn=currentDate;

            //List<UserEmail> emailList= userDTO.UserEmailDTO.Select(x=>UserEmailDTOforCreate.MapToUserEmail(x)).ToList();
            
            //List<UserPhone> phoneList= userDTO.UserPhoneDTO.Select(p=>UserPhoneDTOforCreate.MapToUserPhone(p)).ToList();

            //user.UserPhoneUsers = phoneList;
            //user.UserEmailUsers = emailList;
            //user.UserPersonalUser = userPersonal;
            //user.UserAddressUser= userAddress;

            user.Salt = GenerateSalt();
            user.Password = EncryptData(user.Password,user.Salt);

            var res=await userService.CreateUser(user);


            return RequestUserDTO.MapToUsersDTO(res);
        }

        public async Task<RequestUserDTO> UpdateUser(int id,RequestUserDTO userDTO)
        {
            List<string> exception = [];
            if ( string.IsNullOrEmpty(userDTO.Username.Trim()) || userDTO.Password.Trim().Length <6 ||
                !userDTO.CreatedBy.Contains('@')  || userDTO.ChangedBy == null || !userDTO.ChangedBy.Contains('@')  )
                exception.Add("Enter Correct Details");


            var checkUser =await userService.GetUserByUsername(userDTO.Username);
            if (checkUser != null && checkUser.UserId != id)
                exception.Add("Username Already Exist");
                        
            var user=await userService.GetUserById(id) ;
            if(user==null)
                exception.Add("User is Not Present");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            user.Username = userDTO.Username;
            user.ChangedBy = userDTO.ChangedBy;
            user.ChangedOn = currentDate;

            if (!ValidateEncryptedData(userDTO.Password, user.Password, user.Salt))
            {
                user.Salt = GenerateSalt();
                user.Password = EncryptData(userDTO.Password, user.Salt);
            }
             
            var res=await userService.UpdateUser(user);

            return RequestUserDTO.MapToUsersDTO(res);
        }

        public async Task<bool> DeleteUser(int id, string deletedBy,int deletedById)
        {
            List<string> exception = [];
            if (id <= 0 || string.IsNullOrEmpty(deletedBy.Trim()) || deletedById<=0)
                exception.Add("Enter Valid Details");

            var user = await userService.GetUserById(id);            
            if(user==null)
                exception.Add("User is Not Present");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            user.DeletedBy = deletedBy;
            user.DeletedOn=currentDate;

            var res = await userService.UpdateUser(user);

            await userAddressManager.DeleteUserAddressByUserId(id, deletedById);
            await userEmailManager.DeleteUserEmailByUserId(id, deletedById);
            await userPersonalManager.DeleteUserPersonalByUserId(id, deletedById);  
            await userPhoneManager.DeleteUserPhoneByUserId(id, deletedById);

            return true;
        }

        public async Task<object> Login(string username, string password)
        {
            List<string> exception = [];
            if (string.IsNullOrEmpty(username.Trim()) && string.IsNullOrEmpty(password.Trim()))
                exception.Add("Enter Correct Details");


            var user = await userService.GetUserByUsername(username);
            if(user==null)
                exception.Add("User is Not Exist");


            if (user!=null && !ValidateEncryptedData(password, user.Password, user.Salt))
                exception.Add("Enter Correct Password");

            if (exception.Count != 0)
                throw new ValidationException(String.Join(",\n", exception));

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub,configuration["Jwt:Subject"]),
                new Claim("Id",user.UserId.ToString()),
                new Claim("Username",user.Username)
            };


            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var signin= new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(10),
                signingCredentials: signin);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            var dbUser = ResponseUserDTO.MapToUsersDTO(user);

            var res = new
            {
                token = jwtToken,
                dbUser
            };


            return res;
        }

        private static string EncryptData(string valueToEncrypt,string salt)
        {
                byte[] saltedPassword = Encoding.UTF8.GetBytes(salt + valueToEncrypt);
               
                byte[] hash = SHA256.HashData(saltedPassword);
            return Convert.ToBase64String(hash);
            
        }

        private static string GenerateSalt()
        {
            RandomNumberGenerator random = RandomNumberGenerator.Create();
            byte[] salt = new byte[64];
            random.GetBytes(salt);
           
            return Convert.ToBase64String(salt);
        }

        private static bool ValidateEncryptedData(string valueToValidate, string passwordFromDatabase,string dbSalt)
        {
            string encryptedDbValue = passwordFromDatabase;
            string salt = dbSalt;
            byte[] saltedValue = Encoding.UTF8.GetBytes(salt + valueToValidate);

            byte[] hash = SHA256.HashData(saltedValue);
            string enteredValueToValidate = Convert.ToBase64String(hash);
            return encryptedDbValue.Equals(enteredValueToValidate);

        }

    }
}
