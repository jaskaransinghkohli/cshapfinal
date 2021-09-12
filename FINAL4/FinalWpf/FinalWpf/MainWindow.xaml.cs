//latest
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
using System.Text.RegularExpressions;
using System.Xml;

namespace FinalWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ConnectPatient CP { get; set; } = new ConnectPatient(); 
        string selectedTime;
       
        string slotKey;
        Dictionary<string, string> slots = new Dictionary<string, string>() { { "1", "10:00:00" }, { "2", "11:00:00" }, { "3", "12:00:00" }, { "4", "13:00:00" }, { "5", "14:00:00" } };
      
        List<PatientGender> ptntGender = new List<PatientGender>();
        public MainWindow()
        {
          
            InitializeComponent();
            if(File.Exists("Final4.xml"))
            {
                ReadFromXML();
            }
            else{
                var xmlDoc = new XmlDocument();
             string filePath = "Final4.xml";
            if (!File.Exists(filePath))
            {
                XmlNode rootNode = xmlDoc.CreateElement("AppointmentList");
                xmlDoc.AppendChild(rootNode);
                xmlDoc.Save("Final4.xml");

            }
            xmlDoc.Load(filePath);
            }
            if (File.Exists("Patient.xml"))
            {
                ReadFromPatientXML();
            }
            else
            {
                var xmlDoc = new XmlDocument();
                string filePath = "Patient.xml";
                if (!File.Exists(filePath))
                {
                    XmlNode rootNode = xmlDoc.CreateElement("PatientList");
                    xmlDoc.AppendChild(rootNode);
                    xmlDoc.Save("Patient.xml");

                }
                xmlDoc.Load(filePath);
            }


            if (File.Exists("EmployeeRegistration.xml"))
            {
                ReadFromEmployeeRegistrationXML();
            }
            else
            {
                var xmlDoc = new XmlDocument();
                string filePath = "EmployeeRegistration.xml";
                if (!File.Exists(filePath))
                {
                    XmlNode rootNode = xmlDoc.CreateElement("EmployeeList");
                    xmlDoc.AppendChild(rootNode);
                    xmlDoc.Save("EmployeeRegistration.xml");

                }
                xmlDoc.Load(filePath);
            }

            InitialiseComboBoxes();

            //Accessing Classification.cs
            Classification cf = new Classification("abc");
            cf.Appointments = new List<Appointment>();
            cf.Patients = new List<Patient>();
            
            cf.EmployeeRegistrations = new List<EmployeeRegistration>();
            loadTimeSlot();
            IslandGender();
            DataContext = this;

        }
        public void IslandGender()
        {
           
            ptntGender.Add(new PatientGender("Male"));
            ptntGender.Add(new PatientGender("Female"));
            ptntGender.Add(new PatientGender("Others"));
            cb_patient_gender.ItemsSource = ptntGender;
            cb_patient_gender.DisplayMemberPath = "Gender";
        }

        public void loadTimeSlot()
        {
            timeSlot.Items.Clear();
            foreach (var item in slots)
            {

                ComboBoxItem time = new ComboBoxItem();
                time.Content = slots[item.Key];
                timeSlot.Items.Add(time);

            }
        }
        // Accessing Lists 
        AppointmentList appointmentList = new AppointmentList();
        PatientList patientList = new PatientList();
        
        EmployeeList employeeList = new EmployeeList();

        //We are creating random class object that will help us in allocating Id's in our program

        public Random random = new Random();

        /*_________________________________________ LINQ ________________________________________ */

        //Searching 

        public void SearchAppointmentsByPatient(int pId)
        {
            //Treated patients
            var qry = from appointment in appointmentList.Appointments where appointment.PtntId == pId && appointment.Status == StatusType.Treated select appointment;
            Classification clasf = new Classification("abc");
            clasf.Appointments = new List<Appointment>();
            clasf.Appointments = qry.ToList();
            DataContext = clasf;
            dg_viewAppointments.Items.Refresh();
        }
        public void SearchAppointmentByAppointmentType(AppointmentType appointmentType)
        {
            //Searching for pending services
            var qry = from appointment in appointmentList.Appointments where appointment.AppointmentType== appointmentType && appointment.Status == StatusType.Pending select appointment;
            Classification clasf = new Classification("abc");
            clasf.Appointments = new List<Appointment>();
            clasf.Appointments = qry.ToList();
            DataContext = clasf;
            dg_viewAppointments.Items.Refresh();
        }

        /*__________________________________ XML FILE OPERATIONS____________________________________ */

        //Reading Data from xml
        public void ReadFromXML()
        {
            AppointmentList aptList = null;
            string path = "Final4.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(AppointmentList));
            StreamReader reader = new StreamReader(path);
            aptList = (AppointmentList)serializer.Deserialize(reader);
            reader.Close();
            appointmentList = aptList;


        }
        //writing data to xml
        public void WriteToXML(AppointmentList appointmentList)
        {
            AppointmentList aptList = appointmentList;
            XmlSerializer serializer = new XmlSerializer(typeof(AppointmentList));
            TextWriter txtWriter = new StreamWriter("Final4.xml");
            serializer.Serialize(txtWriter, aptList);
            txtWriter.Close();
            UpdateDataContext();
            dg_viewAppointments.Items.Refresh();
        }
      
       //Reading Employee Registration xml
        public void ReadFromEmployeeRegistrationXML()
        {
            EmployeeList empList = null;
            string path = "EmployeeRegistration.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(EmployeeList));
            StreamReader reader = new StreamReader(path);
            empList = (EmployeeList)serializer.Deserialize(reader);
            reader.Close();
            employeeList = empList;
           
        }
        //Write to Employee Registration xml
        public void WriteToEmployeeRegistrationXML(EmployeeList employeeList)
        {
            EmployeeList empList = employeeList;
            XmlSerializer serializer = new XmlSerializer(typeof(EmployeeList));
            TextWriter txtWriter = new StreamWriter("EmployeeRegistration.xml");
            serializer.Serialize(txtWriter, empList);
            txtWriter.Close();
            InitialiseComboBoxes();
            
            UpdateDataContext();
            EmployeeGrid.Items.Refresh();

        }
        //Reading Review xml
     

        //Reading Patient.xml
        private void ReadFromPatientXML()
        {
            PatientList pntList = null;
            string path = "Patient.xml";
            XmlSerializer serializer = new XmlSerializer(typeof(PatientList));
            StreamReader reader = new StreamReader(path);
            pntList = (PatientList)serializer.Deserialize(reader);
            reader.Close();
            patientList = pntList;
        }


        //writing to Patient.xml
        public void WriteToPatientXml(PatientList patientList)
        {
            PatientList pntList = patientList;
            XmlSerializer serializer = new XmlSerializer(typeof(PatientList));
            TextWriter txtWriter = new StreamWriter("Patient.xml");
            serializer.Serialize(txtWriter, pntList);
            txtWriter.Close();
            InitialiseComboBoxes();
            UpdateDataContext();
            dg_viewPatients.Items.Refresh();
        }

        /*______________________________________________________________________________________________________ */

        public void UpdateDataContext()
        {
            Classification cf = new Classification("abc");
            cf.Appointments = new List<Appointment>();
            cf.Appointments = appointmentList.Appointments;

            cf.Patients = new List<Patient>();
            cf.Patients = patientList.Patients;

            
           

            cf.EmployeeRegistrations = new List<EmployeeRegistration>();
            cf.EmployeeRegistrations = employeeList.EmployeeRegistrations;

            DataContext = cf;
            
        }

        private void InitialiseComboBoxes()
        {
/*            cb_patient_gender.Items.Clear();
*/          comboBoxLocation.Items.Clear();
            comboBoxAppointmentType.Items.Clear();
            comboBoxPatientId.Items.Clear();
            comboboxServicePending.Items.Clear();

            /*foreach(GenderType genderType in Enum.GetValues(typeof(GenderType)))
            {
                cb_patient_gender.Items.Add(genderType.ToString());
            }*/

            foreach(LocationType locationType in Enum.GetValues(typeof(LocationType)))
            {
                comboBoxLocation.Items.Add(locationType.ToString());
            }

            foreach(AppointmentType appointmentType in Enum.GetValues(typeof(AppointmentType)))
            {
                comboBoxAppointmentType.Items.Add(appointmentType.ToString());
                comboboxServicePending.Items.Add(appointmentType.ToString());
            }

            foreach (Patient p in patientList.Patients)
            {
                comboBoxPatientId.Items.Add(int.Parse(p.PatientId.ToString()));
            }

        
            
        }
      

        
        public void SearchPatientByPatientID(int ptntId)
        {
            var qry = from Patient in patientList.Patients where Patient.PatientId == ptntId select Patient;
            Classification cf = new Classification("abc");
            cf.Patients = new List<Patient>();
            cf.Patients = qry.ToList();
            DataContext = cf;
            dg_viewPatients.Items.Refresh();
        }

        /*_______________________Events for View and Add Patient Portal_____________________________ */

        public void savepatient()
        {
            ReadFromPatientXML();
            Patient ptnt = new Patient();
            int rangeMin = 1;
            int rangeMax = 200;
            List<int> ptntId = new List<int>();
            foreach (Patient p in patientList.Patients)
            {
                ptntId.Add(p.PatientId);
            }

            //Validating Patient fields
            if (patientValid) // edited by akhil
            {
                //Generating Random Numbers 
                do
                {
                    ptnt.PatientId = random.Next(rangeMin, rangeMax);
                } while (ptntId.Contains(ptnt.PatientId));
                ptnt.PatientName = txt_patient_name.Text;
                // ptnt.PatientGender = (GenderType)Enum.Parse(typeof(GenderType), cb_patient_gender.SelectedItem.ToString());
                ptnt.PatientAge = int.Parse(txt_patient_age.Text);
                txt_patient_Id.Text = ptnt.PatientId.ToString();

                patientList.Patients.Add(ptnt);
                WriteToPatientXml(patientList);

            }


        }
        private void Add_patient_Click(object sender, RoutedEventArgs e)
        {
            validatePatient();

            Patient ptnt = new Patient();
            int rangeMin = 1;
            int rangeMax = 200;
            List<int> ptntId = new List<int>();
            foreach(Patient p in patientList.Patients)
            {
                ptntId.Add(p.PatientId);
            }

            //Validating Patient fields
            if (patientValid) // edited by akhil
            {
                //Generating Random Numbers 
                do
                {
                    ptnt.PatientId = random.Next(rangeMin, rangeMax);
                } while (ptntId.Contains(ptnt.PatientId));
                ptnt.PatientName = txt_patient_name.Text;
               // ptnt.PatientGender = (GenderType)Enum.Parse(typeof(GenderType), cb_patient_gender.SelectedItem.ToString());
                ptnt.PatientAge = int.Parse( txt_patient_age.Text);
                txt_patient_Id.Text = ptnt.PatientId.ToString();

                patientList.Patients.Add(ptnt);
                WriteToPatientXml(patientList);
                MessageBox.Show("Patient added Successfully \n Your Patient ID is:" + ptnt.PatientId.ToString());

                //Clearing Patient Fields
                txt_patient_name.Text = string.Empty;
                cb_patient_gender.SelectedItem = null;
                txt_patient_age.Text = string.Empty;
                txt_patient_Id.Text = string.Empty;
               
            }
           

        }
        /*_________________________________________________________________________________________________*/

        
        /*_______________________________Refresh/Reload for View and Add Patient Portal_____________________ */
        
        private void RefreshPatient_Click(object sender, RoutedEventArgs e)
        {
            UpdateDataContext();
            dg_viewPatients.Items.Refresh();
        }

        /*__________________________________________________________________________________________________*/
        private bool CheckPatient()
        {
            if(txt_patient_name.Text != string.Empty && txt_patient_age.Text !=string.Empty && cb_patient_gender.SelectedItem != null)
            {
                int patientAge = 0;
                string ageOptions = "";
                ageOptions = txt_patient_age.Text;
                if(!int.TryParse(ageOptions,out patientAge))
                {
                    MessageBox.Show("Patient Age can only be a number");
                    txt_patient_age.Text = string.Empty;
                    return false;

                }
                return true;
            }
            return false;

        }

        /*___________________________Updating Patients in Add And View Patient Portal_________________ */
        private void UpdatePatient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(dg_viewPatients.SelectedItem != null)
                {
                    var presentPatient = (Patient)dg_viewPatients.SelectedItem;
                    foreach(Patient p in patientList.Patients)
                    {
                        if(presentPatient.PatientId == p.PatientId)
                        {
                            p.PatientId = presentPatient.PatientId;
                            p.PatientName = presentPatient.PatientName;
                            p.PatientAge = presentPatient.PatientAge;
                            p.PatientGender = presentPatient.PatientGender;
                            WriteToPatientXml(patientList);
                            MessageBox.Show("Patient Updated Successfully");
                        }
                    }
                    dg_viewPatients.SelectedItem = null;
                }
                else
                {
                    MessageBox.Show("Edit valid Item to update");
                }

            }
            catch
            {
                MessageBox.Show("Edit valid Item to update");
            }
        }
       
        /*________________Deleting Patients from Add and View Patient Portal_____________ */
        private void Button_Delete_Patient_Click(object sender, RoutedEventArgs e)
        {
            if(dg_viewPatients.SelectedItem != null)
            {
                var presentPatient = (Patient)dg_viewPatients.SelectedItem;
                patientList.Patients.Remove(presentPatient);
                WriteToPatientXml(patientList);
                dg_viewPatients.SelectedItem = null;
                MessageBox.Show("Patient Removed Successfully");
            }
            else
            {
                MessageBox.Show("Select a Patient to be removed");
            }

        }


        /*_____________________________________________________________________________________________________ */

        /*______ Searching Patients That Are Treated using Patient ID in View and Fix Appointment Portal______*/
        private void PntntSearch_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxPtntNameSearch.Text != string.Empty)
            {
                int ptntId = int.Parse(textBoxPtntNameSearch.Text);
                List<int> ptntIds = new List<int>();
                foreach (Appointment apt in appointmentList.Appointments)
                {
                    ptntIds.Add(apt.PtntId);
                }
                if (!ptntIds.Contains(ptntId))
                {
                    MessageBox.Show("No Patient ID present");
                }
                else
                {
                    SearchAppointmentsByPatient(ptntId);
                }
            }
            else
            {
                MessageBox.Show("Enter a valid Patient ID");
            }
        }

        /*__________Changing status of pending patient to treated in View and Fix Appointment Portal____________ */
        private void Change_Status_Click(object sender, RoutedEventArgs e)
        {
            if(dg_viewAppointments.SelectedItem != null)
            {
                //Casting
                var presentAppointment = (Appointment)dg_viewAppointments.SelectedItem;
                presentAppointment.Status = StatusType.Treated;
                foreach(Appointment apt in appointmentList.Appointments)
                {
                    if (presentAppointment.AppointmentId == apt.AppointmentId)
                    {
                        apt.AppointmentId = presentAppointment.AppointmentId;
                        WriteToXML(appointmentList);
                        MessageBox.Show("Status Changed :)");
                    }
                }
                dg_viewAppointments.SelectedItem = null;

            }
            else
            {
                MessageBox.Show("Please select an appointment to be treated");
            }
        }

        
        /*__________Searching Patients by their Appointment type In View And Fix Appointment Portal_________ */
    
        private void PendingSearch_Click(object sender, RoutedEventArgs e)
        {
            if(comboboxServicePending.SelectedItem != null)
            {
                AppointmentType aptType = (AppointmentType)Enum.Parse(typeof(AppointmentType), comboboxServicePending.SelectedItem.ToString());
                SearchAppointmentByAppointmentType(aptType);
            }
            else
            {
                MessageBox.Show("Enter a valid sevice");
            }

        }

       

        /*____________ Updating Appointments in View And Fix Appointment Portal_______________*/
        private void button_update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(dg_viewAppointments.SelectedItem != null)
                {
                    var presentAppointment = (Appointment)dg_viewAppointments.SelectedItem;
                    foreach(Appointment apt in appointmentList.Appointments)
                    {
                        if(presentAppointment.AppointmentId == apt.AppointmentId)
                        {
                            apt.AppointmentId = presentAppointment.AppointmentId;
                            apt.PtntId = presentAppointment.PtntId;
                            apt.Datetime = presentAppointment.Datetime;
                            apt.Location = presentAppointment.Location;
                            apt.Status = presentAppointment.Status;
                            apt.AppointmentType = presentAppointment.AppointmentType;
                            WriteToXML(appointmentList);
                            MessageBox.Show("Appointment Updated !!");
                        }
                    }
                    dg_viewAppointments.SelectedItem = null;
                }
                else
                {
                    MessageBox.Show("Kindly select an appointment and edit values to be updated");
                }

            }
            catch(Exception)
            {
                MessageBox.Show("Kindly select an appointment and edit values to be updated");
            }

        }
        /*__________ Event to go back in View and Fix Appointment Portal___________ */
        private void button_Back_Click(object sender, RoutedEventArgs e)
        {
            UpdateDataContext();
            textBoxPtntNameSearch.Text = string.Empty;
            comboboxServicePending.SelectedItem = null;
        }
        /*______Event for booking Appoitnment for patients in View and Fix Appointment Portal_______ */
        private void Book_Appointment_Click(object sender, RoutedEventArgs e)
        {
            Appointment apt = new Appointment();
            int rangeMin = 100;
            int rangeMax = 1000;
            apt.AppointmentId = random.Next(rangeMin, rangeMax);
            List<int> aptId = new List<int>();
            foreach(Appointment app in appointmentList.Appointments)
            {
                aptId.Add(app.AppointmentId);
            }

            //Validating patient fields
            validateAppointment();

            if (appointmentValid)
            {
                //Generating Random Id's
                do
                {
                    apt.AppointmentId = random.Next(rangeMin, rangeMax);
                } while (aptId.Contains(apt.AppointmentId));

                apt.AppointmentType = (AppointmentType)Enum.Parse(typeof(AppointmentType), comboBoxAppointmentType.SelectedItem.ToString());
                apt.PtntId = int.Parse(comboBoxPatientId.SelectedItem.ToString());
                apt.Datetime = selectedTime;
                apt.Location = (LocationType)Enum.Parse(typeof(LocationType), comboBoxLocation.SelectedItem.ToString());
                apt.Status = StatusType.Pending;

                appointmentList.Appointments.Add(apt);
                WriteToXML(appointmentList);
                MessageBox.Show("Congratulations,Appointed is added \n Patient's Appointment ID is :" + apt.AppointmentId.ToString());
                
                //cleaning
                comboBoxAppointmentType.SelectedItem = null;
                comboBoxLocation.SelectedItem = null;
                comboBoxPatientId.SelectedItem = null;
                
            }
           
        }



        /*_________________________________________________________________________________________________ */
        private bool CheckAppointment()
        {
            if(comboBoxAppointmentType.SelectedItem !=null && comboBoxLocation.SelectedItem !=null && comboBoxPatientId.SelectedItem != null)
            {
                return true;
            }
            return false;
        }

        private void Button_registerEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeRegistration employeeRegistration = new EmployeeRegistration();
            int rangeMin = 1;
            int rangeMax = 50;
            List<int> empId = new List<int>();
            foreach(EmployeeRegistration emp in employeeList.EmployeeRegistrations)
            {
                empId.Add(emp.EmpId);
            }
            validateUser();
            //Validating Employee Fields
            if (userValid)
            {
                //Generating Random Employee ID
                do
                {
                    employeeRegistration.EmpId = random.Next(rangeMin, rangeMax);
                } while (empId.Contains(employeeRegistration.EmpId));

                employeeRegistration.EmpUsername = txt_empUserName.Text;
                employeeRegistration.EmpPassword = pb_empPassword.Password;
                employeeId.Text = employeeRegistration.EmpId.ToString();

                employeeList.EmployeeRegistrations.Add(employeeRegistration);
                WriteToEmployeeRegistrationXML(employeeList);
                MessageBox.Show("Registration Completed \n your Employee ID is :" + employeeRegistration.EmpId.ToString());
                
                //Clear fields
                txt_empUserName.Text = null;
                pb_empPassword.Password = null;
                EmployeeGrid.ItemsSource = employeeList.EmployeeRegistrations;
            }
            
        }

        private bool CheckEmployee()
        {
            if(txt_empUserName.Text != "" && pb_empPassword.Password != "")
            {
                return true;
            }
            return false;
        }
        private void LogoutEmployee_Click(object sender, RoutedEventArgs e)
        {
            var loginEmployee = new SignIn();
            loginEmployee.Show();
            this.Close();

        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox selectedSlot = (ComboBox)sender;
            ComboBoxItem selectedCombo = (ComboBoxItem)selectedSlot.SelectedItem;
            selectedTime = selectedCombo.Content.ToString();

            foreach (var item in slots)
            {
                if (slots[item.Key] == selectedTime)
                {
                    slotKey = slots[item.Key];
                }

            }
        }
        private void TimeSelected(object sender, RoutedEventArgs e)
        {
            ComboBoxItem time = (ComboBoxItem)sender;
            selectedTime = time.Content.ToString();
        }

     
        
       
        // validation of patient

        string emptyError = "Must be filled";
        bool nameFlag = false;
        bool genderFlag = false;
        bool ageFlag = false;
        bool patientValid = false;
        private void validateName()
        {

            //patient name validation
            if (txt_patient_name.Text.Length <= 0)
            {
                nameFlag = false;
                nameError.Content = emptyError;
                nameError.Visibility = Visibility.Visible;
                txt_patient_name.BorderBrush = Brushes.Red;

            }
            else if (Regex.IsMatch(txt_patient_name.Text, @"^[a-zA-Z- ]+$") == false)
            {
                nameFlag = false;
                nameError.Content = "Invalid Characters";
                nameError.Visibility = Visibility.Visible;
                txt_patient_name.BorderBrush = Brushes.Red;
            }
            else
            {
                nameFlag = true;
                nameError.Visibility = Visibility.Hidden;
                txt_patient_name.BorderBrush = Brushes.Black;
            }
        }

        private void validateGender()
        {
            //patient gender validation
            if (cb_patient_gender.SelectedIndex<= -1)
            {
                genderFlag = false;
                genderError.Content = emptyError;
                genderError.Visibility = Visibility.Visible;
                cb_patient_gender.BorderBrush = Brushes.Red;
            }
            else
            {
                genderFlag = true;
                genderError.Visibility = Visibility.Hidden;
                txt_patient_name.BorderBrush = Brushes.Black;
            }


        }

        private void validateAge()
        {
            //patient age validation
            int patientAge;
            if (txt_patient_age.Text.Length <= 0)
            {
                ageFlag = false;
                ageError.Content = emptyError;
                ageError.Visibility = Visibility.Visible;
                txt_patient_age.BorderBrush = Brushes.Red;
            }
            else if (!int.TryParse(txt_patient_age.Text, out patientAge) || (patientAge <= 3 || patientAge >= 85))
            {
                ageFlag = false;
                ageError.Content = "Enter a valid age";
                ageError.Visibility = Visibility.Visible;
                txt_patient_age.BorderBrush = Brushes.Red;
            }
            else
            {
                ageFlag = true;
                ageError.Visibility = Visibility.Hidden;
                txt_patient_age.BorderBrush = Brushes.Black;
            }
        }

        bool serviceFlag = false;
        bool patientIdFlag = false;
        bool timeslotFlag = false;
        bool appointmentValid = false;
        private void validateService()
        {
            if (comboBoxAppointmentType.SelectedIndex <= -1)
            {
                serviceFlag = false;
                serviceError.Content = emptyError;
                serviceError.Visibility = Visibility.Visible;
                comboBoxAppointmentType.BorderBrush = Brushes.Red;
            }
            else
            {
                serviceFlag = true;
                serviceError.Visibility = Visibility.Hidden;
                comboBoxAppointmentType.BorderBrush = Brushes.Black;
            }
        }

        private void validatePatientId()
        {
            if (comboBoxPatientId.SelectedIndex <= -1)
            {
                patientIdFlag = false;
                patientError.Content = emptyError;
                patientError.Visibility = Visibility.Visible;
                comboBoxPatientId.BorderBrush = Brushes.Red;
            }
            else
            {
                patientIdFlag = true;
                patientError.Visibility = Visibility.Hidden;
                comboBoxPatientId.BorderBrush = Brushes.Black;
            }
        }


        private void validateTimeslot()
        {
            
                if (timeSlot.SelectedIndex <= -1)
            {
                timeslotFlag = false;
                timeError.Content = emptyError;
                timeError.Visibility = Visibility.Visible;
                timeSlot.BorderBrush = Brushes.Red;
            }
            else
            {
                timeslotFlag = true;
                timeError.Visibility = Visibility.Hidden;
                timeSlot.BorderBrush = Brushes.Black;
            }
        }

        private void validateAppointment()
        {
            validateService();
            validatePatientId();
            validateTimeslot();

            if(serviceFlag && patientIdFlag && timeslotFlag)
            {
                appointmentValid = true;
            }
            else
            {
                appointmentValid = false;
            }
        }

        private void validatePatient()
        {

            validateName();

            validateGender();

            validateAge();

            if(nameFlag && genderFlag && ageFlag)
            {
                patientValid = true;
            }
            else
            {
                patientValid = false;
            }
        }

        // add user validation 

        bool usernameFlag = false;
        bool passwordFlag = false;
        bool userValid = false;
        

        private void validateUserName()
        {

            //username validation
            if (txt_empUserName.Text.Length <= 0)
            {
                usernameFlag = false;
                usernameError.Content = emptyError;
                usernameError.Visibility = Visibility.Visible;
                txt_empUserName.BorderBrush = Brushes.Red;

            }
            else if (Regex.IsMatch(txt_empUserName.Text, @"^[a-zA-Z- ]+$") == false)
            {
                usernameFlag = false;
                usernameError.Content = "Invalid Characters";
                usernameError.Visibility = Visibility.Visible;
                txt_empUserName.BorderBrush = Brushes.Red;
            }
            else
            {
                usernameFlag = true;
                usernameError.Visibility = Visibility.Hidden;
                txt_empUserName.BorderBrush = Brushes.Black;
            }
        }

        private void validatePassword()
        {
            if(pb_empPassword.Password.Length<=0)
            {
                passwordFlag = false;
                passwordError.Content = emptyError;
                passwordError.Visibility = Visibility.Visible;
                pb_empPassword.BorderBrush = Brushes.Red;
            }
            else if(pb_empPassword.Password.Length <=8)
            {
                passwordFlag = false;
                passwordError.Content = "Password must have alteast 8 characters";
                passwordError.Visibility = Visibility.Visible;
                pb_empPassword.BorderBrush = Brushes.Red;
            }
            else
            {
                passwordFlag = true;
                passwordError.Visibility = Visibility.Hidden;
                pb_empPassword.BorderBrush = Brushes.Black;
            }
        }

        private void validateUser()
        {
            validateUserName();
            validatePassword();
            if (usernameFlag && passwordFlag)
            {
                userValid = true;
            }
            else
            {
                userValid = false;
            }
        }

       

        private void displayPatient_Click(object sender, RoutedEventArgs e)
        {
            savepatient();
            Patient ptnt = new Patient();
            BindingOperations.ClearAllBindings(dg_viewPatients);
            dg_viewPatients.ItemsSource = (System.Collections.IEnumerable)patientList.Patients;
        }

        private void EditRow_Click(object sender, RoutedEventArgs e)
        {
            if (dg_viewPatients.SelectedItem != null)
            {
                // Appointment seletedAppt = (Appointment)dg_viewPatients.SelectedItem;
                Patient ptnt = (Patient)dg_viewPatients.SelectedItem;
                txt_patient_name.Text = ptnt.PatientName;
                txt_patient_age.Text = ptnt.PatientAge.ToString();
                txt_patient_Id.Text = ptnt.PatientId.ToString();
                cb_patient_gender.Text = ptnt.PatientGender.ToString();
                Add_patient.Content = "Update Patient";
            }
            else
            {
                MessageBox.Show("Kindly select a row to edit");
            }
        }

        private void SavePatient_update(object sender, RoutedEventArgs e)
        {
            if (dg_viewPatients.SelectedItem != null)
            {
                Patient ptnt = patientList.Patients[dg_viewPatients.SelectedIndex];
                ptnt.PatientName = txt_patient_name.Text;
                ptnt.PatientAge = int.Parse(txt_patient_age.Text);
                ptnt.PatientGender = ((PatientGender)cb_patient_gender.SelectedItem).ptntGender;
                WriteToPatientXml(patientList);
                //Clearing Fields
                txt_patient_name.Text = string.Empty;
                cb_patient_gender.SelectedItem = null;
                txt_patient_age.Text = string.Empty;
                txt_patient_Id.Text = string.Empty;

            }
            else
            {
                MessageBox.Show("Kindly edit to save");
            }
        }

        private void AgeContextXhanges(object sender, DependencyPropertyChangedEventArgs e)
        {
            
            object CurrentTask = null;
            txt_patient_age.DataContext = CurrentTask;
        }

        private void Edit_Row_Click(object sender, RoutedEventArgs e)
        {
            if (dg_viewPatients.SelectedItem != null)
            {
                
                Patient ptnt = (Patient)dg_viewPatients.SelectedItem;
                txt_patient_name.Text = ptnt.PatientName;
                txt_patient_age.Text = ptnt.PatientAge.ToString();
                txt_patient_Id.Text = ptnt.PatientId.ToString();
                cb_patient_gender.Text = ptnt.PatientGender.ToString();
            }
            else
            {
                MessageBox.Show("Please select a row to edit");
            }


        }
    }
}
