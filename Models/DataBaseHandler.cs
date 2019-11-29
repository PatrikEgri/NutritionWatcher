using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using NutritionWatcher.Models;

namespace NutritionWatcher.Models
{
    public class DataBaseHandler
    {
        string database;
        string host;
        SqlConnection _connection;

        public DataBaseHandler()
        {
            database = "NutritionWatcher2";
            host = @"(localdb)\MSSQLLocalDB";
            _connection = new SqlConnection($"Data Source = {host}; Initial Catalog = {database}; Integrated Security = True;");
        }

        public FoodModel GetFoodById(int id)
        {
            try
            {
                FoodModel food = null;

                _connection.Open();

                SqlCommand cmd = new SqlCommand($"SELECT id, name, protein, fat, hydrocarbonate, gramm FROM Food WHERE id = {id};", _connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    food = new FoodModel
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Protein = float.Parse("" + reader.GetDouble(2)),
                        Fat = float.Parse("" + reader.GetDouble(3)),
                        Hydrocarbonate = float.Parse("" + reader.GetDouble(4)),
                        Gramm = reader.GetInt32(5)
                    };
                }

                _connection.Close();

                return food;
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public List<FoodModel> GetFoods()
        {
            try
            {
                List<FoodModel> foods = new List<FoodModel>();

                _connection.Open();

                SqlCommand cmd = new SqlCommand($"SELECT id, name, protein, fat, hydrocarbonate, gramm FROM Food;", _connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    foods.Add(new FoodModel
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Protein = float.Parse("" + reader.GetDouble(2)),
                        Fat = float.Parse("" + reader.GetDouble(3)),
                        Hydrocarbonate = float.Parse("" + reader.GetDouble(4)),
                        Gramm = reader.GetInt32(5)
                    });
                }

                _connection.Close();

                return foods;
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public ConsumptionModel GetConsumptionById(int id)
        {
            try
            {
                ConsumptionModel consumption = null;

                _connection.Open();

                SqlCommand cmd = new SqlCommand($"SELECT id, date, time, owner FROM Consumption WHERE id = {id};", _connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    consumption = new ConsumptionModel
                    {
                        Id = reader.GetInt32(0),
                        Date = reader.GetDateTime(1).ToString("yyyy-MM-dd"),
                        Time = new DateTime(1, 1, 1, reader.GetTimeSpan(2).Hours, reader.GetTimeSpan(2).Minutes, 0).ToString("HH:mm"),
                        UserId = reader.GetInt32(3)
                    };
                }

                _connection.Close();

                return consumption;
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public bool UpdateStyle(int userId, int styleId)
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand($"UPDATE Owner SET style = {styleId} WHERE id = {userId}", _connection);
                SqlDataAdapter adapter = new SqlDataAdapter
                {
                    UpdateCommand = cmd
                };
                adapter.UpdateCommand.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool UpdatePassword(int userId, string newPassword)
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand($"UPDATE Owner SET password = '{Hash(newPassword)}' WHERE id = {userId}", _connection);
                SqlDataAdapter adapter = new SqlDataAdapter
                {
                    UpdateCommand = cmd
                };
                adapter.UpdateCommand.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public List<StyleModel> GetStyles()
        {
            try
            {
                List<StyleModel> styles = new List<StyleModel>();

                _connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT id, name FROM Style", _connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    styles.Add(new StyleModel 
                    { 
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    });
                }

                _connection.Close();

                return styles;
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public bool InsertFood(FoodModel food)
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand($"INSERT INTO Food (name, protein, fat, hydrocarbonate, gramm) " +
                    $"VALUES ('{food.Name}', {food.Protein}, {food.Fat}, {food.Hydrocarbonate}, {food.Gramm});", _connection);
                SqlDataAdapter adapter = new SqlDataAdapter
                {
                    InsertCommand = cmd
                };
                adapter.InsertCommand.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool UpdateFood(FoodModel food)
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand($"UPDATE Food SET name = '{food.Name}', protein = {food.Protein}, " +
                    $"fat = {food.Fat}, hydrocarbonate = {food.Hydrocarbonate}, gramm = {food.Gramm} WHERE id = {food.Id};", _connection);
                SqlDataAdapter adapter = new SqlDataAdapter
                {
                    UpdateCommand = cmd
                };
                adapter.UpdateCommand.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool DeleteFood(int? id)
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand($"DELETE FROM Food WHERE id = {id};", _connection);
                SqlDataAdapter adapter = new SqlDataAdapter
                {
                    DeleteCommand = cmd
                };
                adapter.DeleteCommand.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool InsertUser(RegistrationUserModel user)
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand($"INSERT INTO Owner (username, firstname, lastname, password, email, style, permission) " +
                    $"VALUES ('{user.Username}', '{user.Firstname}', '{user.Lastname}', '{Hash(user.Password) }', '{user.Email}', 1, 2);", _connection);
                SqlDataAdapter adapter = new SqlDataAdapter
                {
                    InsertCommand = cmd
                };
                adapter.InsertCommand.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool UpdateUsername(int id, string username)
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand($"UPDATE Owner SET username = '{username}' WHERE id = {id};", _connection);
                SqlDataAdapter adapter = new SqlDataAdapter
                {
                    UpdateCommand = cmd
                };
                adapter.UpdateCommand.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool UpdateName(int id, string firstname, string lastname)
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand($"UPDATE Owner SET firstname = '{firstname}', lastname = '{lastname}' WHERE id = {id};", _connection);
                SqlDataAdapter adapter = new SqlDataAdapter
                {
                    UpdateCommand = cmd
                };
                adapter.UpdateCommand.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool UpdateEmail(int id, string email)
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand($"UPDATE Owner SET email = '{email}' WHERE id = {id};", _connection);
                SqlDataAdapter adapter = new SqlDataAdapter
                {
                    UpdateCommand = cmd
                };
                adapter.UpdateCommand.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool DeleteUser(int id)
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand($"DELETE FROM Food_Consumption WHERE consumption = (SELECT id FROM Consumption WHERE owner = {id});" +
                                                $"DELETE FROM Consumption WHERE owner = {id};" +
                                                $"DELETE FROM Owner WHERE id = {id};", _connection);
                SqlDataAdapter adapter = new SqlDataAdapter
                {
                    DeleteCommand = cmd
                };
                adapter.DeleteCommand.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public UserModel GetUserById(int id)
        {
            try
            {
                UserModel loggedIn = null;
                _connection.Open();

                SqlCommand cmd = new SqlCommand($"SELECT Owner.id, firstname, lastname, password, email, username, Style.id, Style.name FROM Owner " +
                    $"INNER JOIN Style ON Owner.style = Style.id WHERE Owner.id = {id};", _connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    loggedIn = new UserModel
                    {
                        Id = reader.GetInt32(0),
                        Firstname = reader.GetString(1),
                        Lastname = reader.GetString(2),
                        Password = reader.GetString(3),
                        Email = reader.GetString(4),
                        Username = reader.GetString(5),
                        Style = new StyleModel
                        {
                            Id = reader.GetInt32(6),
                            Name = reader.GetString(7)
                        }
                    };
                }

                _connection.Close();

                return loggedIn;
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public UserModel GetUser(UserModel user)
        {
            try
            {
                UserModel loggedIn = null;
                _connection.Open();

                SqlCommand cmd = new SqlCommand($"SELECT id, firstname, lastname, password, email, username FROM Owner WHERE username = '{user.Username}' " +
                    $"and password = '{Hash(user.Password)}';", _connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    loggedIn = new UserModel
                    {
                        Id = reader.GetInt32(0),
                        Firstname = reader.GetString(1),
                        Lastname = reader.GetString(2),
                        Password = reader.GetString(3),
                        Email = reader.GetString(4),
                        Username = reader.GetString(5)
                    };
                }

                _connection.Close();

                return loggedIn;
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public bool InsertConsumption(ConsumptionModel consumption, int userId)
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand($"INSERT INTO Consumption (date, time, owner) " +
                    $"VALUES ('{consumption.Date}', '{consumption.Time}', {userId});", _connection);
                SqlDataAdapter adapter = new SqlDataAdapter
                {
                    InsertCommand = cmd
                };
                adapter.InsertCommand.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool UpdateConsumption(ConsumptionModel consumption)
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand($"UPDATE Consumption SET date = '{consumption.Date}', time = '{consumption.Time}', " +
                    $"owner = {consumption.UserId} WHERE id = {consumption.Id};", _connection);
                SqlDataAdapter adapter = new SqlDataAdapter
                {
                    UpdateCommand = cmd
                };
                adapter.UpdateCommand.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool DeleteConsumption(ConsumptionModel consumption)
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand($"DELETE FROM Food_Consumption WHERE consumption = {consumption.Id};" +
                                                $"DELETE FROM Consumption WHERE id = {consumption.Id};", _connection);
                SqlDataAdapter adapter = new SqlDataAdapter
                {
                    DeleteCommand = cmd
                };
                adapter.DeleteCommand.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public List<ConsumptionModel> GetConsumptions(int userId)
        {
            List<ConsumptionModel> consumptions = new List<ConsumptionModel>();

            try
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand($"SELECT * FROM Consumption WHERE owner = {userId}", _connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    consumptions.Add(new ConsumptionModel
                    {
                        Id = reader.GetInt32(0),
                        Date = reader.GetDateTime(1).ToString("yyyy-MM-dd"),
                        Time = new DateTime(1, 1, 1, reader.GetTimeSpan(2).Hours, reader.GetTimeSpan(2).Minutes, 0).ToString("HH:mm"),
                        UserId = reader.GetInt32(3)
                    });
                }

                _connection.Close();

                return consumptions;
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public List<CalorieViewModel> GetCalorieViewModels(UserModel user)
        {
            try
            {
                List<CalorieViewModel> calorieViewModels = new List<CalorieViewModel>();

                _connection.Open();

                SqlCommand cmd = new SqlCommand("SELECT Food.id, Food.name, Food.protein, Food.fat, Food.hydrocarbonate, Food.gramm, " +
                                                "Consumption.id, Consumption.date, Consumption.time, " +
                                                "Food_Consumption.id, Food_Consumption.gramm " +
                                                "FROM (Food INNER JOIN Food_Consumption ON Food.id = Food_Consumption.food) " +
                                                "INNER JOIN Consumption ON Food_Consumption.consumption = Consumption.id " +
                                                $"WHERE Consumption.owner = {user.Id}", _connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    calorieViewModels.Add(new CalorieViewModel
                    {
                        Food = new FoodModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Protein = reader.GetFloat(2),
                            Fat = reader.GetFloat(3),
                            Hydrocarbonate = reader.GetFloat(4),
                            Gramm = reader.GetInt32(5)
                        },
                        Consumption = new ConsumptionModel
                        {
                            Id = reader.GetInt32(6),
                            Date = reader.GetDateTime(7).ToString("yyyy-MM-dd"),
                            Time = reader.GetDateTime(8).ToString("HH:mm")
                        },
                        Id = reader.GetInt32(9),
                        ConsumedGramms = reader.GetInt32(10)
                    });
                }

                _connection.Close();

                return calorieViewModels;
            }
            catch (SqlException)
            {
                return null;
            }
        }

        public bool AssignConsumption(ConsumptionAssignmentViewModel assignment)
        {
            try
            {
                _connection.Open();

                SqlCommand cmd = new SqlCommand($"INSERT INTO Food_Consumption (consumption, food, gramm) " +
                    $"VALUES ({assignment.ConsumptionId}, {assignment.FoodId}, {assignment.Gramm});", _connection);
                SqlDataAdapter adapter = new SqlDataAdapter
                {
                    InsertCommand = cmd
                };
                adapter.InsertCommand.ExecuteNonQuery();

                _connection.Close();

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public string Hash(string a)
        {
            return a.GetHashCode() > 0 ? (a.GetHashCode() << 5).GetHashCode().ToString() : (a.GetHashCode() >> 5).GetHashCode().ToString();
        }
    }
}