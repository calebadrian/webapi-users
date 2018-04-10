using System;
using System.Data;
using keepr.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace keepr.Repositories
{
    public class UserRepository : DbContext
    {
        public UserRepository(IDbConnection db) : base(db)
        {
        }

        public UserReturnModel Register(RegisterUserModel creds)
        {
            // encrypt the password??
            //sql
            try
            {
                Guid g = Guid.NewGuid();
                string id = g.ToString();
                User user = new User()
                {
                    Id = id,
                    Username = creds.Username,
                    Email = creds.Email,
                    ProfilePic = creds.profilePic,
                    Password = BCrypt.Net.BCrypt.HashPassword(creds.Password)
                };
                var success = _db.Execute(@"
                INSERT INTO users(
                    id,
                    username,
                    email,
                    profilePic,
                    password
                ) VALUES (
                    @Id,
                    @Username,
                    @Email,
                    @ProfilePic,
                    @Password
                )", user);
                if (success < 1)
                {
                    throw new Exception("EMAIL IN USE");
                }
                return new UserReturnModel()
                {
                    Id = id,
                    Username = user.Username,
                    Email = user.Email,
                    profilePic = user.ProfilePic,
                };
            }
            catch (MySqlException e)
            {
                System.Console.WriteLine("ERROR: " + e.Message);
                return null;
            }
        }

        public UserReturnModel Login(LoginUserModel creds)
        {
            // more sql
            User user = _db.QueryFirstOrDefault<User>(@"
                SELECT * FROM users WHERE email = @Email
            ", creds);
            if (user != null)
            {
                Boolean valid = BCrypt.Net.BCrypt.Verify(creds.Password, user.Password);
                if (valid)
                {
                    return user.GetReturnModel();
                }
            }
            return null;
        }

        internal UserReturnModel GetUserByEmail(string email)
        {
            User user = _db.QueryFirstOrDefault<User>(@"
                SELECT * FROM users WHERE email = @Email
            ", new { email });
            return user.GetReturnModel();
        }

        internal UserReturnModel GetUserById(string id)
        {
            User savedUser = _db.QueryFirstOrDefault<User>(@"
            SELECT * FROM users WHERE id = @id
            ", new { id });
            if (savedUser == null)
            {
                return null;
            }
            else
            {
                return savedUser.GetReturnModel();
            }
        }

        internal UserReturnModel UpdateUser(UserReturnModel user)
        {
            var i = _db.Execute(@"
                UPDATE users SET
                    email = @Email,
                    username = @Username
                WHERE id = @id
            ", user);
            if (i > 0)
            {
                return user;
            }
            return null;

        }

        internal string ChangeUserPassword(ChangeUserPasswordModel user)
        {
            User savedUser = _db.QueryFirstOrDefault<User>(@"
            SELECT * FROM users WHERE id = @id
            ", user);

            var valid = BCrypt.Net.BCrypt.Verify(user.OldPassword, savedUser.Password);
            if (valid)
            {
                user.NewPassword = BCrypt.Net.BCrypt.HashPassword(user.NewPassword);
                var i = _db.Execute(@"
                    UPDATE users SET
                        password = @NewPassword
                    WHERE id = @id
                ", user);
                return "Good Job";
            }
            return "Umm nope!";
        }
    }
}