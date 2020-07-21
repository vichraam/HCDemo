using HealthCatalystService.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCatalystServiceTest
{
    public class TestHelper
    {
        public static People GetPeople(string firstName, string lastName, int age, string addressLine1, string addressLine2, string city, string state, string zip, string interests, string picture = null)
        {
            var people = new People
            {
                Id = 0,
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                AddressLine1 = addressLine1,
                AddressLine2 = addressLine2,
                City = city,
                State = state,
                Interests = interests,
                Zipcode = zip,
                PicturePath = picture
            };
            return people;
        }
    }
}
