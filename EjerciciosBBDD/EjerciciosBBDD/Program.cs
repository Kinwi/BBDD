using System;
using System.Data.SqlClient;

namespace EjerciciosBBDD
{
    class Program
    {
        static void Main(string[] args)
        {
            // Cadena de conexion para conectar la aplicacion de consola con la base de datos
            // Catalog - Nombre de la base de datos a donde nos vamos a conectar
            // Data Source - Nombre del servidor donde vamos a conectarnos
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-T0M5LFO\\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=True");
            //string querySelect = "SELECT * FROM EMPLOYEES"; // No es case sensitive , asi que da igual utilizar mayusculas y minusculas en la query
            string queryUpdate1 = $"Update Employees SET COUNTRY ='{"ESP"}'Where Country ='AUS'";

            Console.WriteLine("Introduzca su apellido para chequear si existe en la BBDD");
            string apellido = Console.ReadLine().ToLower();
            //string querySelect = "SELECT  * FROM EMPLOYEES WHERE LastName ='apellido'";
            string querySelect = $"SELECT * FROM Employees WHERE Lastname = '{apellido}'";
            //string queryUpdate = $"UPDATE  Employees SET Country = '{pais}'";

            //string queryUpdate2 =
            //SqlCommand command = new SqlCommand(query, connection);
            SqlCommand command1 = new SqlCommand(querySelect, connection);
            
            connection.Open();
            //Console.WriteLine(command.ExecuteNonQuery() + " rows affected"); // Utilizamos el comando "ExecuteNonQuery" cuando la instruccion solo se va a ejecutar una vez
            SqlDataReader reader = command1.ExecuteReader();
            bool apellidoCorrecto = false;
            do
            {
                if (reader.Read())
                {
                    Console.WriteLine("Tu apellido " + apellido + " existe en la base de datos");
                    connection.Close();
                    Console.WriteLine("Introduzca su pais para actualizarlo en la base de datos");
                    string pais = Console.ReadLine();
                    string queryUpdate = $"UPDATE  Employees SET Country = '{pais}'WHERE LastName = '{apellido}'";
                    SqlCommand command2 = new SqlCommand(queryUpdate, connection);
                    connection.Open();
                    Console.WriteLine(command2.ExecuteNonQuery() + " rows affected");
                    connection.Close();
                    apellidoCorrecto = true;


                }
                else
                {
                    Console.WriteLine("No existe en la base de datos");
                }
                } while (apellidoCorrecto) ;

                //SqlDataReader reader = command.ExecuteReader();  //Se encarga de leer los registros

                //// Utilizamos un bucle While al no saber cuantos registros me va a leer
                //while (reader.Read()) // Mientras haya registros lee
                //{
                //    Console.WriteLine(reader[0].ToString());
                //    Console.WriteLine(reader[1].ToString());
                //    Console.WriteLine(reader[2].ToString());
                //    Console.WriteLine(reader[3].ToString());
                //    Console.WriteLine(reader[4].ToString());
                //    Console.WriteLine(reader[5].ToString());
                //    Console.WriteLine();

                //}
                //connection.Close();



            }
    }
}
