using System;
using System.Collections.Generic;
namespace BAL.Common
{
    public static class DataValidation
    {
        public static string mobileError = "Mobile No. is not valid.";
        public static string zipCodeError = "Zip Code is not valid.";
        public static string emailError = "Email is not valid.";
        public static string success = "Saved Successfully.";
        public static string deleteSuccess = "Deleted Successfully.";
        public static string bookingCancellationSuccess = "Booking is cancelled successfully.";
        public static string bookingCancellationFailed = "You cannot cancel a booking with 24 hours left.";
        public static string dbError = "Database Error occured while executing this query.";
        public static string userdeleted = "User deleted Successfully";
        public static string emailExist = "Email already exist";
        public static string accidentLimit = "Accident limit completed";
        public static bool IsMobileValid(string mobileNo)
        {
            return true;
        }
        public static bool IsZipCodeValid(int zipCode)
        {
            return Math.Floor(Math.Log10(zipCode) + 1) == 5;
        }
        public static bool IsEmailValid(string email)
        {
            return true;
        }
        public static List<string> CheckValidation(string mobile, int zipCode, string email)
        {
            var responseMessage = new List<string>();
            if (mobile != "")
            {
                responseMessage.Add(IsMobileValid(mobile) ? success : mobileError);
            }
            if (zipCode > 0)
            {
                responseMessage.Add(IsZipCodeValid(zipCode) ? success : zipCodeError);
            }
            if (email == "") return responseMessage;
            responseMessage.Add(IsEmailValid(email) ? success : emailError);
            return responseMessage;
        }
        public static List<string> CheckTwoMobileValidation(string mobile1, string mobile2)
        {
            var responseMessage = new List<string>();
            if (mobile1 != "")
            {
                responseMessage.Add(IsMobileValid(mobile1) ? success : "Mobile #1 No. is not valid.");
            }
            else
            {
                responseMessage.Add(success);
            }
            if (mobile2 != "")
            {
                responseMessage.Add(IsMobileValid(mobile2) ? success : "Mobile #2 No. is not valid.");
            }
            else
            {
                responseMessage.Add(success);
            }
            for (var i = 0; i < responseMessage.Count; i++)
            {
                if (responseMessage[i] == success) continue;
                responseMessage.Add("notValid");
                break;
            }
            return responseMessage;
        }
    }
}