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
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace FinalWpf
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        private string userName;
        private string passwd;
        private bool loginStatus;
        public SignIn()
        {
            InitializeComponent();
            if (File.Exists("EmployeeRegistration.xml"))
                ReadFromEmployeeRegistrationXML();
            Classification classification = new Classification("xyz");
            classification.EmployeeRegistrations = new List<EmployeeRegistration>();
        }
        EmployeeList employeeLst = new EmployeeList();

        //reading employee registration xml
        private void ReadFromEmployeeRegistrationXML()
        {
            EmployeeList myEmpList = null;
            string path = "EmployeeRegistration.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(EmployeeList));
            StreamReader reader = new StreamReader(path);
            myEmpList = (EmployeeList)serializer.Deserialize(reader);
            reader.Close();
            employeeLst = myEmpList;
        }

        //Checking for login credentials
        private void Button_LoginDetails_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("username = admin \n password = admin");

        }

        private void Button_Developer_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Developed By: Jaskaran Singh");

        }

        private void Button_signIn_Click(object sender, RoutedEventArgs e)
        {
            
            userName = username.Text;
            passwd = password.Password;

            if (userName != "" && passwd != "")
            {

                foreach (EmployeeRegistration er in employeeLst.EmployeeRegistrations)
                {
                    if (userName == er.EmpUsername && passwd == er.EmpPassword)
                    {
                        loginStatus = true;
                    }

                }
                if (loginStatus == true)
                {
                    var welcomeForm = new MainWindow();
                    welcomeForm.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Username and Password!");
                }
            }
            else
            {
                MessageBox.Show("Username and Password are required Fields!");
            }
        }
    }
}
