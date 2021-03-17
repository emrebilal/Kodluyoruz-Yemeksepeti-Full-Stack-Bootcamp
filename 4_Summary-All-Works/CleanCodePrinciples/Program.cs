using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanCodePrinciples
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Boolean Comparisons and Expressions 
            bool isValid = false;
            //Dirty Code
            if (isValid == true)
            {

            }
            //this use is more convenient for simplicity and readability
            if (isValid)
            {

            }

            /*-------------------------------------------------------------*/

            int note = 0;
            bool isPassed;
            //Dirty Code
            if (note > 50)
            {
                isPassed = true;
            }
            else
            {
                isPassed = false;
            }
            //reduced signal to noise ratio is preferred for legibility
            bool isPassed2 = note > 50;

            #endregion

            #region Positive Conditional
            bool hasCode = true;
            bool hasNotCode = false;
            //Dirty Code
            if (!hasNotCode) { }

            //Be Positive!
            if (hasCode) { }

            #endregion

            #region Ternary If
            int x = 10, y = 100;
            string result;
            //Dirty Code
            if (x > y)
            {
                result = "x is greater than y";
            }
            else if (x < y)
            {
                result = "x is less than y";
            }
            else
            {
                result = "x is equal to y";
            }

            //Ternary If
            string result2 = x > y ? "x is greater than y"
                                    : x < y ? "x is less than y"
                                        : "x is equal to y";

            #endregion

            #region Definitions
            //Dirty Code
            int value = 10;
            //
            //a lot of code
            //
            if (value > 5) //what is value?
            {

            }

            //Clean Code
            int value2 = 10;
            if (value2 > 5)
            {

            }
            #endregion

            #region Complex Conditions
            string member = "Gold";
            DateTime membershipDate = new DateTime(2016, 12, 25);

            //Dirty Code
            if ((DateTime.Now.Year - membershipDate.Year) >= 5 && member == "Gold")
            {

            }

            //Clean Code
            bool isProvide = (DateTime.Now.Year - membershipDate.Year) >= 5 && member == "Gold";
            if (isProvide)
            {

            }
            #endregion

            #region Linq
            List<User> users = new List<User>();
            List<User> members = new List<User>();

            foreach (var user in users)
            {
                if (user.TypeOfUser == UserType.Member)
                {
                    members.Add(user);
                }
            }

            //Linq
            members = users.Where(u => u.TypeOfUser == UserType.Member).ToList();
            #endregion

        }

        #region Strongly Type
        //Dirty Code
        public void CompareString()
        {
            User user = new User();
            if (user.UserType == "Member")
            {

            }
        }
        //Strongly Type
        public void CompareString2()
        {
            User user = new User();
            if (user.TypeOfUser == UserType.Member)
            {

            }
        }

        #endregion

    }
    class User
    {
        public UserType TypeOfUser { get; set; }
        public string UserType { get; set; }

        #region Positive Conditional 2
        List<int> userId = new List<int> { 1, 2 };
        //Dirty Code
        public bool IsEmpty()
        {
            return !userId.Any();
        }

        //Be Positive!
        public bool IsEmpty2()
        {
            return userId.Count == 0;
        }
        #endregion

    }
    public enum UserType
    {
        Free,
        Member
    }
}
