using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BMI_Rest.Controllers
{
    public class BMIController : ApiController
    {
        public struct bmiStruct
        {
            public double bmi;
            public string risk;
            public string[] more;
        }

        [HttpGet]
        [ActionName("myBMI")]
        public double myBMI(int height, int weight)
        {
            double bmi = Math.Ceiling((double)weight / (height * height) * 703);
            return bmi;
        }

        [HttpGet]
        [ActionName("myHealth")]
        public bmiStruct myHealth(int height, int weight)
        {
            double bmi = myBMI(height, weight);
            string risk = getRiskFactor(bmi);
            string[] more = getAdditionalInfo();

            bmiStruct result = new bmiStruct
            {
                bmi = bmi,
                risk = risk,
                more = more
            };

            return result;
        }

        private string getRiskFactor(double bmi)
        {
            string risk;

            if (bmi < 18)
            {
                risk = "You are underweight with BMI < 18 - Blue Color";
            }
            else if (bmi >= 18 && bmi < 25)
            {
                risk = "You are normal with BMI ≥ 18 and < 25 - Green Color";
            }
            else if (bmi >= 25 && bmi < 30)
            {
                risk = "You are pre-obese with BMI between 25 and 30 – Purple Color";
            }
            else if (bmi >= 30)
            {
                risk = "You are obese with BMI greater than 30 - Red Color";
            }
            else
            {
                risk = "unknown";
            }

            return risk;
        }

        private string[] getAdditionalInfo()
        {
            string[] more = {"https://www.cdc.gov/healthyweight/assessing/bmi/index.html",
"https://www.nhlbi.nih.gov/health/educational/lose_wt/index.htm",
"https://www.ucsfhealth.org/education/body_mass_index_tool/" };

            return more;
        }
    }
}
