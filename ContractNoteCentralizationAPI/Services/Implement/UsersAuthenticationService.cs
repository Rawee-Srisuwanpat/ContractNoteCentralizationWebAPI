using AutoMapper;
using ContractNoteCentralizationAPI.DataAccess.Interface;
using ContractNoteCentralizationAPI.Helper.AutoMapper;
using ContractNoteCentralizationAPI.Helper.ContextDb;
using ContractNoteCentralizationAPI.Model.LogLogin;
using ContractNoteCentralizationAPI.Model.ResetPassword;
using ContractNoteCentralizationAPI.Model.SendOtp;
using ContractNoteCentralizationAPI.Model.UsersAuthentication;
using ContractNoteCentralizationAPI.Model.VerifyOtp;
using ContractNoteCentralizationAPI.Services.Interface;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;
using System.DirectoryServices;
using ContractNoteCentralizationAPI.Helper.AD;
using ContractNoteCentralizationAPI.Model.ManageUser;
using System.Collections.Generic;
using ContractNoteCentralizationAPI.Model.ManageRole;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;

namespace ContractNoteCentralizationAPI.Services.Implement
{
    public class UsersAuthenticationService : IUsersAuthenticationService
    {
        private readonly IRegisterRepository _regisRepository;
        private readonly ContractNoteCentralizationDbContext _context;
        private readonly PrincipalModel _principal;
        public UsersAuthenticationService(IRegisterRepository regisRepository , ContractNoteCentralizationDbContext context , PrincipalModel principal)
        {
            this._regisRepository = regisRepository;
            this._context = context;
            this._principal = principal;
        }

        private (int,string) GenerateOtp(SendOtpReq req)
        {
            int Otp = 0;
            string email = string.Empty;
            try
            {
                int _min = 1000;
                int _max = 9999;
                Random _rdm = new Random();
                Otp =  _rdm.Next(_min, _max);

                using (var ctx = _context)
                {
                    var user = ctx.tbt_manage_user.FirstOrDefault(z => z.user_name == req.user_name  && z.authenticate == "1")
                        ?? throw new Exception("Email Not Found");

                    email = user.email;

                    user.otp = Otp.ToString();
                    user.otp_expire = DateTime.Now.AddMinutes(3);

                    ctx.Update(user);
                    ctx.SaveChanges();


                }
            }
            catch (Exception ex)
            {
                Otp = 0;
                throw;
            }
            return (Otp,email);
        }

        

        public ResetPasswordRes ResetPassword(ResetPasswordReq req)
        {
            var res = new ResetPasswordRes();
            try
            {
                using (var ctx = _context)
                {
                    var user = ctx.tbt_manage_user.FirstOrDefault(z => z.user_name == req.user_name)
                        ?? throw new Exception("email not found");

                    user.password = req.password;
                    ctx.SaveChanges();

                    res.status_code = "00";
                    res.status_text = "OK";

                }
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }

        public SendOtpRes SendOtp(SendOtpReq req)
        {
            var registerRes = new SendOtpRes();
            try
            {
                (int,string) OtpData = GenerateOtp(req);
                SmtpClient smtpClient = new SmtpClient();
                NetworkCredential basicCredential = new NetworkCredential("adminsiis", "12345678");
                MailMessage message = new MailMessage();
                MailAddress fromAddress = new MailAddress("summitcapitalleasing@summitcapital.co.th", "Summit Capital");

                smtpClient.Host = "mail.summitcapital.co.th";
                //smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = basicCredential;
                //smtpClient.EnableSsl = true;
                smtpClient.Port = 25;

                

                message.From = fromAddress;
                //message.CC = sCc;
                message.Subject = "SUMMIT Forgot Password";

                //Set IsBodyHtml to true means you can send HTML email.
                message.IsBodyHtml = true;

                message.Priority = MailPriority.High;
                message.Body = OtpData.Item1.ToString();

                string email = OtpData.Item2;
                     
                string MailTo = email;
                string[] mail = MailTo.Split(',');
                foreach (string s in mail)
                {
                    if (s != "")
                    {
                        //message.Bcc.Add(new MailAddress(s));
                        message.To.Add(new MailAddress(s));
                    }
                }

                message.Sender = fromAddress;
                smtpClient.Send(message);

                registerRes.status_code = "00";
                registerRes.status_text = "The mail has already submited to " + MaskEmail(email);

            }
            catch (Exception)
            {

                throw;
            }
            return registerRes;
        }

        public VerifyOtpRes VerifyOtp(VerifyOtpReq req)
        {
            var res = new VerifyOtpRes();
            try
            {
                using (var ctx = _context)
                {
                    var user = ctx.tbt_manage_user.FirstOrDefault(z => z.user_name == req.user_name && z.otp == req.verify_code)
                        ?? throw new Exception("verify code Not match with email");


                    user = ctx.tbt_manage_user.FirstOrDefault(z => z.user_name == req.user_name && z.otp == req.verify_code && z.otp_expire >= DateTime.Now)
                    ?? throw new Exception("OTP Expire");


                    res.status_code = "00";
                    res.status_text = "OK";

                }
            }
            catch (Exception)
            {
                throw;
            }
            return res;
        }

        public LoginRes Login(LoginReq req)
        {
            var loginRes = new LoginRes();
            try
            {
               
                using (var ctx = _context)
                {
                    //var user = ctx.tbt_manage_user.FirstOrDefault(z => z.user_name == req.user_name) ?? new ManageUserDao() { authenticate = "0" };
                    var user = ctx.tbt_manage_user.FirstOrDefault(z => z.user_name == req.user_name && z.authenticate == "2") 
                        ?? throw new Exception("Please contact the channel Line : Summit Connex");

                    //if (user.authenticate == "1") // 
                    //{
                    //    var userlogin = ctx.tbt_manage_user.FirstOrDefault(z => z.user_name == req.user_name && z.password == req.password)
                    //        ?? throw new Exception("user_name Or Password incorrect");
                    //}
                    
                    
                    
                    //else if (user.authenticate == "2")
                    //var user = ctx.tbt_manage_user.FirstOrDefault(z => z.user_name == req.user_name && z.authenticate == "2") 
                    //    ?? throw new Exception("Please contact the channel Line : Summit Connex");

                    //if (user.authenticate == "1") // 
                    //{
                    //    var userlogin = ctx.tbt_manage_user.FirstOrDefault(z => z.user_name == req.user_name && z.password == req.password)
                    //        ?? throw new Exception("user_name Or Password incorrect");
                    //}
                    //else 
                    
                    if (user.authenticate == "2")
                    {
                        bool isFoundinAD = CheckUserAD(req.user_name, req.password);
                        if (!isFoundinAD) throw new Exception("Password is incorrect");
                    }
                    //else {
                    //    var user_register = ctx.tbt_register.FirstOrDefault(z => z.user_name == req.user_name && z.password == req.password)
                    //        ?? throw new Exception("User Register Not Found");
                    //}


                    LogLginDao logLginDao = new LogLginDao();
                    logLginDao.Id = 0;
                    logLginDao.create_date = DateTime.Now;
                    logLginDao.update_date = DateTime.Now;
                    logLginDao.create_by = req.user_name;
                    logLginDao.update_by = req.user_name;
                    logLginDao.authenticate = user.authenticate;
                    logLginDao.user_name = req.user_name.ToUpper();

                    ctx.Add(logLginDao);

                    ctx.SaveChanges();

                    User user1 = new User();
                    user1.UserName = req.user_name;
                    //user1.Role = user_register.Role_code;
                    // user1.Role =  "Admin";
                    //user1.Role = user.role


                     loginRes.status_code = "00"; 
                    loginRes.status_text = "OK";

                    
                     
                    ManageRoleService manageRoleService = new ManageRoleService(this._context);

                    var payload = manageRoleService.Search(new ManageRoleReq()).payload;
                    var role = payload.Where(z => z.Id_tbm_Role.ToString() == user.role).ToList();

                    

                    //loginRes.payload = user1;


                    loginRes.payload = role ;
                    
                }
            }
            catch (Exception ex)
            {
                loginRes.status_code = "99";
                loginRes.status_text = ex.Message;
            }
            return loginRes;
        }
        private bool CheckUserAD(string username, string password)
        {
            try
            {
                var Domain = _principal.domain;
                var UserAdmin = _principal.userAdmin;
                var PassAdmin = _principal.passAdmin;

                bool isValid = false;
                UserPrincipal usr;

                PrincipalContext pc = new PrincipalContext(ContextType.Domain, Domain, null, UserAdmin, PassAdmin);

               

                
                usr = UserPrincipal.FindByIdentity(pc, username);

                //string? email = usr.EmailAddress;

                bool b = usr.IsAccountLockedOut();

                if (b)
                    usr.UnlockAccount();

                isValid = pc.ValidateCredentials(username, password);

                if (isValid)
                {
                    return true;
                    //userADStatus = new UserADStatusResModel()
                    //{
                    //    massage = "Log in Successfully.",
                    //    status = "Pass",
                    //    success = "true"
                    //};
                }
                else {

                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private string MaskEmail( string s)
        {
            string _PATTERN = @"(?<=[\w]{1})[\w-\._\+%\\]*(?=[\w]{1}@)|(?<=@[\w]{1})[\w-_\+%]*(?=\.)";

            if (!s.Contains("@"))
                return new String('*', s.Length);
            if (s.Split('@')[0].Length < 4)
                return @"*@*.*";
            return Regex.Replace(s, _PATTERN, m => new string('*', m.Length));
        }


    }
}
