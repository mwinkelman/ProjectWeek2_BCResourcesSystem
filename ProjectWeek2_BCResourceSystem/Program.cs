using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjectWeek2_BCResourceSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, string> students = new Dictionary<int, string>();
        
            students.Add(1, "Quinn Bennett");
            students.Add(2, "Sirahn Butler");
            students.Add(3, "Imari Childress");
            students.Add(4, "Jennifer Evans");
            students.Add(5, "Richard Raponi");
            students.Add(6, "Cameron Robinson");
            students.Add(7, "Krista Scholdberg");
            students.Add(8, "Ashley Stewart");
            students.Add(9, "Cadale Thomas");
            students.Add(10, "Kim Vargas");
            students.Add(11, "Mary Winkelman");
   
            List<string> books = new List<string>();
            books.Add("ASP.NET MVC 5");
            books.Add("Assembly Language Tutor");
            books.Add("A Smarter Way to Learn JavaScript");
            books.Add("C# 5.0 All-In-One For Dummies");
            books.Add("Eloquent JavaScript");
            books.Add("Essential C# 6.0");
            books.Add("Implementing Responsive Design");
            books.Add("JavaScript for Kids");
            books.Add("Killer Game Programming in Java");
            books.Add("Programming Interviews Exposed");
            books.Sort();

            CreateStudentFiles(students);
            CreateResourceFiles(books);
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //PROGRAM:
            bool repeat = true;
            do
            {
                ShowMenu();
                Console.WriteLine("Enter the number of a menu item: ");
                string menuInput = Console.ReadLine();
                switch (menuInput)
                {
                    case "1": //ViewStudents Method
                        ViewStudents(students);
                        Console.WriteLine("Press ENTER to return to the Main Menu.");
                        Console.Read();
                        break;
                    case "2": //viewAvailableResources Method                     
                        ViewResources(books);
                        break;
                    case "3": //ViewStudentAccounts Method                        
                        ViewStudentAcct(students);
                        break;
                    case "4": //Checkout Method                      
                        Checkout(books,students);
                        break;
                    case "5": //ReturnMethod                        
                        Return(books, students);
                        break;
                    case "6":
                        Console.WriteLine("\nGOODBYE.\n");
                        repeat = false;
                        Console.WriteLine("Press ENTER to exit the program.");
                        Console.Read();
                        break;
                    default:
                        break;
                }
            }
            while (repeat == true);
        }
        static void CreateStudentFiles(Dictionary<int, string> studentDictionary)
        {
            List<int> studentIDList = new List<int>();
            List<string> studentNAMEList = new List<string>();
            studentNAMEList.Sort();

            foreach (int id in studentDictionary.Keys)
                studentIDList.Add(id);
            foreach (string name in studentDictionary.Values)
                studentNAMEList.Add(name);

            foreach (int id in studentIDList)
            {
                string fileName = CreateFilename(id);                
                if (!File.Exists(fileName))
                {
                    string file = fileName;
                    StringBuilder studentfileSB = new StringBuilder();
                    studentfileSB.AppendLine($"Student: {studentNAMEList[id - 1]}");
                    studentfileSB.AppendLine($"ID: {id}");
                    studentfileSB.AppendLine($"Checked out resources:");
                    string text = studentfileSB.ToString();
                    StreamWriter sw = new StreamWriter(file);
                    using (sw)
                    {
                        sw.Write(text);
                    }
                }
            }
        }
        static void CreateResourceFiles(List<string> bookList)
        {
            if (!File.Exists(@"resourcesAll.txt"))
            {
                string filename = @"resourcesAll.txt";
                StringBuilder allResSB = new StringBuilder();
                foreach (string title in bookList)
                {
                    allResSB.AppendLine(title);
                }
                string allTitles = allResSB.ToString();
                StreamWriter sw = new StreamWriter(filename);
                using (sw)
                {
                    sw.Write(allTitles);
                }
            }

            if (!File.Exists(@"resourcesAvailable.txt"))
            {
                string filename = @"resourcesAvailable.txt";
                StringBuilder availResSB = new StringBuilder();
                foreach (string title in bookList)
                {
                    availResSB.AppendLine(title);
                }
                string availableTitles = availResSB.ToString();
                StreamWriter sw = new StreamWriter(filename);
                using (sw)
                {
                    sw.Write(availableTitles);
                }
            }

            if (!File.Exists(@"resourcesCheckedOut.txt"))
            {
                string filename = @"resourcesCheckedOut.txt";
                StreamWriter chkdoutResSB = new StreamWriter(filename);
                using (chkdoutResSB)
                {
                    chkdoutResSB.Write("");
                }
            }
        }
        static void ShowMenu()
        {
            List<string> mainMenuItems = new List<string>() { "Main Menu:", "View Students", "View Resources", "View Student Accounts", "Checkout Item", "Return Item", "Exit" };
            Console.Clear();
            StringBuilder menuSB = new StringBuilder();
            menuSB.Append(mainMenuItems[0] + "\n");
            for (int i = 1; i < mainMenuItems.Count; i++)
            {
                menuSB.Append($"{i}. {mainMenuItems[i]}\n");
            }
            string menu = menuSB.ToString();
            Console.WriteLine(menu);          
        }
        static void ViewStudents(Dictionary<int,string> studentDictionary)
        {
            Console.Clear();
            ShowMenu();
            string format = "{0,-7} {1,-20}";
            Console.WriteLine(format, "ID:", "NAME:");
            foreach (KeyValuePair<int, string> pair in studentDictionary)
            {
                Console.WriteLine(format, pair.Key, pair.Value);
            }
        }
        static void ViewResources(List<string> bookList)
        {
            Console.Clear();
            ShowMenu();
            string resourcesAll = @"resourcesAll.txt";
            string resourcesAvailable = @"resourcesAvailable.txt";
            string resourcesOut = @"resourcesCheckedOut.txt";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Choose from the following options:\n");
            sb.AppendLine("1. View All Resources");
            sb.AppendLine("2. View Available Resources");
            sb.AppendLine("3. View Checked Out Resources");
            string resourceMenu = sb.ToString();
            Console.WriteLine(resourceMenu);
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.Clear();
                    ShowMenu();
                    Console.WriteLine("All Resources:\n");
                    StreamReader allResourcesSR = new StreamReader(resourcesAll);
                    using (allResourcesSR)
                    {
                        Console.WriteLine(allResourcesSR.ReadToEnd());
                    }
                    break;
                case "2":
                    Console.Clear();
                    ShowMenu();
                    Console.WriteLine("Available Resources:\n");
                    StreamReader availableSR = new StreamReader(resourcesAvailable);
                    using (availableSR)
                    {
                        Console.WriteLine(availableSR.ReadToEnd());
                    }
                    break;
                case "3":
                    Console.Clear();
                    ShowMenu();
                    StreamReader checkedOutSR = new StreamReader(resourcesOut);
                    using (checkedOutSR)
                    {
                        Console.WriteLine(checkedOutSR.ReadToEnd());
                    }
                    string[] check = File.ReadAllLines(resourcesOut);
                    if(check.Length==0)
                        Console.WriteLine("Nothing is checked out.");                    
                    break;               
                default:
                    Console.Clear();
                    ShowMenu();
                    Console.WriteLine("Error: not a valid option");
                    break;
            }
            Console.WriteLine("Press ENTER to return to the Main Menu.");
            Console.Read();
        }
        static void ViewStudentAcct(Dictionary<int,string> studentDictionary)
        {
            Console.Clear();
            ShowMenu();
            Console.WriteLine("Enter the student's ID number:\n");
            ViewStudents(studentDictionary);
            int id = ValidateStudentIDInput(Console.ReadLine(), studentDictionary);
            if (id == 0)
                return;
            string fileName = CreateFilename(id);
            StreamReader sr = new StreamReader(fileName);
            Console.Clear();
            using (sr)
            {
                Console.WriteLine(sr.ReadToEnd());
                Console.WriteLine("\nPress ENTER to return to the Main Menu.");
                Console.Read();
            }
        }
        static void Checkout(List<string> bookList, Dictionary<int, string> studentDictionary)
        {
            //List students for user's choosing:
            Console.Clear();
            ShowMenu();
            string availableFile = @"resourcesAvailable.txt";
            List<string> availableResourcesList = DisplayAvailable(availableFile);
            //Check to see if there are any books available
            if (availableResourcesList.Count == 0)
            {
                Console.WriteLine("Everything is checked out at this time.");
                Console.WriteLine("Press ENTER to return to the Main Menu");
                Console.ReadKey();
                return;
            }
            ViewStudents(studentDictionary);
            Console.WriteLine("\nEnter the ID of the student checking out a resource:");
            int id = ValidateStudentIDInput(Console.ReadLine(), studentDictionary);
            if (id == 0)
                return;
            string fileName = CreateFilename(id);
            string[] fileArray = File.ReadAllLines(fileName);
            string[] studentsResourcesArray=new string[fileArray.Length-3];
            for (int i =3; i<fileArray.Length;i++)
            {
                studentsResourcesArray[i - 3] = fileArray[i];
            }

            //check to see if the student has reached checkout limit by counting lines of the student's file            
            Console.Clear();
            ShowMenu();
            if (studentsResourcesArray.Length>2)
            {
                Console.WriteLine("You have checked out the maximum number of resources (3) allowed.");
                Console.WriteLine("Press ENTER to return to the Main Menu.");
                Console.Read();
                return;
            }
            //display available books:

            Console.Clear();
            ShowMenu();
            Console.WriteLine("\nEnter the number corresponding to the resource you are checking out: ");
            string checkedOutTitle = ValidateChktBookNumInput(Console.ReadLine(), availableResourcesList);          
            if (checkedOutTitle == "")
                return;
            //if (validBookNum) Add title to student's file
            StreamWriter swStudent = new StreamWriter(fileName, true);
            using (swStudent)
            {
                swStudent.WriteLine(checkedOutTitle);
            }

            //Remove title from availableBooks file
            RemoveFromAvailable(checkedOutTitle, availableFile);
 
            //Add title to checked out file
            string checkedoutFile = @"resourcesCheckedOut.txt";
            AddToCheckedOut(checkedOutTitle, checkedoutFile);
            Console.Clear();
            ShowMenu();
            Console.WriteLine($"Checkout successful. {studentDictionary[id]} has checked out \"{checkedOutTitle}\".");                            
            Console.WriteLine("Press ENTER to return to the Main Menu.");
            Console.Read();
        }
        static void Return(List<string> bookList, Dictionary<int, string> studentDictionary)
        {
            Console.Clear();
            ShowMenu();
            ViewStudents(studentDictionary);
            Console.WriteLine("\nEnter the ID of the student checking out a resource:");
            int id = ValidateStudentIDInput(Console.ReadLine(), studentDictionary);
            if (id == 0)
                return;
            string fileName = CreateFilename(id);
            string[] fileArray = File.ReadAllLines(fileName);
            string[] studentsResourcesArray = new string[fileArray.Length - 3];
            for(int i = 3;i<fileArray.Length;i++)
            {
                studentsResourcesArray[i - 3] = fileArray[i];
            }

            //If studentResources is empty, the student has nothing checked out.
            //Let them know:
            Console.Clear();
            ShowMenu();
            if (studentsResourcesArray.Length==0)
            {
                Console.WriteLine("This student has nothing checked out.\n");
                Console.WriteLine("\nPress ENTER to return to the Main Menu.");
                Console.Read();
                return;
            }
            //otherwise display the list of books the student currently has checked out:
            int counter = 0;
            foreach (string title in studentsResourcesArray)
            {
                counter++;
                Console.WriteLine("{0,-7} {1,-20}",counter,title);
            }
            Console.WriteLine("Enter the number corresponding to the book you would like to return:");
            string returnedTitle = ValidateRtrnBookNumInput(Console.ReadLine(), studentsResourcesArray);
            if (returnedTitle == "")
                return;            
            List<string> studentsResourcesList = studentsResourcesArray.ToList();            
            studentsResourcesList.Remove(returnedTitle);
            StreamWriter swStudent = new StreamWriter(fileName);
            using (swStudent)
            {
                for (int i = 0; i < 3; i++)
                    swStudent.WriteLine(fileArray[i]);
                foreach (string title in studentsResourcesList)
                    swStudent.WriteLine(title);
            }

            //Add the book to available file
            string availableFile = @"resourcesAvailable.txt";
            AddToAvailable(returnedTitle, availableFile);
            
            //Remove the book from checked out file
            string checkedoutFile = @"resourcesCheckedOut.txt";
            RemoveFromCheckedOut(returnedTitle, checkedoutFile);
            Console.Clear();
            ShowMenu();
            Console.WriteLine($"Return successful. {studentDictionary[id]} has returned \"{returnedTitle}\".");
            Console.WriteLine("Press ENTER to return to the Main Menu.");
            Console.Read();

        }
        static string CreateFilename(int studentID)
        {
            StringBuilder filenameSB = new StringBuilder();
            filenameSB.Append("student");
            filenameSB.Append(studentID);
            filenameSB.Append(".txt");
            string fileName = @filenameSB.ToString();
            return fileName;
        }
        static int ValidateStudentIDInput(string input,Dictionary<int,string> studentDictionary)
        {
            int id;
            bool validID = int.TryParse(input, out id) && id > 0 && id < studentDictionary.Count;
            if (!validID)
            {
                Console.WriteLine("Error: Not a valid entry");
                Console.WriteLine("Press ENTER to return to the Main Menu.");
                Console.Read();
                return 0;
            }
            return id;
        }
        static string ValidateChktBookNumInput(string input,List<string>availableList)
        {           
            int bookNumber;
            bool validBookNum = int.TryParse(input, out bookNumber) && bookNumber > 0 && bookNumber <= availableList.Count;
            if (!validBookNum)
                return "";
            string checkedOutTitle = availableList[bookNumber - 1];
            return checkedOutTitle;
        }
        static string ValidateRtrnBookNumInput(string input,string[]studentsResourcesArray)
        {
            int bookNum;
            bool validReturn = int.TryParse(input, out bookNum) && bookNum > 0 && bookNum <= studentsResourcesArray.Length;

            //Remove book from student's file
            List<string> studentsResourcesList = studentsResourcesArray.ToList();
            if (!validReturn)
            {
                Console.WriteLine("Error: not a valid entry");
                Console.WriteLine("Press ENTER to return to Main Menu.");
                Console.Read();
                return "";
            }
            string returnedTitle = studentsResourcesArray[bookNum - 1];
            return returnedTitle;
        }
        static void AddToAvailable(string title, string filename)
        {
            string availableFile = filename;
            List<string> availableResourcesList = new List<string>();
            StreamReader srAvailable = new StreamReader(availableFile);
            using (srAvailable)
            {
                string line = "";
                int lineNum = 0;
                while (line != null)
                {
                    lineNum++;
                    line = srAvailable.ReadLine();
                    if (line != null)
                    {
                        availableResourcesList.Add(line);
                    }
                }
            }
            availableResourcesList.Add(title);
            availableResourcesList.Sort();
            StreamWriter swAvailable = new StreamWriter(availableFile);
            using (swAvailable)
            {
                foreach (string line in availableResourcesList)
                {
                    swAvailable.WriteLine(line);
                }
            }
        }
        static void RemoveFromAvailable(string title, string filename)
        {
            string availableFile = filename;
            List<string> availableResourcesList = new List<string>();
            StreamReader srAvailable = new StreamReader(availableFile);
            using (srAvailable)
            {
                string line = "";
                int lineNum = 0;
                while (line != null)
                {
                    lineNum++;
                    line = srAvailable.ReadLine();
                    if (line != null)
                    {
                        availableResourcesList.Add(line);
                    }
                }
            }
            availableResourcesList.Remove(title);
            StreamWriter swAvailable = new StreamWriter(@"resourcesAvailable.txt");
            using (swAvailable)
            {
                foreach (string line in availableResourcesList)
                {
                    swAvailable.WriteLine(line);
                }
            }
        }
        static void AddToCheckedOut(string title, string filename)
        {
            string checkedoutFile = filename;
            List<string> checkedoutResourcesList = new List<string>();
            StreamReader srCheckedOut = new StreamReader(checkedoutFile);
            using (srCheckedOut)
            {
                string line = "";
                int lineNum = 0;
                while (line != null)
                {
                    lineNum++;
                    line = srCheckedOut.ReadLine();
                    if (line != null)
                    {
                        checkedoutResourcesList.Add(line);
                    }
                }
            }
            checkedoutResourcesList.Add(title);
            checkedoutResourcesList.Sort();
            StreamWriter swCheckedOut = new StreamWriter(checkedoutFile);
            using (swCheckedOut)
            {
                foreach (string line in checkedoutResourcesList)
                    swCheckedOut.WriteLine(line);
            }
        }
        static void RemoveFromCheckedOut(string title, string filename)
        {
            string checkedoutFile = filename;
            List<string> checkedoutResourcesList = new List<string>();
            StreamReader srCheckedOut = new StreamReader(checkedoutFile);
            using (srCheckedOut)
            {
                string line = "";
                int lineNum = 0;
                while (line != null)
                {
                    lineNum++;
                    line = srCheckedOut.ReadLine();
                    if (line != null)
                    {
                        checkedoutResourcesList.Add(line);
                    }
                }
            }
            checkedoutResourcesList.Remove(title);
            StreamWriter swCheckedOut = new StreamWriter(checkedoutFile);
            using (swCheckedOut)
            {
                foreach (string line in checkedoutResourcesList)
                    swCheckedOut.WriteLine(line);
            }
        }
        static List<string> DisplayAvailable(string filename)
        {
            string format = "{0,-7} {1,-20}";
            List<string> availableResourcesList = new List<string>();
            StreamReader srAvailable = new StreamReader(filename);
            using (srAvailable)
            {
                string line = "";
                int lineNum = 0;
                while (line != null)
                {
                    lineNum++;
                    line = srAvailable.ReadLine();
                    if (line != null)
                    {
                        availableResourcesList.Add(line);
                    }
                }
            }
            int counter = 0;
            Console.WriteLine("Available Resources:\n");
            foreach (string title in availableResourcesList)
            {
                Console.WriteLine(format, counter + 1, availableResourcesList[counter]);
                counter++;
            }
            return availableResourcesList; 
        }
    }
}
                