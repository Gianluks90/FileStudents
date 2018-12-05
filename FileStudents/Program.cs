using System;
using System.Collections.Generic;

// Per migliorare il programma è più corretto creare più classi anziché utilizzare variabili e metodi statici; Una classe per la lettura da file
// una per l'ottenimento dell'input, ecc...

namespace FileStudents
{
    class Program
    {
        const int ALIGNMENT = -15;          // Questa costante determinerà il numero di caratteri nella tabella stampata;
        static void Main(string[] args)
        {
            string line;
            string[] studentInfos;

            List<Student> students = new List<Student>();
            var file = new System.IO.StreamReader(@"C:\Users\Allievo 7\Documents\Gianluca Marino\FileStudents\file.txt");

            while ((line = file.ReadLine()) != null)
            {
                studentInfos = line.Split(',');
                Student newStudent = new Student(studentInfos[0], studentInfos[1], studentInfos[2], studentInfos[3], studentInfos[4], studentInfos[5]);
                students.Add(newStudent);
            }
            string input;

            while(true)
            {
                Console.WriteLine("Press L to see the students list");
                Console.WriteLine("Press F to search a students by his ID");
                Console.WriteLine("Press V to see the average mark");
                Console.WriteLine("Press M to see the mode mark");
                Console.WriteLine("Press D to see the median mark");
                Console.WriteLine("Press Q to quit");

                MaleFemaleMarks(students);
                AdultUnderageMode(students);

                input = Console.ReadLine().ToUpper();
                switch (input)
                {
                    case "L":
                        PrintList(students);
                        break;
                    case "F":
                        SearchByID(students);
                        break;
                    case "V":
                        AverageMark(students);
                        break;
                    case "M":
                        ModeMark(students);
                        break;
                    case "D":
                        MedianMark(students);
                        break;
                    case "Q":
                        return;
                    default:
                        Console.WriteLine("Not valid input. Try again: ");
                        break;
                }
            }
        }

        public static void PrintList(List<Student> students)
        {
            Console.WriteLine($"{"ID",ALIGNMENT}{"NAME", ALIGNMENT}{"SURNAME", ALIGNMENT}{"AGE", ALIGNMENT}{"GENDER", ALIGNMENT}{"MARK", ALIGNMENT}\n");
            foreach (var item in students)
            {
                Console.WriteLine($"{item.ID, ALIGNMENT}{item.name, ALIGNMENT}{item.surname ,ALIGNMENT}{item.age,ALIGNMENT}{item.gender,ALIGNMENT}{item.mark,ALIGNMENT}");
            }
            Console.WriteLine();
        }

        public static void SearchByID(List<Student> students)
        {
            Console.WriteLine("Insert ID to search: ");
            int id = int.Parse(Console.ReadLine());
            foreach (var item in students)
            {
                if (item.ID == id)
                {
                    Console.WriteLine(item.ID + " " + item.name + " " + item.surname + " " + item.age + " " + item.gender + " " + item.mark);
                    return;
                }
            }
            Console.WriteLine("ID is not in th list of students.");
        }

        public static void AverageMark(List<Student> students)
        {
            float sum = 0;
            int count = 0;
            foreach (var item in students)
            {
                sum += item.mark;
                ++count;
            }

            Console.WriteLine($"The average mark is: {Math.Round(sum / count,2)}");
        }

        public static void ModeMark(List<Student> students)
        {
            int count = 1, modeCount = 0, index = 0;
            for (int i = 0; i < students.Count - 1; i++)
            {
                for (int j = i + 1; j < students.Count; j++)
                {
                    if (students[i].mark == students[j].mark)
                    {
                        ++count;
                    }
                }
                if (count > modeCount)
                {
                    modeCount = count;
                    index = i;
                }
                count = 1;
            }
            Console.WriteLine($"The mode mark is {students[index].mark}: {modeCount} times.");
        }

        public static void MedianMark(List<Student> students)
        {
            float[] marks = new float[students.Count];
            for (int i = 0; i < students.Count; i++)
            {
                marks[i] = students[i].mark;
            }
            InsertionSort(marks);

            if (students.Count % 2 == 0)
            {
                float averageMedian = (marks[students.Count / 2] + marks[(students.Count / 2) - 1]) / 2;
                Console.WriteLine($"The median mark is {averageMedian}");
            }
            else
            {
                Console.WriteLine($"The median mark is {marks[(students.Count/2)]}");
            }
        }

        public static void InsertionSort(float[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                float value = array[i];
                int j = i - 1;
                while (j >= 0 && array[j] > value)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = value;
            }
        }

        public static void MaleFemaleMarks(List<Student> students)
        {
            int minMaleMark = 11, maxFemaleMark = 0;

            foreach (var item in students)
            {
                if(item.mark < minMaleMark && item.gender == "M")
                {
                    minMaleMark = item.mark;
                }
                if (item.mark > maxFemaleMark && item.gender == "F")
                {
                    maxFemaleMark = item.mark;
                }
            }

            if (minMaleMark > maxFemaleMark)
            {
                Console.WriteLine("\nThe minimum mark of the males is greater than the maximum mark of the females");
            }
            else if (minMaleMark < maxFemaleMark)
            {
                Console.WriteLine("\nThe minimum mark of the males is lower than the maximum mark of the females");
            }
            else
            {
                Console.WriteLine("\nThe minimum mark of the males equals the maximum mark of the females");
            }
        }

        public static void AdultUnderageMode(List<Student> students)
        {
            int adultCount = 1, underageCount = 1, adultModeCount = 0, underageModeCount = 0, adultIndex = 0, underageIndex = 0;
            const int MAJORITY = 18;

            for (int i = 0; i < students.Count - 1; i++)
            {
                for (int j = i + 1; j < students.Count; j++)
                {
                    if (students[i].mark == students[j].mark && students[i].age >= MAJORITY && students[j].age >= MAJORITY)
                    {
                        ++adultCount;
                    }
                    if (students[i].mark == students[j].mark && students[i].age < MAJORITY && students[j].age < MAJORITY)
                    {
                        ++underageCount;
                    }
                }
                if (adultCount > adultModeCount)
                {
                    adultModeCount = adultCount;
                    adultIndex = i;
                }
                if (adultCount > adultModeCount)
                {
                    underageModeCount = underageCount;
                    underageIndex = i;
                }
                adultCount = 1;
                underageCount = 1;
            }
            if(students[adultIndex].mark > students[underageIndex].mark)
            {
                Console.WriteLine("The adult students' mode mark is greater than the underage students' mode mark");
            }
            else if (students[adultIndex].mark < students[underageIndex].mark)
            {
                Console.WriteLine("The adult students' mode mark is lower than the underage students' mode mark");
            }
            else if (students[adultIndex].mark == students[underageIndex].mark)
            {
                Console.WriteLine("The adult students' mode mark equals the underage students' mode mark");
            }
        }
    }
}
