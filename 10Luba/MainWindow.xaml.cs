using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _10Luba
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string first, second;
        private int k, l, m, n;
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {

            List<int[,]> list1 = await ReadMatrix(first);
            List<int[,]> list2 = await ReadMatrix(second);
            for (int i = 0; i < list1.Count; i++)
            {
                for (int j = 0; j < list2.Count; j++)
                {
                    Array.Equals(list1[i], list2[j]);
                }
            }
            using (StreamWriter writer = new StreamWriter(second, true))
            {


                for (int i = 0; i < list2.Count; i++)
                {
                    for (int q = 0; q < n; q++)
                    {
                        for (int w = 0; w < m; w++)
                        {
                            await writer.WriteAsync(list1[i][q, w] + " ");
                        }
                        await writer.WriteLineAsync();
                    }
                    await writer.WriteLineAsync();
                }
            }
            using (StreamReader reader = new StreamReader(second))
            {
                string text = await reader.ReadToEndAsync();
                Table2.Text = text;
            }
        }
        private async Task<List<int[,]>> ReadMatrix(string file)
        {
            List<int[,]> list = new List<int[,]>();
            using (StreamReader reader = new StreamReader(file))
            {
                string? line;
                int[,] matrix = new int[n, m];
                int a = 0;
                while ((line = await reader.ReadLineAsync()) != null)
                {

                    if (!line.Equals(""))
                    {
                        string[] mas = line.Split(" ");
                        for (int i = 0; i < mas.Length - 1; i++)
                        {
                            matrix[a, i] = int.Parse(mas[i]);
                        }
                        a++;
                    }
                    else
                    {
                        list.Add(matrix);
                        a = 0;
                        matrix = new int[n, m];
                    }

                }

            }
            return list;
        }
        public MainWindow()
        {
            InitializeComponent();
            first = Environment.CurrentDirectory + "\\first.txt";
            second = Environment.CurrentDirectory + "\\second.txt";
            FileInfo firstFile = new FileInfo(first);
            FileInfo secondFile = new FileInfo(second);
            if (firstFile.Exists) firstFile.Delete();
            else firstFile.Create();
            if (secondFile.Exists) secondFile.Delete();
            else secondFile.Create();

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            k = int.Parse(K.Text);
            l = int.Parse(L.Text);
            m = int.Parse(M.Text);
            n = int.Parse(N.Text);
            GenMatrix(first, k, Table1);
            GenMatrix(second, l, Table2);
        }
        private async void GenMatrix(string file, int a, TextBlock t)
        {
            Random random = new Random();
            for (int x = 1; x <= a; x++)
            {
                int[,] mas = new int[n, m];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        mas[i, j] = random.Next(3, 5);
                        using (StreamWriter writer = new StreamWriter(file, true))
                        {
                            await writer.WriteAsync(mas[i, j] + " ");
                        }
                    }
                    using (StreamWriter writer = new StreamWriter(file, true))
                    {
                        await writer.WriteLineAsync();
                    }
                }
                using (StreamWriter writer = new StreamWriter(file, true))
                {
                    await writer.WriteLineAsync();
                }
            }
            using (StreamReader reader = new StreamReader(file))
            {
                string text = await reader.ReadToEndAsync();
                t.Text = text;
            }
        }
    }
}
