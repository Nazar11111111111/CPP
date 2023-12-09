using Microsoft.AspNetCore.Mvc;
using LaboratoryWorks;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    public class LaboratoryWorksController : Controller
    {
        //// LabsLibrary main page controller
        //[Authorize]
        //public IActionResult Index()
        //{
        //    return View();
        //}


        //// Lab1 controller
        //[Authorize]
        //public IActionResult Lab1()
        //{
        //    return View();
        //}

        //[Authorize]
        //[HttpPost]
        //public string Lab1(string input_path, string output_path)
        //{
        //    return LaboratoryWorks.Lab1.Run(input_path, output_path);
        //}



        //// Lab2 controller
        //[Authorize]
        //public IActionResult Lab2()
        //{
        //    return View();
        //}

        //[Authorize]
        //[HttpPost]
        //public string Lab2(string input_path, string output_path)
        //{
        //    return LaboratoryWorks.Lab2.Run(input_path, output_path);
        //}


        //// Lab3 controller
        //[Authorize]
        //public IActionResult Lab3()
        //{
        //    return View();
        //}

        //[Authorize]
        //[HttpPost]
        //public string Lab3(string input_path, string output_path)
        //{
        //    return LaboratoryWorks.Lab3.Run(input_path, output_path);
        //}




        [Authorize]
        public IActionResult Index()
        {
            return View();
        }


        // Lab1
        [Authorize]
        public IActionResult Lab1()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public string Lab1(string input_path, string output_path)
        {
            try
            {
                string inputText = System.IO.File.ReadAllText(input_path);
                string result = LaboratoryWork1.Run(inputText);

                System.IO.File.WriteAllText(output_path, result);

                return result;
            }
            catch (Exception ex)
            {
                return $"Error in Lab1: {ex.Message}";
            }
        }


        // Lab2
        [Authorize]
        public IActionResult Lab2()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public string Lab2(string input_path, string output_path)
        {
            try
            {
                string inputText = System.IO.File.ReadAllText(input_path);
                string result = LaboratoryWork2.Run(inputText);

                System.IO.File.WriteAllText(output_path, result);

                return result;
            }
            catch (Exception ex)
            {
                return $"Error in Lab2: {ex.Message}";
            }
        }


        // Lab3
        [Authorize]
        public IActionResult Lab3()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public string Lab3(string input_path, string output_path)
        {
            try
            {
                string inputText = System.IO.File.ReadAllText(input_path);
                string result = LaboratoryWork3.Run(inputText);

                System.IO.File.WriteAllText(output_path, result);

                return result;
            }
            catch (Exception ex)
            {
                return $"Error in Lab3: {ex.Message}";
            }
        }
    }
}
