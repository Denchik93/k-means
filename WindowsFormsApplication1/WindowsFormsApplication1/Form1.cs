using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
  

namespace WindowsFormsApplication1
{
    
    public partial class Form1 : Form
    {

        Form2 frm2;
        public Form1()
        {
            InitializeComponent();

              
            }

      

        private void button1_Click(object sender, EventArgs e)
        {
            //Проверка на введенные данные
            if (checkedListBox1.CheckedItems.Count != 0 && textBox1.Text != "0" && textBox1.Text != "")
            {
                frm2 = new Form2(this);
                frm2.Show();
            }
            else if (checkedListBox1.CheckedItems.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы одну переменную");
            }
            if (textBox1.Text == "" || textBox1.Text == "0")
               MessageBox.Show("Введите количесво кластеров");
                              
        }

          private void checkedListBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //Если отмечено больше 3 элементов, то снимаем выделение со всех и отмечаем текущий.
            if (checkedListBox1.CheckedItems.Count > 3)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    checkedListBox1.SetItemChecked(i, false);
                checkedListBox1.SetItemChecked(checkedListBox1.SelectedIndex, true);
            }
        }

         
       

      
    }
            
        }

 
    
  
     

