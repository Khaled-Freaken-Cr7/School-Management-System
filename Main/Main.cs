using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class main
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("|\t School Management System\t |");
                    Console.WriteLine("------------------------------------------\n");
                    Console.WriteLine("1 - Add Student");
                    Console.WriteLine("2 - Edit Student Data");
                    Console.WriteLine("3 - Enroll in Course");
                    Console.WriteLine("4 - Remove Course");
                    Console.WriteLine("5 - Show Student Info");
                    Console.WriteLine("6 - Delete Student");
                    Console.WriteLine("0 - Exit\n");
                    Console.Write("Choice: ");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1": Program.AddStudent(); break;
                        case "2": Program.EditStudent(); break;
                        case "3": Program.EnrollCourse(); break;
                        case "4": Program.RemoveCourse(); break;
                        case "5": Program.ShowStudentInfo(); break;
                        case "6": Program.DeleteStudent(); break;
                        case "0": return;
                        default: Console.WriteLine("Invalid option."); break;
                    }

                    Console.WriteLine("\nEnter any key to continue...");
                    Console.ReadKey();
                    Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Critical error occurred: {ex.Message}");
                }
            }
        }
    }
}
