using System;
using System.IO;
using System.Text;

namespace LibraryForStruct
{
   public class VariousMethods
   {
      // Определяем структуру
      public struct Student
      {
         public string Group;
         public string Surname;
         public string Name;
         public string Dadsname;
         public int Year;
         public char Gender;
         public int Physics;
         public int Math;
         public int Inf;
         public double Grant;
      }

      // Метод записи массива структур в текстовый файл
      public static void WriteStructFileTxt(string path, Student[] students)
      {
         FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Write);
         StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
         int i = 0;
         while (i < students.Length)
         {
            Student person = students[i];
            writer.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}",
               person.Group, person.Surname, person.Name, person.Dadsname, person.Year,
               person.Gender, person.Physics, person.Math, person.Inf, person.Grant);
            i++;
         }

         writer.Close();
      }

      // Метод чтения массива структур из текстового файла
      public static Student[] ReadStructFileTxt(string path, string nameFile)
      {
         Student[] arrayStudent = { };
         // Чтение файла за одну операцию
         string[] allLines = File.ReadAllLines(path, Encoding.UTF8);
         if (allLines == null || allLines.Length == 0)
         {
            Console.WriteLine("Ошибка содержимого файла для чтения {0}", nameFile);
            //Console.WriteLine("Ошибка содержимого файла для чтения {0}. Файл пуст", nameFile);
         }
         else
         {
            // Разделение строки на подстроки по пробелу для определения количества столбцов в строке
            arrayStudent = new Student[allLines.Length];
            int[] сolumnArray = new int[allLines.Length];
            char symbolSpace = ' ';
            int countRow = 0;
            int countSymbol = 0;
            int countСolumn = 0;
            while (countRow < allLines.Length)
            {
               string line = allLines[countRow];
               while (countSymbol < line.Length)
               {
                  if (symbolSpace == line[countSymbol])
                  {
                     countСolumn++;
                  }

                  if (countSymbol == line.Length - 1)
                  {
                     countСolumn++;
                  }

                  countSymbol++;
               }

               сolumnArray[countRow] = countСolumn;
               // 10 количество полей в структуре
               if (countСolumn != 10)
               {
                  Console.WriteLine("Неверный формат строки {0}", countRow);
               }

               countRow++;
               countСolumn = 0;
               countSymbol = 0;
            }

            // Поиск максимального и минимального элемента массива
            // Cчитаем, что максимум - это первый элемент массива
            int max = сolumnArray[0];
            // Cчитаем, что минимум - это первый элемент массива
            int min = сolumnArray[0];
            int columns = 0;
            while (columns < сolumnArray.Length)
            {
               if (max < сolumnArray[columns])
               {
                  max = сolumnArray[columns];
               }

               if (min > сolumnArray[columns])
               {
                  min = сolumnArray[columns];
               }

               columns++;
            }

            //Console.WriteLine("Максимум равен: {0}", max);
            //Console.WriteLine("Минимум равен: {0}", min);

            // Разделение строки на подстроки по пробелу и конвертация подстрок в структуру
            string[] lineArray = new string[max];
            StringBuilder stringModified = new StringBuilder();
            char spaceCharacter = ' ';
            int row = 0;
            int column = 0;
            int countCharacter = 0;
            while (row < allLines.Length)
            {
               string line = allLines[row];
               while (column < сolumnArray[row])
               {
                  while (countCharacter < line.Length)
                  {
                     if (spaceCharacter == line[countCharacter])
                     {
                        string subLine = stringModified.ToString();
                        lineArray[column] = subLine;
                        stringModified.Clear();
                        column++;
                     }
                     else
                     {
                        stringModified.Append(line[countCharacter]);
                     }

                     if (countCharacter == line.Length - 1)
                     {
                        string subLine = stringModified.ToString();
                        lineArray[column] = subLine;
                        stringModified.Clear();
                        column++;
                     }

                     countCharacter++;
                  }

                  arrayStudent[row].Group = lineArray[0];
                  arrayStudent[row].Surname = lineArray[1];
                  arrayStudent[row].Name = lineArray[2];
                  arrayStudent[row].Dadsname = lineArray[3];
                  arrayStudent[row].Year = int.Parse(lineArray[4]);
                  arrayStudent[row].Gender = char.Parse(lineArray[5]);
                  arrayStudent[row].Physics = int.Parse(lineArray[6]);
                  arrayStudent[row].Math = int.Parse(lineArray[7]);
                  arrayStudent[row].Inf = int.Parse(lineArray[8]);
                  arrayStudent[row].Grant = double.Parse(lineArray[9]);

                  countCharacter = 0;
               }

               row++;
               column = 0;
            }
         }

         return arrayStudent;
      }

      // Метод записи массива структур в бинарный файл
      public static void WriteStructFileBin(string path, Student[] students)
      {
         FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
         BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8);
         writer.Write(students.Length);
         int i = 0;
         while (i < students.Length)
         {
            Student person = students[i];
            // Запись строки в UTF-8 с предварительной длиной
            writer.Write(person.Group);
            writer.Write(person.Surname);
            writer.Write(person.Name);
            writer.Write(person.Dadsname);
            writer.Write(person.Year);
            writer.Write(person.Gender);
            writer.Write(person.Physics);
            writer.Write(person.Math);
            writer.Write(person.Inf);
            writer.Write(person.Grant);
            i++;
         }

         stream.Close();
         writer.Close();
      }

      // Метод чтения массива структур из бинарного файла
      public static Student[] ReadStructFileBin(string path)
      {
         FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
         BinaryReader reader = new BinaryReader(stream, Encoding.UTF8);
         int length = reader.ReadInt32();
         Student[] persons = new Student[length];
         int i = 0;
         while (i < length)
         {
            string group = reader.ReadString();
            string surname = reader.ReadString();
            string name = reader.ReadString();
            string dadsname = reader.ReadString();
            int year = reader.ReadInt32();
            char gender = reader.ReadChar();
            int physics = reader.ReadInt32();
            int Math = reader.ReadInt32();
            int inf = reader.ReadInt32();
            double grant = reader.ReadDouble();
            persons[i] = new Student
            {
               Group = group,
               Surname = surname,
               Name = name,
               Dadsname = dadsname,
               Year = year,
               Gender = gender,
               Physics = physics,
               Math = Math,
               Inf = inf,
               Grant = grant
            };

            i++;
         }

         stream.Close();
         reader.Close();
         return persons;
      }

      // Метод расчета среднего балла всех студентов по всем предметам
      public static double AverageScore(Student[] students)
      {
         double medium;
         double allSubjects = 0;
         int i = 0;
         while (i < students.Length)
         {
            double bySubjects = ((students[i].Physics + students[i].Math + students[i].Inf) / 3.0f);
            allSubjects += bySubjects;
            //Console.WriteLine("Cредний балл: {0} {1} - {2:f2}",
            //   students[i].Surname, students[i].Name, bySubjects);
            Console.WriteLine("Cредний балл: {0} {1} - {2:f}",
               students[i].Surname, students[i].Name, bySubjects);
            i++;
         }

         medium = allSubjects / students.Length;
         Console.WriteLine("Средний балл всех студентов по всем предметам: {0:f}", medium);
         return medium;
      }

      // Метод поиска студентов средний балл которых выше, чем общий средний балл
      public static void AverageHigherScore(string path, Student[] student, double medium)
      {
         Console.WriteLine("Студенты, средний балл которых выше, чем общий средний балл:");
         // Определяем количество студентов удовлетворяющих условию для расчета размера массива структур
         int count = 0;
         int i = 0;
         while (i < student.Length)
         {
            double bySubjects = ((student[i].Physics + student[i].Math + student[i].Inf) / 3.0f);
            if (bySubjects > medium)
            {
               count++;
            }

            i++;
         }

         Student[] averageHigher = new Student[count];
         int j = 0;
         int k = 0;
         while (j < student.Length)
         {
            double bySubjects = ((student[j].Physics + student[j].Math + student[j].Inf) / 3.0f);
            if (bySubjects > medium)
            {
               averageHigher[k] = student[j];
               Console.WriteLine("{0} {1}", student[j].Surname, student[j].Name);
               k++;
            }

            j++;
         }

         // Запись массива структур в бинарный файл
         FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
         BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8);
         writer.Write(averageHigher.Length);
         int m = 0;
         while (m < averageHigher.Length)
         {
            // Запись строки в UTF-8 с предварительной длиной
            writer.Write(averageHigher[m].Surname);
            writer.Write(averageHigher[m].Name);
            m++;
         }

         stream.Close();
         writer.Close();
      }

      // Метод поиска несовершеннолетнего студента с худшим средним баллом
      public static void MinorStudentWorstAverage(string path, Student[] student)
      {
         Console.WriteLine("Несовершеннолетние студенты:");
         // Возраст совершеннолетнего студента
         int underage = 18;
         int currentDate = DateTime.Now.Year;
         // Определяем количество студентов удовлетворяющих условию для расчета размера массива структур
         int count = 0;
         int i = 0;
         while (i < student.Length)
         {
            int minorStudent = (currentDate - student[i].Year);
            if (minorStudent < underage)
            {
               count++;
            }

            i++;
         }

         Student[] minor = new Student[count];
         int j = 0;
         int k = 0;
         while (j < student.Length)
         {
            int minorStudent = (currentDate - student[j].Year);
            if (minorStudent < underage)
            {
               minor[k] = student[j];
               Console.WriteLine("{0} {1} {2} {3}",
                  student[j].Group, student[j].Surname, student[j].Name, student[j].Dadsname);
               k++;
            }

            j++;
         }

         // Рассчитываем средний балл несовершеннолетних студентов для добавления в массив структур и расчета худшего среднего балла
         int l = 0;
         double[] average = new double[count];
         double bySubjects;
         while (l < minor.Length)
         {
            bySubjects = ((minor[l].Physics + minor[l].Math + minor[l].Inf) / 3.0f);
            average[l] = bySubjects;
            l++;
         }

         // Cчитаем, что минимум - это первый элемент массива
         double min = average[0];
         int m = 0;
         while (m < average.Length)
         {
            if (min > average[m])
            {
               min = average[m];
            }

            m++;
         }

         Console.WriteLine("Худший средний балл: {0:f}", min);
         //Console.WriteLine("Худший средний балл: {0:f2}", min);

         // Поиск индекса минимума массива
         int n = 0;
         int counter = 0;
         bool flag = false;
         while (n < average.Length && flag == false)
         {
            // Сравниваем значения double используя метод CompareTo(Double) 
            if (average[n].CompareTo(min) == 0)
            {
               counter = n;
               flag = true;
            }

            // Сравниваем значения double используя метод Equals(Double)
            //if (average[n].Equals(min))
            //{
            //   counter = n;
            //   flag = true;
            //}

            n++;
         }

         if (flag)
         {
            Console.WriteLine("Индекс худшего среднего балла: {0}", counter);
         }

         Console.WriteLine("Несовершеннолетний студент с худшим средним баллом:");
         Student worstAverage = minor[counter];
         Console.WriteLine("{0} {1} {2} {3}",
            worstAverage.Group, worstAverage.Surname, worstAverage.Name, worstAverage.Dadsname);

         // Запись структуры в текстовый файл
         FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Write);
         StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
         writer.WriteLine("{0} {1} {2} {3}",
            worstAverage.Group, worstAverage.Surname, worstAverage.Name, worstAverage.Dadsname);
         writer.Close();
      }

      // Метод сортировки массива структур по возрасту
      public static void BubbleSortByAge(Student[] students)
      {
         Console.WriteLine("Отсортированный массив структур по возрасту:");
         // Если нужно сортировать по другим критериям изменяем условие в сортировке:
         // используем string.Compare
         // if (string.Compare(arr[j].Name, arr[j + 1].Name) > 0)
         int n = students.Length;
         int i = 0;
         while (i < n - 1)
         {
            int j = 0;
            while (j < n - i - 1)
            {
               // Сравниваем соседние элементы
               if (students[j].Year > students[j + 1].Year)
               {
                  // Меняем местами структуры
                  Student temp = students[j];
                  students[j] = students[j + 1];
                  students[j + 1] = temp;
               }

               j++;
            }

            i++;
         }

         int index = 0;
         while (index < students.Length)
         {
            Student person = students[index];
            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}",
               person.Group, person.Surname, person.Name, person.Dadsname, person.Year,
               person.Gender, person.Physics, person.Math, person.Inf, person.Grant);
            index++;
         }
      }
   }
}