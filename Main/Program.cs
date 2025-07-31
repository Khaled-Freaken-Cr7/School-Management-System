using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Main
{
    public class Program
    {
        static List<Course> Courses = new List<Course>
        {
            new Course("CS211", "Data Structures Algorithms", 3),
            new Course("IS212", "Database", 3),
            new Course("IS231", "System Analysis and design", 3),
            new Course("MATH201", "MathIII", 3),
            new Course("MATH200", "Probability And Statistics", 2),
        };

        static IStudentRepo studentRepo = new StudentRepo();
        static CourseEnrollmentService courseService = new CourseEnrollmentService();
        static AuthenticationService authService = new AuthenticationService(new Authentication(studentRepo));

        public static void AddStudent()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Student ID: ");
                    string id = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(id))
                    {
                        Console.WriteLine("ID cannot be empty.\n");
                        continue;
                    }

                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(name) || Regex.IsMatch(name, @"[^a-zA-Z\s\-]"))
                    {
                        Console.WriteLine("Name must not contain numbers or special characters (except hyphen '-').\n");
                        continue;
                    }
                    name = Format.FormatName(name);

                    Console.Write("Enter Password (minimum 8 characters): ");
                    string password = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                    {
                        Console.WriteLine("Password must be at least 8 characters long.\n");
                        continue;
                    }

                    if (studentRepo.GetID(id) != null)
                    {
                        Console.WriteLine("Student ID already exists.");
                        continue;
                    }

                    studentRepo.Add(new Student(id, name, password));
                    Console.WriteLine("\nStudent added successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding student: {ex.Message}");
                }
                break;
            }
        }


        public static Student LoginPrompt()
        {
            try
            {
                Console.Write("Enter Student ID: ");
                string id = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(id))
                {
                    Console.WriteLine("ID cannot be empty.\n");
                    return null;
                }

                Console.Write("Enter Password: ");
                string password = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("Password cannot be empty.\n");
                    return null;
                }

                if (authService.Login(id, password))
                {
                    var student = studentRepo.GetID(id);
                    if (student == null) throw new NullReferenceException("Student not found in repository.");
                    return student;
                }

                Console.WriteLine("FAILED\nPassword or Id is wrong.");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
                return null;
            }
        }


        public static void EnrollCourse()
        {
            try
            {
                var student = LoginPrompt();
                if (student == null) return;

                Console.WriteLine("\nAvailable Courses:\n");
                foreach (var c in Courses)
                {
                    Console.WriteLine($"{c.CourseID} - {c.CourseName} ({c.creditHours} credits)");
                }

                while (true)
                {
                    Console.Write("\nEnter Course ID to enroll: ");
                    string courseId = Console.ReadLine()?.Trim().ToUpper();
                    if (string.IsNullOrWhiteSpace(courseId))
                    {
                        Console.WriteLine("Course ID cannot be empty.");
                        continue;
                    }

                    var course = Courses.FirstOrDefault(c => c.CourseID == courseId);
                    if (course == null)
                    {
                        Console.WriteLine("Course not found.");
                        continue;
                    }

                    courseService.Enroll(student, course);
                    Console.WriteLine("Enrolled successfully.");

                    Console.WriteLine("Do you want to enroll another course? (y/n)");
                    string s = Console.ReadLine()?.Trim().ToLower();
                    while (s != "y" && s != "n")
                    {
                        Console.WriteLine("Invalid input. Enter 'y' or 'n'.");
                        s = Console.ReadLine()?.Trim().ToLower();
                    }
                    if (s == "n") break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Enrollment error: {ex.Message}");
            }
        }

     
        public static void RemoveCourse()
        {
            try
            {
                var student = LoginPrompt();
                if (student == null) return;
                while (true)
                {
                    Console.Write("Enter Course ID to remove: ");
                    string courseId = Console.ReadLine()?.Trim().ToUpper();
                    if (string.IsNullOrWhiteSpace(courseId))
                    {
                        Console.WriteLine("Course ID cannot be empty.");
                        continue;
                    }

                    courseService.RemoveCourse(student, courseId);
                    Console.WriteLine("Course removed if existed.");
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing course: {ex.Message}");
            }
        }


        public static void ShowStudentInfo()
        {
            try
            {
                var student = LoginPrompt();
                if (student == null) return;

                Console.WriteLine($"ID: {student.ID}, Name: {student.Name}");
                Console.WriteLine("Courses:");
                if (student.Courses.Count == 0)
                {
                    Console.WriteLine("No courses enrolled.");
                }
                else
                {
                    foreach (var course in student.Courses)
                        Console.WriteLine($"{course.CourseID} - {course.CourseName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error showing student info: {ex.Message}");
            }
        }


        public static void DeleteStudent()
        {
            try
            {
                var student = LoginPrompt();
                if (student == null) return;

                Console.Write("Are you sure you want to delete this student? (y/n): ");
                string confirm = Console.ReadLine()?.Trim().ToLower();
                if (confirm != "y" && confirm != "n")
                {
                    Console.WriteLine("Invalid input. Enter 'y' or 'n'.");
                    return;
                }

                if (confirm == "y")
                {
                    studentRepo.Delete(student.ID);
                    Console.WriteLine("Student deleted.");
                }
                else
                {
                    Console.WriteLine("Deletion canceled.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting student: {ex.Message}");
            }
        }


        public static void EditStudent()
        {
            while (true)
            {
                try
                {
                    var student = LoginPrompt();
                    if (student == null) continue;

                    while (true)
                    {
                        Console.WriteLine($"\nEditing student: ID: {student.ID}, Name: {student.Name}");
                        Console.WriteLine("Select field to edit:");
                        Console.WriteLine("1. Student ID");
                        Console.WriteLine("2. Name");
                        Console.WriteLine("3. Password");
                        Console.WriteLine("4. Finish editing");
                        Console.Write("Enter choice (1-4): ");

                        string choice = Console.ReadLine()?.Trim();
                        if (string.IsNullOrWhiteSpace(choice) || !Regex.IsMatch(choice, @"^[1-4]$"))
                        {
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                            continue;
                        }

                        if (choice == "4")
                        {
                            Console.WriteLine("Student update completed.");
                            break;
                        }

                        bool changesMade = false;

                        if (choice == "1")
                        {
                            Console.Write("Enter new Student ID (current: {0}): ", student.ID);
                            string newId = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(newId))
                            {
                                Console.WriteLine("ID cannot be empty or whitespace.");
                                continue;
                            }
                            if (newId == student.ID)
                            {
                                Console.WriteLine("New ID is the same as the current ID.");
                                continue;
                            }
                            if (studentRepo.GetID(newId) != null)
                            {
                                Console.WriteLine("New Student ID already exists.");
                                continue;
                            }
                            studentRepo.Delete(student.ID);
                            student.UpdateID(newId);
                            studentRepo.Add(student);
                            changesMade = true;
                        }
                        else if (choice == "2")
                        {
                            Console.Write("Enter new Name (current: {0}): ", student.Name);
                            string newName = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(newName))
                            {
                                Console.WriteLine("Name cannot be empty or whitespace.");
                                continue;
                            }
                            if (Regex.IsMatch(newName, @"[^a-zA-Z\s\-]"))
                            {
                                Console.WriteLine("Name must not contain numbers or special characters (except hyphen '-').");
                                continue;
                            }
                            newName = Format.FormatName(newName);
                            student.UpdateName(newName);
                            changesMade = true;
                        }
                        else if (choice == "3")
                        {
                            Console.Write("Enter new Password (minimum 8 characters, current: [hidden]): ");
                            string newPassword = Console.ReadLine()?.Trim();
                            if (string.IsNullOrWhiteSpace(newPassword))
                            {
                                Console.WriteLine("Password cannot be empty or whitespace.");
                                continue;
                            }
                            if (newPassword.Length < 8)
                            {
                                Console.WriteLine("Password must be at least 8 characters long.");
                                continue;
                            }
                            student.UpdatePassword(newPassword);
                            changesMade = true;
                        }

                        if (changesMade)
                        {
                            Console.WriteLine("Field updated successfully.");
                        }
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error editing student: {ex.Message}");
                }
            }
        }
    }
}