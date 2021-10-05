using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Covid.Models
{
    public class Funcoes
    {
        public static string HashTexto(string texto, string nomeHash)
        {
            HashAlgorithm algoritmo = HashAlgorithm.Create(nomeHash);
            if (algoritmo == null)
            {
                throw new ArgumentException("Nome de hash incorreto", "nomeHash");
            }
            byte[] hash = algoritmo.ComputeHash(Encoding.UTF8.GetBytes(texto));
            return Convert.ToBase64String(hash);
        }

        public class Cadastro
        {          
            [Required]
            [MaxLength(250)]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,20})", ErrorMessage = "A senha deve conter aos menos uma letra maiúscula, minúscula e um número. Deve ser no mínimo 6 caracteres")]
            public string Senha { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Senha")]
            [Display(Name = "Confirmar senha")]
            public string ConfirmaSenha { get; set; }
        }

        /* Usuario realiza login na conta */
        public class Login
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [RegularExpression("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,20})", ErrorMessage = "A senha deve conter aos menos uma letra maiúscula, minúscula e um número. Deve ser no mínimo 6 caracteres")]
            public string Senha { get; set; }
        }

        public static string GerarGraficoPizza(string titulo, string dados)
        {
            //dados: ['Work', 11],['Eat', 2],['Commute', 2],['Watch TV', 2],['Sleep', 7]
            string graf = @"< type='text/java' src='https://www.gstatic.com/charts/loader.js'></>
                < type='text/java'>
                    google.charts.load('current', {packages:['corechart']});
                    google.charts.setOnLoadCallback(drawChart);
                    function drawChart() {
                      var data = google.visualization.arrayToDataTable([
                      ['', ''],
                      " + dados + @"
                      ]);

                      var options = {
                        title: '" + titulo + @"',
                        is3D: true, };

                      var chart = new google.visualization.PieChart(document.getElementById('piechart_" + titulo.Replace(" ", "") + @"'));
                            chart.draw(data, options);
                            }
                </>
                <div id='piechart_" + titulo.Replace(" ", "") + @"' style='min-height: 500px;'></div>";
            return graf;
        }



    }
}