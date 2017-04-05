using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArrayList ListaFotos; //Links .jpg de las fotos.
            ListaFotos = new ArrayList(); //Links .jpg de las fotos.
            string laUrl = textBox1.Text; //El instagram en string.
            WebRequest request = WebRequest.Create(laUrl); //Entrando al instagram.
            WebResponse response = request.GetResponse(); //Obteniendo lo que veriamos en el navegador.
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string res = reader.ReadToEnd(); //En string el texto plano del navegador.
            reader.Close(); // Cerrando.
            response.Close(); // Cerrando.
            string[] Divisiones = Regex.Split(res, "</span>"); //Para ir directamente a donde estan las fotos.
            string[] Separadores = new string[] { "_src\": \"", "\"," }; //Filtrar el string.
            string[] Filtrado; // Las fotos en link.
            Filtrado = Divisiones[1].Split(Separadores, StringSplitOptions.None); //El metodo de filtro. LA POSTA
            try{
                for (int i = 0; i < Filtrado.Length; i++)
                { //Aún se debe filtrar mas todavía.
                    if (Filtrado[i][0] == 'h')
                    {
                        ListaFotos.Add(Filtrado[i]);
                    }
                }
            }
                catch(Exception){
                    MessageBox.Show("El instagram es privado", "Atención");
                }

            Directory.CreateDirectory("C:\\Instagram"); // Creo la carpeta.
            Console.WriteLine(ListaFotos[0]);
            for (int i = 0; i < ListaFotos.Count; i++) // FOR para la descarga de fotos.
            {
                WebClient webClient = new WebClient();
                
                webClient.DownloadFile(ListaFotos[i].ToString(), @"C:\\Instagram\\" + i + ".jpg");
            }
            System.Diagnostics.Process.Start(@"C:\Instagram"); // Cuando terminan de descargarse las fotos, abro la carpeta donde estan.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("NOTA: El instagram debe ingresarse de la forma: http:\\\\www.instagram.com\\usuario SIN BARRA AL FINAL\nSi el instagram es privado saltará una advertencia\nEsta edición solo permite bajar las primeras 12 fotos en 2 resoluciones distintas.", "Ayuda");
        }

    }
}
