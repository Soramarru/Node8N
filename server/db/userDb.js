import DBaccess from "./dbAccess.js";
const userDBaccess =
{
    async createUser(hashedPass, username, token)
    {
        try
        {
            const sql = "INSERT INTO `User` (`hashedPass`, `username`, token) VALUES (?, ? , ?)";
            const [results] = await DBaccess.db.execute(sql, [hashedPass, username, token]);
            return results.insertId;
        }
        catch (e)
        {
            return e;
        }
    },
    async getUserTroughName(username)
    {
        const sql = "select * from `User` where username = ?";
        const [results] = await DBaccess.db.execute(sql, [username]);
        if (results.length == 0)
            return false;
        return results[0];
    },
    async getUserTroughToken(token)
    {
        const sql = "SELECT * FROM User WHERE token = ?";
        const [results] = await DBaccess.db.execute(sql, [token]);
        if (results.length == 0)
            return false;
        return results[0];
    },
    async userTokenExist(token)
    {
        const sql = "SELECT * FROM User WHERE token = ?";
        const [results] = await DBaccess.db.execute(sql, [token]);
        return results.length > 0;
    },
    async usernameExist(userName)
    {
        const sql = "SELECT * FROM User WHERE username = ?";
        const [results] = await DBaccess.db.execute(sql, [userName]);
        return results.length > 0;
    },
}

export default userDBaccess;