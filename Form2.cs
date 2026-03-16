using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EXAMEN_PARCIAL
{
    public partial class Form2 : Form
    {
        List<Citas> Citas = new List<Citas>();
        List<Doctores> doctores = new List<Doctores>();
        List<Pacientes> pacientes = new List<Pacientes>();

        string rutaPacientes = "pacientes.txt";
        string rutaDoctores = "doctores.txt";
        string rutaCitas = "citas.txt";

        public Form2()
        {
            InitializeComponent();
        }

        private void registraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Listado();
            CargarDoctores();
            CargarPacientes();
            CargarCitas();
        }

        private void Listado()
        { 
           
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

                        if (!string.IsNullOrWhiteSpace(linea))
                        {
                            string[] datos = linea.Split(',');

                            if (datos.Length >= 3)
                            {
                                Doctores d = new Doctores();

                                d.ID = datos[0];
                                d.Nombre_completo = datos[1];
                                d.Especialidad = datos[2];

                                doctores.Add(d);
                            }
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

                        if (!string.IsNullOrWhiteSpace(linea))
                        {
                            string[] datos = linea.Split(',');

                            if (datos.Length >= 3)
                            {
                                Pacientes p = new Pacientes();

                                p.DPI = datos[0];
                                p.nombre_completo = datos[1];
                                p.telefono = datos[2];

                                pacientes.Add(p);
                            }
                        }
                    }
                }
            }
        }

        private void CargarCitas()
        {
            if (File.Exists(rutaCitas))
            {
                using (StreamReader sr = new StreamReader(rutaCitas))
                {
                    while (!sr.EndOfStream)
                    {
                        string linea = sr.ReadLine();

                        if (!string.IsNullOrWhiteSpace(linea))
                        {
                            string[] datos = linea.Split(',');

                            if (datos.Length >= 4)
                            {
                                Citas cita = new Citas();

                                DateTime fecha;

                                if (DateTime.TryParse(datos[0], out fecha))
                                {
                                    cita.Fecha_de_cita = fecha;
                                }

                                cita.Hora_de_cita = datos[1];
                                cita.ID_doctor = datos[2];
                                cita.DPI_paciente = datos[3];

                                Citas.Add(cita);
                            }
                        }
                    }
                }
            }
        }

    }
}