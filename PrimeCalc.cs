using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calc
{
    public partial class PrimeCalc : Form
    {
        /// <summary>
        /// Первый операнд (до оператора)
        /// </summary>
        double operand;
        /// <summary>
        /// Второй операнд (после оператора)
        /// </summary>
        double op = 0;
        /// <summary>
        /// Код операции
        /// </summary>
        int operation;
        /// <summary>
        /// Флаг наличия в числе десятичного разделителя (не может быть двух разделителей)
        /// </summary>
        bool pointflag = false;
        /// <summary>
        /// Флаг предыдущего равенства. Устанавливается после операции =, сбрасывается в остальных случаях
        /// </summary>
        bool eqflag = false;

        bool opflag = false;

        public PrimeCalc()
        {
            InitializeComponent();
            Txt.Text = "0";
        }
       /// <summary>
       /// Обработчик цифровых кнопок
       /// </summary>   
        private void num_click(object sender, EventArgs e)
        {
            if (Txt.Text == "0" || eqflag) Txt.Text = "";
            this.Txt.Text += ((Button)sender).Text;
            eqflag = false;            
        }
        /// <summary>
        /// Обработчик кнопок операций
        /// </summary>
        private void oper_click(object sender, EventArgs e)
        {

            if (eqflag) MidRes.Text = Txt.Text + ((Button)sender).Text;
            try
            {
                operand = Convert.ToDouble(this.Txt.Text);
            }
            catch (Exception) { Txt.Text = "Неверный формат"; }

            op = 0;
            MidRes.Text = this.Txt.Text + ((Button)sender).Text;
            Txt.Text = "";

            switch (((Button)sender).Text)
            {
                case "+": operation = 1; break;
                case "-": operation = 2; break;
                case "*": operation = 3; break;
                case "/": operation = 4; break;
            }

            pointflag = false;
            eqflag = false;
            opflag = true;
        }

        private void symoper()
        {
            if (true)
            {
                MidRes.Text = Convert.ToString(operand);
                switch (operation)
                {
                    case 1: MidRes.Text += "+"; break;
                    case 2: MidRes.Text += "-"; break;
                    case 3: MidRes.Text += "*"; break;
                    case 4: MidRes.Text += "/"; break;
                }
                MidRes.Text += Convert.ToString(op);
                MidRes.Text += "=";
            }
        }
        private void result()
        {
            try
            {
                if (!eqflag)
                    op = Convert.ToDouble(Txt.Text);
            }
            catch (Exception) { Txt.Text = "Неверный формат"; }
            symoper();
            bool error = false;
            switch (operation)
            {
                case 1: operand += op; break;
                case 2: operand -= op; break;
                case 3: operand *= op; break;
                case 4:
                    if (op!= 0)
                        operand /= op;
                    else { Txt.Text = "На ноль делить нельзя"; error = true; }
                    break;
            }
            if (!error) Txt.Text = Convert.ToString(operand);
            eqflag = true;
            opflag = false;
        }


      
        /// <summary>
        /// Обработчик кнопки =
        /// </summary>
        private void Res_Click(object sender, EventArgs e)
        {
            result();                     
        }
        /// <summary>
        /// Обработчик кнопки .
        /// </summary>
        private void point_click(object sender, EventArgs e)
        {
            if (eqflag)
            {
                op = operand = 0;
                Txt.Text = "0,";
                pointflag = true;
                eqflag = false;
                return;
            }
            if (!pointflag)
            {
                Txt.Text += ",";
                pointflag = true;
            }
        }

        /// <summary>
        /// Обработчик кнопки СЕ
        /// </summary>
        private void CE_Click(object sender, EventArgs e)
        {
            MidRes.Text = "";
            Txt.Text = "0";
            operand = op = 0;
            operation = 0;
            pointflag = false;
            eqflag = false;
            opflag = true;
        }
        /// <summary>
        /// Обработчик нажатия клавиш + - * /
        /// </summary>
        /// <param name="a">+-*/</param>
        private void OperBoard(char a)
        {
            if (eqflag)
            {
                MidRes.Text = Convert.ToString(operand) + a;
            }
            else
            {
                operand = Convert.ToDouble(Txt.Text);
                MidRes.Text = Txt.Text;
                MidRes.Text += a;
                Clipboard.SetText(operand.ToString());
            }
            Txt.Text = "0";
            opflag = true;
            pointflag = false;
            eqflag = false;
        }

        private void keyboard(object sender, KeyPressEventArgs e)
        {

            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)|| e.KeyChar == ','))
                e.Handled = true;

            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == ',')
            {
                if (this.Txt.Text == "0"||eqflag) this.Txt.Text = "";
            }
            else
            {
                switch (e.KeyChar)
                {
                    case '+': OperBoard(e.KeyChar); operation = 1; return;
                    case '-': OperBoard(e.KeyChar); operation = 2; return; 
                    case '*': OperBoard(e.KeyChar); operation = 3; return; 
                    case '/': OperBoard(e.KeyChar); operation = 4; return;                   
                }
                if (e.KeyChar == (char)13) { result(); return; }
                Txt.Text = "Только цифры";
            }                     
        }


    }
}
