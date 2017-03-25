using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {//Функции расчета расстояния до точек
        double evkl(double x, double y, double x1, double y1)
        {

            return System.Math.Sqrt(System.Math.Pow(x1 - x, 2) + System.Math.Pow(y1 - y, 2));
        }
        double evkl1(double x, double y, double x1, double y1, double z, double z1)
        {

            return System.Math.Sqrt(System.Math.Pow(x1 - x, 2) + System.Math.Pow(y1 - y, 2) + System.Math.Pow(z1 - z, 2));
        }
        double evkl2(double x, double x1)
        {

            return System.Math.Sqrt(System.Math.Pow(x1 - x, 2));
        }
        double manh (double x, double y, double x1, double y1)
        {
            return System.Math.Abs(x1 - x) + System.Math.Abs(y1 - y);
        }
        double manh1(double x, double y, double x1, double y1, double z, double z1)
        {

            return (System.Math.Abs(x1 - x) + System.Math.Abs(y1 - y) + System.Math.Abs(z1 - z));
        }
        double manh2(double x, double x1)
        {

            return System.Math.Abs(x1 - x);
        }

        int k ;
        int kolichestvo = 150;
        int n,m, m1, m2, m3, m4,mm,m11;
        int[] kl = new int[50];
        int[] vib = new int[3];
        int manev = 0;
        //исходный массив точек
        float[,] array1 = new float[150, 5];
        //double[,] array2 = new double[150, 2];
        double m1x, m1y,m1z;
        Bitmap bitmap;
        public Form2(Form1 f1)
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            k = Convert.ToInt32(f1.textBox1.Text);
            m1 = Convert.ToInt32(f1.checkedListBox1.GetItemChecked(0));
            m2 = Convert.ToInt32(f1.checkedListBox1.GetItemChecked(1));
            m3 = Convert.ToInt32(f1.checkedListBox1.GetItemChecked(2));
            m4 = Convert.ToInt32(f1.checkedListBox1.GetItemChecked(3));
            //Выбор метрики
            if (f1.comboBox1.SelectedIndex == 0)
                manev = 1;
            else
                manev = 2;
            //
            int c = 1,c1=0;
            //Выбор переменных для кластеризации
            for (int i = 0; i<f1.checkedListBox1.Items.Count ; i++)
            {
                if (Convert.ToInt32(f1.checkedListBox1.GetItemChecked(i))==1)
                {
                    vib[c1] = c;
                    c1++;
                }
                c++;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = null;
            //количество переменных для кластеризации
            if (m1 == 1 && m2 == 0 && m3 == 0 && m4 == 0 || m1 == 0 && m2 == 1 && m3 == 0 && m4 == 0 || m1 == 0 && m2 == 0 && m3 == 1 && m4 == 0 || m1 == 0 && m2 == 0 && m3 == 0 && m4 == 1)
                mm = 1;
            if (m1 == 1 && m2 == 1 && m3 == 0 && m4 == 0 || m1 == 1 && m2 == 0 && m3 == 1 && m4 == 0 || m1 == 1 && m2 == 0 && m3 == 0 && m4 == 1 || m1 == 0 && m2 == 1 && m3 == 1 && m4 == 0 || m1 == 0 && m2 == 1 && m3 == 0 && m4 == 1 || m1 == 0 && m2 == 0 && m3 == 1 && m4 == 1)
                mm = 2;
            if (m1 == 0 && m2 == 1 && m3 == 1 && m4 == 1 || m1 == 1 && m2 == 1 && m3 == 1 && m4 == 0 || m1 == 1 && m2 == 0 && m3 == 1 && m4 == 1 || m1 == 1 && m2 == 1 && m3 == 0 && m4 == 1)
                mm = 3;
            Graphics g =Graphics.FromImage(bitmap);
            //Graphics g = chart1.CreateGraphics();
            Pen p = new Pen(Color.Black);
            //p.Width = 2;
            g.TranslateTransform( 0, pictureBox1.Height-50);
            string[] str = new string[30];
            str[0] = "0"; str[1] = "1"; str[2] = "2"; str[3] = "3"; str[4] = "4"; str[5] = "5"; str[6] = "6"; str[7] = "7"; str[8] = "8"; str[9] = "9"; str[10] = "10"; str[11] = "11"; str[12] = "12"; str[13] = "13"; str[14] = "14"; str[15] = "15"; str[16] = "16"; str[17] = "17"; str[18] = "18";
           
            if (mm == 2||mm==3 )
            {
                //str[0] = "1"; str[1] = "2"; str[2] = "3"; str[3] = "4"; str[4] = "5"; str[5] = "6"; str[6] = "7"; str[7] = "8"; str[8] = "9"; str[9] = "10"; str[10] = "11"; str[11] = "12"; str[12] = "13"; str[13] = "14"; str[14] = "15";
                //Оси x y
                g.DrawLine(new Pen(Brushes.Black, 2), new Point(20, 0), new Point(pictureBox1.Width, 0));
                g.DrawLine(new Pen(Brushes.Black, 2), new Point(20, 0), new Point(20, -pictureBox1.Height));
                int z = 0;
                //Сетка 
                for (int i = 20; i < pictureBox1.Width; )
                {
                    g.DrawLine(p, new Point(i, 0), new Point(i, -pictureBox1.Height));
                    g.DrawString(str[z], new Font("Times New Roman", 12), Brushes.Black, new PointF(i - 5, 5));
                    z++;
                    i = i + 100;
                }
                z = 0;
                for (int i = 0; i < pictureBox1.Height; )
                {
                    g.DrawLine(p, new Point(20, -i), new Point(pictureBox1.Width, -i));
                    g.DrawString(str[z], new Font("Times New Roman", 12), Brushes.Black, new PointF(-3, -i - 6));
                    i = i + 50;
                    z++;
                }
            }
            if(mm==1)
            {
               //Оси
                g.DrawLine(new Pen(Brushes.Black, 2), new Point(20, -50), new Point(pictureBox1.Width, -50));
                g.DrawLine(new Pen(Brushes.Black, 2), new Point(20, 0), new Point(20, -pictureBox1.Height));
                int z = 0;
                //Сетка
                for (int i = 20; i < pictureBox1.Width; )
                {
                    g.DrawLine(p, new Point(i, 0), new Point(i, -pictureBox1.Height));
                    g.DrawString(str[z], new Font("Times New Roman", 12), Brushes.Black, new PointF(i - 5, 5));
                    z++;
                    i = i + 50;
                }
                z = 0;
                for (int i = 0; i < pictureBox1.Height; )
                {
                    g.DrawLine(p, new Point(20, -i), new Point(pictureBox1.Width, -i));
                    i = i + 50;
                    g.DrawString(str[z], new Font("Times New Roman", 12), Brushes.Black, new PointF(-3, -i - 6));
                    
                    z++;
                }

            }
     
            float[,] centr1 = new float[k, 4];
            double[,] centr2 = new double[k, 4];
            int[] kol1 = new int[k];
            double merror = 0, merror1 = 0;
            float[,] min1 = new float[kolichestvo, k];
            float[,] min2 = new float[kolichestvo, k];
            float[,] min3 = new float[kolichestvo, k];
            float[,] min4 = new float[kolichestvo, k];
            //Рандомный выбор начальных центров
            Random rnd = new Random();
             for(int i=0;i<k;i++){

              kl[i]=rnd.Next(0,150);

             }
           //Считывание файла и запись в массив           
            StreamReader sr = new StreamReader("data.csv");
            string vel = sr.ReadLine();
            string[] vel1 = vel.Split(new char[] { ';' });
            n = 0;
            m = 0;
            m11 = 0;
           
             while (!sr.EndOfStream)
             {
              string temp = sr.ReadLine();
              string[] arr = temp.Split(new char[] { ';' });
              for (int i = 0; i < 4; i++){
                 array1[n, i]=float.Parse(arr[i]);                 
           
               }
              n++;
                         
                  }
            // Центры кластеров
                for (int i = 0; i < k; i++)
                 {   
                     centr1[i, 0] = array1[kl[i], 0];
                     centr1[i, 1] = array1[kl[i], 1];
                     centr1[i, 2] = array1[kl[i], 2];
                     centr1[i, 3] = array1[kl[i], 3];
                }
              
            int t = 0;
            
            do
            {  //+++++++++++++++++++++++++++++++++++++++++++++++++++
                if (t != 0)
                {// записываем полученные центры для проверки
                    for (int i = 0; i < k; i++)
                   {

                        centr2[i, 0] = centr1[i, 0];
                        centr2[i, 1] = centr1[i, 1];
                        centr2[i, 2] = centr1[i, 2];
                        centr2[i, 3] = centr1[i, 3];
                    }
                }
                t++;
                //m2x = array1[kl[1], 0];
                //m2y = array1[kl[1], 2];
            //++++++++++++++++++++++++++++++++++++++++++++++++++
                // double m11x = array1[0,0], m11y=array1[0,1], m12x=array1[0,2],m12y=array1[0,3];
                // double m21x = array1[43, 0], m21y = array1[43, 1], m22x = array1[43, 2], m22y = array1[43, 3];
                //     double m31x = array1[63, 0], m31y = array1[63, 1], m32x = array1[63, 2], m32y = array1[63, 3];
                double[,] rast11 = new double[kolichestvo, k];
                double[,] rast12 = new double[kolichestvo, 4];
                double[,] rast13 = new double[kolichestvo, 4];
                
                double[] min = new double[kolichestvo];
                
                for (int i = 0; i < kolichestvo; i++)
                {
                    for (int j = 0; j < k; j++)
                    {
                        min1[i, j] = 0;
                        min2[i, j] = 0;
                        min3[i, j] = 0;
                        min4[i, j] = 0;
                    }
                }                           
             
                //double[,] min3 = new double[150, 3];
                
                                           
                //// double []min=new double [150];
                //Цикл для поиска расстояния
                for (int j = 0; j < k; j++)
                {
                    m1x = centr1[j, vib[0]-1];
                    if(mm==2||mm==3)//array1[kl[j], 0];
                    m1y = centr1[j, vib[1]-1]; //array1[kl[j], 2];
                    if(mm==3)
                    m1z = centr1[j, vib[2]-1];
                    for (int i = 0; i < kolichestvo; i++)
                    {
                        //  if (i == 0)
                        //   {
                        //   rast11[i, j] = evkl(array1[i, 0], array1[i, 2], m1x, m1y);
                        //   min[n2] = rast11[i, j];
                        //   i++;
                        // }
                        // n1++;
                        //     double p1=System.Math.Pow((array1[i, 0] - array1[i, 1]),2),p2=System.Math.Pow((m11x-m11y),2);
                        if (mm == 1)
                        {
                            if(manev==1)
                            rast11[i, j] = evkl2(array1[i, vib[0] - 1], m1x);
                            else
                            rast11[i, j] = manh2(array1[i, vib[0] - 1], m1x);
                        }
                        if (mm == 2)
                        {
                            if (manev == 1)
                                rast11[i, j] = evkl(array1[i, vib[0] - 1], array1[i, vib[1] - 1], m1x, m1y);
                            else
                                rast11[i, j] = manh(array1[i, vib[0] - 1], array1[i, vib[1] - 1], m1x, m1y);
                        }
                        if (mm == 3)
                        {
                            if (manev == 1)
                                rast11[i, j] = evkl1(array1[i, vib[0] - 1], array1[i, vib[1] - 1], m1x, m1y, array1[i, vib[2] - 1], m1z);
                            else
                                rast11[i, j] = manh1(array1[i, vib[0] - 1], array1[i, vib[1] - 1], m1x, m1y, array1[i, vib[2] - 1], m1z);
                            //     //rast11[i, 1] = (System.Math.Pow(array1[i, 2] - array1[i, 3], 2)) + System.Math.Pow(m12x - m12y, 2);
                        }
         
                    }
                }
               
               //int c = 0;
                int ct = 0;
               //цикл для поиска минимального расстояния
                for (int i = 0; i < kolichestvo; i++)
                {
                    //if (cp == 0)
                    //    c++;
                    min[i] = rast11[i, 0];
                    min1[i, 0] = array1[i, 0];
                    min2[i, 0] = array1[i, 1];
                    min3[i, 0] = array1[i, 2];
                    min4[i, 0] = array1[i, 3];
                   // cp = 0;
                    ct = 0;
                    for (int j = 1; j < k; j++)
                    {
                        if (rast11[i, j] > min[i] && ct==0)
                        {
                            //kol1[j - 1]++;
                            ct = 1;
                        }
                            
                        if (rast11[i, j] < min[i])
                        {
                            min[i] = rast11[i, j];
                            min1[i, 0] = 0;
                            min2[i, 0] = 0;
                            min3[i, 0] = 0;
                            min4[i, 0] = 0;
                            min1[i, j-1] = 0;
                            min2[i, j-1] = 0;
                            min3[i, j-1] = 0;
                            min4[i, j-1] = 0;
                            min1[i, j] = array1[i, 0];
                            min2[i, j] = array1[i, 1];
                            min3[i, j] = array1[i, 2];
                            min4[i, j] = array1[i, 3];
                            
                        }

                        
                    }
                }
                //подсчет коичества элементов принадлежащих определенному кластеру
                for (int i = 0; i < k; i++)
                    kol1[i] = 0;
                
                for (int i = 0; i < k; i++)
                {
                    for (int j = 0; j < kolichestvo; j++)
                    {
                        if (min1[j, i] != 0)
                            kol1[i]++;

                    }
                }
                //рассчет суммы квадратов ошибок
                merror1 = merror;
                    merror = 0;
                for (int i = 0; i < kolichestvo; i++)
                    merror = merror + System.Math.Pow(min[i], 2);
                //рассчет центральных точек кластера
                float centr11, centr21, centr31, centr41;
                for (int i = 0; i < k; i++)
                {
                    centr11 = 0; centr21 = 0; centr31 = 0; centr41 = 0;
                    for (int j = 0; j < kolichestvo; j++)
                    {

                        centr11 = centr11 + min1[j, i];
                        centr21 = centr21 + min2[j, i];
                        centr31 = centr31 + min3[j, i];
                        centr41 = centr41 + min4[j, i];

                    }
                    centr1[i, 0] = centr11 / kol1[i];
                    centr1[i, 1] = centr21 / kol1[i];
                    centr1[i, 2] = centr31 / kol1[i];
                    centr1[i, 3] = centr41 / kol1[i];

                    //   centr1[i,1] = min1[i, 2];

                }
                
                for (int i = 0; i < k; i++)
                {
                    if (centr1[i, 0] == centr2[i, 0] && centr1[i, 1] == centr2[i, 1] && centr1[i, 2] == centr2[i, 2] && centr1[i, 3] == centr2[i, 3])
                    {
                        m11++;
                    }
                    if (m11 == k)
                        m = 1;
                }
               // if (t > 500)
                   // m = 1;
                if (merror == merror1)
                    m = 1;
            } while (m != 1);
             Random r = new Random();
             textBox1.Text = Convert.ToString(merror);
             textBox2.Text = Convert.ToString(t);
            int viv=0,viv1=0,viv2=0;
            //Рисуем точки на Picturebox
             if(mm==2)
                        {
                            if (m1 != 0 && m2 != 0)
                                viv = 1;
                            if (m1 != 0 && m3 != 0)
                                viv = 2;
                            if (m1 != 0 && m4 != 0)
                                viv = 3;
                            if (m2 != 0 && m3 != 0)
                                viv = 4;
                            if (m2 != 0 && m4 != 0)
                                viv = 5;
                            if (m3 != 0 && m4 != 0)
                                viv = 6;
                                                  
                       }
            if(mm==1)
            {   
                if(m1!=0)
                viv1=1;
                if(m2!=0)
                viv1=2;
                if(m3!=0)
                viv1=3;
                if(m4!=0)
                viv1=4;
            }
            if(mm==3)
            {
                if(m1!=0&&m2!=0&&m3!=0)
                    viv2=1;
                if(m1!=0&&m3!=0&&m4!=0)
                    viv2=2;
                if(m2!=0&&m3!=0&&m4!=0)
                    viv2=3;
                if(m1!=0&&m2!=0&&m4!=0)
                    viv2 = 4;
            }
           for (int j = 0; j < k; j++)
            { 
                Pen pen = new Pen(Color.FromArgb(255, r.Next(255), r.Next(255), r.Next(255)));
                Brush b1 = pen.Brush;
                for (int i = 0; i < kolichestvo; i++)
                {
                  //g.FillRectangle(Brushes.Red, centr1[i, 0]*60, -centr1[i, 1]*50, 1, 1);
                    if (min1[i, j] != 0 )
                    {
                       // chart1.Series[j].Points.AddXY(min1[i, j], min3[i, j]);
                        if (mm == 2||mm==3)
                        {
                            if (viv == 1 || viv2 == 1 || viv2 == 4)
                                g.FillEllipse(b1, min1[i, j] * 100, -min2[i, j] * 50, 4, 4);
                            if (viv == 2||viv2==2)
                                g.FillEllipse(b1, min1[i, j] * 100, -min3[i, j] * 50, 4, 4);
                            if (viv == 3)
                                g.FillEllipse(b1, min1[i, j] * 100, -min4[i, j] * 50, 4, 4);
                            if (viv == 4 || viv2 == 3)
                                g.FillEllipse(b1, min2[i, j] * 100, -min3[i, j] * 50, 4, 4);
                            if (viv == 5)
                                g.FillEllipse(b1, min2[i, j] * 100, -min4[i, j] * 50, 4, 4);
                            if (viv == 6)
                                g.FillEllipse(b1, min3[i, j] * 100, -min4[i, j] * 50, 4, 4);
                        }
                        if(mm==1)
                        {
                            if(viv1==1)
                                g.FillEllipse(b1, min1[i, j] * 100, -50, 4, 4);
                            if (viv1 == 2)
                                g.FillEllipse(b1, min2[i, j] * 100, -50, 4, 4);
                            if (viv1 == 3)
                                g.FillEllipse(b1, min3[i, j] * 100, -50, 4, 4);
                            if (viv1 == 4)
                                g.FillEllipse(b1, min4[i, j] * 100, -50, 4, 4);
                        }
                        //if(mm==3)
                        //{
                        //    if(viv2==1)
                        //    {
                        //        xx = (float)(-min3[i, j] * Math.Sin(90) + min1[i, j] * Math.Cos(90));
                        //        yy = (float)(-(min3[i, j] * Math.Cos(90) + min1[i, j] * Math.Sin(90) + min2[i, j] * Math.Cos(90)));
                        //        g.FillEllipse(b1, -xx * 100, yy * 50, 4, 4);

                        //    }
                        //}
                        

                    }

                   
                        Pen pen1 = new Pen(Color.Black);
                        Brush b = pen1.Brush;
                        //chart1.Series[j].Points.AddXY(centr1[j, 0], centr1[j, 2]);
                    //if(mm==3)
                    //{
                    //    xx = (float)(-centr1[j, vib[0] - 1] * Math.Sin(90) + centr1[j, vib[1] - 1] * Math.Cos(90));
                    //    yy = (float)(-(centr1[j, vib[2] - 1] * Math.Cos(90) + centr1[j, vib[0] - 1] * Math.Sin(90) + centr1[j, vib[1] - 1] * Math.Cos(90)));
                    //    g.FillEllipse(b1, -xx * 100, yy * 50, 8, 8);
                    //}
                    //Рисование центров кластеров
                    if(mm==2||mm==3)
                        g.FillEllipse(b1, centr1[j, vib[0]-1] * 100, -centr1[j, vib[1]-1] * 50, 8, 8);
                    if(mm==1)
                        g.FillEllipse(b1, centr1[j, vib[0] - 1] * 100, -50, 8, 8);
                    
                }

            }
            pictureBox1.Image = bitmap;
            //Вывод количества элементов в каждом кластере в Listbox
            listBox1.Items.Clear();
            string s;
            for (int i = 0; i < k; i++)
            {
                s = "Количество элементов кластера" +" "+ (i+1) +" "+ "="+kol1[i];
                listBox1.Items.Add(s);
            }        
        }

        


    }
}
