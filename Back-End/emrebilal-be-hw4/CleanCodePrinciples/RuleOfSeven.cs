using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanCodePrinciples
{
    class RuleOfSeven
    {
        #region Variables
        //Dirty Code
        public bool Validate(string password)
        {
            int minPasswordLenght = 5;
            int maxPasswordLenght = 25;

            if (password.Length < minPasswordLenght)
                return false;
            if (password.Length > maxPasswordLenght)
                return false;

            return true;
        }

        //Clean Code
        public bool Validate2(string password)
        {
            int minPasswordLenght = 5;
            if (password.Length < minPasswordLenght)
                return false;

            int maxPasswordLenght = 25;
            if (password.Length > maxPasswordLenght)
                return false;

            return true;
        }

        #endregion

        #region Parameters
        //Dirty Code
        public void SaveUser(string userName,
                             string password,
                             string email,
                             bool sendEmail,
                             bool sendBill,
                             bool printReport)
        {

        }

        //Clean Code
        public void SaveUser2(string userName, string password, string email)
        {

        }
        public void sendEmail()
        {

        }
        public void sendBill()
        {

        }
        public void printReport()
        {

        }

        #endregion

        #region Exctract Method
        //Dirty Code
        public void Sample()
        {
            if (true)
            {
                if (true)
                {
                    do
                    {
                        // 
                    } while (true);
                }
            }
        }

        //Clean Code
        public void Sample2()
        {
            if (true)
            {
                if (true)
                {
                    DoSomething();
                }
            }
        }
        private void DoSomething()
        {
            do
            {
                //
            } while (true);
        }

        #endregion

        #region Fail Fast
        //Dirty Code
        public void Login(string userName, string password)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                //
                if (!string.IsNullOrWhiteSpace(password))
                {
                    //Login
                }
                else
                {
                    throw new ArgumentNullException(); //Exception
                }
            }
            else
            {
                throw new ArgumentNullException(); //Exception
            }
        }

        //Clean Code
        public void Login2(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException();
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException();

            // Login
        }

        #endregion

        #region Return Early
        //Dirty Code
        private bool ValidUserName(string userName)
        {
            int minUserNameLenght = 5;
            int maxUserNameLenght = 20;
            bool isValid = false;

            if (userName.Length >= minUserNameLenght)
            {
                if (userName.Length <= maxUserNameLenght)
                {
                    bool isAlphaNumeric = userName.All(Char.IsLetterOrDigit);
                    if (isAlphaNumeric)
                    {
                        isValid = true;
                    }
                }
            }
            return isValid;
        }

        //Clean Code
        private bool ValidUserName2(string userName)
        {
            int minUserNameLenght = 6;
            if (userName.Length < minUserNameLenght)
                return false;
            int maxUserNameLenght = 20;
            if (userName.Length > maxUserNameLenght)
                return false;
            bool isAlphaNumeric = userName.All(Char.IsLetterOrDigit);
            if (!isAlphaNumeric)
                return false;

            return true;
        }
        #endregion
    }
}
