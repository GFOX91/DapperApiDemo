using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;

/// <summary>
/// Used to access data from the users table
/// </summary>
public class UserData : IUserData
{
    private readonly ISqlDataAccess _db;

    public UserData(ISqlDataAccess db)
    {
        _db = db;
    }

    /// <summary>
    /// Retrieve all users
    /// </summary>
    public Task<IEnumerable<UserModel>> GetUsers() =>
        _db.LoadData<UserModel, dynamic>("dbo.spUser_GetAll", new { });

    /// <summary>
    /// Retrieve a user by Id 
    /// </summary>
    /// <param name="id">The id used to retrieve the user</param>
    public async Task<UserModel?> GetUser(int id)
    {
        var results = await _db.LoadData<UserModel, dynamic>(
            "dbo.spUser_Get",
            new { Id = id });
        return results.FirstOrDefault();
    }

    /// <summary>
    /// Insert a new user into the DB
    /// </summary>
    /// <param name="user">The user being persisted to the DB</param>
    public Task InsertUser(UserModel user) =>
        _db.SaveData("dbo.spUser_Insert", new { user.FirstName, user.LastName });

    /// <summary>
    /// Update a existing user in the DB
    /// </summary>
    /// <param name="user">The user being updated in the DB</param>
    public Task UpdateUser(UserModel user) =>
        _db.SaveData("dbo.spUser_Update", user);

    /// <summary>
    /// Deletes a user from the DB
    /// </summary>
    /// <param name="user">The id of the user to be deleted</param>
    public Task DeleteUser(int id) =>
        _db.SaveData("dbo.spUser_Delete", new { Id = id });

}
