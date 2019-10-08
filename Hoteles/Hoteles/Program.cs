using System;
using System.Data.SqlClient;

namespace Hoteles
{
    class Program
    {
        static SqlConnection connection = new SqlConnection("Data Source=DESKTOP-T0M5LFO\\SQLEXPRESS;Initial Catalog=Hoteles;Integrated Security=True");
        static void Main(string[] args)
        {
              // LLamadas al Menu
              HOTEL();

            }
        public static void HOTEL()
        {
            Console.WriteLine("Bienvenido a la aplicacion interactiva HOTELS");
            Console.WriteLine("");
            Console.WriteLine("MENU");
            Console.WriteLine("1.Register Client");
            Console.WriteLine("2.Edit Client");
            Console.WriteLine("3.CheckIn");
            Console.WriteLine("4.CheckOut");
            Console.WriteLine("5.Check rooms");
            Console.WriteLine("6.Exit");
            Console.WriteLine("");
            Console.WriteLine("Introduzca el NUMERO asociado a la accion de menu ");
            int select = Convert.ToInt32(Console.ReadLine());


            switch (select)
            {
                case 1:
                    registerClient(); // Metodo para registrar cliente
                    break;
                case 2:
                    editClient(); 
                    break;

                case 3:
                    checkIn(); // Metodo para realizar el CheckIn
                    break;

                case 4:
                    //checkOut(); //Metodo para realizar el CheckOut
                    break;

                case 5:
                   /// checkRooms(); // Metodo para hacer consultas sobre las habitaciones
                    break;

                case 6:
                    Console.WriteLine("Exit");
                    break;

                default:
                    Console.WriteLine("No Option Menu");
                    break;
            }
        }

        // REGISTRAR CLIENTE --------------------------------------------------------
        public static void registerClient()
        {
            Console.Write("Enter the Client Name:  ");
            string name = Console.ReadLine();

            Console.Write("Enter the Client Surname:  ");
            string surname = Console.ReadLine();

            Console.Write("Enter the Client DNI:  ");
            string dni = Console.ReadLine();

            
            string registryQuery = $"INSERT INTO Clients (Firstname,Surname,DNI) VALUES ('{name}','{surname}','{dni}')";
            SqlCommand command = new SqlCommand(registryQuery, connection);
            connection.Open();
            command.ExecuteNonQuery();

            if (command.ExecuteNonQuery() > 0)
            {
                Console.WriteLine("El clinte se ha introducido correctamente");
            }
            else

            {
                Console.WriteLine("No se ha introducido el cliente");
            }

            connection.Close();

            }


            //EDITAR CLIENTE ---------------------------------------------------------------
            public static void editClient()
            {
            connection.Open();
            Console.WriteLine("Introduzca el DNI del cliente que desea modificar");
            string DNI = Console.ReadLine();
            string editSelect = $"SELECT  * FROM Clients WHERE DNI = '{DNI}'";
            SqlCommand command = new SqlCommand(editSelect, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                // Pedimos los nuevos datos al cliente
                Console.WriteLine("Introduzca el nuevo nombre");
                string newName = Console.ReadLine();
                Console.WriteLine("Introduzca el nuevo apellido");
                string newSurname = Console.ReadLine();


                string updateClient = $"UPDATE Clients SET Firstname = '{newName}', Surname = '{newSurname}' WHERE DNI = '{DNI}'";
                SqlCommand command1 = new SqlCommand(updateClient, connection);
                connection.Close();
                connection.Open(); // Cerramos consulta previa
                Console.WriteLine(command1.ExecuteNonQuery()); // Ejecutamos la UPDATE

            }
            else
            {
                Console.WriteLine("El cliente no existe");
                connection.Close();
                editClient();
            }

        }

        // CHECKIN -----------------------------------------------

        public static void checkIn()
        {
           
            Console.WriteLine("Introduzca el DNI ");
            string DNI =Console.ReadLine();
            string editSelect = $"SELECT  * FROM Clients WHERE DNI = '{DNI}'";
            SqlCommand command = new SqlCommand(editSelect, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                connection.Close();
                string roomsAvailable = $"SELECT  * FROM Rooms WHERE Available = 'Y'";
                SqlCommand command1 = new SqlCommand(roomsAvailable, connection);
                connection.Open();
                SqlDataReader reader1 = command1.ExecuteReader();

                while (reader1.Read())
                {
                    Console.WriteLine($"{reader1[0].ToString()} {reader1[1].ToString()}");
                    

                }
                    connection.Close();
                    connection.Open();
                    Console.WriteLine("Introduca la habitacion a reservar");
                    string roomAvailable = Console.ReadLine();
                    string updateStateRoom = $"UPDATE Rooms SET Available = 'N' WHERE ID = '{roomAvailable}'";
                    SqlCommand command2 = new SqlCommand(updateStateRoom, connection);
                    Console.WriteLine(command2.ExecuteNonQuery());

                connection.Close();

            }
            else
            {
                Console.WriteLine("El cliente no esta registrado. No se puede realizar el CheckIn");
                connection.Close();
                registerClient(); // LLamamos al metodo para el regsitro del cliente
            }

        }








        //SqlDataReader reader = command.ExecuteReader();

        //while (reader.Read())
        //{
        //    Console.WriteLine("El cliente se ha introducido correctamente");
        //    Console.WriteLine($"{reader["name"].ToString()} {reader["surname"].ToString()} {reader["dni"].ToString()}");
        //}






    }
}
