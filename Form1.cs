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

namespace EXAMEN_PARCIAL
{
    public partial class Form1 : Form
    {
        List<Pacientes> pacientes = new List<Pacientes>();
        List<Citas> citas = new List<Citas>();
        List<Doctores> doctores = new List<Doctores>();
        string rutaPacientes = "pacientes.txt";
        string rutaDoctores = "doctores.txt";
        string ruta = "citas.txt";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarPacientes();
            CargarDoctores();
            CargarCitas();
            ActualizarDataGridView();
        }

        private void CargarCitas()
        {
            if (File.Exists(ruta))
            {
                using (StreamReader sr = new StreamReader(ruta))
                {
                    while (!sr.EndOfStream)
                    {
                        string linea = sr.ReadLine();
                        string[] datos = linea.Split(',');
                        if (datos.Length >= 3)
                        {
                            Citas c = new Citas();
                            c.ID_doctor = datos[0];
                            c.DPI_paciente = datos[1];
                            c.Fecha_de_cita = DateTime.Parse(datos[2]);
                            c.Hora_de_cita = datos[3];
                            citas.Add(c);
                        }
                    }
                }
            }
        }
        private void ActualizarDataGridView()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = citas;

        }
        private void CargarDoctores()
        {
            if (File.Exists(rutaDoctores))
            {
                using (StreamReader sr = new StreamReader(rutaDoctores))
                {
                    while (!sr.EndOfStream)
                    {
                        string linea = sr.ReadLine();
                        string[] datos = linea.Split(',');

                        if (datos.Length >= 2)
                        {
                            Doctores d = new Doctores();
                            d.ID = datos[0];
                            d.Nombre_completo = datos[1];
                            d.Especialidad = datos[2];

                            doctores.Add(d);

                            comboBox1.Items.Add(d.ID + "-" + d.Nombre_completo + "-" + d.Especialidad);
                        }
                    }
                }
            }
        }
        private void CargarPacientes()
        {
            if (File.Exists(rutaPacientes))
            {
                using (StreamReader sr = new StreamReader(rutaPacientes))
                {
                    while (!sr.EndOfStream)
                    {
                        string linea = sr.ReadLine();
                        string[] datos = linea.Split(',');

                        if (datos.Length >= 2)
                        {
                            Pacientes p = new Pacientes();
                            p.DPI = datos[0];
                            p.nombre_completo = datos[1];
                            p.telefono = datos[2];

                            pacientes.Add(p);

                            comboBox2.Items.Add(p.DPI + "-" + p.nombre_completo + "-" + p.telefono);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedIndex >= 0 && comboBox2.SelectedIndex >= 0)
                {
                    string doctorSeleccionado = comboBox1.SelectedItem.ToString();
                    string pacienteSeleccionado = comboBox2.SelectedItem.ToString();
                    string[] doctorDatos = doctorSeleccionado.Split('-');
                    string[] pacienteDatos = pacienteSeleccionado.Split('-');
                    Citas nuevaCita = new Citas
                    {
                        ID_doctor = doctorDatos[0],
                        DPI_paciente = pacienteDatos[0],
                        Fecha_de_cita = dateTimePicker1.Value.Date,
                        Hora_de_cita = dateTimePicker2.Value.ToString("HH:mm")
                    };
                    citas.Add(nuevaCita);
                    using (StreamWriter sw = new StreamWriter(ruta, true))
                    {
                        sw.WriteLine($"{nuevaCita.ID_doctor},{nuevaCita.DPI_paciente},{nuevaCita.Fecha_de_cita:yyyy-MM-dd},{nuevaCita.Hora_de_cita}");
                    }
                    MessageBox.Show("Cita agendada exitosamente.");
                    ActualizarDataGridView();
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un doctor y un paciente.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agendar la cita: " + ex.Message);
            }
        }

        private void visualizacionDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();

        }
    }

}
