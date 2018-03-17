using System;
using System.Data.SqlClient;

namespace Phonebook.TestTools.Data
{
	public static class IdentityManager
	{
		private const string HashedPassword = "AQAAAAEAACcQAAAAENVDkipdv61VqXJlEbAxO86zoOHwxyNszMUdvu8CKyAM2HTJnaaNmHLTNJB4nXSBsQ=="; //P@ssword1

		private const string InsertUserSql = @"INSERT INTO dbo.AspNetUsers (Id, AccessFailedCount, EmailConfirmed, LockoutEnabled, NormalizedUserName, PhoneNumberConfirmed, PasswordHash, TwoFactorEnabled, UserName) VALUES (@0, @1, @2, @3, @4, @5, @6, @7, @8)";
		private const string InsertClaimsSql = @"INSERT INTO dbo.AspNetUserClaims (ClaimType, ClaimValue, UserId) VALUES (@0, @1, @2)";
		private const string DeleteUserSql = @"DELETE FROM AspNetUsers WHERE Id = @0";
		private const string DeleteClaimsSql = @"DELETE FROM AspNetUserClaims WHERE UserId = @0";

		public static Guid CreateNewUserWithPassword1(string username)
		{
			var userGuid = Guid.NewGuid();

			var insertUserParams = new object[] { userGuid, 0, 0, 0, username.ToUpper(), 0, HashedPassword, 0, username};
			
			try
			{
				RunSql(InsertUserSql, insertUserParams);
			}
			catch
			{
				throw;
			}

			return userGuid;
		}

		public static Guid CreateNewUserWithPassword1AndAllClaims(string username)
		{
			var userGuid = Guid.NewGuid();

			var insertUserParams = new object[] { userGuid, 0, 0, 0, username.ToUpper(), 0, HashedPassword, 0, username };

			try
			{
				RunSql(InsertUserSql, insertUserParams);
				AddClaim(userGuid, "phonebookAPI.read");
				AddClaim(userGuid, "phonebookAPI.write");
			}
			catch
			{
				throw;
			}

			return userGuid;
		}

		public static void AddClaim(Guid userId, string claim)
		{
			var parameters = new object[] { "scope", claim, userId.ToString() };

			RunSqlWithNoReturnData(InsertClaimsSql, parameters);
		}

		public static void DeleteUser(Guid userId)
		{
			var parameters = new object[] { userId.ToString() };

			RunSqlWithNoReturnData(DeleteClaimsSql, parameters);
			RunSqlWithNoReturnData(DeleteUserSql, parameters);
		}

		public static void DeleteAllClaimsForUser(Guid userId)
		{
			var parameters = new object[] { userId.ToString() };

			RunSqlWithNoReturnData(DeleteClaimsSql, parameters);
		}

		private static void RunSqlWithNoReturnData(string sql, object[] parameters)
		{
			try
			{
				RunSql(sql, parameters);
			}
			catch
			{
				throw;
			}
		}

		private static void RunSql(string sql, object[] insertUserParams)
		{
			using (var conn = new SqlConnection(DataManager.IdentityServerConnString))
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.Connection = conn;
					cmd.CommandText = sql;

					var count = 0;
					foreach (var parameter in insertUserParams)
					{
						cmd.Parameters.AddWithValue("@" + count, parameter);
						count++;
					}

					cmd.CommandTimeout = 300;
					cmd.ExecuteNonQuery();
				}
			}
		}
	}
}
