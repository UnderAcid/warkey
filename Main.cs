using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using WGame;
namespace WKey
{
    public partial class Main : Form
    {
        /*
         * WKey - программа для переназначения предметов на клавиши.
         * Используется WGame.dll min 1.12
         * Программа сделана как пример по работе с WGame а также для своего удобства т.к ничего не подключает
         * и не нагружает компьютер ;)
         * Сварганил Der_SySLIK специально для D3Scene.ru
         */
        Keys[] keycode = new Keys[7];
        bool waractive = false; //Переменная чтоб единожды узнать активен ли WC3 и инициализировать WC3Read
        GlobalKeyboardHook gHook; 
        public Main()
        {
            InitializeComponent();
        }
        void savekeys()
        {
            string generate = "";
            for (int i = 1; i <= 6; i++)
            {
                generate += keycode[i].ToString() + ":";
            }
            generate = generate.Substring(0, generate.Length - 1);
            System.IO.File.WriteAllText("keys.txt", generate);
        }
        void loadkeys()
        {
            string full = System.IO.File.ReadAllText("keys.txt");
            string[] array = full.Split(':');

        }
        private void _keydata(object sender, KeyEventArgs e)
        {
            (sender as Button).Text = e.KeyValue.ToString() + "(" + e.KeyData.ToString() + ")";
            savekeys();
            keycode[Convert.ToInt32((sender as Button).Tag)] = e.KeyCode;
        }

        private void _start_Click(object sender, EventArgs e)
        {
            gHook.hook();
        }

        private void _exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            gHook = new GlobalKeyboardHook();        
            gHook.KeyDown += new KeyEventHandler(MyKeyDown);
            foreach (Keys key in Enum.GetValues(typeof(Keys))) gHook.HookedKeys.Add(key);

        }
        public void MyKeyDown(object sender, KeyEventArgs e)
        {
            if (waractive == false) { Process[] processes = Process.GetProcessesByName("war3"); if (processes.Length > 0) { WC3Read.Init(); waractive = true; } }
            for (int i = 1; i <= 6; i++)
            {
                if (keycode[i] == e.KeyCode) 
                {
                    if (WC3Read.Chat() == false)
                    {
                        switch (i)
                        {
                            case 1:
                                WC3Send.SendNum("7");
                                break;
                            case 2:
                                WC3Send.SendNum("8");
                                break;
                            case 3:
                                WC3Send.SendNum("4");
                                break;
                            case 4:
                                WC3Send.SendNum("5");
                                break;
                            case 5:
                                WC3Send.SendNum("1");
                                break;
                            case 6:
                                WC3Send.SendNum("2");
                                break;
                        }
                    }
                }
            }
        }
    }

}
