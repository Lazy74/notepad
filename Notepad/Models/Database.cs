using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Notepad.Models
{
    public class Database
    {
        private readonly static string ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Notepads;Integrated Security=SSPI;";

        /// <summary>
        /// Получить данные о всех блокнотах
        /// </summary>
        /// <returns></returns>
        public static List<StructureNotepad> LoadNotepads()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT * " +
                                      "FROM [Data] ";

                    using (var reader = cmd.ExecuteReader())
                    {
                        List<StructureNotepad> structureNotepads = new List<StructureNotepad>();

                        while (reader.Read())
                        {
                            int id;

                            try
                            {
                                id = Convert.ToInt32(reader["Id"]);
                            }
                            catch (Exception)
                            {
                                id = 0;
                            }

                            StructureNotepad structure = new StructureNotepad();

                            structure.Id = id;
                            structure.Name = reader["name"].ToString();
                            structure.Content = reader["content"].ToString();

                            structureNotepads.Add(structure);
                        }

                        return structureNotepads;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Создать новый блокнот
        /// </summary>
        /// <param name="name">Название блокнота</param>
        /// <returns></returns>
        public static bool AddNotepad(string name)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "INSERT INTO Data (name, content) " +
                                      "VALUES (@name, @content);";

                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@content", "");

                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;

                        // TODO Записать лог
                    }
                }
            }
        }

        /// <summary>
        /// Обновление блокнота
        /// </summary>
        /// <param name="structureNotepad">Модель блокнота</param>
        /// <returns></returns>
        public static bool UpdateNotepad(StructureNotepad structureNotepad)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "UPDATE [Data] " +
                                      "SET " +
                                            "content = @content " +
                                      "WHERE Id = @Id;";

                    cmd.Parameters.AddWithValue("@content", structureNotepad.Content);
                    cmd.Parameters.AddWithValue("@Id", structureNotepad.Id);


                    try
                    {
                        cmd.ExecuteNonQuery();     // синхронно
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                        // TODO сделать лог
                    }

                }
            }
        }

        /// <summary>
        /// Лог использования контроллеров
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static bool AddRecordLog(string typeController, string controller, string action)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "INSERT INTO Log (dateTime, typeController, controller, action) " +
                                      "VALUES (@dateTime, @typeController, @controller, @action);";

                    cmd.Parameters.AddWithValue("@typeController", typeController);
                    cmd.Parameters.AddWithValue("@controller", controller);
                    cmd.Parameters.AddWithValue("@action", action);
                    cmd.Parameters.AddWithValue("@dateTime", DateTime.Now);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;

                        // TODO Записать лог
                    }
                }
            }
        }

        /// <summary>
        /// Удалить блокнот
        /// </summary>
        /// <param name="name">Название блокнота</param>
        /// <returns></returns>
        public static bool DeleteNotepad(string name)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "DELETE FROM Data " +
                                      "WHERE name = @name";

                    cmd.Parameters.AddWithValue("@name", name);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;

                        // TODO Записать лог
                    }
                }
            }
        }

        public static List<LogModel> GetLogList()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT * " +
                                      "FROM [Log] ";

                    using (var reader = cmd.ExecuteReader())
                    {
                        List<LogModel> logModels = new List<LogModel>();

                        while (reader.Read())
                        {
                            int id;

                            try
                            {
                                id = Convert.ToInt32(reader["Id"]);
                            }
                            catch (Exception)
                            {
                                id = 0;
                            }

                            LogModel logModel = new LogModel();

                            logModel.Id = id;
                            logModel.dateTime = (DateTime)reader["dateTime"];
                            logModel.typeController = reader["typeController"].ToString();
                            logModel.controller = reader["controller"].ToString();
                            logModel.action = reader["action"].ToString();

                            logModels.Add(logModel);
                        }
                        return logModels;
                    }
                }
            }
        }
    }
}