using Lesson2_Homework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson2_Homework
{
    public class Validator : IValidator<Work_order>
    {
        public bool Check(Work_order work_Order)
        {
            bool valid = true;

            if (work_Order._Car == null || work_Order._Client == null || work_Order._Services == null)
            {
                Console.WriteLine("Not all required data has been entered.");
                valid = false;
            }
           
            if(work_Order._Car.Engine == "Electrical")
            {
                foreach(var wos in work_Order._Services)
                {
                    if (wos.Description == "Replacing filters and oils")
                        valid = false;
                }
            }

            if(work_Order._Car.Engine == "Gas" || work_Order._Car.Engine == "Petrol" || work_Order._Car.Engine == "Diesel")
            {
                foreach (var wos in work_Order._Services)
                {
                    if (wos.Description == "Battery Replacement")
                        valid = false;
                }
            }

            if (work_Order._Car.VIN.Length != 17)
                valid = false;

            return valid;
        }
    }
}
