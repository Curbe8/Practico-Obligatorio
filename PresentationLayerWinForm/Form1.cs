using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PresentationLayerWinForm
{
    public partial class Form1 : Form
    {
        // Intefaz que se utilizara
        private ServiceLayer.IServiceEmployees IEmp = new ServiceLayer.ServiceEmployees();
        public Form1()
        {
            InitializeComponent();
            List<Employee> lista = IEmp.GetAllEmployees();

            listView1.Items.Clear();

            ListViewItem item;
            lista.ForEach(emp =>
            {
                item = new ListViewItem(emp.Id.ToString());
                item.SubItems.Add(emp.Name);
                item.SubItems.Add(emp.StartDate.ToString());
                if (emp.GetType() == typeof(FullTimeEmployee))
                    item.SubItems.Add("Mensual");
                else
                    item.SubItems.Add("Jornalero");
                listView1.Items.Add(item);
            });
            buttonCancelar.Visible = false;
            buttonModificar.Visible = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            List<Employee> lista = IEmp.GetAllEmployees();

            listView1.Items.Clear();

            ListViewItem item;
            lista.ForEach(emp =>
            {
                item = new ListViewItem(emp.Id.ToString());
                item.SubItems.Add(emp.Name);
                item.SubItems.Add(emp.StartDate.ToString());
                if (emp.GetType() == typeof(Shared.Entities.FullTimeEmployee))
                    item.SubItems.Add("Mensual");
                else
                    item.SubItems.Add("Jornalero");
                listView1.Items.Add(item);
            });
        }

        private void ButtonAgregar_Click(object sender, EventArgs e)
        {
            String nombre = textNombre.Text, tipo = comboTipo.Text;
            Shared.Entities.Employee nuevoEmp;

            if (nombre.Equals("") || tipo.Equals("") || textId.Text.Equals("") || textSalario.Text.Equals(""))
                labelMensaje.Text = "Faltan datos";
            else
            {
                try
                {
                    int id = int.Parse(textId.Text), salario = int.Parse(textSalario.Text);
                    if (tipo.Equals("Jornalero"))
                    {
                        nuevoEmp = new Shared.Entities.PartTimeEmployee()
                        {
                            Id = id,
                            HourlyRate = salario,
                            Name = nombre,
                            StartDate = new DateTime()
                        };
                    }
                    else
                    {
                        nuevoEmp = new Shared.Entities.FullTimeEmployee()
                        {
                            Id = id,
                            Salary = salario,
                            Name = nombre,
                            StartDate = new DateTime()
                        };
                    }
                    IEmp.AddEmployee(nuevoEmp);
                    this.Button1_Click(sender, e);
                }
                catch (Shared.Exception.EntidadDuplicada ex)
                {
                    labelMensaje.Text = ex.Message;
                }
            }
        }

        private void ButtonEliminar_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem itemBorrar in listView1.SelectedItems)
            {
                IEmp.DeleteEmployee(int.Parse(itemBorrar.Text));
            }
            textId.Text = "";
            textNombre.Text = "";
            textSalario.Text = "";
            comboTipo.Text = "";
            buttonAgregar.Visible = true;
            buttonCancelar.Visible = false;
            buttonModificar.Visible = false;
            this.Button1_Click(sender, e);
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem itemSeleccionado in listView1.SelectedItems)
            {
                Shared.Entities.Employee emp = IEmp.GetEmployee(int.Parse(itemSeleccionado.Text));
                textId.Text = emp.Id.ToString();
                textNombre.Text = emp.Name;
                if (emp.GetType() == typeof(Shared.Entities.FullTimeEmployee))
                {
                    Shared.Entities.FullTimeEmployee empFull = (Shared.Entities.FullTimeEmployee)emp;
                    textSalario.Text = empFull.Salary.ToString();
                    comboTipo.Text = "Mensual";
                } else
                {
                    Shared.Entities.PartTimeEmployee empPart = (Shared.Entities.PartTimeEmployee)emp;
                    textSalario.Text = empPart.HourlyRate.ToString();
                    comboTipo.Text = "Jornalero";
                }
            }
            buttonAgregar.Visible = false;
            buttonCancelar.Visible = true;
            buttonModificar.Visible = true;
        }

        private void ButtonModificar_Click(object sender, EventArgs e)
        {
            String nombre = textNombre.Text, tipo = comboTipo.Text;
            Shared.Entities.Employee nuevoEmp;

            if (nombre.Equals("") || tipo.Equals("") || textId.Text.Equals("") || textSalario.Text.Equals(""))
                labelMensaje.Text = "Faltan datos";
            else
            {
                try
                {
                    int id = int.Parse(textId.Text), salario = int.Parse(textSalario.Text);
                    if (tipo.Equals("Jornalero"))
                    {
                        nuevoEmp = new Shared.Entities.PartTimeEmployee()
                        {
                            Id = id,
                            HourlyRate = salario,
                            Name = nombre,
                            StartDate = new DateTime()
                        };
                    }
                    else
                    {
                        nuevoEmp = new Shared.Entities.FullTimeEmployee()
                        {
                            Id = id,
                            Salary = salario,
                            Name = nombre,
                            StartDate = new DateTime()
                        };
                    }
                    IEmp.UpdateEmployee(nuevoEmp);
                    this.Button1_Click(sender, e);
                }
                catch (Shared.Exception.EntidadDuplicada ex)
                {
                    labelMensaje.Text = ex.Message;
                }
            }
            textId.Text = "";
            textNombre.Text = "";
            textSalario.Text = "";
            comboTipo.Text = "";
            buttonAgregar.Visible = true;
            buttonCancelar.Visible = false;
            buttonModificar.Visible = false;
        }

        private void ButtonCancelar_Click(object sender, EventArgs e)
        {
            textId.Text = "";
            textNombre.Text = "";
            textSalario.Text = "";
            comboTipo.Text = "";
            buttonAgregar.Visible = true;
            buttonCancelar.Visible = false;
            buttonModificar.Visible = false;
        }
    }
}
