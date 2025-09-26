using System;
using System.IO;
using System.Text;

namespace LibraryForStruct
{
   public class VariousMethodsForStructBusiness
   {
      // Определяем структуру
      public struct Business
      {
         public string Company;
         public string Department;
         public double Profit;
      }

      // Метод записи массива структур в текстовый файл
      public static void WriteStructFileTxt(string path, Business[] firm)
      {
         FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Write);
         StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);
         int i = 0;
         while (i < firm.Length)
         {
            Business person = firm[i];
            writer.WriteLine("{0} {1} {2}", person.Company, person.Department, person.Profit);
            i++;
         }

         writer.Close();
      }

      // Метод поиска прибыльных и убыточных подразделений
      public static string ProfitAnalysis(Business[] firm)
      {
         // Определяем количество прибыльных и убыточных подразделений
         int profitHigher = 0;
         int profitLow = 0;
         int i = 0;
         while (i < firm.Length)
         {
            if (firm[i].Profit > 0)
            {
               profitHigher++;
            }
            else
            {
               profitLow++;
            }

            i++;
         }

         string result = null;
         if (profitHigher > profitLow)
         {
            string higher = "Прибыльных подразделений " + profitHigher + " больше чем убыточных " + profitLow;
            Console.WriteLine(higher);
            result = higher;
         }
         if (profitHigher < profitLow)
         {
            string low = "Убыточных подразделений " + profitLow + " больше чем прибыльных " + profitHigher;
            Console.WriteLine(low);
            result = low;
         }
         if (profitHigher == profitLow)
         {
            string equally = "Прибыльных " + profitHigher + " и убыточных " + profitLow + " подразделений поровну";
            Console.WriteLine(equally);
            result = equally;
         }

         return result;
      }

      // Метод поиска подразделения с наибольшим профицитом 
      public static string ProfitMax(Business[] firm)
      {
         // Определяем подразделение с наибольшим профицитом 
         // Cчитаем, что максимум - это первый элемент структуры
         double max = firm[0].Profit;
         string company = firm[0].Company;
         int row = 0;
         while (row < firm.Length)
         {
            if (max < firm[row].Profit)
            {
               max = firm[row].Profit;
               company = firm[row].Company;
            }

            row++;
         }

         string higher = "Подразделение с наибольшим профицитом: " + max + " - " + company;
         Console.WriteLine(higher);
         return higher;
      }

      // Метод расчета среднего профицита по всем подразделениям
      public static double AverageScore(Business[] firm)
      {
         double medium;
         double allSubjects = 0;
         int i = 0;
         while (i < firm.Length)
         {
            double bySubjects = firm[i].Profit;
            allSubjects += bySubjects;
            i++;
         }

         medium = allSubjects / firm.Length;
         Console.WriteLine("Средний профицит по всем подразделениям: {0:f}", medium);
         return medium;
      }

      // Метод поиска подразделений профицит которых выше, чем средний профицит
      public static void AverageHigherScore(string path, Business[] firm, double medium)
      {
         Console.WriteLine("Подразделения профицит которых выше, чем средний профицит:");
         // Определяем количество подразделений удовлетворяющих условию для расчета размера массива структур
         int count = 0;
         int i = 0;
         while (i < firm.Length)
         {
            double bySubjects = firm[i].Profit;
            if (bySubjects > medium)
            {
               count++;
            }

            i++;
         }

         Business[] averageHigher = new Business[count];
         int j = 0;
         int k = 0;
         while (j < firm.Length)
         {
            double bySubjects = firm[j].Profit;
            if (bySubjects > medium)
            {
               averageHigher[k] = firm[j];
               Console.WriteLine("{0} {1}", firm[j].Company, firm[j].Profit);
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
            writer.Write(averageHigher[m].Company);
            writer.Write(averageHigher[m].Profit);
            m++;
         }

         stream.Close();
         writer.Close();
      }

      // Метод поиска подразделений профицит которых ниже, чем средний профицит
      public static void AverageLowScore(string path, Business[] firm, double medium)
      {
         Console.WriteLine("Подразделения профицит которых ниже, чем средний профицит:");
         // Определяем количество подразделений удовлетворяющих условию для расчета размера массива структур
         int count = 0;
         int i = 0;
         while (i < firm.Length)
         {
            double bySubjects = firm[i].Profit;
            if (bySubjects > medium)
            {
               count++;
            }

            i++;
         }

         Business[] averageHigher = new Business[count];
         int j = 0;
         int k = 0;
         while (j < firm.Length)
         {
            double bySubjects = firm[j].Profit;
            if (bySubjects < medium)
            {
               averageHigher[k] = firm[j];
               Console.WriteLine("{0} {1}", firm[j].Company, firm[j].Profit);
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
            writer.Write(averageHigher[m].Company);
            writer.Write(averageHigher[m].Profit);
            m++;
         }

         stream.Close();
         writer.Close();
      }
   }
}